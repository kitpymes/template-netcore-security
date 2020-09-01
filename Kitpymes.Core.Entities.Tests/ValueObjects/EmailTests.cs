using Kitpymes.Core.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Entities.ValueObjects.Tests
{
    [TestClass]
    public class EmailTests
    {
        #region IsNull

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void IsNull_Passing_Valid_Value_Returns_False(string value)
        {
            var expected = false;

            var actual = Email.Create(value);

            Assert.AreEqual(expected, actual.IsNull);
        }

        [TestMethod]
        public void IsNull_Passing_Null_Value_Returns_True()
        {
            var expected = true;

            var actual = Email.Null;

            Assert.AreEqual(expected, actual.IsNull);
        }

        #endregion IsNull

        #region Create

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        [DataRow("ñandu@gmail.com")]
        [DataRow("ádsads@ggg.cc")]
        [DataRow("aaads@ggg.cc")]
        [DataRow("bb_ads@ggg.cc")]
        public void Create_Passing_Valid_Value_Returns_Email(string expected)
        {
            var actual = Email.Create(expected);

            Assert.AreEqual(expected, actual.Value);
        }

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.._ff")]
        [DataRow("34fsds@@sdd.f")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("34?fsds@sdd.yy")]
        public void Create_Passing_InvalidOrNull_Value_Returns_ApplicationException(string? value)
        {
            Assert.ThrowsException<ApplicationException>(() => Email.Create(value));
        }

        #endregion Create

        #region Change

        [DataTestMethod]
        [DataRow("aaa@aaa.aaa", "hhh@hhh.hhh")]
        [DataRow("bbbs@bbb.bbb", "ppp@ppp.ppp")]
        public void Change_Passing_Valid_Values_Returns_Email(string value, string expected)
        {
            var actual = Email.Create(value);

            actual.Change(expected);

            Assert.AreEqual(expected, actual.Value);
        }

        #endregion Change

        #region Equals

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void Equals_Passing_Valid_Values_Returns_True(string value)
        {
            var left = Email.Create(value);
            var right = Email.Create(value);

            var isEqual = left.Equals(right);

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void Equals_Passing_Valid_Distinct_Values_Returns_False(string value)
        {
            var left = Email.Create("jkjk@jk.jkh");
            var right = Email.Create(value);

            var isEqual = left.Equals(right);

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void OperatorEqual_Passing_Valid_Values_Returns_True(string value)
        {
            var left = Email.Create(value);
            var right = Email.Create(value);

            var isEqual = left == right;

            Assert.IsTrue(isEqual);
        }

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void OperatorEqual_Passing_Valid_Distinct_Values_Returns_False(string value)
        {
            var left = Email.Create("jkjk@jk.jkh");
            var right = Email.Create(value);

            var isEqual = left == right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_False(string value)
        {
            var left = Email.Create(value);
            var right = Email.Create(value);

            var isEqual = left != right;

            Assert.IsFalse(isEqual);
        }

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.ff")]
        [DataRow("34fsds@sdd.fgg")]
        public void OperatorNotEqual_Passing_Valid_Values_Returns_True(string value)
        {
            var left = Email.Create("jkjk@jk.jkh");
            var right = Email.Create(value);

            var isEqual = left != right;

            Assert.IsTrue(isEqual);
        }

        #endregion Equals

        #region ToNormalize

        [DataTestMethod]
        [DataRow("ñandu@gmail.com")]
        [DataRow("ádsads@ggg.cc")]
        [DataRow("aaads@ggg.cc")]
        [DataRow("bb_ads@ggg.cc")]
        public void ToNormalize_Passing_Valid_Values_Returns_Address_String_Format(string email)
        {
            var expected = email.ToReplaceSpecialChars("@");
            var result = Email.Create(email);

            var actual = result.ToNormalize();

            Assert.AreEqual(expected, actual);
        }

        #endregion ToNormalize
    }
}
