// -----------------------------------------------------------------------
// <copyright file="EncryptorOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Kitpymes.Core.Shared;

    /*
       Clase de configuración EncryptorOptions
       Contiene las propiedades para la configuración de la encriptación
    */

    /// <summary>
    /// Clase de configuración <c>EncryptorOptions</c>.
    /// Contiene las propiedades para la configuración de la encriptación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la encriptación.</para>
    /// </remarks>
    public class EncryptorOptions
    {
        /// <summary>
        /// Obtiene la configuración para la encriptación.
        /// </summary>
        public EncryptorSettings EncryptorSettings { get; private set; } = new EncryptorSettings();

        /// <summary>
        /// Obtiene o establece un valor que indica el servicio esta habilitado.
        /// </summary>
        /// <param name="enabled">Habilita o desabilita.</param>
        /// <returns>EncryptorOptions.</returns>
        public EncryptorOptions WithEnabled(bool enabled = true)
        {
            EncryptorSettings.Enabled = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece el nombre de la aplicación.
        /// </summary>
        /// <param name="applicationName">Nombre de la aplicación.</param>
        /// <returns>EncryptorOptions.</returns>
        public EncryptorOptions WithApplicationName(string applicationName = EncryptorSettings.DefaultApplicationName)
        {
            EncryptorSettings.ApplicationName = applicationName;

            return this;
        }

        /// <summary>
        /// Obtiene o establece los días que dura la protección, debe ser >= 7.
        /// </summary>
        /// <param name="keyLifetimeFromDays">Cantidad de días.</param>
        /// <returns>EncryptorOptions | ApplicationException: si keyLifetimeFromDays menor que 7.</returns>
        public EncryptorOptions WithKeyLifetimeFromDays(int keyLifetimeFromDays = EncryptorSettings.DefaultKeyLifetimeFromDays)
        {
            EncryptorSettings.KeyLifetimeFromDays = keyLifetimeFromDays.ToIsLessThrow(7, nameof(keyLifetimeFromDays));

            return this;
        }

        /// <summary>
        /// Obtiene o establece la ruta donde se guardaran las encriptaciones.
        /// </summary>
        /// <param name="persistKeysToFileSystem">Ruta.</param>
        /// <returns>EncryptorOptions.</returns>
        public EncryptorOptions WithPersistKeysToFileSystems(string persistKeysToFileSystem = EncryptorSettings.DefaultPersistKeysToFileSystem)
        {
            EncryptorSettings.PersistKeysToFileSystem = persistKeysToFileSystem
                .ToIsNullOrEmptyThrow(nameof(persistKeysToFileSystem))
                .ToIsDirectoryThrow(nameof(persistKeysToFileSystem));

            return this;
        }
    }
}
