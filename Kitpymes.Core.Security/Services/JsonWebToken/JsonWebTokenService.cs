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
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;
    using Microsoft.IdentityModel.Tokens;

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
        public JsonWebTokenService(JsonWebTokenSettings settings) => JsonWebTokenSettings = settings;

        private JsonWebTokenSettings JsonWebTokenSettings { get; }

        /// <inheritdoc/>
        public (string Token, string Expire) Encode(IList<Claim> claims, Dictionary<string, object>? headers = null)
        {
            claims.ToIsNullOrEmptyThrow(nameof(claims));

            (string Token, string Expire) encode = (string.Empty, string.Empty);

            try
            {
                var expires = DateTime.UtcNow.Add(new TimeSpan(
                       JsonWebTokenSettings.Expire.Days!.Value,
                       JsonWebTokenSettings.Expire.Hours!.Value,
                       JsonWebTokenSettings.Expire.Minutes!.Value,
                       JsonWebTokenSettings.Expire.Seconds!.Value));

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: JsonWebTokenSettings.ValidIssuer,
                    audience: JsonWebTokenSettings.ValidAudience,
                    claims,
                    notBefore: DateTime.UtcNow,
                    expires,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JsonWebTokenSettings.Key!)),
                        SecurityAlgorithms.HmacSha512));

                if (headers != null)
                {
                    foreach ((string key, object value) in headers)
                    {
                        jwtSecurityToken.Header.Add(key, value);
                    }
                }

                encode.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                encode.Expire = expires.ToShortDateString();
            }
            catch (Exception ex)
            {
               Shared.Util.Check.Throw(ex.ToFullMessage());
            }

            return encode;
        }

        /// <inheritdoc/>
        public Dictionary<string, object> Decode(string? token)
        => new JwtSecurityTokenHandler().ReadJwtToken(token.ToIsNullOrEmptyThrow(nameof(token))).Payload;

        /// <inheritdoc/>
        public async Task<(string Token, string Expire)> EncodeAsync(IList<Claim> claims, Dictionary<string, object>? headers = null)
        => await Task.FromResult(Encode(claims, headers)).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<Dictionary<string, object>> DecodeAsync(string? token)
        => await Task.FromResult(Decode(token)).ConfigureAwait(false);
    }
}
