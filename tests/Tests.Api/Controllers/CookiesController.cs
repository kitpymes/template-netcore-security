using Kitpymes.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System;

namespace Tests.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CookiesController : ControllerBase
    {
        private ICookiesService _cookiesService;

        public CookiesController(ICookiesService cookiesService)
        {
            _cookiesService = cookiesService;
        }

        [HttpPost("login")]
        public async Task<IResult> Login()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, "Carlos"),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim("LastUpdated", "LastUpdated"),
            };

            await _cookiesService.SignInAsync(claims, options => 
            {
                options.IsPersistent = false;
            });

            return Result.Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = "Cookies")]
        [HttpGet("values")]
        public IResult Get() 
        => Result<string[]>.Ok(new[] { "value1", "value2" });
    }
}
