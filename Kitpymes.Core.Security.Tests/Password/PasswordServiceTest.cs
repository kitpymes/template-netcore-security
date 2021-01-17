using Kitpymes.Core.Security;
using Kitpymes.Core.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Kitypmes.Core.Security.Tests
{
    [TestClass]
	public class PasswordServiceTest
    {
        private readonly IServiceCollection services;

        private IPasswordService? passwordService;

        public PasswordServiceTest()
        {
            services = new ServiceCollection();
        }

        [TestInitialize]
        public void TestInitialize() { }

        #region DefaultSettings

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void DefaultSettings_Passing_Valid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(x => x.WithEnabled())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            var isValid = passwordService.Verify(plainPassword, hashPassword!);

            Assert.IsTrue(isValid);
        }

        [DataTestMethod]
        [DataRow("Passw")]
        [DataRow("1a_e")]
        public void DefaultSettings_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(x => x.WithEnabled())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
        }

        #endregion DefaultSettings

        #region AppSettings

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void AppSettings_Passing_Valid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFileName = "FakeAppSettings";

            var configuration = services
                .ToConfiguration(directoryPath, (jsonFileName, true, true))
                .ToService<IConfiguration>();

            var passwordSettings = configuration
                .GetSection(nameof(SecuritySettings))
                .GetSection(nameof(PasswordSettings))
                .Get<PasswordSettings>();

            passwordService = services.LoadSecurity(sec => sec.WithPassword(passwordSettings)).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            var isValid = passwordService.Verify(plainPassword, hashPassword!);

            Assert.IsTrue(isValid);
        }

        #endregion AppSettings

        #region RequireDigit

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequireDigit_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(options => options.WithEnabled().WithRequireDigit())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.Contains(PasswordErrorResult.RequireDigit));
        }

        #endregion RequireDigit

        #region RequiredMinLength

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequiredMinLength_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(options => options.WithEnabled().WithRequiredMinLength(30))).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.Contains(PasswordErrorResult.RequiredMinLength));
        }

        #endregion RequiredMinLength

        #region RequiredUniqueChars

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequiredUniqueChars_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(options => options.WithEnabled().WithRequiredUniqueChars())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.Contains(PasswordErrorResult.RequiredUniqueChars));
        }

        #endregion RequiredUniqueChars

        #region RequireEspecialCharacters

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequireEspecialCharacters_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(options => options.WithEnabled().WithRequireEspecialCharacters())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.Contains(PasswordErrorResult.RequireEspecialChars));
        }

        #endregion RequireEspecialCharacters

        #region RequireLowercase

        [DataTestMethod]
        [DataRow("PASSWORD")]
        public void RequireLowercase_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(options => options.WithEnabled().WithRequireLowercase())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.Contains(PasswordErrorResult.RequireLowercase));
        }

        #endregion RequireLowercase

        #region RequireUppercase

        [DataTestMethod]
        [DataRow("password")]
        public void RequireUppercase_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadSecurity(sec => sec.WithPassword(options => options.WithEnabled().WithRequireUppercase())).GetPassword();

            var (hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.Contains(PasswordErrorResult.RequireUppercase));
        }

        #endregion RequireUppercase
    }
}