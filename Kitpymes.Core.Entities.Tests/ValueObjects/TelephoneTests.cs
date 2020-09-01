using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class TelephoneTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void IsNull_Passing_Valid_Values_Returns_False(string prefix, long number)
        {
            var expected = false;

            var actual = Telephone.Create(prefix, number);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Value_Returns_True()
        {
            var expected = true;

            var actual = Telephone.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void Create_Passing_Valid_Values_Returns_Telephone(string prefix, long number)
        {
            var actual = Telephone.Create(prefix, number);

            Assert.AreEqual(prefix, actual.Prefix);
            Assert.AreEqual(number, actual.Number);
        }

        [DataTestMethod]
        [DataRow(null, 43543543543)]
        [DataRow("", 8768768768)]
        [DataRow("4324324", 0)]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string? prefix, long number)
        {
            Assert.ThrowsException<ApplicationException>(() => Telephone.Create(prefix, number));
        }

        #endregion Create

        #region Change

        [TestMethod]
        public void Change_Passing_Valid_Values_Returns_Telephone()
        {
            var prefixExpected = "343434F";
            var numberExpected = 9393933333;
            var actual = Telephone.Create("123", 787687675465468);

            actual.ChangePrefix(prefixExpected).ChangeNumber(numberExpected);

            Assert.AreEqual(prefixExpected, actual.Prefix);
            Assert.AreEqual(numberExpected, actual.Number);
        }

        #endregion Change

        #region ToString

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void ToString_Passing_Valid_Values_Returns_Value_String_Format(string prefix, long number)
        {
            var expected = $"{prefix} {number}";
            var result = Telephone.Create(prefix, number);

            var actual = result.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToString

        #region Equals

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void Equals_Passing_Valid_Values_Returns_True(string prefix, long number)
        {
            var left = Telephone.Create(prefix, number);
            var right = Telephone.Create(prefix, number);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void Equals_Passing_Valid_Distinct_Values_Returns_False(string prefix, long number)
        {
            var left = Telephone.Create(prefix, number);
            var right = Telephone.Create("4344", 9999999999);

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string prefix, long number)
        {
            var left = Telephone.Create(prefix, number);
            var right = Telephone.Create(prefix, number);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void OperatorEqual_Passing_Valid_Distinct_Values_Returns_False(string prefix, long number)
        {
            var left = Telephone.Create(prefix, number);
            var right = Telephone.Create("4344", 9999999999);

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string prefix, long number)
        {
            var left = Telephone.Create(prefix, number);
            var right = Telephone.Create(prefix, number);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("456", 43543543543)]
        [DataRow("D45", 86554654)]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string prefix, long number)
        {
            var left = Telephone.Create(prefix, number);
            var right = Telephone.Create("4344", 9999999999);

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals
    }
}
