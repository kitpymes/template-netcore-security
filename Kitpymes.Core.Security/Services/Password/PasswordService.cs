// -----------------------------------------------------------------------
// <copyright file="PasswordService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using Kitpymes.Core.Shared;

    /*
        Clase para encriptar contraseñas PasswordService
        Contiene los métodos para encriptar contraseñas
    */

    /// <summary>
    /// Clase para encriptar contraseñas <c>PasswordService</c>.
    /// Contiene los métodos para encriptar contraseñas.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todos los métodos para la encriptación de contraseñas.</para>
    /// </remarks>
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PasswordService"/>.
        /// </summary>
        /// <param name="settings">Configuración de la contraseña.</param>
        public PasswordService(PasswordSettings settings) => PasswordSettings = settings;

        private PasswordSettings PasswordSettings { get; }

        /// <inheritdoc/>
        public (bool hasErrors, string? hashPassword, List<PasswordResult>? errors) Create(string? plainPassword)
        {
            var (hasErrors, errors) = Validate(plainPassword);

            var hashPassword = hasErrors ? null : Shared.Util.Hash.CreatePassword(plainPassword!);

            return (hasErrors, hashPassword, errors);
        }

        /// <inheritdoc/>
        public bool Verify(string? plainPassword, string hashPassword)
        => !Validate(plainPassword).hasErrors && Shared.Util.Hash.VerifyPassword(hashPassword, plainPassword!);

        /// <inheritdoc/>
        public (bool hasErrors, List<PasswordResult>? errors) Validate(string? plainPassword)
        {
            var errors = new List<PasswordResult>();

            if (plainPassword.ToIsNullOrEmpty())
            {
                errors.Add(PasswordResult.NullOrEmpty);
            }

            if (PasswordSettings != null)
            {
                if (PasswordSettings.RequireDigit.HasValue
                    && PasswordSettings.RequireDigit.Value
                    && !plainPassword.ToIsDigit())
                {
                    errors.Add(PasswordResult.RequireDigit);
                }

                if (PasswordSettings.RequiredMinLength.HasValue
                    && plainPassword.ToIsLess(PasswordSettings.RequiredMinLength.Value))
                {
                    errors.Add(PasswordResult.RequiredMinLength);
                }

                if (PasswordSettings.RequiredUniqueChars.HasValue
                    && PasswordSettings.RequiredUniqueChars.Value
                    && !plainPassword.ToIsUniqueChars())
                {
                    errors.Add(PasswordResult.RequiredUniqueChars);
                }

                if (PasswordSettings.RequireEspecialChars.HasValue
                    && PasswordSettings.RequireEspecialChars.Value
                    && !plainPassword.ToIsEspecialChars())
                {
                    errors.Add(PasswordResult.RequireEspecialChars);
                }

                if (PasswordSettings.RequireLowercase.HasValue
                    && PasswordSettings.RequireLowercase.Value
                    && !plainPassword.ToIsLowercase())
                {
                    errors.Add(PasswordResult.RequireLowercase);
                }

                if (PasswordSettings.RequireUppercase.HasValue
                    && PasswordSettings.RequireUppercase.Value
                    && !plainPassword.ToIsUppercase())
                {
                    errors.Add(PasswordResult.RequireUppercase);
                }
            }

            var hasErrors = errors.Any();

            return (hasErrors, hasErrors ? errors : null);
        }
    }
}