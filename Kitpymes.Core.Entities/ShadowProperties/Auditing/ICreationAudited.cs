// -----------------------------------------------------------------------
// <copyright file="ICreationAudited.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Genera un campo con la fecha de creación de un registro.
    /// </summary>
    public interface ICreationAudited
    {
        /// <summary>
        /// Fecha de creación de un registro.
        /// </summary>
        public const string CreatedDate = nameof(CreatedDate);

        /// <summary>
        /// Id del usuario que crea el registro.
        /// </summary>
        public const string CreatedUserId = nameof(CreatedUserId);
    }
}
