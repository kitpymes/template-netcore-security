// -----------------------------------------------------------------------
// <copyright file="EntityBaseString.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using System;
    using Kitpymes.Core.Shared;

    /// <summary>
    /// Entidad base para cadenas.
    /// </summary>
    public abstract class EntityBaseString : EntityBase<string>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EntityBaseString"/>.
        /// Crea una clave automática.
        /// </summary>
        protected EntityBaseString()
            : this(Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.CurrentCulture)) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EntityBaseString"/>.
        /// Crea una clave custom.
        /// </summary>
        /// <param name="id">Clave para la entidad.</param>
        protected EntityBaseString(string? id)
           : base(id.ToIsNullOrEmptyThrow(nameof(id))) { }
    }
}
