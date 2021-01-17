// -----------------------------------------------------------------------
// <copyright file="EncryptionSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Security.Cryptography;
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    /*
       Clase de configuración EncryptionSettings
       Contiene las propiedades para la encriptación de la contraseña
   */

    /// <summary>
    /// Clase de configuración <c>EncryptionSettings</c>.
    /// Contiene las propiedades para la encriptación de la contraseña.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la encriptación de la contraseña.</para>
    /// </remarks>
    public class EncryptionSettings
    {
        #region Default Values

        /// <summary>
        /// Valor por defecto que indica el formato de los bytes.
        /// </summary>
        public const byte DefaultFormatMarker = 0xC0;

        /// <summary>
        /// Valor por defecto que indica la cantidad de iteraciones.
        /// </summary>
        public const int DefaultIterCount = 50000;

        /// <summary>
        /// Valor por defecto que indica la cantidad de btyes de la cabezera.
        /// </summary>
        public const int DefaultHeaderByteLength = 13;

        /// <summary>
        /// Valor por defecto que indica el algoritmo que se utilizara para la  derivación de claves.
        /// <list type="bullet">
        /// <item>
        /// <term>0</term>
        /// <description>HMACSHA1: The HMAC algorithm (RFC 2104) using the SHA-1 hash function (FIPS 180-4).</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>HMACSHA256: The HMAC algorithm (RFC 2104) using the SHA-256 hash function (FIPS 180-4).</description>
        /// </item>
        /// <item>
        /// <term>2</term>
        /// <description>HMACSHA512: The HMAC algorithm (RFC 2104) using the SHA-512 hash function (FIPS 180-4).</description>
        /// </item>
        /// </list>
        /// <para><strong>Default:</strong> 1.</para>
        /// </summary>
        public static readonly int DefaultKeyDerivationAlgorithm = KeyDerivationPrf.HMACSHA256.ToValue();

        /// <summary>
        /// Valor por defecto que indica la longitud de la encriptación.
        /// <list type="bullet">
        /// <item>
        /// <term>16</term>
        /// <description>BITS128: 128 bits / 8 = 16 bytes.</description>
        /// </item>
        /// <item>
        /// <term>32</term>
        /// <description>BITS256: 256 bits / 8 = 32 bytes.</description>
        /// </item>
        /// <item>
        /// <term>64</term>
        /// <description>BITS512: 512 bits / 8 = 64 bytes.</description>
        /// </item>
        /// </list>
        /// <para><strong>Default:</strong> 16.</para>
        /// </summary>
        public static readonly int DefaultSaltLength = SaltBytesLength.BITS128.ToValue();

        /// <summary>
        /// Valor por defecto que indica el nombre de un algoritmo hash criptográfico.
        /// <list type="bullet">
        /// <item>
        /// <descripction>MD5</descripction>
        /// </item>
        /// <item>
        /// <descripction>SHA1</descripction>
        /// </item>
        /// <item>
        /// <descripction>SHA256</descripction>
        /// </item>
        /// <item>
        /// <descripction>SHA384</descripction>
        /// </item>
        /// <item>
        /// <descripction>SHA256</descripction>
        /// </item>
        /// </list>
        /// <para><strong>Default:</strong> SHA256.</para>
        /// </summary>
        public static readonly string DefaultAlgorithmName = HashAlgorithmName.SHA256.ToString();

        #endregion Default Values

        private byte _formatMarker = DefaultFormatMarker;
        private int _iterCount = DefaultIterCount;
        private int _headerByteLength = DefaultHeaderByteLength;
        private int _keyDerivationAlgorithm = DefaultKeyDerivationAlgorithm;
        private int _saltLength = DefaultSaltLength;
        private string _algorithmName = DefaultAlgorithmName;

        /// <summary>
        /// Obtiene o establece un valor que indica el formato de los bytes.
        /// <para><strong>Default:</strong> <see cref="DefaultFormatMarker"/> = 0xC0.</para>
        /// </summary>
        public byte? FormatMarker
        {
            get => _formatMarker;
            set
            {
                if (value.HasValue)
                {
                    _formatMarker = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica la cantidad de iteraciones.
        /// <para><strong>Default:</strong> <see cref="DefaultIterCount"/> = 50000.</para>
        /// </summary>
        public int? IterCount
        {
            get => _iterCount;
            set
            {
                if (value.HasValue)
                {
                    _iterCount = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica la cantidad de btyes de la cabezera.
        /// <para><strong>Default:</strong> <see cref="DefaultHeaderByteLength"/> = 13.</para>
        /// </summary>
        public int? HeaderByteLength
        {
            get => _headerByteLength;
            set
            {
                if (value.HasValue)
                {
                    _headerByteLength = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si se habilita el servicio de contraseña.
        /// <para><strong>Default:</strong> <see cref="DefaultKeyDerivationAlgorithm"/> = 1.</para>
        /// </summary>
        public int? KeyDerivationAlgorithm
        {
            get => _keyDerivationAlgorithm;
            set
            {
                if (value.HasValue)
                {
                    _keyDerivationAlgorithm = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica la longitud de la encriptación.
        /// <para><strong>Default:</strong> <see cref="DefaultSaltLength"/> = 16.</para>
        /// </summary>
        public int? SaltLength
        {
            get => _saltLength;
            set
            {
                if (value.HasValue)
                {
                    _saltLength = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica el nombre de un algoritmo hash criptográfico.
        /// <para><strong>Default:</strong> <see cref="DefaultAlgorithmName"/> = SHA256.</para>
        /// </summary>
        public string? AlgorithmName
        {
            get => _algorithmName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _algorithmName = value;
                }
            }
        }
    }
}
