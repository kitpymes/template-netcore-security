// -----------------------------------------------------------------------
// <copyright file="StringId.cs" company="Kitpymes">
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
    /// Objeto de valor para id de cadena.
    /// </summary>
    public sealed class StringId : ValueObjectBase
    {
        private StringId() { }

        private StringId(string? id) => Value = id.ToIsNullOrEmptyThrow(nameof(id));

        /// <summary>
        /// Obtiene el id vacio.
        /// </summary>
        public static StringId Null => new StringId();

        /// <summary>
        /// Obtiene un valor que indica si el id esta vacio.
        /// </summary>
        public bool IsNull => Value.ToIsNullOrEmpty();

        /// <summary>
        /// Obtiene el valor del id.
        /// </summary>
        public string? Value { get; private set; }

        /// <summary>
        /// Crea un nuevo id.
        /// </summary>
        /// <returns>StringId.</returns>
        [return: NotNull]
        public static StringId Create() => Create(Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.CurrentCulture));

        /// <summary>
        /// Crea un nuevo id.
        /// </summary>
        /// <param name="id">Nuevo id.</param>
        /// <returns>StringId | ApplicationException.</returns>
        [return: NotNull]
        public static StringId Create(string? id) => new StringId(id);

        /// <summary>
        /// Devuelve el id.
        /// </summary>
        /// <returns>string.</returns>
        public override string ToString() => $"{Value}";

        /// <inheritdoc/>
        protected override System.Collections.Generic.IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
