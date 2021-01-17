// -----------------------------------------------------------------------
// <copyright file="CustomCookieAuthenticationEvents.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Security
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    /*
       Clase de eventos CustomCookieAuthenticationEvents
       Contiene los eventos de la autenticación por cookies
    */

    /// <summary>
    /// Clase de eventos <c>CustomCookieAuthenticationEvents</c>.
    /// Contiene los eventos de la autenticación por cookies.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los eventos de la autenticación por cookies.</para>
    /// </remarks>
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CustomCookieAuthenticationEvents"/>.
        /// </summary>
        /// <param name="httpContextAccessor">Servicio para tener acceso al http context.</param>
        public CustomCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var cookiesService = context.HttpContext.RequestServices.GetRequiredService<ICookiesService>();

            var lastChanged = context.Principal?.FindFirstValue("LastUpdated");

            var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            // var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            // || !userRepository.ValidateLastChanged(userId, lastChanged))
            if (string.IsNullOrEmpty(lastChanged))
            {
                context.RejectPrincipal();
                await cookiesService.SignOutAsync();
            }
        }

        ///// <inheritdoc/>
        // public override async Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        // {
        //    var binding = context.HttpContext.Features.Get<Microsoft.AspNetCore.Http.Features.ITlsTokenBindingFeature>()?.GetProvidedTokenBindingId();
        //    var tlsTokenBinding = binding == null ? null : Convert.ToBase64String(binding);
        //    var cookie = context.Options.CookieManager.GetRequestCookie(context.HttpContext, context.Options.Cookie.Name);
        //    if (cookie != null)
        //    {
        //        var ticket = context.Options.TicketDataFormat.Unprotect(cookie, tlsTokenBinding);

        // var expiresUtc = ticket.Properties.ExpiresUtc;
        //        var currentUtc = DateTime.UtcNow;
        //        if (expiresUtc != null && expiresUtc.Value < currentUtc)
        //        {
        //            context.RedirectUri += "&p1=yourparameter";
        //        }
        //    }

        // context.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        // context.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET");

        // context.HttpContext.Response.Redirect(context.RedirectUri);

        // await Task.CompletedTask;
        // }

        /// <inheritdoc/>
        public override async Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> redirectContext)
        {
            var username = httpContextAccessor.HttpContext?.User?.Identity?.Name;

            var optionalData = new Dictionary<string, IList<string>>();
            optionalData.AddOrUpdate("AccessDeniedDateTime", DateTime.Now.ToLongDateString());

            if (!username.ToIsNullOrEmpty())
            {
                optionalData.AddOrUpdate("Username", username);
            }

            await Task.CompletedTask;

            throw new UnauthorizedAccessException(httpContextAccessor.HttpContext?.ToDetails(optionalData));
        }
    }
}
