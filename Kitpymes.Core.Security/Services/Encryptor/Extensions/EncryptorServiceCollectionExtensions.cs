// -----------------------------------------------------------------------
// <copyright file="EncryptorServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /*
        Clase de extensión EncryptorServiceCollectionExtensions
        Contiene las extensiones de los servicios de encriptación
    */

    /// <summary>
    /// Clase de extensión <c>EncryptorServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios de encriptación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para la encriptación.</para>
    /// </remarks>
    public static class EncryptorServiceCollectionExtensions
    {
        /// <summary>
        /// Obtiene el proveedor de encriptación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <returns>IEncryptorProvider | ApplicationException: si no se encuentra el servicio IEncryptorProvider.</returns>
        public static IEncryptorService GetEncryptor(this IServiceCollection services)
        => services.ToService<IEncryptorService>()
            .ToIsNullOrEmptyThrow(Shared.Util.Messages.NotFound(nameof(IEncryptorService)));

        /// <summary>
        /// Carga el servicio de encriptación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="enabled">Si se habilita el servicio de encriptación.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection LoadEncryptor(
           this IServiceCollection services,
           bool enabled = true)
        => services.LoadEncryptor(new EncryptorSettings { Enabled = enabled });

        /// <summary>
        /// Carga el servicio de encriptación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de encriptación.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection LoadEncryptor(
            this IServiceCollection services,
            EncryptorSettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.Enabled.HasValue && config.Enabled.Value)
            {
                if (!services.ToExists<IDataProtectionProvider>())
                {
                    services.AddDataProtection();
                }

                var dataProtectionProvider = services.ToService<IDataProtectionProvider>();

                services.TryAddSingleton<IEncryptorService>(new EncryptorService(dataProtectionProvider));
            }

            return services;
        }
    }
}
