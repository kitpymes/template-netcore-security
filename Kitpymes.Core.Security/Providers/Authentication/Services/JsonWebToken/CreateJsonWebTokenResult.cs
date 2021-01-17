// -----------------------------------------------------------------------
// <copyright file="CreateJsonWebTokenResult.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
     Clase de resultado CreateJsonWebTokenResult
     Contiene las propiedades que va a devolver cuando se crea un JWT
   */

    /// <summary>
    /// Clase de resultado <c>CreateJsonWebTokenResult</c>.
    /// Contiene las propiedades que va a devolver cuando se crea un JWT.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que va a devolver cuando se crea un JWT.</para>
    /// </remarks>
    public class CreateJsonWebTokenResult
    {
        /// <summary>
        /// Obtiene o establece el token creado.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de expiración del token.
        /// </summary>
        public string? Expire { get; set; }
    }
}
