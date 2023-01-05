// -----------------------------------------------------------------------
// <copyright file="AuthenticationServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /*
        Clase de extensión AuthenticationServiceCollectionExtensions
        Contiene las extensiones de los servicios para la autenticación
    */

    /// <summary>
    /// Clase de extensión <c>AuthenticationServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios para la autenticación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para los servicios de autenticación.</para>
    /// </remarks>
    internal static class AuthenticationServiceCollectionExtensions
    {
        /// <summary>
        /// Carga el servicio de autenticación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="configuration">Configuración del servicio de autenticación.</param>
        /// <returns>IServiceCollection | ApplicationException: si SecuritySettings o AuthenticationSettings es nulo.</returns>
        public static IServiceCollection LoadAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration?
                .GetSection(nameof(SecuritySettings))?
                    .GetSection(nameof(AuthenticationSettings))?
                        .Get<AuthenticationSettings>();

            return services.LoadAuthentication(settings);
        }

        /// <summary>
        /// Carga el servicio de autenticación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de autenticación.</param>
        /// <returns>IServiceCollection | ApplicationException: si AuthenticationSettings es nulo.</returns>
        public static IServiceCollection LoadAuthentication(this IServiceCollection services, Action<AuthenticationSettings> options)
        {
            var settings = options.ToConfigureOrDefault();

            return services.LoadAuthentication(settings);
        }

        /// <summary>
        /// Carga el servicio de autenticación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de autenticación.</param>
        /// <returns>IServiceCollection | ApplicationException: si AuthenticationSettings es nulo.</returns>
        public static IServiceCollection LoadAuthentication(this IServiceCollection services, AuthenticationSettings? settings)
        {
            var config = settings.ThrowIfNullOrEmpty(nameof(settings));

            if (config.JsonWebTokenSettings?.Enabled == true)
            {
                services.LoadAuthJsonWebToken(config.JsonWebTokenSettings);
            }

            if (config.CookiesSettings?.Enabled == true)
            {
                services.LoadAuthCookies(config.CookiesSettings);
            }

            if (config.OAuthSettings?.Enabled == true)
            {
                // services.LoadOAuth(config.OAuthSettings);
                throw new NotImplementedException();
            }

            return services;
        }
    }
}