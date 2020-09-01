// -----------------------------------------------------------------------
// <copyright file="EntityBaseLong.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Entidad base para enteros.
    /// </summary>
    public abstract class EntityBaseLong : EntityBase<long>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EntityBaseLong"/>.
        /// </summary>
        /// <param name="id">Clave para la entidad.</param>
        protected EntityBaseLong(long id)
            : base(id) { }
    }
}
