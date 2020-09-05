// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /*
       Clase de extensión JsonWebTokenServiceCollectionExtensions
       Contiene las extensiones de los servicios para el token de sesión
    */

    /// <summary>
    /// Clase de extensión <c>JsonWebTokenServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios para el token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para el token de sesión.</para>
    /// </remarks>
    public static class JsonWebTokenServiceCollectionExtensions
    {
        /// <summary>
        /// Obtiene el proveedor de tokens.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <returns>v | ApplicationException: si no se encuentra el servicio IJsonWebTokenProvider.</returns>
        public static IJsonWebTokenService GetJsonWebToken(this IServiceCollection services)
        => services.ToService<IJsonWebTokenService>()
            .ToIsNullOrEmptyThrow(Shared.Util.Messages.NotFound(nameof(IJsonWebTokenService)));

        /// <summary>
        /// Carga el servicio de tokens.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de tokens.</param>
        /// <param name="enabled">Si se habilita el del servicio de tokens.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection LoadJsonWebToken(
            this IServiceCollection services,
            Action<JsonWebTokenOptions>? options = null,
            bool enabled = true)
        {
            var config = options.ToConfigureOrDefault();

            config.JsonWebTokenSettings.Enabled = enabled;

            return services.LoadJsonWebToken(config.JsonWebTokenSettings);
        }

        /// <summary>
        /// Carga el servicio de tokens.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de tokens.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection LoadJsonWebToken(
            this IServiceCollection services,
            JsonWebTokenSettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.Enabled.HasValue && config.Enabled.Value)
            {
                services.TryAddSingleton<IJsonWebTokenService>(new JsonWebTokenService(config));
            }

            return services;
        }
    }
}
