// -----------------------------------------------------------------------
// <copyright file="PasswordErrorResult.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /// <summary>
    /// Errores de una contraseña.
    /// </summary>
    public enum PasswordErrorResult
    {
        /// <summary>
        /// La contraseña en nula o vacia.
        /// </summary>
        RequiredValue,

        /// <summary>
        /// La contraseña requiere que contenga algún digito decimal.
        /// </summary>
        RequireDigit,

        /// <summary>
        /// La contraseña requiere un mínimo de caracteres.
        /// </summary>
        RequiredMinLength,

        /// <summary>
        /// La contraseña requiere que contenga caracteres únicos.
        /// </summary>
        RequiredUniqueChars,

        /// <summary>
        /// La contraseña requiere que contenga algún caracter especial.
        /// </summary>
        RequireEspecialChars,

        /// <summary>
        /// La contraseña requiere que contenga algún caracter en minúscula.
        /// </summary>
        RequireLowercase,

        /// <summary>
        /// La contraseña requiere que contenga algún caracter en mayúscula.
        /// </summary>
        RequireUppercase,
    }
}
