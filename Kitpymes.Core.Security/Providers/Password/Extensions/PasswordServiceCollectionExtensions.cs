// -----------------------------------------------------------------------
// <copyright file="PasswordServiceCollectionExtensions.cs" company="Kitpymes">
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
        Clase de extensión PasswordServiceCollectionExtensions
        Contiene las extensiones de los servicios de contraseña
    */

    /// <summary>
    /// Clase de extensión <c>PasswordServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios de contraseña.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para la contraseña.</para>
    /// </remarks>
    public static class PasswordServiceCollectionExtensions
    {
        /// <summary>
        /// Obtiene el servicio de contraseñas.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <returns>IPasswordService | ApplicationException: si no encuentra el servicio IPasswordService.</returns>
        public static IPasswordService GetPassword(this IServiceCollection services)
        => services.ToService<IPasswordService>()
            .ToIsNullOrEmptyThrow(Shared.Util.Messages.NotFound(nameof(IPasswordService)));

        /// <summary>
        /// Carga el servicio de contraseñas.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de contraseñas.</param>
        /// <returns>IServiceCollection.</returns>
        internal static IServiceCollection LoadPassword(
            this IServiceCollection services,
            Action<PasswordOptions> options)
         => services.LoadPassword(options.ToConfigureOrDefault().PasswordSettings);

        /// <summary>
        /// Carga el servicio de contraseñas.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de contraseñas.</param>
        /// <returns>IServiceCollection | ApplicationException: si PasswordSettings es nulo.</returns>
        internal static IServiceCollection LoadPassword(
            this IServiceCollection services,
            PasswordSettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.Enabled.HasValue && config.Enabled.Value)
            {
                services.TryAddSingleton<IPasswordService>(new PasswordService(config));
            }

            return services;
        }
    }
}
