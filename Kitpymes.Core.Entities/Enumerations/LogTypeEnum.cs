// -----------------------------------------------------------------------
// <copyright file="LogTypeEnum.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /*
       Clase de enumeraciones para el log de las acciones de una entidad EnumerationBaseInt<LogType>
       Contiene la lista de las acciones de una entidad
    */

    /// <summary>
    /// Clase de enumeraciones para el log de las acciones de una entidad <c>EnumerationBaseInt</c>.
    /// Contiene la lista de las acciones de una entidad.
    /// <list type="bullet">
    ///     <item>
    ///         <term>None = 1</term>
    ///         <description>No hubo ningún cambio de una entidad</description>
    ///     </item>
    ///     <item>
    ///         <term>Created = 2</term>
    ///         <description>Creación de una entidad</description>
    ///     </item>
    ///     <item>
    ///         <term>Updated = 3</term>
    ///         <description>Actualización de una entidad</description>
    ///     </item>
    ///     <item>
    ///         <term>Deleted = 4</term>
    ///         <description>Eliminación de una entidad</description>
    ///     </item>
    ///     <item>
    ///         <term>Changed = 5</term>
    ///         <description>Cambio de estado de una entidad</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los tipo de log.</para>
    /// </remarks>
    public class LogTypeEnum : EnumerationBaseInt<LogTypeEnum>
    {
        /// <summary>
        /// No hubo cambios.
        /// </summary>
        public static readonly LogTypeEnum None = new LogTypeEnum(1, nameof(None));

        /// <summary>
        /// Creación.
        /// </summary>
        public static readonly LogTypeEnum Created = new LogTypeEnum(2, nameof(Created));

        /// <summary>
        /// Actualización.
        /// </summary>
        public static readonly LogTypeEnum Updated = new LogTypeEnum(3, nameof(Updated));

        /// <summary>
        /// Eliminación.
        /// </summary>
        public static readonly LogTypeEnum Deleted = new LogTypeEnum(4, nameof(Deleted));

        /// <summary>
        /// Cambio de estado.
        /// </summary>
        public static readonly LogTypeEnum Changed = new LogTypeEnum(5, nameof(Changed));

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="LogTypeEnum"/>.
        /// </summary>
        /// <param name="value">Valor de log.</param>
        /// <param name="name">Nombre del log.</param>
        private LogTypeEnum(int value, string name)
            : base(value, name) { }
    }
}
