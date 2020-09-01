// -----------------------------------------------------------------------
// <copyright file="ITenant.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Propiedad para entidades que dependen de un inquilino.
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Id del inquilino.
        /// </summary>
        public const string TenantId = nameof(TenantId);
    }
}
