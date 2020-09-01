// -----------------------------------------------------------------------
// <copyright file="CardTypeEnum.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    /*
        Clase de enumeraciones para las tarjetas de débito o crédito EnumerationBaseInt<CardType>
        Contiene la lista de tarjetas de débito o crédito
    */

    /// <summary>
    /// Clase de enumeraciones para las tarjetas de débito o crédito <c>EnumerationBaseInt</c>.
    /// Contiene la lista de tarjetas de débito o crédito.
    /// <list type="bullet">
    ///     <item>
    ///         <term>Amex = 1</term>
    ///         <description>Tarjeta American Express</description>
    ///     </item>
    ///     <item>
    ///         <term>Visa = 2</term>
    ///         <description>Tarjeta Visa</description>
    ///     </item>
    ///     <item>
    ///         <term>MasterCard = 3</term>
    ///         <description>Tarjeta Master Card</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las tarjetas de débito o crédito.</para>
    /// </remarks>
    public class CardTypeEnum : EnumerationBaseInt<CardTypeEnum>
    {
        /// <summary>
        /// Tarjeta American Express.
        /// </summary>
        public static readonly CardTypeEnum Amex = new CardTypeEnum(1, "American Express", nameof(Amex));

        /// <summary>
        /// Tarjeta American Visa.
        /// </summary>
        public static readonly CardTypeEnum Visa = new CardTypeEnum(2, nameof(Visa), nameof(Visa));

        /// <summary>
        /// Tarjeta Master Card.
        /// </summary>
        public static readonly CardTypeEnum MasterCard = new CardTypeEnum(3, "Master Card", nameof(MasterCard));

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CardTypeEnum"/>.
        /// </summary>
        /// <param name="value">Valor de la tarjeta.</param>
        /// <param name="name">Nombre de la tarjeta.</param>
        /// <param name="shortName">Nombre corto de la tarjeta.</param>
        private CardTypeEnum(int value, string name, string shortName)
            : base(value, name, shortName) { }

        /// <summary>
        /// Obtiene un valor que indica si la tarjeta es american express.
        /// </summary>
        public bool IsAmex => Name == Amex.Name;

        /// <summary>
        /// Obtiene un valor que indica si la tarjeta es visa.
        /// </summary>
        public bool IsVisa => Name == Visa.Name;

        /// <summary>
        /// Obtiene un valor que indica si la tarjeta es MasterCard.
        /// </summary>
        public bool IsMasterCard => Name == MasterCard.Name;
    }
}
