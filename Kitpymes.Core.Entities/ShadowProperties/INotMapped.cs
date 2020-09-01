// -----------------------------------------------------------------------
// <copyright file="INotMapped.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Propiedad para entidades que no queremos mapear a la base de datos.
    /// </summary>
    public interface INotMapped
    {
        /// <summary>
        /// Propiedad de seguimiento.
        /// </summary>
        public const string NotMapped = nameof(NotMapped);
    }
}
