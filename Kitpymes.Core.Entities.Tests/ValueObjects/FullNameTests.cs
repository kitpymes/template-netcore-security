using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class FullNameTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("Tenerife", "San Bartoleme De Tirajana", "Las Palmas")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void IsNull_Passing_Valid_Values_Returns_False(string firstName, string lastName, string middleName)
        {
            var expected = false;

            var actual = FullName.Create(firstName, lastName, middleName);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Value_Returns_True()
        {
            var expected = true;

            var actual = FullName.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void Create_Passing_Valid_Values_Returns_FullName(string firstName, string lastName, string middleName)
        {
            var actual = FullName.Create(firstName, lastName, middleName);

            Assert.AreEqual(firstName, actual.FirstName);
            Assert.AreEqual(lastName, actual.LastName);
            Assert.AreEqual(middleName, actual.MiddleName);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", null, "Tenerife")]
        [DataRow(null, "Barcelona", "España")]
        [DataRow("Sabadell", "", "España")]
        [DataRow("", "Sabadell", "España")]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string firstName, string lastName, string middleName)
        {
            Assert.ThrowsException<ApplicationException>(() => FullName.Create(firstName, lastName, middleName));
        }

        #endregion Create

        #region Change

        [TestMethod]
        public void Change_Passing_Valid_Values_Returns_FullName()
        {
            var expected = "carlos";
            var actual = FullName.Create("pepe", "marcos", "adrian");

            actual
                .ChangeFirstName(expected)
                .ChangeLastName(expected)
                .ChangeMiddleName(expected);

            Assert.AreEqual(expected, actual.FirstName);
            Assert.AreEqual(expected, actual.LastName);
            Assert.AreEqual(expected, actual.MiddleName);
        }

        #endregion Change

        #region ToString

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas")]
        [DataRow("Sabadell", "Barcelona")]
        public void ToString_Passing_Valid_Values_Returns_Value_String_Format(string firstName, string lastName)
        {
            var expected = $"{firstName} {lastName}";
            var result = FullName.Create(firstName, lastName);

            var actual = result.ToString();

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void ToFullString_Passing_Valid_Values_Returns_Value_String_Format(string firstName, string lastName, string middleName)
        {
            var expected = $"{firstName} {middleName}, {lastName}";
            var result = FullName.Create(firstName, lastName, middleName);

            var actual = result.ToFullString();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToString

        #region Equals

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void Equals_Passing_Valid_Values_Returns_True(string firstName, string lastName, string middleName)
        {
            var left = FullName.Create(firstName, lastName, middleName);
            var right = FullName.Create(firstName, lastName, middleName);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void Equals_Passing_Valid_Distinct_Values_Returns_False(string firstName, string lastName, string middleName)
        {
            var left = FullName.Create(firstName, lastName, middleName);
            var right = FullName.Create("Españá", "Españà", "Españä");

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string firstName, string lastName, string middleName)
        {
            var left = FullName.Create(firstName, lastName, middleName);
            var right = FullName.Create(firstName, lastName, middleName);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void OperatorEqual_Passing_Valid_Distinct_Values_Returns_False(string firstName, string lastName, string middleName)
        {
            var left = FullName.Create(firstName, lastName, middleName);
            var right = FullName.Create("Españá", "Españà", "Españä");

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string firstName, string lastName, string middleName)
        {
            var left = FullName.Create(firstName, lastName, middleName);
            var right = FullName.Create(firstName, lastName, middleName);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Sabadell", "Barcelona", "España")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string firstName, string lastName, string middleName)
        {
            var left = FullName.Create(firstName, lastName, middleName);
            var right = FullName.Create("Españá", "Españà", "Españä");

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals
    }
}
