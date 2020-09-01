// -----------------------------------------------------------------------
// <copyright file="EnumerationBaseInt.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /*
        Clase base de las enumeraciones de enteros EnumerationBaseInt<TEnum, int>
        Contiene las acciones comunes para las enumeraciones de enteros
    */

    /// <summary>
    /// Clase base de una enumeración de enteros <c>EnumerationBaseInt</c>.
    /// Contiene las acciones comunes de las enumeraciones de enteros.
    /// </summary>
    /// <typeparam name="TEnum">Tipo de enumeración.</typeparam>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las acciones necesarias para las enumeraciones de enteros.</para>
    /// </remarks>
    public abstract class EnumerationBaseInt<TEnum> : EnumerationBase<TEnum, int>
        where TEnum : EnumerationBase<TEnum, int>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EnumerationBaseInt{TEnum}"/>.
        /// </summary>
        /// <param name="value">Clave de la enumeración.</param>
        /// <param name="name">Nombre de la enumeración.</param>
        /// <param name="shortName">Nombre corto de la enumeración.</param>
        protected EnumerationBaseInt(int value, string name, string? shortName = null)
            : base(value, name, shortName) { }
    }
 }
