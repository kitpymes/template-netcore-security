// -----------------------------------------------------------------------
// <copyright file="SecurityOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;
    using Microsoft.Extensions.Configuration;

    /*
       Clase de configuración SecurityOptions
       Contiene las propiedades para la configuración de la seguridad
    */

    /// <summary>
    /// Clase de configuración <c>SecurityOptions</c>.
    /// Contiene las propiedades para la configuración de la seguridad.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la seguridad.</para>
    /// </remarks>
    public class SecurityOptions
    {
        /// <summary>
        /// Obtiene la configuración de la seguridad.
        /// </summary>
        public SecuritySettings SecuritySettings { get; private set; } = new SecuritySettings();

        #region Encryptor

        /// <summary>
        /// Indica si se utilizara el servicio de encriptación.
        /// </summary>
        /// <param name="configuration">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si EncryptorSettings es nulo.</returns>
        public SecurityOptions WithEncryptor(IConfiguration configuration)
        {
            var settings = configuration?.GetSection(nameof(SecuritySettings))?.GetSection(nameof(EncryptorSettings))?.Get<EncryptorSettings>();

            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return WithEncryptor(config);
        }

        /// <summary>
        /// Indica si se utilizara el servicio de encriptación.
        /// </summary>
        /// <param name="options">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si EncryptorSettings es nulo.</returns>
        public SecurityOptions WithEncryptor(Action<EncryptorOptions> options)
        => WithEncryptor(options.ToConfigureOrDefault().EncryptorSettings);

        /// <summary>
        /// Indica si se utilizara el servicio de encriptación.
        /// </summary>
        /// <param name="settings">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si EncryptorSettings es nulo.</returns>
        public SecurityOptions WithEncryptor(EncryptorSettings settings)
        {
            SecuritySettings.EncryptorSettings = settings;

            return this;
        }

        #endregion Encryptor

        #region Authentication

        /// <summary>
        /// Indica si se utilizara el servicio de autenticación.
        /// </summary>
        /// <param name="configuration">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si AuthenticationSettings es nulo.</returns>
        public SecurityOptions WithAuthentication(IConfiguration configuration)
        {
            var settings = configuration?.GetSection(nameof(SecuritySettings))?.GetSection(nameof(AuthenticationSettings))?.Get<AuthenticationSettings>();

            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return WithAuthentication(config);
        }

        /// <summary>
        /// Indica si se utilizara el servicio de autenticación.
        /// </summary>
        /// <param name="options">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si AuthenticationSettings es nulo.</returns>
        public SecurityOptions WithAuthentication(Action<AuthenticationOptions> options)
        => WithAuthentication(options.ToConfigureOrDefault().AuthenticationSettings);

        /// <summary>
        /// Indica si se utilizara el servicio de autenticación.
        /// </summary>
        /// <param name="settings">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si AuthenticationSettings es nulo.</returns>
        public SecurityOptions WithAuthentication(AuthenticationSettings settings)
        {
            SecuritySettings.AuthenticationSettings = settings.ToIsNullOrEmptyThrow(nameof(settings));

            SecuritySettings.AuthenticationSettings.Enabled = true;

            return this;
        }

        #endregion Authentication

        #region Password

        /// <summary>
        /// Indica si se utilizara el servicio para las contraseña.
        /// </summary>
        /// <param name="configuration">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si PasswordSettings es nulo.</returns>
        public SecurityOptions WithPassword(IConfiguration configuration)
        {
            var settings = configuration?.GetSection(nameof(SecuritySettings))?.GetSection(nameof(PasswordSettings))?.Get<PasswordSettings>();

            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return WithPassword(config);
        }

        /// <summary>
        /// Indica si se utilizara el servicio para las  contraseña.
        /// </summary>
        /// <param name="options">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si PasswordSettings es nulo.</returns>
        public SecurityOptions WithPassword(Action<PasswordOptions> options)
        => WithPassword(options.ToConfigureOrDefault().PasswordSettings);

        /// <summary>
        /// Indica si se utilizara el servicio para las contraseña.
        /// </summary>
        /// <param name="settings">Configuración del servicio.</param>
        /// <returns>SecurityOptions | ApplicationException: si PasswordSettings es nulo.</returns>
        public SecurityOptions WithPassword(PasswordSettings settings)
        {
            SecuritySettings.PasswordSettings = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return this;
        }

        #endregion Password
    }
}
