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

        /// <summary>
        /// Indica si se utilizara el servicio de encriptación.
        /// </summary>
        /// <param name="enabled">Si habilita el servicio.</param>
        /// <returns>SecurityOptions.</returns>
        public SecurityOptions WithEncryptor(bool enabled = true)
        {
            SecuritySettings.EncryptorSettings.Enabled = enabled;

            return this;
        }

        /// <summary>
        /// Indica si se utilizara el servicio del token de sesión.
        /// </summary>
        /// <param name="options">Configuración del servicio.</param>
        /// <param name="enabled">Si habilita el servicio.</param>
        /// <returns>SecurityOptions.</returns>
        public SecurityOptions WithJsonWebToken(Action<JsonWebTokenOptions> options, bool enabled = true)
        {
            var config = options.ToConfigureOrDefault();

            config.JsonWebTokenSettings.Enabled = enabled;

            return WithJsonWebToken(config.JsonWebTokenSettings);
        }

        /// <summary>
        /// Indica si se utilizara el servicio del token de sesión.
        /// </summary>
        /// <param name="settings">Configuración del servicio.</param>
        /// <returns>SecurityOptions.</returns>
        public SecurityOptions WithJsonWebToken(JsonWebTokenSettings settings)
        {
            SecuritySettings.JsonWebTokenSettings = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return this;
        }

        /// <summary>
        /// Indica si se utilizara el servicio para las  contraseña.
        /// </summary>
        /// <param name="options">Configuración del servicio.</param>
        /// <param name="enabled">Si habilita el servicio.</param>
        /// <returns>SecurityOptions.</returns>
        public SecurityOptions WithPassword(Action<PasswordOptions> options, bool enabled = true)
        {
            var config = options.ToConfigureOrDefault();

            config.PasswordSettings.Enabled = enabled;

            return WithPassword(config.PasswordSettings);
        }

        /// <summary>
        /// Indica si se utilizara el servicio para las contraseña.
        /// </summary>
        /// <param name="settings">Configuración del servicio.</param>
        /// <returns>SecurityOptions.</returns>
        public SecurityOptions WithPassword(PasswordSettings settings)
        {
            SecuritySettings.PasswordSettings = settings.ToIsNullOrEmptyThrow(nameof(settings));

            return this;
        }
    }
}
