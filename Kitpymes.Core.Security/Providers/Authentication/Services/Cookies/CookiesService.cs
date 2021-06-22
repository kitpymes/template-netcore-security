// -----------------------------------------------------------------------
// <copyright file="CookiesService.cs" company="Kitpymes">
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
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;

    /*
        Clase para autenticarse por medio de cookies CookiesService
        Contiene la funcionalidad para autenticarse por medio de cookies
    */

    /// <summary>
    /// Interfaz para el token de sesión <c>CookiesService</c>.
    /// Contiene la funcionalidad para autenticarse por medio de cookies.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar toda la funcionalidad para autenticarse por medio de cookies.</para>
    /// </remarks>
    public class CookiesService : ICookiesService
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CookiesService"/>.
        /// </summary>
        /// <param name="settings">Configuración de la autenticación.</param>
        /// <param name="authenticationService">Servicio para autenticarse.</param>
        /// <param name="httpContextAccessor">Servicio para acceder al cotexto.</param>
        public CookiesService(CookiesSettings settings, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            CookiesSettings = settings;
            AuthenticationService = authenticationService;
            HttpContextAccessor = httpContextAccessor;
        }

        private CookiesSettings CookiesSettings { get; }

        private IAuthenticationService AuthenticationService { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private AuthenticationProperties? AuthenticationProperties { get; set; }

        /// <inheritdoc/>
        public async Task SignInAsync(IEnumerable<Claim> claims, Action<AuthenticationProperties>? options = null)
        {
            if (HttpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(HttpContextAccessor.HttpContext));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookiesSettings.AuthenticateScheme));

            AuthenticationProperties = options.ToConfigureOrDefault();

            await AuthenticationService.SignInAsync(
                HttpContextAccessor.HttpContext,
                CookiesSettings.AuthenticateScheme,
                claimsPrincipal,
                AuthenticationProperties)
            .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task SignOutAsync()
        {
            if (HttpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(HttpContextAccessor.HttpContext));
            }

            await AuthenticationService.SignOutAsync(
                HttpContextAccessor.HttpContext,
                CookiesSettings.AuthenticateScheme,
                AuthenticationProperties);
        }
    }
}