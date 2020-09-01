// -----------------------------------------------------------------------
// <copyright file="Address.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using System.Diagnostics.CodeAnalysis;
    using Kitpymes.Core.Shared;

    /// <summary>
    /// Objeto de valor para las direcciones.
    /// </summary>
    public sealed class Address : ValueObjectBase
    {
        private Address() { }

        private Address(string street, int number, string postalCode, string city, string state, string country)
        => ChangeStreet(street)
            .ChangeNumber(number)
            .ChangePostalCode(postalCode)
            .ChangeCity(city)
            .ChangeState(state)
            .ChangeCountry(country);

        /// <summary>
        /// Obtiene una dirección vacia.
        /// </summary>
        public static Address Null => new Address();

        /// <summary>
        /// Obtiene un valor que indica si la dirección esta vacia.
        /// </summary>
        public bool IsNull => Shared.Util.Check.IsNullOrEmpty(Street, City, State, Country, PostalCode).HasErrors;

        /// <summary>
        /// Obtiene la dirección.
        /// </summary>
        public string? Street { get; private set; }

        /// <summary>
        /// Obtiene el número.
        /// </summary>
        public int? Number { get; private set; }

        /// <summary>
        /// Obtiene el código postal.
        /// </summary>
        public string? PostalCode { get; private set; }

        /// <summary>
        /// Obtiene la ciudad.
        /// </summary>
        public string? City { get; private set; }

        /// <summary>
        /// Obtiene el estado.
        /// </summary>
        public string? State { get; private set; }

        /// <summary>
        /// Obtiene el pais.
        /// </summary>
        public string? Country { get; private set; }

        /// <summary>
        /// Crea una nueva dirección completa.
        /// </summary>
        /// <param name="street">Dirección.</param>
        /// <param name="number">Número.</param>
        /// <param name="postalCode">Código postal.</param>
        /// <param name="city">Ciudad.</param>
        /// <param name="state">Estado.</param>
        /// <param name="country">Pais.</param>
        /// <returns>Address | ApplicationException.</returns>
        [return: NotNull]
        public static Address Create(string street, int number, string postalCode, string city, string state, string country)
        => new Address(street, number, postalCode, city, state, country);

        /// <summary>
        /// Modifica la dirección.
        /// </summary>
        /// <param name="street">Dirección nueva.</param>
        /// <returns>Address.</returns>
        [return: NotNull]
        public Address ChangeStreet(string? street)
        {
            Street = street.ToIsNullOrEmptyThrow(nameof(street));

            return this;
        }

        /// <summary>
        /// Modifica el número de la dirección.
        /// </summary>
        /// <param name="number">Número nuevo.</param>
        /// <returns>Address.</returns>
        [return: NotNull]
        public Address ChangeNumber(int number)
        {
            Number = number.ToIsLessThrow(1, nameof(number));

            return this;
        }

        /// <summary>
        /// Modifica el código postal.
        /// </summary>
        /// <param name="postalCode">Código postal nuevo.</param>
        /// <returns>Address.</returns>
        [return: NotNull]
        public Address ChangePostalCode(string? postalCode)
        {
            PostalCode = postalCode.ToIsNullOrEmptyThrow(nameof(postalCode));

            return this;
        }

        /// <summary>
        /// Modifica la cuidad.
        /// </summary>
        /// <param name="city">Ciudad nuevo.</param>
        /// <returns>Address.</returns>
        [return: NotNull]
        public Address ChangeCity(string? city)
        {
            City = city.ToIsNullOrEmptyThrow(nameof(city));

            return this;
        }

        /// <summary>
        /// Modifica el estado.
        /// </summary>
        /// <param name="state">Estado nuevo.</param>
        /// <returns>Address.</returns>
        [return: NotNull]
        public Address ChangeState(string? state)
        {
            State = state.ToIsNullOrEmptyThrow(nameof(state));

            return this;
        }

        /// <summary>
        /// Modifica el pais.
        /// </summary>
        /// <param name="country">Pais nuevo.</param>
        /// <returns>Address.</returns>
        [return: NotNull]
        public Address ChangeCountry(string? country)
        {
            Country = country.ToIsNullOrEmptyThrow(nameof(country));

            return this;
        }

        /// <summary>
        /// Devuelve la dirección completa.
        /// </summary>
        /// <returns>"{Street} {Number}, {PostalCode} - {City} {State}, {Country}".</returns>
        public override string ToString() => $"{Street} {Number}, {PostalCode} - {City} {State}, {Country}";

        /// <inheritdoc/>
        protected override System.Collections.Generic.IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return PostalCode;
            yield return City;
            yield return State;
            yield return Country;
        }
    }
}
