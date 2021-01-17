using Kitpymes.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Kitpymes.Core.Shared.Util;

namespace Tests.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsonWebTokenController : ControllerBase
    {
        private IJsonWebTokenService _jsonWebTokenService;

        public JsonWebTokenController(IJsonWebTokenService jsonWebTokenService)
        {
            _jsonWebTokenService = jsonWebTokenService;
        }

        [HttpPost("login")]
        public IResult Login()
        {
            var jsonWebTokenResult = _jsonWebTokenService
                .Create(new List<Claim> { new Claim(ClaimTypes.Name, "Carlos"), new Claim(ClaimTypes.Email, "carlos@gmail.com") });

            return Result<CreateJsonWebTokenResult>.Ok(jsonWebTokenResult);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("values")]
        public IResult Get() 
        => Result<string[]>.Ok(new[] { "value1", "value2" });
    }
}
