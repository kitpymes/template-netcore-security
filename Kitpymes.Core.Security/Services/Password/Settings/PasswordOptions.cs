// -----------------------------------------------------------------------
// <copyright file="PasswordOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Kitpymes.Core.Shared;

    /*
       Clase de configuración PasswordOptions
       Contiene las propiedades para la configuración de la contraseña
    */

    /// <summary>
    /// Clase de configuración <c>PasswordOptions</c>.
    /// Contiene las propiedades para la configuración de la contraseña.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la contraseña.</para>
    /// </remarks>
    public class PasswordOptions
    {
        /// <summary>
        /// Obtiene la configuración de la contraseña.
        /// </summary>
        public PasswordSettings PasswordSettings { get; private set; } = new PasswordSettings();

        /// <summary>
        /// Indica si requiere como mínimo un número entre 0-9.
        /// </summary>
        /// <param name="requireDigit">Si requiere algún digito decimal.</param>
        /// <returns>PasswordOptions.</returns>
        public PasswordOptions WithRequireDigit(bool requireDigit = true)
        {
            PasswordSettings.RequireDigit = requireDigit;

            return this;
        }

        /// <summary>
        /// Indica si requiere como mínimo un carácter en minúscula.
        /// </summary>
        /// <param name="requireLowercase">Si requiere algún carácter en minúscula.</param>
        /// <returns>PasswordOptions.</returns>
        public PasswordOptions WithRequireLowercase(bool requireLowercase = true)
        {
            PasswordSettings.RequireLowercase = requireLowercase;

            return this;
        }

        /// <summary>
        /// Indica si requiere como mínimo un carácter en mayúscula.
        /// </summary>
        /// <param name="requireUppercase">Si requiere algún carácter en mayúscula.</param>
        /// <returns>PasswordOptions.</returns>
        public PasswordOptions WithRequireUppercase(bool requireUppercase = true)
        {
            PasswordSettings.RequireUppercase = requireUppercase;

            return this;
        }

        /// <summary>
        /// Indica si requiere como mínimo un carácter especial.
        /// </summary>
        /// <param name="requireEspecialChars">Si requiere algún carácter especial.</param>
        /// <returns>PasswordOptions.</returns>
        public PasswordOptions WithRequireEspecialCharacters(bool requireEspecialChars = true)
        {
            PasswordSettings.RequireEspecialChars = requireEspecialChars;

            return this;
        }

        /// <summary>
        /// Indica si no se pueden repetir los caracteres.
        /// </summary>
        /// <param name="requiredUniqueChars">Si requiere que no se repitan los caracteres.</param>
        /// <returns>PasswordOptions.</returns>
        public PasswordOptions WithRequiredUniqueChars(bool requiredUniqueChars = true)
        {
            PasswordSettings.RequiredUniqueChars = requiredUniqueChars;

            return this;
        }

        /// <summary>
        /// Indica si requiere una longitud mínima.
        /// </summary>
        /// <param name="requiredMinLength">Si requiere una longitud mínima.</param>
        /// <returns>PasswordOptions | ApplicationException: si requiredMinLength es menor que1 .</returns>
        public PasswordOptions WithRequiredMinLength(int requiredMinLength = 6)
        {
            PasswordSettings.RequiredMinLength = requiredMinLength.ToIsLessThrow(1, nameof(requiredMinLength));

            return this;
        }
    }
}
