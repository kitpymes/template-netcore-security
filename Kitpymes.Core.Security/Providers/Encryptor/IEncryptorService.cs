// -----------------------------------------------------------------------
// <copyright file="IEncryptorService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;

    /*
        Interfaz para encriptar IEncryptorService
        Contiene las firmas para encriptar
    */

    /// <summary>
    /// Interfaz para encriptar <c>IEncryptorService</c>.
    /// Contiene las firmas para encriptar.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todas las firmas para la encriptación.</para>
    /// </remarks>
    public interface IEncryptorService
    {
        /// <summary>
        /// Encripta una cadena.
        /// </summary>
        /// <param name="value">Valor a encriptar.</param>
        /// <param name="lifetime">Duración.</param>
        /// <returns>string | ApplicationException: si el valor es nulo o vacio.</returns>
        string Encrypt(string? value, TimeSpan? lifetime = null);

        /// <summary>
        /// Desencripta un cadena.
        /// </summary>
        /// <param name="value">Valor a desencriptar.</param>
        /// <returns>string | ApplicationException: si el valor es nulo o vacio.</returns>
        string Decrypt(string? value);

        /// <summary>
        /// Encripta un objeto.
        /// </summary>
        /// <param name="value">Objeto a encriptar.</param>
        /// <param name="lifetime">Duración.</param>
        /// <returns>string | ApplicationException: si el objeto es nulo.</returns>
        string Encrypt<T>(T value, TimeSpan? lifetime = null)
            where T : class;

        /// <summary>
        /// Desencripta un objeto.
        /// </summary>
        /// <param name="value">Objeto a desencriptar.</param>
        /// <returns>string | ApplicationException: si el objeto es nulo.</returns>
        T Decrypt<T>(string? value)
            where T : class, new();
    }
}
