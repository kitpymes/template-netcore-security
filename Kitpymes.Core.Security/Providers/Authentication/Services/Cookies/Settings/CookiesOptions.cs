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
      Clase de configuraci�n CookiesSettings
      Contiene las propiedades para la configuraci�n de la autenticaci�n por cookies
   */

    /// <summary>
    /// Clase de configuraci�n <c>CookiesSettings</c>.
    /// Contiene las propiedades para la configuraci�n de la autenticaci�n por cookies.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuraci�n de la autenticaci�n por cookies.</para>
    /// </remarks>
    public class CookiesOptions
    {
        /// <summary>
        /// Obtiene la configuraci�n de la autenticaci�n por cookies.
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
        /// Obtiene o establece el esquema de autenticaci�n.
        /// </summary>
        /// <param name="authenticateScheme">Nombre del esquema de autenticaci�n.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si authenticateScheme es nulo o vacio.</returns>
        public CookiesOptions WithScheme(string authenticateScheme = CookiesSettings.DefaultAuthenticateScheme)
        {
            CookiesSettings.AuthenticateScheme = authenticateScheme.ToIsNullOrEmptyThrow(nameof(authenticateScheme));

            return this;
        }

        /// <summary>
        /// Obtiene o establece el nombre de la cookie.
        /// </summary>
        /// <param name="cookieName">Nombre de la cookie.</param>
        /// <returns>CookiesOptions | ApplicationException: si name es nulo o vacio.</returns>
        public CookiesOptions WithCookieName(string cookieName)
        {
            CookiesSettings.CookieName = cookieName.ToIsNullOrEmptyThrow(nameof(cookieName));

            return this;
        }

        /// <summary>
        /// Obtiene o establece la configuraci�n del tiempo de expiraci�n de la cookie.
        /// </summary>
        /// <param name="days">Valor en d�as.</param>
        /// <param name="hours">Valor en horas.</param>
        /// <param name="minutes">Valor en minutos.</param>
        /// <param name="seconds">Valor en segundos.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si algun valor es menor que cero.</returns>
        public CookiesOptions WithExpire(int days = ExpireSettings.DefaultDays, int hours = ExpireSettings.DefaultHours, int minutes = ExpireSettings.DefaultMinutes, int seconds = ExpireSettings.DefaultSeconds)
        {
            CookiesSettings.Expire = new ExpireSettings
            {
                Days = days.ToIsLessThrow(0, nameof(days)),
                Hours = hours.ToIsLessThrow(0, nameof(hours)),
                Minutes = minutes.ToIsLessThrow(0, nameof(minutes)),
                Seconds = seconds.ToIsLessThrow(0, nameof(seconds)),
            };

            return this;
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si se vuelve a generar una nueva cookie cuando este por vencer.
        /// </summary>
        /// <param name="enabled">Si se habilita o debalitia la generaci�n de una nueva cookie cuando este por vencer.</param>
        /// <returns>CookiesOptions.</returns>
        public CookiesOptions WithSlidingExpiration(bool enabled = CookiesSettings.DefaultSlidingExpiration)
        {
            CookiesSettings.SlidingExpiration = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece un valor que indica que no se puede acceder a la cookie a trav�s de JavaScript, solo estara disponible para el servidor.
        /// </summary>
        /// <param name="enabled">Si se habilita o debalitia el acceso a la cookie desde el navegador.</param>
        /// <returns>CookiesOptions.</returns>
        public CookiesOptions WithHttpOnly(bool enabled = CookiesSettings.DefaultHttpOnly)
        {
            CookiesSettings.HttpOnly = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece el path del inicio se sesi�n.
        /// </summary>
        /// <param name="loginPath">Path del inicio se sesi�n.</param>
        /// <returns>CookiesOptions | ApplicationException: si loginPath es nulo o vacio.</returns>
        public CookiesOptions WithLoginPath(string loginPath)
        {
            CookiesSettings.LoginPath = loginPath.ToIsNullOrEmptyThrow(nameof(loginPath));

            return this;
        }

        /// <summary>
        /// Obtiene o establece el path del fin se sesi�n.
        /// </summary>
        /// <param name="logoutPath">Path del fin se sesi�n.</param>
        /// <returns>CookiesOptions | ApplicationException: si logoutPath es nulo o vacio.</returns>
        public CookiesOptions WithLogoutPath(string logoutPath)
        {
            CookiesSettings.LogoutPath = logoutPath.ToIsNullOrEmptyThrow(nameof(logoutPath));

            return this;
        }

        /// <summary>
        /// Obtiene o establece el path del acceso denegado.
        /// </summary>
        /// <param name="accessDeniedPath">Path del acceso denegado.</param>
        /// <returns>CookiesOptions | ApplicationException: si accessDeniedPath es nulo o vacio.</returns>
        public CookiesOptions WithAccessDeniedPath(string accessDeniedPath)
        {
            CookiesSettings.AccessDeniedPath = accessDeniedPath.ToIsNullOrEmptyThrow(nameof(accessDeniedPath));

            return this;
        }

        /// <summary>
        /// Obtiene o establece los par�metros para el path de inicio o fin de sesi�n.
        /// </summary>
        /// <param name="returnUrlParameter">Par�metros para el path de inicio o fin de sesi�n.</param>
        /// <returns>CookiesOptions | ApplicationException: si returnUrlParameter es nulo o vacio.</returns>
#pragma warning disable CA1054 // Los par�metros de URI no deben ser cadenas
        public CookiesOptions WithReturnUrlParameter(string returnUrlParameter)
#pragma warning restore CA1054 // Los par�metros de URI no deben ser cadenas
        {
            CookiesSettings.ReturnUrlParameter = returnUrlParameter.ToIsNullOrEmptyThrow(nameof(returnUrlParameter));

            return this;
        }
    }
}
