// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;
    using Microsoft.IdentityModel.Tokens;
    using UTIL = Kitpymes.Core.Shared.Util;

    /*
        Clase para el token de sesión JsonWebTokenService
        Contiene los métodos para el token de sesión
    */

    /// <summary>
    /// Clase para el token de sesión <c>JsonWebTokenService</c>.
    /// Contiene los métodos para el token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos para el token de sesión.</para>
    /// </remarks>
    public class JsonWebTokenService : IJsonWebTokenService
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="JsonWebTokenService"/>.
        /// </summary>
        /// <param name="settings">Configuración para el token de sesión.</param>
        public JsonWebTokenService(JsonWebTokenSettings settings)
         => JsonWebTokenSettings = settings;

        private JsonWebTokenSettings JsonWebTokenSettings { get; }

        /// <inheritdoc/>
        public CreateJsonWebTokenResult Create(IEnumerable<Claim> claims, IDictionary<string, IEnumerable<string>>? headers = null)
        {
            var errors = new List<string>();

            if (claims.ToIsNullOrAny())
            {
                errors.Add(UTIL.Messages.NullOrAny(nameof(claims)));
            }

            if (JsonWebTokenSettings.PrivateKey.ToIsNullOrEmpty())
            {
                errors.Add(UTIL.Messages.NullOrEmpty(nameof(JsonWebTokenSettings.PrivateKey)));
            }

            if (errors.Any())
            {
                UTIL.Check.Throw(errors.ToSerialize());
            }

            var expires = DateTime.UtcNow.Add(new TimeSpan(
               JsonWebTokenSettings.Expire.Days!.Value,
               JsonWebTokenSettings.Expire.Hours!.Value,
               JsonWebTokenSettings.Expire.Minutes!.Value,
               JsonWebTokenSettings.Expire.Seconds!.Value));

            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(JsonWebTokenSettings.PrivateKey!), out _);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JsonWebTokenSettings.Issuer,
                audience: JsonWebTokenSettings.Audience,
                claims,
                notBefore: DateTime.UtcNow,
                expires,
                signingCredentials);

            if (headers != null)
            {
                foreach ((string key, object value) in headers)
                {
                    jwtSecurityToken.Header.Add(key, value);
                }
            }

            var result = new CreateJsonWebTokenResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expire = expires.ToString(System.Globalization.CultureInfo.CurrentCulture),
            };

            return result;
        }

        /// <inheritdoc/>
        public GetJsonWebTokenResult Get(string? token)
        {
            token?.ToIsNullOrAnyThrow(nameof(token));

            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var result = new GetJsonWebTokenResult
            {
                Audiences = jwtSecurityToken.Audiences,
                Expire = jwtSecurityToken.ValidTo.ToString(System.Globalization.CultureInfo.CurrentCulture),
                Header = jwtSecurityToken.Header,
                Payload = jwtSecurityToken.Payload,
                Issuer = jwtSecurityToken.Issuer,
            };

            foreach (var claim in jwtSecurityToken.Claims)
            {
                result.Claims.Add(claim.Type, claim.Value);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<CreateJsonWebTokenResult> CreateAsync(IEnumerable<Claim> claims, IDictionary<string, IEnumerable<string>>? headers = null)
        => await Task.FromResult(Create(claims, headers)).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<GetJsonWebTokenResult> GetAsync(string? token)
        => await Task.FromResult(Get(token)).ConfigureAwait(false);
    }
}
