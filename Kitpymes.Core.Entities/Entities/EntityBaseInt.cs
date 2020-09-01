// -----------------------------------------------------------------------
// <copyright file="EntityBaseInt.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Entidad base para enteros.
    /// </summary>
    public abstract class EntityBaseInt : EntityBase<int>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EntityBaseInt"/>.
        /// </summary>
        /// <param name="id">Clave para la entidad.</param>
        protected EntityBaseInt(int id)
            : base(id) { }
    }
}
