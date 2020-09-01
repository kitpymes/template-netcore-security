using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class StringIdTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("Tenerife")]
        [DataRow("Espania")]
        public void IsNull_Passing_Valid_Value_Returns_False(string value)
        {
            var expected = false;

            var actual = StringId.Create(value);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Value_Returns_True()
        {
            var expected = true;

            var actual = StringId.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [TestMethod]
        public void Create_Default_Passing_Valid_Values_Returns_StringId()
        {
            var actual = StringId.Create();

            Assert.AreEqual(actual.ToString(), actual.Value);
        }

        [DataTestMethod]
        [DataRow("Tenerife")]
        [DataRow("Espania")]
        public void Create_Passing_Valid_Values_Returns_StringId(string value)
        {
            var actual = StringId.Create(value);

            Assert.AreEqual(value, actual.Value);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string? value)
        {
            Assert.ThrowsException<ApplicationException>(() => StringId.Create(value));
        }

        #endregion Create

        #region ToString

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void ToString_Passing_Valid_Values_Returns_StringId(string value)
        {
            var expected = value;
            var result = StringId.Create(value);

            var actual = result.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToString

        #region Equals

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void Equals_Passing_Valid_Values_Returns_True(string value)
        {
            var left = StringId.Create(value);
            var right = StringId.Create(value);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void Equals_Passing_Valid_Distinct_Values_Returns_False(string value)
        {
            var left = StringId.Create(value);
            var right = StringId.Create("pepe");

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string value)
        {
            var left = StringId.Create(value);
            var right = StringId.Create(value);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void OperatorEqual_Passing_Valid_Distinct_Values_Returns_False(string value)
        {
            var left = StringId.Create(value);
            var right = StringId.Create("carlos");

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string value)
        {
            var left = StringId.Create(value);
            var right = StringId.Create(value);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana")]
        [DataRow("Sabadell")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string value)
        {
            var left = StringId.Create(value);
            var right = StringId.Create("carlos");

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals
    }
}
