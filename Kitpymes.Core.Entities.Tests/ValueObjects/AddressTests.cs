using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class AddressTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void IsNull_Passing_Valid_Address_Returns_False(string street, int number, string postalCode, string city, string state, string country)
        {
            var expected = false;

            var actual = Address.Create(street, number, postalCode, city, state, country);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Address_Returns_True()
        {
            var expected = true;

            var actual = Address.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void Create_Passing_Valid_Values_Returns_Address(string street, int number, string postalCode, string city, string state, string country)
        {
            var actual = Address.Create(street, number, postalCode, city, state, country);

            Assert.AreEqual(street, actual.Street);
            Assert.AreEqual(number, actual.Number);
            Assert.AreEqual(postalCode, actual.PostalCode);
            Assert.AreEqual(city, actual.City);
            Assert.AreEqual(state, actual.State);
            Assert.AreEqual(country, actual.Country);
        }

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", null)]
        [DataRow(null, 789, "DF7956", "Sabadell", "Barcelona", "España")]
        [DataRow("Tenerife", -10, "DF7956", "Sabadell", "Barcelona", "España")]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string street, int number, string postalCode, string city, string state, string country)
        {
            Assert.ThrowsException<ApplicationException>(() => Address.Create(street, number, postalCode, city, state, country));
        }

        #endregion Create

        #region Change

        [TestMethod]
        public void Change_Passing_Valid_Values_Returns_Address()
        {
            var expected = Guid.NewGuid().ToString();
            var expectedNumber = new Random().Next(11, 20);
            var actual = Address.Create(Guid.NewGuid().ToString(), new Random().Next(1, 10), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            actual
                .ChangeCity(expected)
                .ChangeCountry(expected)
                .ChangeNumber(expectedNumber)
                .ChangeState(expected)
                .ChangeStreet(expected)
                .ChangePostalCode(expected);

            Assert.AreEqual(expected, actual.Street);
            Assert.AreEqual(expectedNumber, actual.Number);
            Assert.AreEqual(expected, actual.PostalCode);
            Assert.AreEqual(expected, actual.City);
            Assert.AreEqual(expected, actual.State);
            Assert.AreEqual(expected, actual.Country);
        }

        #endregion Change

        #region ToString

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void ToString_Passing_Valid_Values_Returns_Address_String_Format(string street, int number, string postalCode, string city, string state, string country)
        {
            var expected = $"{street} {number}, {postalCode} - {city} {state}, {country}";
            var address = Address.Create(street, number, postalCode, city, state, country);

            var actual = address.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToString

        #region Equals

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void Equals_Passing_Valid_Values_Returns_True(string street, int number, string postalCode, string city, string state, string country)
        {
            var left = Address.Create(street, number, postalCode, city, state, country);
            var right = Address.Create(street, number, postalCode, city, state, country);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void Equals_Passing_Valid_Values_Returns_False(string street, int number, string postalCode, string city, string state, string country)
        {
            var left = Address.Create(Guid.NewGuid().ToString(), number, postalCode, city, state, country);
            var right = Address.Create(street, 567777, postalCode, city, state, country);

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string street, int number, string postalCode, string city, string state, string country)
        {
            var left = Address.Create(street, number, postalCode, city, state, country);
            var right = Address.Create(street, number, postalCode, city, state, country);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void OperatorEqual_Passing_Valid_Values_Returns_False(string street, int number, string postalCode, string city, string state, string country)
        {
            var left = Address.Create(Guid.NewGuid().ToString(), number, postalCode, city, state, country);
            var right = Address.Create(street, 567777, postalCode, city, state, country);

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string street, int number, string postalCode, string city, string state, string country)
        {
            var left = Address.Create(street, number, postalCode, city, state, country);
            var right = Address.Create(street, number, postalCode, city, state, country);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("Tenerife", 23, "34567", "San Bartoleme De Tirajana", "Las Palmas", "España")]
        [DataRow("Pader", 789, "DF7956", "Sabadell", "Barcelona", "España")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string street, int number, string postalCode, string city, string state, string country)
        {
            var left = Address.Create(Guid.NewGuid().ToString(), number, postalCode, city, state, country);
            var right = Address.Create(street, 567777, postalCode, city, state, country);

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals
    }
}
