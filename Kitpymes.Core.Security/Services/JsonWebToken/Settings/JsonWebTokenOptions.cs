// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using Kitpymes.Core.Shared;

    /*
        Clase para las opciones del token de sesión JsonWebTokenOptions
        Contiene las opciones para la configuración del token de sesión
    */

    /// <summary>
    /// Clase para las opciones del token de sesión <c>JsonWebTokenOptions</c>.
    /// Contiene las opciones para la configuración del token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos para las opciones del token de sesión.</para>
    /// </remarks>
    public class JsonWebTokenOptions
    {
        /// <summary>
        /// Obtiene la configuración del token de sesión.
        /// </summary>
        public JsonWebTokenSettings JsonWebTokenSettings { get; private set; } = new JsonWebTokenSettings();

        /// <summary>
        /// Agrega el esquema de autenticación.
        /// </summary>
        /// <param name="authenticateScheme">Nombre del esquema de autenticación.</param>
        /// <param name="challengeScheme">Nombre del esquema de desafio.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si authenticateScheme es nulo o vacio.</returns>
        public JsonWebTokenOptions WithScheme(string authenticateScheme = "Bearer", string challengeScheme = "Bearer")
        {
            JsonWebTokenSettings.AuthenticateScheme = authenticateScheme.ToIsNullOrEmptyThrow(nameof(authenticateScheme));
            JsonWebTokenSettings.ChallengeScheme = challengeScheme;

            return this;
        }

        /// <summary>
        /// Valida la clave de firma de emisor.
        /// </summary>
        /// <param name="key">Clave unica.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si key es nulo o vacio.</returns>
        public JsonWebTokenOptions WithValidateIssuerSigningKey(string key)
        {
            JsonWebTokenSettings.Key = key.ToIsNullOrEmptyThrow(nameof(key));

            return this;
        }

        /// <summary>
        /// Valida el emisor.
        /// </summary>
        /// <param name="validIssuer">Emisor a validar.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si validIssuer es nulo o vacio.</returns>
        public JsonWebTokenOptions WithValidateIssuer(string validIssuer)
        {
            JsonWebTokenSettings.ValidIssuer = validIssuer.ToIsNullOrEmptyThrow(nameof(validIssuer));

            return this;
        }

        /// <summary>
        /// Valida la audiencia.
        /// </summary>
        /// <param name="validAudience">Audiencia a validar.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si validAudience es nulo o vacio.</returns>
        public JsonWebTokenOptions WithValidateAudience(string validAudience)
        {
            JsonWebTokenSettings.ValidAudience = validAudience.ToIsNullOrEmptyThrow(nameof(validAudience));

            return this;
        }

        /// <summary>
        /// Valida si se expiro la clave.
        /// </summary>
        /// <param name="date">Fecha a validar para el ciclo de vida.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si lifetimeValidator es nulo.</returns>
        public JsonWebTokenOptions WithValidateLifetime(DateTime date)
        {
            JsonWebTokenSettings.LifetimeValidator = (before, expires, token, param)
                => expires > date.ToIsNullOrEmptyThrow(nameof(date));

            return this;
        }

        /// <summary>
        /// Indica si los tokens deben tener un valor de 'caducidad'.
        /// </summary>
        /// <param name="requireExpirationTime">Si queremos que el token tenga un valor de cadicidad.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithRequireExpirationTime(bool requireExpirationTime = true)
        {
            JsonWebTokenSettings.RequireExpirationTime = requireExpirationTime;

            return this;
        }

        /// <summary>
        /// Tiempo de expiración del token.
        /// </summary>
        /// <param name="days">Valor en días.</param>
        /// <param name="hours">Valor en horas.</param>
        /// <param name="minutes">Valor en minutos.</param>
        /// <param name="seconds">Valor en segundos.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si algun valor es menor que cero.</returns>
        public JsonWebTokenOptions WithExpire(int days = 30, int hours = 0, int minutes = 0, int seconds = 0)
        {
            JsonWebTokenSettings.Expire = new ExpireSettings
            {
                Days = days.ToIsLessThrow(0, nameof(days)),
                Hours = hours.ToIsLessThrow(0, nameof(hours)),
                Minutes = minutes.ToIsLessThrow(0, nameof(minutes)),
                Seconds = seconds.ToIsLessThrow(0, nameof(seconds)),
            };

            return this;
        }
    }
}
