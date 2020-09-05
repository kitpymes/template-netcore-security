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
        private bool _enabled = false;

        /// <summary>
        /// Obtiene o establece un valor que indica si se habilita el servicio de encriptación.
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
    }
}
