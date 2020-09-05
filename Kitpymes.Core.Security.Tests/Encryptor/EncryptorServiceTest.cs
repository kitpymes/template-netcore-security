using Kitpymes.Core.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitypmes.Core.Security.Tests
{
    [TestClass]
	public class EncryptorServiceTest
    {
        private readonly IServiceCollection services;

        private readonly IEncryptorService encryptorService;

        public EncryptorServiceTest()
        {
            services = new ServiceCollection();

            encryptorService = services.LoadEncryptor().GetEncryptor();
        }

        [TestInitialize]
        public void TestInitialize() { }

		[TestMethod]
		public void Encrypt_Passing_Value_String_Returns_Value_Encrypted()
		{
            var expected = Guid.NewGuid().ToString();

            var encrypt = encryptorService.Encrypt(expected);

            var actual = encryptorService.Decrypt(encrypt);

            Assert.AreEqual(expected, actual);
		}

        [TestMethod]
        public void Encrypt_Passing_Value_Object_Returns_Object_Encrypted()
        {
            var expected = new FakeUser
            {
                Id = new Random().Next(),

                Role = Guid.NewGuid().ToString(),

                Email = Guid.NewGuid().ToString(),

                Permissions = new [] { Guid.NewGuid().ToString() , Guid.NewGuid().ToString() , Guid.NewGuid().ToString() }
            };

            var encrypt = encryptorService.Encrypt(expected);

            var actual = encryptorService.Decrypt<FakeUser>(encrypt);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Permissions.First(), actual.Permissions.First());
        }
    }
}
