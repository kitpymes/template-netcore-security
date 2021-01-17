// -----------------------------------------------------------------------
// <copyright file="GetJsonWebTokenResult.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /*
     Clase de resultado GetJsonWebTokenResult
     Contiene las propiedades que va a devolver cuando se obtiene un JWT
   */

    /// <summary>
    /// Clase de resultado <c>GetJsonWebTokenResult</c>.
    /// Contiene las propiedades que va a devolver cuando se obtiene un JWT.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que va a devolver cuando se obtiene un JWT.</para>
    /// </remarks>
    public class GetJsonWebTokenResult
    {
        /// <summary>
        /// Obtiene o establece las claims del token.
        /// </summary>
#pragma warning disable CA2227 // Las propiedades de colección deben ser de solo lectura
        public IDictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
#pragma warning restore CA2227 // Las propiedades de colección deben ser de solo lectura

        /// <summary>
        /// Obtiene o establece las audiencias del token.
        /// </summary>
        public IEnumerable<string>? Audiences { get; set; }

        /// <summary>
        /// Obtiene o establece los headers del token.
        /// </summary>
#pragma warning disable CA2227 // Las propiedades de colección deben ser de solo lectura
        public IDictionary<string, object>? Header { get; set; }
#pragma warning restore CA2227 // Las propiedades de colección deben ser de solo lectura

        /// <summary>
        /// Obtiene o establece los payload del token.
        /// </summary>
#pragma warning disable CA2227 // Las propiedades de colección deben ser de solo lectura
        public IDictionary<string, object>? Payload { get; set; }
#pragma warning restore CA2227 // Las propiedades de colección deben ser de solo lectura

        /// <summary>
        /// Obtiene o establece la fecha de expiración del token.
        /// </summary>
        public string? Expire { get; set; }

        /// <summary>
        /// Obtiene o establece el emisor del token.
        /// </summary>
        public string? Issuer { get; set; }
    }
}
