// -----------------------------------------------------------------------
// <copyright file="Currency.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Kitpymes.Core.Shared;

    /// <summary>
    /// Objeto de valor para el dinero.
    /// </summary>
    public sealed class Currency : ValueObjectBase
    {
        private Currency()
            : this((CodeName)Enum.Parse(typeof(CodeName), CultureInfo.CurrentCulture.ThreeLetterISOLanguageName))
        { }

        private Currency(CodeName code) => ChangeCurrency(code);

        /// <summary>
        /// Un mapeo de códigos de moneda y códigos numéricos ISO 4217.
        /// Lista: https://es.iban.com/currency-codes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:Enumeration items should be documented", Justification = "<pendiente>")]
        public enum CodeName
        {
            ///// <summary>
            ///// Dirham DE EAU
            ///// </summary>
            // [Description("Dirham DE EAU")]
            // AED = 784,

            ///// <summary>
            ///// Afgani afgano
            ///// </summary>
            // [Description("Afgani afgano")]
            // AFN = 971,

            ///// <summary>
            ///// Lek
            ///// </summary>
            // [Description("Lek")]
            // ALL = 008,

            ///// <summary>
            ///// Dram armenio
            ///// </summary>
            // [Description("Dram armenio")]
            // AMD = 051,

            ///// <summary>
            ///// Florín antillano neerlandés
            ///// </summary>
            // [Description("Florín antillano neerlandés")]
            // ANG = 532,

            ///// <summary>
            ///// Kwanza angoleño
            ///// </summary>
            // [Description("Kwanza angoleño")]
            // AOA = 973,

            ///// <summary>
            ///// Peso argentino
            ///// </summary>
            // [Description("Peso argentino")]
            // ARS = 032,

            ///// <summary>
            ///// Dólar australiano
            ///// </summary>
            // [Description("Dólar australiano")]
            // AUD = 036,

            // AWG = 533,
            // AZN = 944,
            // BAM = 977,
            // BBD = 052,
            // BDT = 050,
            // BGN = 975,
            // BHD = 048,
            // BIF = 108,
            // BMD = 060,
            // BND = 096,
            // BOB = 068,
            // BOV = 984,
            // BRL = 986,
            // BSD = 044,
            // BTN = 064,
            // BWP = 072,
            // BYR = 974,
            // BZD = 084,
            // CAD = 124,
            // CDF = 976,
            // CHE = 947,
            // CHF = 756,
            // CHW = 948,
            // CLF = 990,
            // CLP = 152,
            // CNY = 156,
            // COP = 170,
            // COU = 970,
            // CRC = 188,
            // CUC = 931,
            // CUP = 192,
            // CVE = 132,
            // CZK = 203,
            // DJF = 262,
            // DKK = 208,
            // DOP = 214,
            // DZD = 012,
            // EGP = 818,
            // ERN = 232,
            // ETB = 230,

            /// <summary>
            /// Euro
            /// </summary>
            [Description("Euro")]
            EUR = 978,

            // FJD = 242,
            // FKP = 238,
            // GBP = 826,
            // GEL = 981,
            // GHS = 936,
            // GIP = 292,
            // GMD = 270,
            // GNF = 324,
            // GTQ = 320,
            // GYD = 328,
            // HKD = 344,
            // HNL = 340,
            // HRK = 191,
            // HTG = 332,
            // HUF = 348,
            // IDR = 360,
            // ILS = 376,
            // INR = 356,
            // IQD = 368,
            // IRR = 364,
            // ISK = 352,
            // JMD = 388,
            // JOD = 400,
            // JPY = 392,
            // KES = 404,
            // KGS = 417,
            // KHR = 116,
            // KMF = 174,
            // KPW = 408,
            // KRW = 410,
            // KWD = 414,
            // KYD = 136,
            // KZT = 398,
            // LAK = 418,
            // LBP = 422,
            // LKR = 144,
            // LRD = 430,
            // LSL = 426,
            // LYD = 434,
            // MAD = 504,
            // MDL = 498,
            // MGA = 969,
            // MKD = 807,
            // MMK = 104,
            // MNT = 496,
            // MOP = 446,
            // MRO = 478,
            // MUR = 480,
            // MVR = 462,
            // MWK = 454,
            // MXN = 484,
            // MXV = 979,
            // MYR = 458,
            // MZN = 943,
            // NAD = 516,
            // NGN = 566,
            // NIO = 558,
            // NOK = 578,
            // NPR = 524,
            // NZD = 554,
            // OMR = 512,
            // PAB = 590,
            // PEN = 604,
            // PGK = 598,
            // PHP = 608,
            // PKR = 586,
            // PLN = 985,
            // PYG = 600,
            // QAR = 634,
            // RON = 946,
            // RSD = 941,
            // RUB = 643,
            // RWF = 646,
            // SAR = 682,
            // SBD = 090,
            // SCR = 690,
            // SDG = 938,
            // SEK = 752,
            // SGD = 702,
            // SHP = 654,
            // SLL = 694,
            // SOS = 706,
            // SRD = 968,
            // SSP = 728,
            // STD = 678,
            // SVC = 222,
            // SYP = 760,
            // SZL = 748,
            // THB = 764,
            // TJS = 972,
            // TMT = 934,
            // TND = 788,
            // TOP = 776,
            // TRY = 949,
            // TTD = 780,
            // TWD = 901,
            // TZS = 834,
            // UAH = 980,
            // UGX = 800,

            /// <summary>
            /// Dólar estadounidense
            /// </summary>
            [Description("Dólar estadounidense")]
            USD = 840,

            // USN = 997,
            // UYI = 940,
            // UYU = 858,
            // UZS = 860,
            // VEF = 937,
            // VND = 704,
            // VUV = 548,
            // WST = 882,
            // XAF = 950,
            // XAG = 961,
            // XAU = 959,
            // XBA = 955,
            // XBB = 956,
            // XBC = 957,
            // XBD = 958,
            // XCD = 951,
            // XDR = 960,
            // XOF = 952,
            // XPD = 964,
            // XPF = 953,
            // XPT = 962,
            // XSU = 994,
            // XTS = 963,
            // XUA = 965,
            // XXX = 999,
            // YER = 886,
            // ZAR = 710,
            // ZMW = 967,
            // ZWL = 932
        }

        /// <summary>
        /// Obtiene un simbolo por defecto.
        /// </summary>
        /// <returns>Currency | ApplicationException.</returns>
        public static Currency Default => new Currency();

        /// <summary>
        /// Obtiene el símbolo del tipo de moneda.
        /// </summary>
        public string? Symbol { get; private set; }

        /// <summary>
        /// Obtiene el código del tipo de moneda.
        /// </summary>
        public string? Code { get; private set; }

        /// <summary>
        /// Obtiene el nombre del tipo de moneda.
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Crea un dinero.
        /// </summary>
        /// <param name="code">Tipo de moneda.</param>
        /// <returns>Currency | ApplicationException.</returns>
        [return: NotNull]
        public static Currency Create(CodeName code) => new Currency(code);

        /// <summary>
        /// Modifica el tipo de moneda.
        /// </summary>
        /// <param name="code">Tipo de moneda.</param>
        /// <returns>Money.</returns>
        [return: NotNull]
        public Currency ChangeCurrency(CodeName code)
        {
            var codeString = code.ToIsNullOrEmptyThrow(nameof(code)).ToString();

            if (codeString != Code)
            {
                var cultureInfo = GetCultureInfo(codeString)
                    .ToIsNullOrEmptyWithMessageThrow($"Currency code: {code} is not supported");

                Symbol = cultureInfo.NumberFormat.CurrencySymbol;

                Code = codeString;

                Name = new RegionInfo(cultureInfo.Name).CurrencyNativeName;
            }

            return this;
        }

        /// <summary>
        /// Devuelve la cantidad de dinero.
        /// </summary>
        /// <returns>string: Symbol.</returns>
        public override string ToString() => $"{Symbol}";

        /// <inheritdoc/>
        protected override System.Collections.Generic.IEnumerable<object?> GetEqualityComponents()
        {
            yield return Symbol;
            yield return Code;
            yield return Name;
        }

        private static CultureInfo? GetCultureInfo(string code)
        => CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .FirstOrDefault(culture => new RegionInfo(culture.LCID).ISOCurrencySymbol == code);
    }
}
