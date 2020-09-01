// -----------------------------------------------------------------------
// <copyright file="TenantSession.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Propiedades de la sesión del inquilino.
    /// </summary>
    public class TenantSession
    {
        /// <summary>
        /// Obtiene o establece si esta habilitado el uso de inquilinos.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Obtiene o establece el id del inquilino.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del subdominio del inquilino.
        /// </summary>
        public string? Subdomain { get; set; }
    }
}
