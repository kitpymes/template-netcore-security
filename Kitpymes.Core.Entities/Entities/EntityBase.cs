// -----------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Kitpymes.Core.Shared;

    /// <summary>
    /// Entidad base.
    /// </summary>
    /// <typeparam name="TKey">Entity key type.</typeparam>
    public abstract class EntityBase<TKey> : IEquatable<TKey>, IEntityBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EntityBase{TKey}"/>.
        /// </summary>
        /// <param name="key">Clave única de una entidad.</param>
        protected EntityBase(TKey key) => Id = key.ToIsNullOrEmptyThrow(nameof(key));

        /// <summary>
        /// Obtiene la clave de la entidad.
        /// </summary>
        public virtual TKey Id { get; private set; }

        #region Equals

        /// <summary>
        /// Verifica que dos objetos son iguales.
        /// </summary>
        /// <param name="left">Objeto fuente.</param>
        /// <param name="right">Objeto destino.</param>
        /// <returns>true | false.</returns>
        public static bool operator ==(EntityBase<TKey> left, EntityBase<TKey> right)
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
        /// Verifica que dos objetos no son iguales.
        /// </summary>
        /// <param name="left">Objeto fuente.</param>
        /// <param name="right">Objeto destino.</param>
        /// <returns>true | false.</returns>
        public static bool operator !=(EntityBase<TKey> left, EntityBase<TKey> right) => !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                Shared.Util.Check.Throw($"Invalid comparison of entities of different types: {GetType()} and {obj.GetType()}");
            }

            return obj is EntityBase<TKey> entity && Equals(entity.Id);
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] TKey other) => Id?.Equals(other) ?? false;

        /// <inheritdoc/>
        public override int GetHashCode() => (GetType().Name + Id).GetHashCode(StringComparison.OrdinalIgnoreCase);

        #endregion Equals
    }
}
