// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Security.Cryptography;
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;

    /*
       Clase de extensión JsonWebTokenServiceCollectionExtensions
       Contiene las extensiones de los servicios para la autenticación por token
    */

    /// <summary>
    /// Clase de extensión <c>JsonWebTokenServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios para la autenticación por token.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para la autenticación por token.</para>
    /// </remarks>
    public static class JsonWebTokenServiceCollectionExtensions
    {
        /// <summary>
        /// Obtiene el servicio de autenticación por token.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <returns>IJsonWebTokenService | ApplicationException: si no se encuentra el servicio IJsonWebTokenProvider.</returns>
        public static IJsonWebTokenService GetAuthJsonWebToken(this IServiceCollection services)
        => services.ToService<IJsonWebTokenService>()
            .ToIsNullOrEmptyThrow(Shared.Util.Messages.NotFound(nameof(IJsonWebTokenService)));

        /// <summary>
        /// Carga el servicio de autenticación por token.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Configuración del servicio de tokens.</param>
        /// <returns>IServiceCollection | ApplicationException: si PrivateKey o PublicKey son nulas o vacias.</returns>
        internal static IServiceCollection LoadAuthJsonWebToken(
            this IServiceCollection services,
            Action<JsonWebTokenOptions> options)
        => services.LoadAuthJsonWebToken(options.ToConfigureOrDefault().JsonWebTokenSettings);

        /// <summary>
        /// Carga el servicio de autenticación por token.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración del servicio de tokens.</param>
        /// <returns>IServiceCollection | ApplicationException: si JsonWebTokenSettings es nulo.</returns>
        internal static IServiceCollection LoadAuthJsonWebToken(
            this IServiceCollection services,
            JsonWebTokenSettings settings)
        {
            var config = settings.ToIsNullOrEmptyThrow(nameof(settings));

            if (config.Enabled.HasValue && config.Enabled.Value)
            {
                var validPrivateKey = config.PrivateKey.ToIsNullOrEmptyThrow(nameof(config.PrivateKey));

                var validPublicKey = config.PublicKey.ToIsNullOrEmptyThrow(nameof(config.PublicKey));

#if debug
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
#endif

                var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(Convert.FromBase64String(validPublicKey), out _);
                var securityKey = new RsaSecurityKey(rsa)
                {
                    CryptoProviderFactory = new CryptoProviderFactory()
                    {
                        CacheSignatureProviders = false,
                    },
                };

                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = config.AuthenticateScheme;

                        options.DefaultChallengeScheme = config.ChallengeScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = config.RequireHttpsMetadata!.Value;

                        options.SaveToken = config.SaveToken!.Value;

                        options.IncludeErrorDetails = config.IncludeErrorDetails!.Value;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = securityKey,

                            ValidateIssuer = !string.IsNullOrWhiteSpace(config.Issuer),
                            ValidIssuer = config.Issuer,

                            ValidateAudience = !string.IsNullOrWhiteSpace(config.Audience),
                            ValidAudience = config.Audience,

                            ValidateLifetime = config.ValidateLifetime!.Value,
                            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires > DateTime.UtcNow,

                            RequireExpirationTime = config.RequireExpirationTime!.Value,

                            // Establezca clockskew en cero para que los tokens caduquen exactamente a la hora de vencimiento del token (en lugar de 5 minutos más tarde)
                            ClockSkew = TimeSpan.Zero,
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnChallenge = context => throw new UnauthorizedAccessException(context.ToDetails()),
                        };
                    });

                services.TryAddSingleton<IJsonWebTokenService>(new JsonWebTokenService(config));
            }

            return services;
        }
    }
}