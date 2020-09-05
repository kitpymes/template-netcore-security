// -----------------------------------------------------------------------
// <copyright file="ExpireSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
        Clase para la configuración de la fecha de expiración del token de sesión ExpireSettings
        Contiene los métodos para la configuración de la fecha de expiración del token de sesión
    */

    /// <summary>
    /// Clase para la configuración de la fecha de expiración del token de sesión <c>ExpireSettings</c>.
    /// Contiene los métodos para la configuración de la fecha de expiración del token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos para la configuración de la fecha de expiración del token de sesión.</para>
    /// </remarks>
    public class ExpireSettings
    {
        private int _days = 30;
        private int _hours = 0;
        private int _minutes = 0;
        private int _seconds = 0;

        /// <summary>
        /// Obtiene o establece el día de expiración.
        /// </summary>
        public int? Days
        {
            get => _days;
            set
            {
                if (value.HasValue)
                {
                    _days = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la hora de expiración.
        /// </summary>
        public int? Hours
        {
            get => _hours;
            set
            {
                if (value.HasValue)
                {
                    _hours = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece los minutos de expiración.
        /// </summary>
        public int? Minutes
        {
            get => _minutes;
            set
            {
                if (value.HasValue)
                {
                    _minutes = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece los segundos de expiración.
        /// </summary>
        public int? Seconds
        {
            get => _seconds;
            set
            {
                if (value.HasValue)
                {
                    _seconds = value.Value;
                }
            }
        }
    }
}
