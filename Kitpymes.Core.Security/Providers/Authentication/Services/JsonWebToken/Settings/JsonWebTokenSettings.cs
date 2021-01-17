// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        /// <summary>
        /// Valor por defecto que indica si esta habilitado el servicio.
        /// </summary>
        public const bool DefaultEnabled = false;

        /// <summary>
        /// Valor por defecto que indica el esquema de autenticación.
        /// </summary>
        public const string DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Valor por defecto que indica el esquema de autenticación.
        /// </summary>
        public const string DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Valor por defecto que indica si se requiere HTTPS para la dirección o autoridad de metadatos.
        /// Esto debe desactivarse solo en entornos de desarrollo.
        /// </summary>
        public const bool DefaultRequireHttpsMetadata = true;

        /// <summary>
        /// Valor por defecto que indica si se requiere HTTPS para la dirección o autoridad de metadatos.
        /// Esto debe desactivarse solo en entornos de desarrollo.
        /// </summary>
        public const bool DefaultSaveToken = true;

        /// <summary>
        /// Valor por defecto que indica si los errores de validación del token deben devolverse al cliente mediante el encabezado WWW-Authenticate.
        /// </summary>
        public const bool DefaultIncludeErrorDetails = true;

        /// <summary>
        /// Valor por defecto que indica si los tokens deben tener un valor de 'caducidad'.
        /// </summary>
        public const bool DefaultRequireExpirationTime = true;

        /// <summary>
        /// Valor por defecto que indica si se valida la vida útil del token.
        /// </summary>
        public const bool DefaultValidateLifetime = true;

        private bool _enabled = DefaultEnabled;
        private string _authenticateScheme = DefaultAuthenticateScheme;
        private string _challengeScheme = DefaultChallengeScheme;
        private bool _requireHttpsMetadata = DefaultRequireHttpsMetadata;
        private bool _saveToken = DefaultSaveToken;
        private bool _includeErrorDetails = DefaultIncludeErrorDetails;
        private bool _requireExpirationTime = DefaultRequireExpirationTime;
        private bool _validateLifetime = DefaultValidateLifetime;

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
        /// <para><strong>Default:</strong> <see cref="DefaultAuthenticateScheme"/> = "Bearer".</para>
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
        /// Obtiene o establece el esquema de cambio de autenticación.
        /// <para><strong>Default:</strong> <see cref="DefaultChallengeScheme"/> = "Bearer".</para>
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
        /// Obtiene o establece si se requiere HTTPS para la dirección o autoridad de metadatos.
        /// Esto debe desactivarse solo en entornos de desarrollo.
        /// <para><strong>Default:</strong> <see cref="DefaultRequireHttpsMetadata"/> = true.</para>
        /// </summary>
        public bool? RequireHttpsMetadata
        {
            get => _requireHttpsMetadata;
            set
            {
                if (value.HasValue)
                {
                    _requireHttpsMetadata = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si los tokens deben tener un valor de 'caducidad'.
        /// <para><strong>Default:</strong> <see cref="DefaultRequireExpirationTime"/> = true.</para>
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

        /// <summary>
        /// Obtiene o establece si el token de portador debe almacenarse en Microsoft.AspNetCore.Authentication.AuthenticationProperties después de una autorización exitosa.
        /// <para><strong>Default:</strong> <see cref="DefaultSaveToken"/> = true.</para>
        /// </summary>
        public bool? SaveToken
        {
            get => _saveToken;
            set
            {
                if (value.HasValue)
                {
                    _saveToken = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece si los errores de validación del token deben devolverse al cliente mediante el encabezado WWW-Authenticate.
        /// <para><strong>Default:</strong> <see cref="DefaultIncludeErrorDetails"/> = true.</para>
        /// </summary>
        public bool? IncludeErrorDetails
        {
            get => _includeErrorDetails;
            set
            {
                if (value.HasValue)
                {
                    _includeErrorDetails = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece si se valida la vida útil del token.
        /// <para><strong>Default:</strong> <see cref="DefaultValidateLifetime"/> = true.</para>
        /// </summary>
        public bool? ValidateLifetime
        {
            get => _validateLifetime;
            set
            {
                if (value.HasValue)
                {
                    _validateLifetime = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la configuración del tiempo de expiración del token de sesión.
        /// </summary>
        public ExpireSettings Expire { get; set; } = new ExpireSettings();

        /// <summary>
        /// Obtiene o establece la clave pública de sesión.
        /// </summary>
        public string? PublicKey { get; set; }

        /// <summary>
        /// Obtiene o establece la clave privada de sesión.
        /// </summary>
        public string? PrivateKey { get; set; }

        /// <summary>
        /// Obtiene o establece la url base del cliente.
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Obtiene o establece la url base del receptor.
        /// </summary>
        public string? Audience { get; set; }
    }
}
