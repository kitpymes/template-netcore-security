// -----------------------------------------------------------------------
// <copyright file="StatusEnum.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using Kitpymes.Core.Shared;

    /*
       Clase de enumeraciones de estado EnumerationBaseInt<Status>
       Contiene la lista de estados
    */

    /// <summary>
    /// Clase de enumeraciones de estado <c>EnumerationBaseInt</c>.
    /// Contiene la lista de estados.
    /// <list type="bullet">
    ///     <item>
    ///         <term>Inactive = 0</term>
    ///         <description>Estado inactivo</description>
    ///     </item>
    ///     <item>
    ///         <term>Active = 1</term>
    ///         <description>Estado activo</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los tipo de estados.</para>
    /// </remarks>
    public class StatusEnum : EnumerationBaseInt<StatusEnum>
    {
        /// <summary>
        /// Estado inactivo.
        /// </summary>
        public static readonly StatusEnum Inactive = new StatusEnum(0, nameof(Inactive));

        /// <summary>
        /// Estado activo.
        /// </summary>
        public static readonly StatusEnum Active = new StatusEnum(1, nameof(Active));

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="StatusEnum"/>.
        /// </summary>
        /// <param name="value">Valor del estado.</param>
        /// <param name="name">Nombre del estado.</param>
        private StatusEnum(int value, string name)
            : base(value, name) { }

        /// <summary>
        /// Obtiene un valor que indica si el estado es activo.
        /// </summary>
        public bool IsActive => Name == Active.Name;

        /// <summary>
        /// Obtiene un valor que indica si el estado es inactivo.
        /// </summary>
        public bool IsInactive => Name == Inactive.Name;

        /// <summary>
        /// Convierte un booleano en una enumeración.
        /// </summary>
        /// <param name="status">Booleano a convertir.</param>
        /// <returns>Status.</returns>
        public static StatusEnum ToEnum(bool status)
        => status == true ? Active : Inactive;

        /// <summary>
        /// Convierte un estado en booleano.
        /// </summary>
        /// <param name="status">Nombre del estado.</param>
        /// <returns>true | false.</returns>
        public static bool ToBool(StatusEnum status)
        => status.ToIsNullOrEmptyThrow(nameof(status)).Name == Active.ToString();

        /// <summary>
        /// Convierte un estado en booleano.
        /// </summary>
        /// <param name="status">Nombre del estado.</param>
        /// <returns>true | false.</returns>
        public static bool ToBool(string? status)
        {
            status
                .ToIsNullOrEmptyThrow(nameof(status))
                .ToIsThrow(() => status != Active.ToString() && status != Inactive.ToString(), Shared.Util.Messages.NotFound(nameof(status)));

            return status == Active.ToString();
        }
    }
}
