// -----------------------------------------------------------------------
// <copyright file="EncryptorServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.IO;
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
            .ThrowIfNullOrEmpty(Shared.Util.Messages.NotFound(nameof(IEncryptorService)));

        /// <summary>
        /// Carga el servicio de encriptación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de encriptación.</param>
        /// <returns>IServiceCollection.</returns>
        internal static IServiceCollection LoadEncryptor(
           this IServiceCollection services,
           Action<EncryptorOptions> settings)
        => services.LoadEncryptor(settings.ToConfigureOrDefault().EncryptorSettings);

        /// <summary>
        /// Carga el servicio de encriptación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de encriptación.</param>
        /// <returns>IServiceCollection | ApplicationException: si EncryptorSettings es nulo.</returns>
        internal static IServiceCollection LoadEncryptor(
            this IServiceCollection services,
            EncryptorSettings settings)
        {
            var config = settings.ThrowIfNullOrEmpty(nameof(settings));

            if (config.Enabled == true)
            {
                services.LoadEncryptor(x =>
                {
                    var environment = services.ToEnvironment();

                    x.SetApplicationName(config.ApplicationName!);

#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
                    x.ProtectKeysWithDpapi();
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma

                    x.SetDefaultKeyLifetime(TimeSpan.FromDays(config.KeyLifetimeFromDays!.Value));

                    if (!config.PersistKeysToFileSystem.IsNullOrEmpty())
                    {
                        if (!config.PersistKeysToFileSystem.IsDirectory())
                        {
                            Directory.CreateDirectory(config.PersistKeysToFileSystem);
                        }

                        x.PersistKeysToFileSystem(new DirectoryInfo(config.PersistKeysToFileSystem));
                    }
                });
            }

            return services;
        }

        /// <summary>
        /// Carga el servicio de encriptación.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="dataProtectionBuilder">Configuración del servicio de encriptación.</param>
        /// <returns>IServiceCollection | ApplicationException: si dataProtectionBuilder es nulo.</returns>s
        internal static IServiceCollection LoadEncryptor(
            this IServiceCollection services,
            Action<IDataProtectionBuilder> dataProtectionBuilder)
        {
            var dataProtector = dataProtectionBuilder.ThrowIfNullOrEmpty(nameof(dataProtectionBuilder));

            var dataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(EncryptorSettings.DefaultPersistKeysToFileSystem), dataProtector);

            services.TryAddSingleton<IEncryptorService>(new EncryptorService(dataProtectionProvider));

            return services;
        }
    }
}
