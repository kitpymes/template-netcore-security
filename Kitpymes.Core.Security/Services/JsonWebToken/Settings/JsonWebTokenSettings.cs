// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Text;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;

    /*
        Clase para la configuración del token de sesión JsonWebTokenSettings
        Contiene las propiedades para la configuración del token de sesión
    */

    /// <summary>
    /// Clase para la configuración del token de sesión <c>JsonWebTokenSettings</c>.
    /// Contiene las propiedades para la configuración del token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración del token de sesión.</para>
    /// </remarks>
    public class JsonWebTokenSettings
    {
        private bool _enabled = false;
        private string _key = "DDF3620A-4539-4EBF-A18E-0D0C435D44D2";
        private string _authenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        private string _challengeScheme = JwtBearerDefaults.AuthenticationScheme;
        private bool _requireExpirationTime = true;

        /// <summary>
        /// Obtiene los parámetros para las validaciones del token de sesión.
        /// </summary>
        [JsonIgnore]
        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
        {
            ValidateIssuerSigningKey = !string.IsNullOrWhiteSpace(Key),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),

            ValidateIssuer = !string.IsNullOrWhiteSpace(ValidIssuer),
            ValidIssuer = ValidIssuer,

            ValidateAudience = !string.IsNullOrWhiteSpace(ValidAudience),
            ValidAudience = ValidAudience,

            ValidateLifetime = !(LifetimeValidator is null),
            LifetimeValidator = LifetimeValidator,

            // Tiempo de caducidad del búfer, el tiempo efectivo total es igual al tiempo más el tiempo de caducidad de Jwt. Si no está configurado, el valor predeterminado es 5 minutos.
            ClockSkew = TimeSpan.FromSeconds(30),

            RequireExpirationTime = _requireExpirationTime,
        };

        /// <summary>
        /// Obtiene o establece la validación del token de sesión.
        /// </summary>
        [JsonIgnore]
        public LifetimeValidator LifetimeValidator { get; set; } = (before, expires, token, param) => expires > DateTime.UtcNow;

        /// <summary>
        /// Obtiene o establece la configuración del tiempo de expiración del token de sesión.
        /// </summary>
        public ExpireSettings Expire { get; set; } = new ExpireSettings();

        /// <summary>
        /// Obtiene o establece un valor que indica si se habilita el servicio del token de sesión.
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
        /// Obtiene o establece la audiencia del token de sesión.
        /// </summary>
        public string? ValidAudience { get; set; }

        /// <summary>
        /// Obtiene o establece el emisor del token de sesión.
        /// </summary>
        public string? ValidIssuer { get; set; }

        /// <summary>
        /// Obtiene o establece la clave unica.
        /// </summary>
        public string? Key
        {
            get => _key;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _key = value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece el esquema de autenticación.
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
        /// Obtiene o establece el esquema de desafio.
        /// </summary>
        public string? ChallengeScheme
        {
            get => _challengeScheme;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _challengeScheme = value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si los tokens deben tener un valor de 'caducidad'.
        /// </summary>
        public bool? RequireExpirationTime
        {
            get => _requireExpirationTime;
            set
            {
                if (value.HasValue)
                {
                    _requireExpirationTime = value.Value;
                }
            }
        }
    }
}
