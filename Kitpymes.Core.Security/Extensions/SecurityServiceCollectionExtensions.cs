// -----------------------------------------------------------------------
// <copyright file="SecurityServiceCollectionExtensions.cs" company="Kitpymes">
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
        Clase de extensión SecurityServiceCollectionExtensions
        Contiene las extensiones de los servicios de seguridad
    */

    /// <summary>
    /// Clase de extensión <c>SecurityServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios de seguridad.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para la seguridad.</para>
    /// </remarks>
    public static class SecurityServiceCollectionExtensions
    {
        /// <summary>
        /// Carga el servicio de seguridad.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="configuration">Configuración del servicio de seguridad.</param>
        /// <returns>IServiceCollection | ApplicationException: si SecuritySettings es nulo.</returns>
        public static IServiceCollection LoadSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration?.GetSection(nameof(SecuritySettings))?.Get<SecuritySettings>();

            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return services.LoadSecurity(config);
        }

        /// <summary>
        /// Carga el servicio de seguridad.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de seguridad.</param>
        /// <returns>IServiceCollection | ApplicationException: si SecuritySettings es nulo.</returns>
        public static IServiceCollection LoadSecurity(
            this IServiceCollection services,
            Action<SecurityOptions> options)
        => services.LoadSecurity(options.ToConfigureOrDefault().SecuritySettings);

        /// <summary>
        /// Carga el servicio de seguridad.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de seguridad.</param>
        /// <returns>IServiceCollection | ApplicationException: si SecuritySettings es nulo.</returns>
        public static IServiceCollection LoadSecurity(
            this IServiceCollection services,
            SecuritySettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.EncryptorSettings?.Enabled == true)
            {
                services.LoadEncryptor(config.EncryptorSettings);
            }

            if (config.AuthenticationSettings?.Enabled == true)
            {
                services.LoadAuthentication(config.AuthenticationSettings);
            }

            if (config.PasswordSettings?.Enabled == true)
            {
                services.LoadPassword(config.PasswordSettings);
            }

            return services;
        }
    }
}
