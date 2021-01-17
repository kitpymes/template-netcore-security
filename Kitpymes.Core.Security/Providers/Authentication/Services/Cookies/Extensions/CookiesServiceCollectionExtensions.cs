// -----------------------------------------------------------------------
// <copyright file="CookiesServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;

    /*
       Clase de extensión CookiesServiceCollectionExtensions
       Contiene las extensiones de los servicios para la autenticación por cookies
    */

    /// <summary>
    /// Clase de extensión <c>JsonWebTokenServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios para la autenticación por cookies.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para la autenticación por cookies.</para>
    /// </remarks>
    public static class CookiesServiceCollectionExtensions
    {
        /// <summary>
        /// Obtiene el servicio de autenticación por cookies.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <returns>ICookiesService | ApplicationException: si no se encuentra el servicio ICookiesService.</returns>
        public static ICookiesService GetAuthCookies(this IServiceCollection services)
        => services.ToService<ICookiesService>()
            .ToIsNullOrEmptyThrow(Shared.Util.Messages.NotFound(nameof(ICookiesService)));

        /// <summary>
        /// Carga el servicio de autenticación por cookies.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de autenticación por cookies.</param>
        /// <returns>IServiceCollection | ApplicationException: si AuthenticateScheme o CookieName son nulo o vacio.</returns>
        internal static IServiceCollection LoadAuthCookies(
            this IServiceCollection services,
            Action<CookiesOptions> options)
        => services.LoadAuthCookies(options.ToConfigureOrDefault().CookiesSettings);

        /// <summary>
        /// Carga el servicio de autenticación por cookies.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de autenticación por cookies.</param>
        /// <returns>IServiceCollection | ApplicationException: si CookiesSettings es nulo.</returns>
        internal static IServiceCollection LoadAuthCookies(
            this IServiceCollection services,
            CookiesSettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.Enabled.HasValue && config.Enabled.Value)
            {
                var enviroment = services.ToEnvironment();

                services
                    .AddAuthentication(config.AuthenticateScheme)
                    .AddCookie(options =>
                    {
                        // limita la cookie a HTTPS. Recomiendo configurar esto en Siempre en prod. Déjelo configurado en Ninguno en local.
                        options.Cookie.SecurePolicy = enviroment.IsDevelopment()
                            ? CookieSecurePolicy.None : CookieSecurePolicy.Always;

                        // indica si el navegador puede usar la cookie con solicitudes entre sitios.
                        // Para la autenticación OAuth, configúrelo en Lax.
                        // Estoy configurando esto en Estricto porque la cookie de autenticación es solo para un único sitio.
                        // Establecer esto en Ninguno no establece un valor de encabezado de cookie.
                        options.Cookie.SameSite = SameSiteMode.Strict;

                        options.Cookie.Name = config.CookieName;

                        options.Cookie.HttpOnly = config.HttpOnly!.Value;

                        options.ExpireTimeSpan = new TimeSpan(
                           config.Expire.Days!.Value,
                           config.Expire.Hours!.Value,
                           config.Expire.Minutes!.Value,
                           config.Expire.Seconds!.Value);

                        options.LoginPath = config.LoginPath;

                        options.LogoutPath = config.LogoutPath;

                        options.AccessDeniedPath = config.AccessDeniedPath;

                        options.ReturnUrlParameter = config.ReturnUrlParameter;

                        options.SlidingExpiration = config.SlidingExpiration!.Value;

                        // options.EventsType = typeof(CustomCookieAuthenticationEvents);
                    });

                var authenticationService = services.ToService<IAuthenticationService>();

                if (!services.ToExists<IHttpContextAccessor>())
                {
                    services.AddHttpContextAccessor();
                }

                var httpContextAccessor = services.ToService<IHttpContextAccessor>();

                // services.AddScoped<CustomCookieAuthenticationEvents>();
                services.TryAddSingleton<ICookiesService>(new CookiesService(config, authenticationService, httpContextAccessor));
            }

            return services;
        }
    }
}
