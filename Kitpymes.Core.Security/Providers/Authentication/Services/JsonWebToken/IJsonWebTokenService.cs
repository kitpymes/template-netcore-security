// -----------------------------------------------------------------------
// <copyright file="IJsonWebTokenService.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /*
        Interfaz para el token de sesión IJsonWebTokenService
        Contiene las firmas para el token de sesión
    */

    /// <summary>
    /// Interfaz para el token de sesión <c>IJsonWebTokenService</c>.
    /// Contiene las firmas para el token de sesión.
    /// </summary>
    /// <remarks>
    /// <para>En esta interfaz se pueden agregar todas las firmas para el token de sesión.</para>
    /// </remarks>
    public interface IJsonWebTokenService
    {
        /// <summary>
        /// Craa un token de sesión simétrico.
        /// </summary>
        /// <param name="claims">Lista de información a codificar.</param>
        /// <param name="headers">Lista de valores a codificar.</param>
        /// <returns>CreateJsonWebTokenResult | InternalServerError: si surge algún error inesperado.</returns>
        CreateJsonWebTokenResult Create(IEnumerable<Claim> claims, IDictionary<string, IEnumerable<string>>? headers = null);

        /// <summary>
        /// Decodifica el token de sesión.
        /// </summary>
        /// <param name="token">Clave de sesión.</param>
        /// <returns>GetJsonWebTokenResult | InternalServerError: si surge algún error inesperado.</returns>
        GetJsonWebTokenResult Get(string? token);

        /// <summary>
        /// Codifica el token de sesión.
        /// </summary>
        /// <param name="claims">Lista de información a codificar.</param>
        /// <param name="headers">Lista de valores a codificar.</param>
        /// <returns>CreateJsonWebTokenResult | InternalServerError: si surge algún error inesperado.</returns>
        Task<CreateJsonWebTokenResult> CreateAsync(IEnumerable<Claim> claims, IDictionary<string, IEnumerable<string>>? headers = null);

        /// <summary>
        /// Decodifica el token de sesión.
        /// </summary>
        /// <param name="token">Clave de sesión.</param>
        /// <returns>GetJsonWebTokenResult | InternalServerError: si surge algún error inesperado.</returns>
        Task<GetJsonWebTokenResult> GetAsync(string? token);
    }
}