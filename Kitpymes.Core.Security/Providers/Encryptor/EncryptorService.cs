// -----------------------------------------------------------------------
// <copyright file="EncryptorService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.DataProtection;

    /*
        Clase para encriptar EncryptorService
        Contiene los m�todos para encriptar
    */

    /// <summary>
    /// Clase para encriptar <c>EncryptorService</c>.
    /// Contiene los m�todos para encriptar.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todos los m�todos para la encriptaci�n.</para>
    /// </remarks>
    public class EncryptorService : IEncryptorService
    {
        private readonly IDataProtector? _protector;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EncryptorService"/>.
        /// </summary>
        /// <param name="provider">Proveedor de protecci�n.</param>
        public EncryptorService(IDataProtectionProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            _protector = provider.CreateProtector(typeof(EncryptorService).Name);
        }

        /// <inheritdoc/>
        public string Encrypt(string value, TimeSpan? lifetime = null)
        {
            if (_protector is null)
            {
                throw new ArgumentNullException(nameof(_protector));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (lifetime.HasValue)
            {
                return _protector.ToTimeLimitedDataProtector().Protect(value, lifetime.Value);
            }

            return _protector.Protect(value);
        }

        /// <inheritdoc/>
        public string Decrypt(string value)
        {
            if (_protector is null)
            {
                throw new ArgumentNullException(nameof(_protector));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return _protector.Unprotect(value);
        }

        /// <inheritdoc/>
        public string Encrypt<T>(T value, TimeSpan? lifetime = null)
            where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Encrypt(value.ToSerialize(), lifetime);
        }

        /// <inheritdoc/>
        public TResult? Decrypt<TResult>(string value)
            where TResult : class, new()
        => Decrypt(value).ToDeserialize<TResult>();
    }
}