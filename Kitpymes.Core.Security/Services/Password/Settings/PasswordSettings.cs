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
        private bool _enabled = false;
        private bool _requireDigit = false;
        private bool _requireLowercase = false;
        private bool _requireUppercase = false;
        private bool _requireEspecialChars = false;
        private bool _requiredUniqueChars = false;
        private int _requiredMinLength = 6;

        /// <summary>
        /// Obtiene o establece un valor que indica si se habilita el servicio de contraseña.
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
        /// Por defecto: false.
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
        /// Por defecto: false.
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
        /// Por defecto: false.
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
        /// Por defecto: false.
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
        /// Por defecto: false.
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
        /// Por defecto: 6.
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
    }
}
