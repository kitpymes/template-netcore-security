using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class NameTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void IsNull_Passing_Valid_Value_Returns_False(string name)
        {
            var expected = false;

            var actual = Name.Create(name);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Value_Returns_True()
        {
            var expected = true;

            var actual = Name.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void Create_Passing_Valid_Values_Returns_Name(string name)
        {
            var actual = Name.Create(name);

            Assert.AreEqual(name, actual.Value);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string? name)
        {
            Assert.ThrowsException<ApplicationException>(() => Name.Create(name));
        }

        #endregion Create

        #region Change

        [TestMethod]
        public void Change_Passing_Valid_Values_Returns_Name()
        {
            var expected = "carlos";
            var actual = Name.Create("pepe");

            actual.Change(expected);

            Assert.AreEqual(expected, actual.Value);
        }

        #endregion Change

        #region ToString

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void ToString_Passing_Valid_Values_Returns_Name(string name)
        {
            var expected = name;
            var result = Name.Create(name);

            var actual = result.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToString

        #region Equals

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void Equals_Passing_Valid_Values_Returns_True(string name)
        {
            var left = Name.Create(name);
            var right = Name.Create(name);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void Equals_Passing_Valid_Distinct_Values_Returns_False(string name)
        {
            var left = Name.Create(name);
            var right = Name.Create("pepe");

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string name)
        {
            var left = Name.Create(name);
            var right = Name.Create(name);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void OperatorEqual_Passing_Valid_Distinct_Values_Returns_False(string name)
        {
            var left = Name.Create(name);
            var right = Name.Create("carlos");

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string name)
        {
            var left = Name.Create(name);
            var right = Name.Create(name);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("España")]
        [DataRow("Españá")]
        [DataRow("Españà")]
        [DataRow("Españä")]
        [DataRow("Españâ")]
        [DataRow("Españã")]
        [DataRow("Españç")]
        [DataRow("Españħ")]
        [DataRow("Esp añâ")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string name)
        {
            var left = Name.Create(name);
            var right = Name.Create("carlos");

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals

        #region ToNormalize

        [DataTestMethod]
        [DataRow("España", "Espana")]
        [DataRow("Españá", "Espana")]
        [DataRow("Españà", "Espana")]
        [DataRow("Españä", "Espana")]
        [DataRow("Españâ", "Espana")]
        [DataRow("Españã", "Espana")]
        [DataRow("Españç", "Espanc")]
        [DataRow("Españħ", "Espanh")]
        [DataRow("Esp añâ", "Esp ana")]
        public void ToNormalize_Passing_Valid_Values_Returns_Name_String_Format(string value, string expected)
        {
            var result = Name.Create(value);

            var actual = result.ToNormalize();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToNormalize
    }
}
