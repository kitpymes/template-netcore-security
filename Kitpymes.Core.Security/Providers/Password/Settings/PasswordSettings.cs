// -----------------------------------------------------------------------
// <copyright file="PasswordSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
       Clase de configuración PasswordSettings
       Contiene las propiedades para la configuración de la contraseña
    */

    /// <summary>
    /// Clase de configuración <c>PasswordSettings</c>.
    /// Contiene las propiedades para la configuración de la contraseña.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la contraseña.</para>
    /// </remarks>
    public class PasswordSettings
    {
        #region Default Values

        /// <summary>
        /// Valor por defecto que indica si esta habilitado el servicio.
        /// </summary>
        public const bool DefaultEnabled = false;

        /// <summary>
        /// Valor por defecto que indica si requiere como mínimo un número entre 0-9.
        /// </summary>
        public const bool DefaultRequireDigit = false;

        /// <summary>
        /// Valor por defecto que indica si requiere como mínimo un carácter en minúscula.
        /// </summary>
        public const bool DefaultRequireLowercase = false;

        /// <summary>
        /// Valor por defecto que indica si requiere como mínimo un carácter en mayúscula.
        /// </summary>
        public const bool DefaultRequireUppercase = false;

        /// <summary>
        /// Valor por defecto que indica si requiere como mínimo un carácter especial.
        /// </summary>
        public const bool DefaultRequireEspecialChars = false;

        /// <summary>
        /// Valor por defecto que indica si no se pueden repetir los caracteres.
        /// </summary>
        public const bool DefaultRequiredUniqueChars = false;

        /// <summary>
        /// Valor por defecto que indica si requiere una longitud mínima.
        /// </summary>
        public const int DefaultRequiredMinLength = 6;

        #endregion Default Values

        private bool _enabled = DefaultEnabled;
        private bool _requireDigit = DefaultRequireDigit;
        private bool _requireLowercase = DefaultRequireLowercase;
        private bool _requireUppercase = DefaultRequireUppercase;
        private bool _requireEspecialChars = DefaultRequireEspecialChars;
        private bool _requiredUniqueChars = DefaultRequiredUniqueChars;
        private int _requiredMinLength = DefaultRequiredMinLength;

        /// <summary>
        /// Obtiene o establece un valor que indica si se habilita el servicio de contraseña.
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
        /// Obtiene o establece un valor que indica si requiere como mínimo un número entre 0-9.
        /// <para><strong>Default:</strong> <see cref="DefaultRequireDigit"/> = false.</para>
        /// </summary>
        public bool? RequireDigit
        {
            get => _requireDigit;
            set
            {
                if (value.HasValue)
                {
                    _requireDigit = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si requiere como mínimo un carácter en minúscula.
        /// <para><strong>Default:</strong> <see cref="DefaultRequireLowercase"/> = false.</para>
        /// </summary>
        public bool? RequireLowercase
        {
            get => _requireLowercase;
            set
            {
                if (value.HasValue)
                {
                    _requireLowercase = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si requiere como mínimo un carácter en mayúscula.
        /// <para><strong>Default:</strong> <see cref="DefaultRequireUppercase"/> = false.</para>
        /// </summary>
        public bool? RequireUppercase
        {
            get => _requireUppercase;
            set
            {
                if (value.HasValue)
                {
                    _requireUppercase = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si requiere como mínimo un carácter especial.
        /// <para><strong>Default:</strong> <see cref="DefaultRequireEspecialChars"/> = false.</para>
        /// </summary>
        public bool? RequireEspecialChars
        {
            get => _requireEspecialChars;
            set
            {
                if (value.HasValue)
                {
                    _requireEspecialChars = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si no se pueden repetir los caracteres.
        /// <para><strong>Default:</strong> <see cref="DefaultRequiredUniqueChars"/> = false.</para>
        /// </summary>
        public bool? RequiredUniqueChars
        {
            get => _requiredUniqueChars;
            set
            {
                if (value.HasValue)
                {
                    _requiredUniqueChars = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si requiere una longitud mínima.
        /// <para><strong>Default:</strong> <see cref="DefaultRequiredMinLength"/> = 3.</para>
        /// </summary>
        public int? RequiredMinLength
        {
            get => _requiredMinLength;
            set
            {
                if (value.HasValue)
                {
                    _requiredMinLength = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece las propiedades para la configuración de la encriptación.
        /// </summary>
        public EncryptionSettings EncryptionSettings { get; set; } = new EncryptionSettings();
    }
}
