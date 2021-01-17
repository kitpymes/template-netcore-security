// -----------------------------------------------------------------------
// <copyright file="EncryptorSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
       Clase de configuración EncryptorSettings
       Contiene las propiedades para la configuración de la encriptación
    */

    /// <summary>
    /// Clase de configuración <c>EncryptorSettings</c>.
    /// Contiene las propiedades para la configuración de la encriptación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la encriptación.</para>
    /// </remarks>
    public class EncryptorSettings
    {
        /// <summary>
        /// Valor por defecto que indica si esta habilitado el servicio.
        /// </summary>
        public const bool DefaultEnabled = false;

        /// <summary>
        /// Valor por defecto que indica el nombre de la aplicación.
        /// </summary>
        public const string DefaultApplicationName = "App";

        /// <summary>
        /// Valor por defecto que indica los días que dura la protección, debe ser mayor o igual a 7 días.
        /// </summary>
        public const int DefaultKeyLifetimeFromDays = 30;

        /// <summary>
        /// Valor por defecto que indica la ruta donde se guardaran las encriptaciones.
        /// </summary>
        public const string DefaultPersistKeysToFileSystem = @"bin\keys";

        private bool _enabled = DefaultEnabled;

        private string _applicationName = DefaultApplicationName;

        private int _keyLifetimeFromDays = DefaultKeyLifetimeFromDays;

        private string _persistKeysToFileSystem = DefaultPersistKeysToFileSystem;

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
        /// Obtiene o establece el nombre de la aplicación.
        /// <para><strong>Default:</strong> <see cref="DefaultApplicationName"/> = "App".</para>
        /// </summary>
        public string? ApplicationName
        {
            get => _applicationName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _applicationName = value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece los días que dura la protección, debe ser >= 7.
        /// <para><strong>Default:</strong> <see cref="DefaultKeyLifetimeFromDays"/> = 30.</para>
        /// </summary>
        public int? KeyLifetimeFromDays
        {
            get => _keyLifetimeFromDays;
            set
            {
                if (value.HasValue && value.Value >= 7)
                {
                    _keyLifetimeFromDays = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la ruta donde se guardaran las encriptaciones.
        /// <para><strong>Default:</strong> <see cref="DefaultPersistKeysToFileSystem"/> = "bin\debug\keys".</para>
        /// </summary>
        public string? PersistKeysToFileSystem
        {
            get => _persistKeysToFileSystem;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _persistKeysToFileSystem = value;
                }
            }
        }
    }
}
