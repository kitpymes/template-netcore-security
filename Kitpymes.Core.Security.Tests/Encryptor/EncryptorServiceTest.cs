using Kitpymes.Core.Security;
using Kitpymes.Core.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Kitypmes.Core.Security.Tests
{
    [TestClass]
    public class EncryptorServiceTest
    {
        private readonly IServiceCollection services;

        private IEncryptorService? _encryptorService;

        public EncryptorServiceTest() => services = new ServiceCollection();

        [TestInitialize]
        public void TestInitialize() { }

        #region EncryptorOptions

        [TestMethod]
        public void EncryptorOptions_Encrypt_Passing_Value_String_Returns_Value_Encrypted()
        {
            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(x => x
               .WithEnabled()
               .WithApplicationName(nameof(EncryptorServiceTest))
               .WithPersistKeysToFileSystems("Fakes/PersistKeysToFileSystems"))).GetEncryptor();

            var expected = Guid.NewGuid().ToString();

            var encrypt = _encryptorService.Encrypt(expected);

            var actual = _encryptorService.Decrypt(encrypt);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncryptorOptions_Encrypt_Passing_Value_String_WithLifetime_Returns_CryptographicException_Exprided()
        {
            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(x => x
               .WithEnabled()
               .WithApplicationName(nameof(EncryptorServiceTest))
               .WithPersistKeysToFileSystems("Fakes/PersistKeysToFileSystems"))).GetEncryptor();

            var expected = Guid.NewGuid().ToString();

            var encrypt = _encryptorService.Encrypt(expected, TimeSpan.FromSeconds(2));

            // 3 segundos para que expire.
            Thread.Sleep(3000);

            Assert.ThrowsException<CryptographicException>(() => _encryptorService.Decrypt(encrypt));
        }

        [TestMethod]
        public void EncryptorOptions_Encrypt_Passing_Value_Object_Returns_Object_Encrypted()
        {
            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(x => x
              .WithEnabled()
              .WithApplicationName(nameof(EncryptorServiceTest))
              .WithPersistKeysToFileSystems("Fakes/PersistKeysToFileSystems"))).GetEncryptor();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Role = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Permissions = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var encrypt = _encryptorService.Encrypt(expected);

            var actual = _encryptorService.Decrypt<FakeUser>(encrypt);

            Assert.AreEqual(expected.ToSerialize(), actual.ToSerialize());
        }

        #endregion EncryptorOptions

        #region EncryptorSettings

        [TestMethod]
        public void EncryptorSettings_Encrypt_Passing_Value_String_Returns_Value_Encrypted()
        {
            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(new EncryptorSettings {
                Enabled = true,
                ApplicationName = nameof(EncryptorServiceTest),
                KeyLifetimeFromDays = 8,
                PersistKeysToFileSystem = "Fakes/PersistKeysToFileSystems"
            })).GetEncryptor();

            var expected = Guid.NewGuid().ToString();

            var encrypt = _encryptorService.Encrypt(expected);

            var actual = _encryptorService.Decrypt(encrypt);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncryptorSettings_Encrypt_Passing_Value_Object_Returns_Object_Encrypted()
        {
            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(new EncryptorSettings
            {
                Enabled = true,
                ApplicationName = nameof(EncryptorServiceTest),
                KeyLifetimeFromDays = 8,
                PersistKeysToFileSystem = "Fakes/PersistKeysToFileSystems"
            })).GetEncryptor();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Role = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Permissions = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var encrypt = _encryptorService.Encrypt(expected);

            var actual = _encryptorService.Decrypt<FakeUser>(encrypt);

            Assert.AreEqual(expected.ToSerialize(), actual.ToSerialize());
        }

        #endregion EncryptorSettings

        #region AppSettings

        [TestMethod]
        public void AppSettings_Encrypt_Passing_Value_String_Returns_Value_Encrypted()
        {
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFileName = "FakeAppSettings";

            var configuration = services
                .ToConfiguration(directoryPath, (jsonFileName, true, true))
                .ToService<IConfiguration>();

            var settings = configuration
                .GetSection(nameof(SecuritySettings))
                .GetSection(nameof(EncryptorSettings)).Get<EncryptorSettings>();

            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(settings)).GetEncryptor();

            var expected = Guid.NewGuid().ToString();

            var encrypt = _encryptorService.Encrypt(expected);

            var actual = _encryptorService.Decrypt(encrypt);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppSettings_Encrypt_Passing_Value_Object_Returns_Object_Encrypted()
        {
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFileName = "FakeAppSettings";

            var configuration = services
                .ToConfiguration(directoryPath, (jsonFileName, true, true))
                .ToService<IConfiguration>();

            var settings = configuration
                .GetSection(nameof(SecuritySettings))
                .GetSection(nameof(EncryptorSettings)).Get<EncryptorSettings>();

            _encryptorService = services.LoadSecurity(sec => sec.WithEncryptor(settings)).GetEncryptor();

            var expected = new FakeUser
            {
                Id = new Random().Next(),
                Role = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Permissions = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var encrypt = _encryptorService.Encrypt(expected);

            var actual = _encryptorService.Decrypt<FakeUser>(encrypt);

            Assert.AreEqual(expected.ToSerialize(), actual.ToSerialize());
        }

        #endregion AppSettings
    }
}