// -----------------------------------------------------------------------
// <copyright file="IRowVersion.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Propiedad de seguimiento para evitar conflictos de simultaneidad.
    /// </summary>
    public interface IRowVersion
    {
        /// <summary>
        /// Propiedad de seguimiento.
        /// </summary>
        public const string IsRowVersion = nameof(IsRowVersion);
    }
}
