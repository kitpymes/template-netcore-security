// -----------------------------------------------------------------------
// <copyright file="IPasswordService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Collections.Generic;

    /*
        Interfaz para encriptar contrase�as IPasswordService
        Contiene las firmas para encriptar contrase�as
    */

    /// <summary>
    /// Interfaz para encriptar <c>IPasswordService</c>.
    /// Contiene las firmas para encriptar.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todas las firmas para la encriptaci�n de contrase�as.</para>
    /// </remarks>
    public interface IPasswordService
    {
        /// <summary>
        /// Crea una contrase�a plana random.
        /// </summary>
        /// <returns>string | null: si no se pudo crear la contrase�a.</returns>
        string? CreateRandom();

        /// <summary>
        /// Crea una contrase�a.
        /// </summary>
        /// <param name="plainPassword">Contrase�a de texto a cifrar.</param>
        /// <returns>(string? hashPassword, List{PasswordErrorResult} errors).</returns>
        (string? hashPassword, List<PasswordErrorResult> errors) Create(string? plainPassword);

        /// <summary>
        /// Verifica si una contrase�a es correcta.
        /// </summary>
        /// <param name="plainPassword">Contrase�a de cadena.</param>
        /// <param name="hashPassword">Contrase�a cifrada.</param>
        /// <returns>bool | ApplicationException: si plainPassword o hashPassword es nulo o vacio.</returns>
        bool Verify(string? plainPassword, string? hashPassword);

        /// <summary>
        /// Valida una contrase�a.
        /// </summary>
        /// <param name="plainPassword">Contrase�a de cadena.</param>
        /// <returns>List{PasswordErrorResult} errors.</returns>
        List<PasswordErrorResult> Validate(string? plainPassword);
    }
}
