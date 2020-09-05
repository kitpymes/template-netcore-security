using Kitpymes.Core.Shared;
using Kitpymes.Core.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Kitypmes.Core.Security.Tests
{
    [TestClass]
	public class JsonWebTokenServiceTest
    {
        private readonly IServiceCollection services;

        private readonly IJsonWebTokenService jsonWebTokenService;

        public JsonWebTokenServiceTest()
        {
            services = new ServiceCollection();

            jsonWebTokenService = services.LoadJsonWebToken().GetJsonWebToken();
        }

        [TestInitialize]
        public void TestInitialize() { }

        [TestMethod]
		public void JsonWebToken_Encode_Decode_Claim()
		{
            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.UserData, expected.ToSerialize()) };

            (string Token, string Expire) = jsonWebTokenService.Encode(claims);

            var decoded = jsonWebTokenService.Decode(Token);

            if (decoded.TryGetValue(ClaimTypes.UserData, out var actual))
            {
                var actualToDeserialize = actual.ToString()?.ToDeserialize<FakeUser>();

                Assert.AreEqual(expected.ToSerialize(), actual);
                Assert.AreEqual(expected.Id, actualToDeserialize?.Id);
                Assert.AreEqual(expected.Email, actualToDeserialize?.Email);
                Assert.AreEqual(expected.Role, actualToDeserialize?.Role);
                Assert.AreEqual(expected.Permissions.First(), actualToDeserialize?.Permissions.First());
            }
        }
    }
}
