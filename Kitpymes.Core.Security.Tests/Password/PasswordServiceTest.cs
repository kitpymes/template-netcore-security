using Kitpymes.Core.Security;
using Kitpymes.Core.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
            passwordService = services.LoadPassword().GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            var isValid = passwordService.Verify(plainPassword, hashPassword!);

            Assert.IsTrue(isValid);
        }

        [DataTestMethod]
        [DataRow("Passw")]
        [DataRow("1a_e")]
        public void DefaultSettings_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadPassword().GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
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

            passwordService = services.LoadPassword(passwordSettings).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

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
            passwordService = services.LoadPassword(options => options.WithRequireDigit()).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
            Assert.IsTrue(errors!.Contains(PasswordResult.RequireDigit));
        }

        #endregion RequireDigit

        #region RequiredMinLength

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequiredMinLength_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadPassword(options => options.WithRequiredMinLength(30)).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
            Assert.IsTrue(errors!.Contains(PasswordResult.RequiredMinLength));
        }

        #endregion RequiredMinLength

        #region RequiredUniqueChars

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequiredUniqueChars_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadPassword(options => options.WithRequiredUniqueChars()).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
            Assert.IsTrue(errors!.Contains(PasswordResult.RequiredUniqueChars));
        }

        #endregion RequiredUniqueChars

        #region RequireEspecialCharacters

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(PasswordServiceTest))]
        public void RequireEspecialCharacters_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadPassword(options => options.WithRequireEspecialCharacters()).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
            Assert.IsTrue(errors!.Contains(PasswordResult.RequireEspecialChars));
        }

        #endregion RequireEspecialCharacters

        #region RequireLowercase

        [DataTestMethod]
        [DataRow("PASSWORD")]
        public void RequireLowercase_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadPassword(options => options.WithRequireLowercase()).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
            Assert.IsTrue(errors!.Contains(PasswordResult.RequireLowercase));
        }

        #endregion RequireLowercase

        #region RequireUppercase

        [DataTestMethod]
        [DataRow("password")]
        public void RequireUppercase_Passing_Invalid_Value_Returns_HasErrorsAndErrors(string plainPassword)
        {
            passwordService = services.LoadPassword(options => options.WithRequireUppercase()).GetPassword();

            var (hasErrors, hashPassword, errors) = passwordService.Create(plainPassword);

            Assert.IsTrue(hasErrors);
            Assert.IsTrue(errors!.Contains(PasswordResult.RequireUppercase));
        }

        #endregion RequireUppercase
    }
}