// -----------------------------------------------------------------------
// <copyright file="OAuthServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;
    using Microsoft.Extensions.DependencyInjection;

    /*
       Clase de extensión OAuthServiceCollectionExtensions
       Contiene las extensiones de los servicios para el token de sesión
    */

    /// <summary>
    /// Clase de extensión <c>OAuthServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios para el token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para el token de sesión.</para>
    /// </remarks>
    internal static class OAuthServiceCollectionExtensions
    {
        /// <summary>
        /// Obtiene el proveedor de tokens.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <returns>IJsonWebTokenService | ApplicationException: si no se encuentra el servicio IJsonWebTokenProvider.</returns>
        public static IJsonWebTokenService GetAuthOAuth(this IServiceCollection services)
        => services.ToService<IJsonWebTokenService>()
            .ToIsNullOrEmptyThrow(Shared.Util.Messages.NotFound(nameof(IJsonWebTokenService)));

        /// <summary>
        /// Carga el servicio de tokens.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de tokens.</param>
        /// <returns>IServiceCollection | ApplicationException: si JsonWebTokenSettings es nulo.</returns>
        public static IServiceCollection LoadAuthOAuth(
            this IServiceCollection services,
            Action<OAuthSettings> options)
        => services.LoadAuthOAuth(options.ToConfigureOrDefault());

        /// <summary>
        /// Carga el servicio de tokens.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de tokens.</param>
        /// <returns>IServiceCollection | ApplicationException: si JsonWebTokenSettings es nulo.</returns>
        public static IServiceCollection LoadAuthOAuth(
            this IServiceCollection services,
            OAuthSettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.Enabled.HasValue && config.Enabled.Value)
            {
                /*
                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                var environment = services.ToEnvironment();

                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = config.AuthenticateScheme;

                        options.DefaultChallengeScheme = config.ChallengeScheme;
                    }).AddOAuth(config.ChallengeScheme, options =>
                    {
                        options.ClientId = Configuration["GitHub:ClientId"];
                        options.ClientSecret = Configuration["GitHub:ClientSecret"];
                        options.CallbackPath = new PathString("/signin-github");

                        options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                        options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                        options.UserInformationEndpoint = "https://api.github.com/user";

                        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                        options.ClaimActions.MapJsonKey("urn:github:login", "login");
                        options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                        options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                        options.Events = new OAuthEvents
                        {
                            OnCreatingTicket = async context =>
                            {
                                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted).ConfigureAwait(false);
                                response.EnsureSuccessStatusCode();

                                var user = Newtonsoft.Json.Linq.JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                                context.RunClaimActions(user);
                            },
                        };
                    });

                 services.TryAddSingleton<IOAuthService>(new OAuthService(config));

                */
            }

            return services;
        }
    }
}
