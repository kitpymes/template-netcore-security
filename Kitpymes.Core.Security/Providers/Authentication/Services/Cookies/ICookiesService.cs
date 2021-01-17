// -----------------------------------------------------------------------
// <copyright file="ICookiesService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;

    /*
        Interfaz para autenticarse por medio de cookies ICookiesService
        Contiene las firmas para autenticarse por medio de cookies
    */

    /// <summary>
    /// Interfaz para autenticarse por medio de cookiesn <c>ICookiesService</c>.
    /// Contiene las firmas para autenticarse por medio de cookies.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todas las firmas para autenticarse por medio de cookies.</para>
    /// </remarks>
    public interface ICookiesService
    {
        /// <summary>
        /// Inicie sesión por medio de cookies.
        /// </summary>
        /// <param name="claims">Datos que queremos almacenar en la autenticación.</param>
        /// <param name="options">Configuración de la autenticación.</param>
        /// <returns>Task.</returns>
        Task SignInAsync(IEnumerable<Claim> claims, Action<AuthenticationProperties>? options = null);

        /// <summary>
        /// Cierra una sesión de cookies.
        /// </summary>
        /// <returns>Task.</returns>
        Task SignOutAsync();
    }
}