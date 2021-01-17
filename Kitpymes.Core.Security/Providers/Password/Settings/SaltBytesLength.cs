// -----------------------------------------------------------------------
// <copyright file="SaltBytesLength.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    /*
        Clase de enumeración SaltBytesLength
        Contiene las enumeraciones de la longitud de los saltos para la encriptación.
    */

    /// <summary>
    /// Clase de enumeración <c>SaltBytesLength</c>.
    /// Contiene las enumeraciones de la longitud de la encriptación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las enumeraciones de la longitud de la encriptación.</para>
    /// </remarks>
    public enum SaltBytesLength
    {
        /// <summary>
        /// 128 bits / 8 = 16 bytes.
        /// </summary>
        BITS128 = 16,

        /// <summary>
        /// 256 bits / 8 = 32 bytes.
        /// </summary>
        BITS256 = 32,

        /// <summary>
        /// 512 bits / 8 = 64 bytes.
        /// </summary>
        BITS512 = 64,
    }
}
