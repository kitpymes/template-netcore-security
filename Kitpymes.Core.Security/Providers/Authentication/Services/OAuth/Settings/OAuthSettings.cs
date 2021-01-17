// -----------------------------------------------------------------------
// <copyright file="OAuthSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Authentication.OAuth;

    /*
       Clase de configuración OAuthSettings
       Contiene las propiedades para la configuración del OAuth
    */

    /// <summary>
    /// Clase de configuración <c>OAuthSettings</c>.
    /// Contiene las propiedades para la configuración del OAuth.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración del OAuth.</para>
    /// </remarks>
    public class OAuthSettings
    {
        // https://www.jerriepelser.com/blog/authenticate-oauth-aspnet-core-2/

        // https://www.red-gate.com/simple-talk/dotnet/net-development/oauth-2-0-with-github-in-asp-net-core/

        // https://medium.com/@mauridb/using-oauth2-middleware-with-asp-net-core-2-0-b31ffef58cd0

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
        /// Obtiene o establece las opciones del OAuth.
        /// </summary>
        [JsonIgnore]
        public OAuthOptions? OAuthOptions { get; set; }
    }
}