// -----------------------------------------------------------------------
// <copyright file="FullName.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Entities
{
    using System.Diagnostics.CodeAnalysis;
    using Kitpymes.Core.Shared;

    /// <summary>
    /// Objeto de valor para nombres de personas.
    /// </summary>
    public sealed class FullName : ValueObjectBase
    {
        private FullName() { }

        private FullName(string firstName, string lastName, string? middleName = null)
        {
            ChangeFirstName(firstName).ChangeLastName(lastName);

            if (!middleName.ToIsNullOrEmpty())
            {
                ChangeMiddleName(middleName);
            }
        }

        /// <summary>
        /// Obtiene un nombre vacio.
        /// </summary>
        public static FullName Null => new FullName();

        /// <summary>
        /// Obtiene un valor que indica si el nombre esta vacio.
        /// </summary>
        public bool IsNull => FirstName.ToIsNullOrEmpty();

        /// <summary>
        /// Obtiene el primer nombre.
        /// </summary>
        public string? FirstName { get; private set; }

        /// <summary>
        /// Obtiene el segundo nombre.
        /// </summary>
        public string? MiddleName { get; private set; }

        /// <summary>
        /// Obtiene el apellido.
        /// </summary>
        public string? LastName { get; private set; }

        /// <summary>
        /// Crea un nuevo nombre completo.
        /// </summary>
        /// <param name="firstName">Primer nombre.</param>
        /// <param name="lastName">Segundo nombre.</param>
        /// <param name="middleName">Apellido.</param>
        /// <returns>FullName | ApplicationException.</returns>
        [return: NotNull]
        public static FullName Create(string firstName, string lastName, string? middleName = null)
        => new FullName(firstName, lastName, middleName);

        /// <summary>
        /// Modifica el primer nombre.
        /// </summary>
        /// <param name="firstName">Primer nombre nuevo.</param>
        /// <returns>FullName.</returns>
        [return: NotNull]
        public FullName ChangeFirstName(string? firstName)
        {
            FirstName = firstName.ToIsNameThrow(nameof(firstName));

            return this;
        }

        /// <summary>
        /// Modifica el segundo nombre.
        /// </summary>
        /// <param name="middleName">Segundo nombre nuevo.</param>
        /// <returns>FullName.</returns>
        [return: NotNull]
        public FullName ChangeMiddleName(string? middleName)
        {
            MiddleName = middleName.ToIsNameThrow(nameof(middleName));

            return this;
        }

        /// <summary>
        /// Modifica el apellido.
        /// </summary>
        /// <param name="lastName">Apellido nuevo.</param>
        /// <returns>FullName.</returns>
        [return: NotNull]
        public FullName ChangeLastName(string? lastName)
        {
            LastName = lastName.ToIsNameThrow(nameof(lastName));

            return this;
        }

        /// <summary>
        /// Devuelve el nombre y apellido.
        /// </summary>
        /// <returns>"{FirstName} {LastName}".</returns>
        public override string ToString() => $"{FirstName} {LastName}";

        /// <summary>
        /// Devuelve el nombre completo.
        /// </summary>
        /// <returns>"{FirstName} {MiddleName}, {LastName}".</returns>
        public string ToFullString() => $"{FirstName} {MiddleName}, {LastName}";

        /// <inheritdoc/>
        protected override System.Collections.Generic.IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return MiddleName;
            yield return LastName;
        }
    }
}
