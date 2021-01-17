// -----------------------------------------------------------------------
// <copyright file="JsonWebTokenOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using Kitpymes.Core.Shared;

    /*
        Clase para las opciones del token de sesi�n JsonWebTokenOptions
        Contiene las opciones para la configuraci�n del token de sesi�n
    */

    /// <summary>
    /// Clase para las opciones del token de sesi�n <c>JsonWebTokenOptions</c>.
    /// Contiene las opciones para la configuraci�n del token de sesi�n.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los m�todos para las opciones del token de sesi�n.</para>
    /// </remarks>
    public class JsonWebTokenOptions
    {
        /// <summary>
        /// Obtiene la configuraci�n del token de sesi�n.
        /// </summary>
        public JsonWebTokenSettings JsonWebTokenSettings { get; private set; } = new JsonWebTokenSettings();

        /// <summary>
        /// Obtiene o establece un valor que indica el servicio esta habilitado.
        /// </summary>
        /// <param name="enabled">Si se habilita el servicio.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithEnabled(bool enabled = true)
        {
            JsonWebTokenSettings.Enabled = enabled;

            return this;
        }

        /// <summary>
        /// Obtiene o establece el esquema de autenticaci�n.
        /// </summary>
        /// <param name="authenticateScheme">Nombre del esquema de autenticaci�n.</param>
        /// <param name="challengeScheme">Nombre del esquema de desafio.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si authenticateScheme es nulo o vacio.</returns>
        public JsonWebTokenOptions WithScheme(string authenticateScheme = JsonWebTokenSettings.DefaultAuthenticateScheme, string challengeScheme = JsonWebTokenSettings.DefaultChallengeScheme)
        {
            JsonWebTokenSettings.AuthenticateScheme = authenticateScheme.ToIsNullOrEmptyThrow(nameof(authenticateScheme));
            JsonWebTokenSettings.ChallengeScheme = challengeScheme.ToIsNullOrEmptyThrow(nameof(challengeScheme));

            return this;
        }

        /// <summary>
        /// Obtiene o establece si se requiere HTTPS para la direcci�n o autoridad de metadatos.
        /// Esto debe desactivarse solo en entornos de desarrollo.
        /// </summary>
        /// <param name="requireHttpsMetadata">Si se desea validar la vida �til del token.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithRequireHttpsMetadata(bool requireHttpsMetadata = JsonWebTokenSettings.DefaultRequireHttpsMetadata)
        {
            JsonWebTokenSettings.RequireHttpsMetadata = requireHttpsMetadata;

            return this;
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si los tokens deben tener un valor de 'caducidad'.
        /// </summary>
        /// <param name="requireExpirationTime">Si queremos que el token tenga un valor de cadicidad.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithRequireExpirationTime(bool requireExpirationTime = JsonWebTokenSettings.DefaultRequireExpirationTime)
        {
            JsonWebTokenSettings.RequireExpirationTime = requireExpirationTime;

            return this;
        }

        /// <summary>
        /// Obtiene o establece si el token de portador debe almacenarse en Microsoft.AspNetCore.Authentication.AuthenticationProperties despu�s de una autorizaci�n exitosa.
        /// </summary>
        /// <param name="saveToken">Si queremos que el token se guarde como propiedad.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithSaveToken(bool saveToken = JsonWebTokenSettings.DefaultSaveToken)
        {
            JsonWebTokenSettings.SaveToken = saveToken;

            return this;
        }

        /// <summary>
        /// Obtiene o establece si los errores de validaci�n del token deben devolverse al cliente mediante el encabezado WWW-Authenticate.
        /// </summary>
        /// <param name="includeErrorDetails">Si queremos que los errores de validaci�n se devuelvan al cliente.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithIncludeErrorDetails(bool includeErrorDetails = JsonWebTokenSettings.DefaultIncludeErrorDetails)
        {
            JsonWebTokenSettings.IncludeErrorDetails = includeErrorDetails;

            return this;
        }

        /// <summary>
        /// Valida la vida �til del token.
        /// </summary>
        /// <param name="validateLifetime">Si se desea validar la vida �til del token.</param>
        /// <returns>JsonWebTokenOptions.</returns>
        public JsonWebTokenOptions WithValidateLifetime(bool validateLifetime = JsonWebTokenSettings.DefaultValidateLifetime)
        {
            JsonWebTokenSettings.ValidateLifetime = validateLifetime;

            return this;
        }

        /// <summary>
        /// Obtiene o establece la configuraci�n del tiempo de expiraci�n del token de sesi�n.
        /// </summary>
        /// <param name="days">Valor en d�as.</param>
        /// <param name="hours">Valor en horas.</param>
        /// <param name="minutes">Valor en minutos.</param>
        /// <param name="seconds">Valor en segundos.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si algun valor es menor que cero.</returns>
        public JsonWebTokenOptions WithExpire(int days = ExpireSettings.DefaultDays, int hours = ExpireSettings.DefaultHours, int minutes = ExpireSettings.DefaultMinutes, int seconds = ExpireSettings.DefaultSeconds)
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

        /// <summary>
        /// Obtiene o establece la clave p�blica de sesi�n.
        /// </summary>
        /// <param name="publicKey">Clave �nica.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si key es nulo o vacio.</returns>
        public JsonWebTokenOptions WithPublicKey(string publicKey)
        {
            JsonWebTokenSettings.PublicKey = publicKey.ToIsNullOrEmptyThrow(nameof(publicKey));

            return this;
        }

        /// <summary>
        /// Obtiene o establece la clave privada de sesi�n.
        /// </summary>
        /// <param name="privateKey">Clave �nica.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si key es nulo o vacio.</returns>
        public JsonWebTokenOptions WithPrivateKey(string privateKey)
        {
            JsonWebTokenSettings.PrivateKey = privateKey.ToIsNullOrEmptyThrow(nameof(privateKey));

            return this;
        }

        /// <summary>
        /// Nombre del cliente a validar.
        /// </summary>
        /// <param name="issuer">Emisor a validar.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si issuer es nulo o vacio.</returns>
        public JsonWebTokenOptions WithIssuer(string issuer)
        {
            JsonWebTokenSettings.Issuer = issuer.ToIsNullOrEmptyThrow(nameof(issuer));

            return this;
        }

        /// <summary>
        /// Nombre del receptor a validar.
        /// </summary>
        /// <param name="audience">Audiencia a validar.</param>
        /// <returns>JsonWebTokenOptions | ApplicationException: si audience es nulo o vacio.</returns>
        public JsonWebTokenOptions WithAudience(string audience)
        {
            JsonWebTokenSettings.Audience = audience.ToIsNullOrEmptyThrow(nameof(audience));

            return this;
        }
    }
}
