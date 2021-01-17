// -----------------------------------------------------------------------
// <copyright file="JwtBearerChallengeContextExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

     /*
        Clase de extensión JwtBearerChallengeContextExtensions
        Contiene las extensiones del contexto de JwtBearer
     */

    /// <summary>
    /// Clase de extensión <c>JwtBearerChallengeContextExtensions</c>.
    /// Contiene las extensiones del contexto de JwtBearer.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones del contexto de JwtBearer.</para>
    /// </remarks>
    public static class JwtBearerChallengeContextExtensions
    {
        /// <summary>
        /// Detalle del contexto.
        /// </summary>
        /// <param name="context">Contexto.</param>
        /// <returns>string.</returns>
        public static string ToDetails(this JwtBearerChallengeContext context)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(context.Error))
            {
                sb.Append($"AuthError: {context.Error} |");
            }

            if (!string.IsNullOrWhiteSpace(context.ErrorDescription))
            {
                sb.Append($" AuthErrorDescription: {context.ErrorDescription} |");
            }

            if (!string.IsNullOrWhiteSpace(context.ErrorUri))
            {
                sb.Append($" AuthErrorUri: {context.ErrorUri} |");
            }

            if (!string.IsNullOrWhiteSpace(context.Options.Challenge))
            {
                sb.Append($" WWWAuthenticate: {context.Options.Challenge} |");
            }

            if (context.AuthenticateFailure != null)
            {
                sb.Append($" AuthExceptionType: {context.AuthenticateFailure.GetBaseException().GetType().Name} | ");

                sb.Append($" AuthExceptionMessage: {context.AuthenticateFailure.Message}");
            }

            return sb.ToString();
        }
    }
}
