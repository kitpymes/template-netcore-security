using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class SubdomainTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void IsNull_Passing_Valid_Value_Returns_False(string value)
        {
            var expected = false;

            var actual = Subdomain.Create(value);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Value_Returns_True()
        {
            var expected = true;

            var actual = Subdomain.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [DataTestMethod]
        [DataRow("google")]
        [DataRow("www.hackerone1-com")]
        [DataRow("stage_hackerone.com")]
        public void Create_Passing_Valid_Values_Returns_Subdomain(string value)
        {
            var actual = Subdomain.Create(value);

            Assert.AreEqual(value, actual.Value);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("___dasfs$·%%$%$$·%$")]
        [DataRow("..¬¬¬sdffdsfdssd.dd")]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string? value)
        {
            Assert.ThrowsException<ApplicationException>(() => Subdomain.Create(value));
        }

        #endregion Create

        #region Change

        [TestMethod]
        public void Change_Passing_Valid_Values_Returns_Subdomain()
        {
            var expected = "app.hackerone.com";
            var actual = Subdomain.Create("stage.hackerone.com");

            actual.Change(expected);

            Assert.AreEqual(expected, actual.Value);
        }

        #endregion Change

        #region ToString

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void ToString_Passing_Valid_Values_Returns_Subdomain(string value)
        {
            var expected = value;
            var result = Subdomain.Create(value);

            var actual = result.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToString

        #region Equals

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void Equals_Passing_Valid_Values_Returns_True(string value)
        {
            var left = Subdomain.Create(value);
            var right = Subdomain.Create(value);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void Equals_Passing_Valid_Distinct_Values_Returns_False(string value)
        {
            var left = Subdomain.Create(value);
            var right = Subdomain.Create("pepe.dasdsad.rr");

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string value)
        {
            var left = Subdomain.Create(value);
            var right = Subdomain.Create(value);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void OperatorEqual_Passing_Valid_Distinct_Values_Returns_False(string value)
        {
            var left = Subdomain.Create(value);
            var right = Subdomain.Create("carlpopoos.ffff.ff");

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string value)
        {
            var left = Subdomain.Create(value);
            var right = Subdomain.Create(value);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("app.hackerone1.com")]
        [DataRow("www.hackerone-com")]
        [DataRow("stage_hackerone.com")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string value)
        {
            var left = Subdomain.Create(value);
            var right = Subdomain.Create("carlos.fffff.ff");

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals
    }
}
