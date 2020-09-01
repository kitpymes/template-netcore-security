// -----------------------------------------------------------------------
// <copyright file="ValueObjectBase.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /*
        Clase base para los objetos de valor ValueObjectBase
        Contiene las acciones comunes a todos los objetos de valor
    */

    /// <summary>
    /// Clase base para los objetos de valor <c>ValueObjectBase</c>.
    /// Contiene las acciones comunes a todos los objetos de valor.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las acciones necesarias para los objetos de valor.</para>
    /// </remarks>
    public abstract class ValueObjectBase : INotMapped
    {
        /// <summary>
        /// Verifica si dos objetos son iguales.
        /// </summary>
        /// <param name="left">Objeto fuente.</param>
        /// <param name="right">Objeto destino.</param>
        /// <returns>true | false.</returns>
        public static bool operator ==(ValueObjectBase left, ValueObjectBase right)
        {
            if (left is null && right is null)
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Verifica si dos objetos no son iguales.
        /// </summary>
        /// <param name="left">Objeto fuente.</param>
        /// <param name="right">Objeto destino.</param>
        /// <returns>true | false.</returns>
        public static bool operator !=(ValueObjectBase left, ValueObjectBase right)
        => !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                Shared.Util.Check.Throw($"Invalid comparison of Value Objects of different types: {GetType()} and {obj.GetType()}");
            }

            return obj is ValueObjectBase valueObject && GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        => GetEqualityComponents().Aggregate(1, (current, obj) => HashCode.Combine(current, obj));

        /// <summary>
        /// Para comparar las propiedades de un objeto por valor.
        /// </summary>
        /// <returns>IEnumerable{object?}.</returns>
        protected abstract IEnumerable<object?> GetEqualityComponents();
    }
}