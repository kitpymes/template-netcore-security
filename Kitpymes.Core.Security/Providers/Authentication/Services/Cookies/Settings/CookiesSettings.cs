// -----------------------------------------------------------------------
// <copyright file="CookiesSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Microsoft.AspNetCore.Authentication.Cookies;

    /*
       Clase de configuración CookiesSettings
       Contiene las propiedades para la configuración de la autenticación por cookies
    */

    /// <summary>
    /// Clase de configuración <c>CookiesSettings</c>.
    /// Contiene las propiedades para la configuración de la autenticación por cookies.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la autenticación por cookies.</para>
    /// </remarks>
    public class CookiesSettings
    {
        /// <summary>
        /// Valor por defecto que indica si esta habilitado el servicio.
        /// </summary>
        public const bool DefaultEnabled = false;

        /// <summary>
        /// Valor por defecto que indica si se vuelve a generar una nueva cookie cuando este por vencer.
        /// </summary>
        public const bool DefaultSlidingExpiration = true;

        /// <summary>
        /// Valor por defecto que indica el esquema de autenticación.
        /// </summary>
        public const string DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        /// <summary>
        /// Valor por defecto que indica que no se puede acceder a la cookie a través de JavaScript, solo estara disponible para el servidor.
        /// </summary>
        public const bool DefaultHttpOnly = true;

        private bool _enabled = DefaultEnabled;
        private string _authenticateScheme = DefaultAuthenticateScheme;
        private bool _slidingExpiration = DefaultSlidingExpiration;
        private bool _httpOnly = DefaultHttpOnly;

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
        /// Obtiene o establece el esquema de autenticación.
        /// <para><strong>Default:</strong> <see cref="DefaultAuthenticateScheme"/> = "Cookies".</para>
        /// </summary>
        public string? AuthenticateScheme
        {
            get => _authenticateScheme;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _authenticateScheme = value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si se vuelve a generar una nueva cookie cuando este por vencer.
        /// <para><strong>Default:</strong> <see cref="DefaultSlidingExpiration"/> = true.</para>
        /// </summary>
        public bool? SlidingExpiration
        {
            get => _slidingExpiration;
            set
            {
                if (value.HasValue)
                {
                    _slidingExpiration = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica que no se puede acceder a la cookie a través de JavaScript, solo estara disponible para el servidor.
        /// <para><strong>Default:</strong> <see cref="DefaultHttpOnly"/> = true.</para>
        /// </summary>
        public bool? HttpOnly
        {
            get => _httpOnly;
            set
            {
                if (value.HasValue)
                {
                    _httpOnly = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la configuración del tiempo de expiración de la cookie.
        /// </summary>
        public ExpireSettings Expire { get; set; } = new ExpireSettings();

        /// <summary>
        /// Obtiene o establece el nombre de la cookie.
        /// </summary>
        public string? CookieName { get; set; }

        /// <summary>
        /// Obtiene o establece el path del inicio se sesión.
        /// </summary>
        public string? LoginPath { get; set; }

        /// <summary>
        /// Obtiene o establece el path del fin se sesión.
        /// </summary>
        public string? LogoutPath { get; set; }

        /// <summary>
        /// Obtiene o establece el path del acceso denegado.
        /// </summary>
        public string? AccessDeniedPath { get; set; }

        /// <summary>
        /// Obtiene o establece los parámetros para el path de inicio o fin de sesión.
        /// </summary>
#pragma warning disable CA1056 // Las propiedades URI no deben ser cadenas
        public string? ReturnUrlParameter { get; set; }
#pragma warning restore CA1056 // Las propiedades URI no deben ser cadenas
    }
}
