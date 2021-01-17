// -----------------------------------------------------------------------
// <copyright file="PasswordService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    /*
        Clase para encriptar contrase�as PasswordService
        Contiene los m�todos para encriptar contrase�as
    */

    /// <summary>
    /// Clase para encriptar contrase�as <c>PasswordService</c>.
    /// Contiene los m�todos para encriptar contrase�as.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todos los m�todos para la encriptaci�n de contrase�as.</para>
    /// </remarks>
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PasswordService"/>.
        /// </summary>
        /// <param name="settings">Configuraci�n de la contrase�a.</param>
        public PasswordService(PasswordSettings settings) => PasswordSettings = settings;

        private PasswordSettings PasswordSettings { get; }

        /// <inheritdoc/>
        public (string? hashPassword, List<PasswordErrorResult> errors) Create(string? plainPassword)
        {
            var errors = Validate(plainPassword);

            var hashPassword = errors.Count > 0 ? null : CreatePassword(plainPassword!);

            return (hashPassword, errors);
        }

        /// <inheritdoc/>
        public bool Verify(string? plainPassword, string? hashPassword)
        {
            var errors = Validate(plainPassword);

            var isValid = !errors.Any() && VerifyPassword(hashPassword, plainPassword);

            return isValid;
        }

        /// <inheritdoc/>
        public List<PasswordErrorResult> Validate(string? plainPassword)
        {
            var errors = new List<PasswordErrorResult>();

            if (plainPassword.ToIsNullOrEmpty())
            {
                errors.Add(PasswordErrorResult.RequiredValue);
            }
            else if (PasswordSettings != null)
            {
                if (PasswordSettings.RequireDigit.HasValue
                    && PasswordSettings.RequireDigit.Value
                    && !plainPassword.ToIsDigit())
                {
                    errors.Add(PasswordErrorResult.RequireDigit);
                }

                if (PasswordSettings.RequiredMinLength.HasValue
                    && plainPassword.ToIsLess(PasswordSettings.RequiredMinLength.Value))
                {
                    errors.Add(PasswordErrorResult.RequiredMinLength);
                }

                if (PasswordSettings.RequiredUniqueChars.HasValue
                    && PasswordSettings.RequiredUniqueChars.Value
                    && !plainPassword.ToIsUniqueChars())
                {
                    errors.Add(PasswordErrorResult.RequiredUniqueChars);
                }

                if (PasswordSettings.RequireEspecialChars.HasValue
                    && PasswordSettings.RequireEspecialChars.Value
                    && !plainPassword.ToIsEspecialChars())
                {
                    errors.Add(PasswordErrorResult.RequireEspecialChars);
                }

                if (PasswordSettings.RequireLowercase.HasValue
                    && PasswordSettings.RequireLowercase.Value
                    && !plainPassword.ToIsLowercase())
                {
                    errors.Add(PasswordErrorResult.RequireLowercase);
                }

                if (PasswordSettings.RequireUppercase.HasValue
                    && PasswordSettings.RequireUppercase.Value
                    && !plainPassword.ToIsUppercase())
                {
                    errors.Add(PasswordErrorResult.RequireUppercase);
                }
            }

            return errors;
        }

        #region Private

        private string CreatePassword(string plainPassword)
        {
            var settings = PasswordSettings.EncryptionSettings;

            var salt = CreateSalt(settings.SaltLength!.Value);

            var subkey = KeyDerivation.Pbkdf2(plainPassword, salt, settings.KeyDerivationAlgorithm!.Value.ToEnum<KeyDerivationPrf>(), settings.IterCount!.Value, settings.SaltLength!.Value);

            var outputBytes = new byte[settings.HeaderByteLength!.Value + salt.Length + subkey.Length];

            outputBytes[0] = settings.FormatMarker!.Value;

            WriteNetworkByteOrder(outputBytes, 1, (uint)settings.KeyDerivationAlgorithm!.Value);
            WriteNetworkByteOrder(outputBytes, 5, (uint)settings.IterCount!.Value);
            WriteNetworkByteOrder(outputBytes, 9, (uint)settings.SaltLength!.Value);

            Buffer.BlockCopy(salt, 0, outputBytes, settings.HeaderByteLength!.Value, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, settings.HeaderByteLength!.Value + settings.SaltLength!.Value, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        private bool VerifyPassword(string? hashedPassword, string? plainPassword)
        {
            var plain = plainPassword.ToIsNullOrEmptyThrow(nameof(plainPassword));
            var hash = hashedPassword.ToIsNullOrEmptyThrow(nameof(hashedPassword));

            byte[] decodedHashedPassword;

            try
            {
                decodedHashedPassword = Convert.FromBase64String(hash);
            }
            catch (Exception)
            {
                return false;
            }

            var settings = PasswordSettings.EncryptionSettings;

            if (decodedHashedPassword.Length == 0 || decodedHashedPassword[0] != settings.FormatMarker!.Value)
            {
                return false;
            }

            try
            {
                var shaUInt = ReadNetworkByteOrder(decodedHashedPassword, 1);

                var verifyPrf = shaUInt switch
                {
                    0 => KeyDerivationPrf.HMACSHA1,
                    1 => KeyDerivationPrf.HMACSHA256,
                    2 => KeyDerivationPrf.HMACSHA512,
                    _ => KeyDerivationPrf.HMACSHA256,
                };

                if (verifyPrf != settings.KeyDerivationAlgorithm!.Value.ToEnum<KeyDerivationPrf>())
                {
                    return false;
                }

                var verifyAlgorithmName = shaUInt switch
                {
                    0 => HashAlgorithmName.SHA1,
                    1 => HashAlgorithmName.SHA256,
                    2 => HashAlgorithmName.SHA512,
                    _ => HashAlgorithmName.SHA256,
                };

                if (verifyAlgorithmName != settings.AlgorithmName.ToHashAlgorithmName(HashAlgorithmName.SHA256))
                {
                    return false;
                }

                int iterCountRead = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);

                if (iterCountRead != settings.IterCount!.Value)
                {
                    return false;
                }

                int saltLengthRead = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                if (saltLengthRead != settings.SaltLength!.Value)
                {
                    return false;
                }

                byte[] salt = new byte[settings.SaltLength!.Value];

                Buffer.BlockCopy(decodedHashedPassword, settings.HeaderByteLength!.Value, salt, 0, salt.Length);

                int subkeyLength = decodedHashedPassword.Length - settings.HeaderByteLength!.Value - salt.Length;

                if (subkeyLength != settings.SaltLength!.Value)
                {
                    return false;
                }

                byte[] expectedSubkey = new byte[subkeyLength];

                Buffer.BlockCopy(decodedHashedPassword, settings.HeaderByteLength!.Value + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                byte[] actualSubkey = new byte[settings.SaltLength!.Value];

                actualSubkey = KeyDerivation.Pbkdf2(plain, salt, settings.KeyDerivationAlgorithm!.Value.ToEnum<KeyDerivationPrf>(), settings.IterCount!.Value, subkeyLength);

                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;

            for (var i = 0; i < a.Length; i++)
            {
                areSame &= a[i] == b[i];
            }

            return areSame;
        }

        private uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)buffer[offset + 0] << 24)
                | ((uint)buffer[offset + 1] << 16)
                | ((uint)buffer[offset + 2] << 8)
                | ((uint)buffer[offset + 3]);
        }

        private void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        private byte[] CreateSalt(int saltLength)
        {
            byte[] randomBytes = new byte[saltLength];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomBytes);

            return randomBytes;
        }

        #endregion Private
    }
}