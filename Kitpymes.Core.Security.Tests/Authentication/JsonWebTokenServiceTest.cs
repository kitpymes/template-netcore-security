using Kitpymes.Core.Shared;
using UTIL = Kitpymes.Core.Shared.Util;
using Kitpymes.Core.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Kitypmes.Core.Security.Tests
{
    [TestClass]
	public class JsonWebTokenServiceTest
    {
        private readonly IServiceCollection services;

        private IJsonWebTokenService? _jsonWebTokenService;

        public JsonWebTokenServiceTest()
        {
            services = new ServiceCollection();
        }

        [TestInitialize]
        public void TestInitialize() { }

        #region JsonWebTokenOptions

        [TestMethod]
        public void JsonWebTokenOptions_Passing_Empties_Settings_Returns_ApplicationException_GetAuthJsonWebToken()
        {
            Assert.ThrowsException<ApplicationException>(() => services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(x => { }))).GetAuthJsonWebToken());
        }

        [TestMethod]
        public void JsonWebTokenOptions_Passing_Enabled_Settings_Returns_ApplicationException()
        {
            Assert.ThrowsException<ApplicationException>(() => services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(x => x.WithEnabled()))));
        }

        [TestMethod]
        public void JsonWebTokenOptions_Passing_Empties_Claims_To_Service_Returns_ApplicationException()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            _jsonWebTokenService = services.LoadSecurity(sec => 
                sec.WithAuthentication(auth => 
                    auth.WithJsonWebToken(options => 
                        options.WithEnabled().WithPrivateKey(privateKey).WithPublicKey(publicKey))))
                            .GetAuthJsonWebToken();

            var claims = new List<Claim>();

            Assert.ThrowsException<ApplicationException>(() => _jsonWebTokenService.Create(claims));
        }

        [TestMethod] 
        public void JsonWebTokenOptions_Passing_Claims_To_Service_Returns_Values()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            _jsonWebTokenService = services.LoadSecurity(sec =>
                sec.WithAuthentication(auth =>
                    auth.WithJsonWebToken(options =>
                        options.WithEnabled().WithPrivateKey(privateKey).WithPublicKey(publicKey))))
                            .GetAuthJsonWebToken();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.UserData, expected.ToSerialize()) };

            var createResult = _jsonWebTokenService.Create(claims);

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            if (getResult.Claims.TryGetValue(ClaimTypes.UserData, out var actual))
            {
                var actualToDeserialize = actual.ToString()?.ToDeserialize<FakeUser>();

                Assert.AreEqual(expected.ToSerialize(), actual);
                Assert.AreEqual(expected.Id, actualToDeserialize?.Id);
                Assert.AreEqual(expected.Email, actualToDeserialize?.Email);
                Assert.AreEqual(expected.Role, actualToDeserialize?.Role);
                CollectionAssert.AreEqual(expected.Permissions.ToList(), actualToDeserialize?.Permissions?.ToList());
            }
        }

        [TestMethod]
        public void JsonWebTokenOptions_Passing_Claims_And_Headers_Returns_Values()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            _jsonWebTokenService = services.LoadSecurity(sec =>
                sec.WithAuthentication(auth =>
                    auth.WithJsonWebToken(options =>
                        options.WithEnabled().WithPrivateKey(privateKey).WithPublicKey(publicKey))))
                            .GetAuthJsonWebToken();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var jtiExpected = Guid.NewGuid().ToString();

            var claims = new List<Claim> { 
                new Claim(ClaimTypes.UserData, expected.ToSerialize()), 
                new Claim(JwtRegisteredClaimNames.Jti, jtiExpected) 
            };

            var headers = new Dictionary<string, object>() { 
                { nameof(expected.Email) , expected.Email } 
            };

            var createResult = _jsonWebTokenService.Create(claims);

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            if (getResult.Payload!.TryGetValue(ClaimTypes.UserData, out var actual))
            {
                var actualToDeserialize = actual.ToString()?.ToDeserialize<FakeUser>();

                Assert.AreEqual(expected.ToSerialize(), actual);
                Assert.AreEqual(expected.Id, actualToDeserialize?.Id);
                Assert.AreEqual(expected.Email, actualToDeserialize?.Email);
                Assert.AreEqual(expected.Role, actualToDeserialize?.Role);
                CollectionAssert.AreEqual(expected.Permissions.ToList(), actualToDeserialize?.Permissions?.ToList());
            }

            if (getResult.Header!.TryGetValue(JwtRegisteredClaimNames.Jti, out var jti))
            {
                Assert.AreEqual(jtiExpected, jti);
            }

            if (getResult.Header.TryGetValue(nameof(expected.Email), out var email))
            {
                Assert.AreEqual(expected.Email, email);
            }
        }

        [TestMethod]
        public void JsonWebTokenOptions_Passing_All_Values_Returns_Values()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();

            var audienceExpected = Guid.NewGuid().ToString();
            var issuerExpected = Guid.NewGuid().ToString();
            var privateKeyExpected = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKeyExpected = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var jtiExpected = Guid.NewGuid().ToString();
            var typeExpected = "JWT";
            var userExpected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            _jsonWebTokenService = services.LoadSecurity(sec =>
                sec.WithAuthentication(auth =>
                    auth.WithJsonWebToken(options =>
                        options
                            .WithEnabled()
                            .WithPrivateKey(privateKeyExpected)
                            .WithPublicKey(publicKeyExpected)
                            .WithAudience(audienceExpected)
                            .WithIssuer(issuerExpected))))
                                .GetAuthJsonWebToken();

            var headers = new Dictionary<string, IEnumerable<string>>();
            headers.AddOrUpdate(nameof(userExpected.Email), userExpected.Email);

            var createResult = _jsonWebTokenService.Create(
                claims: new List<Claim> {
                    new Claim(ClaimTypes.UserData, userExpected.ToSerialize()),
                    new Claim(JwtRegisteredClaimNames.Jti, jtiExpected)
                },
                headers
            );

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            Assert.AreEqual(issuerExpected, getResult.Issuer);
            Assert.IsTrue(getResult.Audiences?.Contains(audienceExpected));

            // Claims

            var claimUserActual = getResult.Claims.First(x => x.Key == ClaimTypes.UserData).Value;
            Assert.AreEqual(userExpected.ToSerialize(), claimUserActual);

            var claimJtiActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Jti).Value;
            Assert.AreEqual(jtiExpected, claimJtiActual);

            var claimIssActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Iss).Value;
            Assert.AreEqual(issuerExpected, claimIssActual);

            var claimAudActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Aud).Value;
            Assert.AreEqual(audienceExpected, claimAudActual);

            // Payload

            var payloadUserActual = getResult.Payload?.First(x => x.Key == ClaimTypes.UserData).Value;
            Assert.AreEqual(userExpected.ToSerialize(), payloadUserActual);

            var payloadJtiActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Jti).Value;
            Assert.AreEqual(jtiExpected, payloadJtiActual);

            var payloadIssActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Iss).Value;
            Assert.AreEqual(issuerExpected, payloadIssActual);

            var payloadAudActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Aud).Value;
            Assert.AreEqual(audienceExpected, payloadAudActual);

            // Header

            var headerTypActual = getResult.Header?.First(x => x.Key == JwtRegisteredClaimNames.Typ).Value;
            Assert.AreEqual(typeExpected, headerTypActual);

            var headerEmailActual = getResult.Header?.First(x => x.Key == nameof(userExpected.Email)).Value;
            Assert.IsNotNull(headerEmailActual);
        }

        #endregion JsonWebTokenOptions

        #region JsonWebTokenSettings

        [TestMethod]
        public void JsonWebTokenSettings_Passing_Empties_Settings_Returns_ApplicationException_GetAuthJsonWebToken()
        {
            Assert.ThrowsException<ApplicationException>(() => services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(new JsonWebTokenSettings()))).GetAuthJsonWebToken());
        }

        [TestMethod]
        public void JsonWebTokenSettings_Passing_Enabled_Settings_Returns_ApplicationException()
        {
            Assert.ThrowsException<ApplicationException>(() => services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(new JsonWebTokenSettings 
            {
                 Enabled = true
            }))));
        }

        [TestMethod]
        public void JsonWebTokenSettings_Passing_Empties_Claims_Returns_ApplicationException()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            _jsonWebTokenService = services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(new JsonWebTokenSettings
            {
                Enabled = true,
                PrivateKey = privateKey,
                PublicKey = publicKey
            })))
            .GetAuthJsonWebToken();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var claims = new List<Claim>();

            Assert.ThrowsException<ApplicationException>(() => _jsonWebTokenService.Create(claims));
        }

        [TestMethod]
        public void JsonWebTokenSettings_Passing_Claims_Returns_Values()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            _jsonWebTokenService = services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(new JsonWebTokenSettings
                {
                    Enabled = true,
                    PrivateKey = privateKey,
                    PublicKey = publicKey
                })))
                .GetAuthJsonWebToken();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.UserData, expected.ToSerialize()) };

            var createResult = _jsonWebTokenService.Create(claims);

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            if (getResult.Payload!.TryGetValue(ClaimTypes.UserData, out var actual))
            {
                var actualToDeserialize = actual.ToString()?.ToDeserialize<FakeUser>();

                Assert.AreEqual(expected.ToSerialize(), actual);
                Assert.AreEqual(expected.Id, actualToDeserialize?.Id);
                Assert.AreEqual(expected.Email, actualToDeserialize?.Email);
                Assert.AreEqual(expected.Role, actualToDeserialize?.Role);
                CollectionAssert.AreEqual(expected.Permissions.ToList(), actualToDeserialize?.Permissions?.ToList());
            }
        }

        [TestMethod]
        public void JsonWebTokenSettings_Passing_Claims_And_Headers_Returns_Values()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            _jsonWebTokenService = services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(new JsonWebTokenSettings
            {
                Enabled = true,
                PrivateKey = privateKey,
                PublicKey = publicKey
            })))
            .GetAuthJsonWebToken();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var jtiExpected = Guid.NewGuid().ToString();

            var claims = new List<Claim> {
                new Claim(ClaimTypes.UserData, expected.ToSerialize()),
                new Claim(JwtRegisteredClaimNames.Jti, jtiExpected)
            };

            var headers = new Dictionary<string, object>() {
                { nameof(expected.Email) , expected.Email }
            };

            var createResult = _jsonWebTokenService.Create(claims);

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            if (getResult.Payload!.TryGetValue(ClaimTypes.UserData, out var actual))
            {
                var actualToDeserialize = actual.ToString()?.ToDeserialize<FakeUser>();

                Assert.AreEqual(expected.ToSerialize(), actual);
                Assert.AreEqual(expected.Id, actualToDeserialize?.Id);
                Assert.AreEqual(expected.Email, actualToDeserialize?.Email);
                Assert.AreEqual(expected.Role, actualToDeserialize?.Role);
                CollectionAssert.AreEqual(expected.Permissions.ToList(), actualToDeserialize?.Permissions?.ToList());
            }

            if (getResult.Header!.TryGetValue(JwtRegisteredClaimNames.Jti, out var jti))
            {
                Assert.AreEqual(jtiExpected, jti);
            }

            if (getResult.Header.TryGetValue(nameof(expected.Email), out var email))
            {
                Assert.AreEqual(expected.Email, email);
            }
        }

        [TestMethod]
        public void JsonWebTokenSettings_Passing_All_Values_Returns_Values()
        {
            // Para crear claves RSA online: https://dotnetfiddle.net/EgxEwu
            using RSA rsa = RSA.Create();

            var audienceExpected = Guid.NewGuid().ToString();
            var issuerExpected = Guid.NewGuid().ToString();
            var privateKeyExpected = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKeyExpected = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var jtiExpected = Guid.NewGuid().ToString();
            var typeExpected = "JWT";
            var userExpected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            _jsonWebTokenService = services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(new JsonWebTokenSettings
            {
                Enabled = true,
                PrivateKey = privateKeyExpected,
                PublicKey = publicKeyExpected,
                Audience = audienceExpected,
                Issuer = issuerExpected
            })))
            .GetAuthJsonWebToken();

            var headers = new Dictionary<string, IEnumerable<string>>();
            headers.AddOrUpdate(nameof(userExpected.Email), userExpected.Email);

            var createResult = _jsonWebTokenService.Create(
                claims: new List<Claim> {
                    new Claim(ClaimTypes.UserData, userExpected.ToSerialize()),
                    new Claim(JwtRegisteredClaimNames.Jti, jtiExpected)
                },
                headers
            );

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            Assert.AreEqual(issuerExpected, getResult.Issuer);
            Assert.IsTrue(getResult.Audiences?.Contains(audienceExpected));

            // Claims

            var claimUserActual = getResult.Claims.First(x => x.Key == ClaimTypes.UserData).Value;
            Assert.AreEqual(userExpected.ToSerialize(), claimUserActual);

            var claimJtiActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Jti).Value;
            Assert.AreEqual(jtiExpected, claimJtiActual);

            var claimIssActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Iss).Value;
            Assert.AreEqual(issuerExpected, claimIssActual);

            var claimAudActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Aud).Value;
            Assert.AreEqual(audienceExpected, claimAudActual);

            // Payload

            var payloadUserActual = getResult.Payload?.First(x => x.Key == ClaimTypes.UserData).Value;
            Assert.AreEqual(userExpected.ToSerialize(), payloadUserActual);

            var payloadJtiActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Jti).Value;
            Assert.AreEqual(jtiExpected, payloadJtiActual);

            var payloadIssActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Iss).Value;
            Assert.AreEqual(issuerExpected, payloadIssActual);

            var payloadAudActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Aud).Value;
            Assert.AreEqual(audienceExpected, payloadAudActual);

            // Header

            var headerTypActual = getResult.Header?.First(x => x.Key == JwtRegisteredClaimNames.Typ).Value;
            Assert.AreEqual(typeExpected, headerTypActual);

            var headerEmailActual = getResult.Header?.First(x => x.Key == nameof(userExpected.Email)).Value;
            Assert.IsNotNull(headerEmailActual);
        }

        #endregion JsonWebTokenSettings

        #region AppSettings

        [TestMethod]
        public void AppSettings_Passing_All_Values_Returns_Values()
        {
            var typeExpected = "JWT";
            var userExpected = new FakeUser
            {
                Id = new Random().Next(),
                Email = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFileName = "FakeAppSettings";

            var configuration = services
                .ToConfiguration(directoryPath, (jsonFileName, true, true))
                .ToService<IConfiguration>();

            var settings = configuration?
                .GetSection(nameof(SecuritySettings))
                .GetSection(nameof(AuthenticationSettings))
                .GetSection(nameof(JsonWebTokenSettings)).Get<JsonWebTokenSettings>();

            _jsonWebTokenService = services.LoadSecurity(sec => sec.WithAuthentication(auth => auth.WithJsonWebToken(settings!))).GetAuthJsonWebToken();

            var headers = new Dictionary<string, IEnumerable<string>>();
            headers.AddOrUpdate(nameof(userExpected.Email), userExpected.Email);

            var createResult = _jsonWebTokenService.Create(
                claims: new List<Claim> {
                    new Claim(ClaimTypes.UserData, userExpected.ToSerialize())
                },
                headers
            );

            Assert.IsNotNull(createResult);

            var getResult = _jsonWebTokenService.Get(createResult.Token);

            Assert.IsNotNull(getResult);

            // Claims

            var claimUserActual = getResult.Claims.First(x => x.Key == ClaimTypes.UserData).Value;
            Assert.AreEqual(userExpected.ToSerialize(), claimUserActual);

            var claimIssActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Iss).Value;
            Assert.AreEqual(getResult.Issuer, claimIssActual);

            var claimAudActual = getResult.Claims.First(x => x.Key == JwtRegisteredClaimNames.Aud).Value;
            Assert.IsTrue(getResult.Audiences?.Contains(claimAudActual));

            // Payload

            var payloadUserActual = getResult.Payload?.First(x => x.Key == ClaimTypes.UserData).Value;
            Assert.AreEqual(userExpected.ToSerialize(), payloadUserActual);

            var payloadIssActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Iss).Value;
            Assert.AreEqual(getResult.Issuer, payloadIssActual);

            var payloadAudActual = getResult.Payload?.First(x => x.Key == JwtRegisteredClaimNames.Aud).Value;
            Assert.IsTrue(getResult.Audiences?.Contains(payloadAudActual));

            // Header

            var headerTypActual = getResult.Header?.First(x => x.Key == JwtRegisteredClaimNames.Typ).Value;
            Assert.AreEqual(typeExpected, headerTypActual);

            var headerEmailActual = getResult.Header?.First(x => x.Key == nameof(userExpected.Email)).Value;
            Assert.IsNotNull(headerEmailActual);
        }

        #endregion AppSettings
    }
}
