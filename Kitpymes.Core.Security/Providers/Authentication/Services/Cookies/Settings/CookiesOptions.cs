// -----------------------------------------------------------------------
// <copyright file="CookiesOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Kitpymes.Core.Shared;

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
    public class CookiesOptions
    {
        /// <summary>
        /// Obtiene la configuración de la autenticación por cookies.
        /// </summary>
        public CookiesSettings CookiesSettings { get; private set; } = new CookiesSettings();

        /// <summary>
        /// Obtiene o establece un valor que indica el servicio esta habilitado.
        /// </summary>
        /// <param name="enabled">Si se habilita el servicio.</param>
        /// <returns>CookiesOptions.</returns>
        public CookiesOptions WithEnabled(bool enabled = true)
        {
            CookiesSettings.Enabled = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece el esquema de autenticación.
        /// </summary>
        /// <param name="authenticateScheme">Nombre del esquema de autenticación.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si authenticateScheme es nulo o vacio.</returns>
        public CookiesOptions WithScheme(string authenticateScheme = CookiesSettings.DefaultAuthenticateScheme)
        {
            CookiesSettings.AuthenticateScheme = authenticateScheme.ThrowIfNullOrEmpty(nameof(authenticateScheme));

            return this;
        }

        /// <summary>
        /// Obtiene o establece el nombre de la cookie.
        /// </summary>
        /// <param name="cookieName">Nombre de la cookie.</param>
        /// <returns>CookiesOptions | ApplicationException: si name es nulo o vacio.</returns>
        public CookiesOptions WithCookieName(string cookieName)
        {
            CookiesSettings.CookieName = cookieName.ThrowIfNullOrEmpty(nameof(cookieName));

            return this;
        }

        /// <summary>
        /// Obtiene o establece la configuración del tiempo de expiración de la cookie.
        /// </summary>
        /// <param name="days">Valor en días.</param>
        /// <param name="hours">Valor en horas.</param>
        /// <param name="minutes">Valor en minutos.</param>
        /// <param name="seconds">Valor en segundos.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si algun valor es menor que cero.</returns>
        public CookiesOptions WithExpire(int days = ExpireSettings.DefaultDays, int hours = ExpireSettings.DefaultHours, int minutes = ExpireSettings.DefaultMinutes, int seconds = ExpireSettings.DefaultSeconds)
        {
            CookiesSettings.Expire = new ExpireSettings
            {
                Days = days.ThrowIfLess(0, nameof(days)),
                Hours = hours.ThrowIfLess(0, nameof(hours)),
                Minutes = minutes.ThrowIfLess(0, nameof(minutes)),
                Seconds = seconds.ThrowIfLess(0, nameof(seconds)),
            };

            return this;
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si se vuelve a generar una nueva cookie cuando este por vencer.
        /// </summary>
        /// <param name="enabled">Si se habilita o debalitia la generación de una nueva cookie cuando este por vencer.</param>
        /// <returns>CookiesOptions.</returns>
        public CookiesOptions WithSlidingExpiration(bool enabled = CookiesSettings.DefaultSlidingExpiration)
        {
            CookiesSettings.SlidingExpiration = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece un valor que indica que no se puede acceder a la cookie a través de JavaScript, solo estara disponible para el servidor.
        /// </summary>
        /// <param name="enabled">Si se habilita o debalitia el acceso a la cookie desde el navegador.</param>
        /// <returns>CookiesOptions.</returns>
        public CookiesOptions WithHttpOnly(bool enabled = CookiesSettings.DefaultHttpOnly)
        {
            CookiesSettings.HttpOnly = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece el path del inicio se sesión.
        /// </summary>
        /// <param name="loginPath">Path del inicio se sesión.</param>
        /// <returns>CookiesOptions | ApplicationException: si loginPath es nulo o vacio.</returns>
        public CookiesOptions WithLoginPath(string loginPath)
        {
            CookiesSettings.LoginPath = loginPath.ThrowIfNullOrEmpty(nameof(loginPath));

            return this;
        }

        /// <summary>
        /// Obtiene o establece el path del fin se sesión.
        /// </summary>
        /// <param name="logoutPath">Path del fin se sesión.</param>
        /// <returns>CookiesOptions | ApplicationException: si logoutPath es nulo o vacio.</returns>
        public CookiesOptions WithLogoutPath(string logoutPath)
        {
            CookiesSettings.LogoutPath = logoutPath.ThrowIfNullOrEmpty(nameof(logoutPath));

            return this;
        }

        /// <summary>
        /// Obtiene o establece el path del acceso denegado.
        /// </summary>
        /// <param name="accessDeniedPath">Path del acceso denegado.</param>
        /// <returns>CookiesOptions | ApplicationException: si accessDeniedPath es nulo o vacio.</returns>
        public CookiesOptions WithAccessDeniedPath(string accessDeniedPath)
        {
            CookiesSettings.AccessDeniedPath = accessDeniedPath.ThrowIfNullOrEmpty(nameof(accessDeniedPath));

            return this;
        }

        /// <summary>
        /// Obtiene o establece los parámetros para el path de inicio o fin de sesión.
        /// </summary>
        /// <param name="returnUrlParameter">Parámetros para el path de inicio o fin de sesión.</param>
        /// <returns>CookiesOptions | ApplicationException: si returnUrlParameter es nulo o vacio.</returns>
#pragma warning disable CA1054 // Los parámetros de URI no deben ser cadenas
        public CookiesOptions WithReturnUrlParameter(string returnUrlParameter)
#pragma warning restore CA1054 // Los parámetros de URI no deben ser cadenas
        {
            CookiesSettings.ReturnUrlParameter = returnUrlParameter.ThrowIfNullOrEmpty(nameof(returnUrlParameter));

            return this;
        }
    }
}
