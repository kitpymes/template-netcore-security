// -----------------------------------------------------------------------
// <copyright file="EncryptorService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.DataProtection;

    /*
        Clase para encriptar EncryptorService
        Contiene los métodos para encriptar
    */

    /// <summary>
    /// Clase para encriptar <c>EncryptorService</c>.
    /// Contiene los métodos para encriptar.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todos los métodos para la encriptación.</para>
    /// </remarks>
    public class EncryptorService : IEncryptorService
    {
        private readonly IDataProtector? _protector;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EncryptorService"/>.
        /// </summary>
        /// <param name="provider">Proveedor de protección.</param>
        public EncryptorService(IDataProtectionProvider provider)
        => _protector = provider.ToIsNullOrEmptyThrow(nameof(provider)).CreateProtector(GetType().FullName);

        /// <inheritdoc/>
        public string Encrypt(string? value)
        => _protector.Protect(value.ToIsNullOrEmptyThrow(nameof(value)));

        /// <inheritdoc/>
        public string Decrypt(string? value)
        => _protector.Unprotect(value.ToIsNullOrEmptyThrow(nameof(value)));

        /// <inheritdoc/>
        public string Encrypt<T>(T value)
            where T : class
        => Encrypt(value.ToIsNullOrEmptyThrow(nameof(value)).ToSerialize());

        /// <inheritdoc/>
        public T Decrypt<T>(string? value)
            where T : class, new()
        => Decrypt(value.ToIsNullOrEmptyThrow(nameof(value))).ToDeserialize<T>();
    }
}