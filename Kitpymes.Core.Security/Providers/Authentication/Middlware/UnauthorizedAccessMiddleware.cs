// -----------------------------------------------------------------------
// <copyright file="UnauthorizedAccessMiddleware.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;
    using Kitpymes.Core.Shared.Util;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /*
        Clase del middlware UnauthorizedAccessMiddleware
        Contiene el mensaje de devolución de los errores de acceso al sistema
    */

    /// <summary>
    /// Clase del middlware <c>UnauthorizedAccessMiddleware</c>.
    /// Contiene el mensaje de devolución de los errores de acceso al sistema.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades requeridas para captar los errores de acceso al sistema.</para>
    /// </remarks>
    public class UnauthorizedAccessMiddleware
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnauthorizedAccessMiddleware"/>.
        /// </summary>
        /// <param name="requestDelegate">Una función que puede procesar una solicitud HTTP.</param>
        /// <param name="loggerFactory">Representa un tipo utilizado para configurar el registro de errores.</param>
        public UnauthorizedAccessMiddleware(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            RequestDelegate = requestDelegate;

            Logger = loggerFactory.CreateLogger<UnauthorizedAccessMiddleware>();
        }

        private RequestDelegate RequestDelegate { get; }

        private ILogger<UnauthorizedAccessMiddleware> Logger { get; }

        private string? RequestBody { get; set; }

        /// <summary>
        /// Devuelve el mensaje configurado al cliente si ocurre una excepción.
        /// </summary>
        /// <param name="httpContext">Encapsula toda la información específica de una solicitud HTTP.</param>
        /// <returns>Task.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext != null)
                {
                    await ReadRequestBodyAsync(httpContext).ConfigureAwait(false);
                }

                await RequestDelegate(httpContext).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception).ConfigureAwait(false);
            }
        }

        private async Task ReadRequestBodyAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength, CultureInfo.CurrentCulture)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

            RequestBody = Encoding.UTF8.GetString(buffer).ToRemove("\r\n", " ");

            request.Body.Position = 0;
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            if (exception is UnauthorizedAccessException)
            {
                var environment = httpContext.RequestServices.ToEnvironment();

                var exceptionTypeName = exception.GetType().Name;

                var detailsOptionalData = new Dictionary<string, IList<string>>();

                if (!RequestBody.ToIsNullOrEmpty())
                {
                    detailsOptionalData.AddOrUpdate(nameof(RequestBody), RequestBody);
                }

                var details = httpContext.ToDetails(detailsOptionalData);

                var result = Result.Unauthorized();

                if (environment.IsDevelopment())
                {
                    result.Message = exception.ToFullMessage();
                    result.Exception = exceptionTypeName;
                    result.Details = details;
                }
                else
                {
                    Logger.LogError(exception.ToFullMessage(), details);
                }

                var headers = new Dictionary<string, IList<string>>();
                headers.AddOrUpdate(nameof(Exception), exceptionTypeName);

                await httpContext.Response.ToResultAsync(
                    status: HttpStatusCode.Unauthorized,
                    message: result.ToJson(),
                    headers: headers).ConfigureAwait(false);
            }
        }
    }
}
