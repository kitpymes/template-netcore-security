// -----------------------------------------------------------------------
// <copyright file="IModificationAudited.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Genere un campo con la fecha de modificación de un registro.
    /// </summary>
    public interface IModificationAudited
    {
        /// <summary>
        /// Fecha de modificación de un registro.
        /// </summary>
        public const string ModifiedDate = nameof(ModifiedDate);

        /// <summary>
        /// Id del usuario que modifica el registro.
        /// </summary>
        public const string ModifiedUserId = nameof(ModifiedUserId);
    }
}
