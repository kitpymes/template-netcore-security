// -----------------------------------------------------------------------
// <copyright file="IPasswordService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Collections.Generic;

    /*
        Interfaz para encriptar contraseñas IPasswordService
        Contiene las firmas para encriptar contraseñas
    */

    /// <summary>
    /// Interfaz para encriptar <c>IPasswordService</c>.
    /// Contiene las firmas para encriptar.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todas las firmas para la encriptación de contraseñas.</para>
    /// </remarks>
    public interface IPasswordService
    {
        /// <summary>
        /// Crea una contraseña.
        /// </summary>
        /// <param name="plainPassword">Contraseña de texto a cifrar.</param>
        /// <returns>(bool isValid, string? hashPassword, List{PasswordResult}? errors).</returns>
        (bool hasErrors, string? hashPassword, List<PasswordResult>? errors) Create(string? plainPassword);

        /// <summary>
        /// Verifica si una contraseña es correcta.
        /// </summary>
        /// <param name="plainPassword">Contraseña de cadena.</param>
        /// <param name="hashPassword">Contraseña cifrada.</param>
        /// <returns>string | ApplicationException: si plainPassword o hashPassword es nulo o vacio.</returns>
        bool Verify(string? plainPassword, string hashPassword);

        /// <summary>
        /// Valida una contraseña.
        /// </summary>
        /// <param name="plainPassword">Contraseña de cadena.</param>
        /// <returns>(bool isValid, List{PasswordResult}? errors).</returns>
        (bool hasErrors, List<PasswordResult>? errors) Validate(string? plainPassword);
    }
}
