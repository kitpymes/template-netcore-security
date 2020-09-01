// -----------------------------------------------------------------------
// <copyright file="AppSession.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /// <summary>
    /// Propiedades de la sesión de la aplicación.
    /// </summary>
    public static class AppSession
    {
        /// <summary>
        /// Obtiene o establece la clave de sesión.
        /// </summary>
        public static string? Key { get; set; }

        /// <summary>
        /// Obtiene o establece el tenant.
        /// </summary>
        public static TenantSession? Tenant { get; set; }

        /// <summary>
        /// Obtiene o establece el usuario.
        /// </summary>
        public static UserSession? User { get; set; }

        /// <summary>
        /// Obtiene o establece si es ambiente de desarrollo.
        /// </summary>
        public static bool? IsDevelopment { get; set; }
    }
}
