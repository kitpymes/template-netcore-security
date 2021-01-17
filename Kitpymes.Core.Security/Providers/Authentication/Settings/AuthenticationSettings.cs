// -----------------------------------------------------------------------
// <copyright file="AuthenticationSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
       Clase de configuración AuthenticationSettings
       Contiene las propiedades para la configuración de la autenticación
    */

    /// <summary>
    /// Clase de configuración <c>AuthenticationSettings</c>.
    /// Contiene las propiedades para la configuración de la autenticación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la autenticación.</para>
    /// </remarks>
    public class AuthenticationSettings
    {
        /// <summary>
        /// Valor por defecto que indica si esta habilitado el servicio.
        /// </summary>
        public const bool DefaultEnabled = false;

        private bool _enabled = DefaultEnabled;

        /// <summary>
        /// Obtiene o establece un valor que indica el servicio esta habilitado.
        /// <para><strong>Default:</strong> <see cref="DefaultEnabled"/> = false.</para>
        /// </summary>
        public bool? Enabled
        {
            get => _enabled;
            set
            {
                if (value.HasValue)
                {
                    _enabled = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la configuración para controlar el comportamiento de la autenticación con Json Web Token.
        /// </summary>
        public JsonWebTokenSettings? JsonWebTokenSettings { get; set; }

        /// <summary>
        /// Obtiene o establece la configuración para controlar el comportamiento de la autenticación con Cookies.
        /// </summary>
        public CookiesSettings? CookiesSettings { get; set; }

        /// <summary>
        /// Obtiene o establece la configuración para controlar el comportamiento de la autenticación con OAuth.
        /// </summary>
        public OAuthSettings? OAuthSettings { get; set; }
    }
}
