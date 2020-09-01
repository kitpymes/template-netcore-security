// -----------------------------------------------------------------------
// <copyright file="IDeletionAudited.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Genera un campo con la fecha de eliminación de un registro.
    /// </summary>
    public interface IDeletionAudited
    {
        /// <summary>
        /// Fecha de eliminación de un registro.
        /// </summary>
        public const string DeletedDate = nameof(DeletedDate);

        /// <summary>
        /// Id del usuario que elimina el registro.
        /// </summary>
        public const string DeletedUserId = nameof(DeletedUserId);
    }
}
