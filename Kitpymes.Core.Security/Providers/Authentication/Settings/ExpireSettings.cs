// -----------------------------------------------------------------------
// <copyright file="ExpireSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
        Clase para la configuraci�n de la fecha de expiraci�n ExpireSettings
        Contiene los m�todos para la configuraci�n de la fecha de expiraci�n
    */

    /// <summary>
    /// Clase para la configuraci�n de la fecha de expiraci�n <c>ExpireSettings</c>.
    /// Contiene los m�todos para la configuraci�n de la fecha de expiraci�n.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los m�todos para la configuraci�n de la fecha de expiraci�n.</para>
    /// </remarks>
    public class ExpireSettings
    {
        /// <summary>
        /// Valor por defecto que indica el d�a de expiraci�n.
        /// </summary>
        public const int DefaultDays = 30;

        /// <summary>
        /// Valor por defecto que indica la hora de expiraci�n.
        /// </summary>
        public const int DefaultHours = 0;

        /// <summary>
        /// Valor por defecto que indica los minutos de expiraci�n.
        /// </summary>
        public const int DefaultMinutes = 0;

        /// <summary>
        /// Valor por defecto que indica los segundos de expiraci�n.
        /// </summary>
        public const int DefaultSeconds = 0;

        private int _days = DefaultDays;
        private int _hours = DefaultHours;
        private int _minutes = DefaultMinutes;
        private int _seconds = DefaultSeconds;

        /// <summary>
        /// Obtiene o establece el d�a de expiraci�n.
        /// <para><strong>Default:</strong> <see cref="DefaultDays"/> = 30.</para>
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
        /// Obtiene o establece la hora de expiraci�n.
        /// <para><strong>Default:</strong> <see cref="DefaultHours"/> = 0.</para>
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
        /// Obtiene o establece los minutos de expiraci�n.
        /// <para><strong>Default:</strong> <see cref="DefaultMinutes"/> = 0.</para>
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
        /// Obtiene o establece los segundos de expiraci�n.
        /// <para><strong>Default:</strong> <see cref="DefaultSeconds"/> = 0.</para>
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
