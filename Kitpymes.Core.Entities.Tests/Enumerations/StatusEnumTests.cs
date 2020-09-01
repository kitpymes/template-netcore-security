using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitpymes.Core.Entities.Enumerations.Tests
{
    [TestClass]
    public class StatusEnumTests
    {
        #region ToEnum String

        [TestMethod]
        public void ToEnum_Passing_Valid_String_Name_Returns_Enumeracion()
        {
            var name = "Active";
            var expected = StatusEnum.Active;

            var actual = StatusEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_String_Name_Returns_Null()
        {
            var name = Guid.NewGuid().ToString();
            StatusEnum? expected = null;

            var actual = StatusEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum String

        #region ToEnum Int

        [TestMethod]
        public void ToEnum_Passing_Valid_Int_value_Returns_Enumeracion()
        {
            var value = 0;
            var expected = StatusEnum.Inactive;

            var actual = StatusEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_Int_Value_Returns_Null()
        {
            var value = new Random().Next();
            StatusEnum? expected = null;

            var actual = StatusEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum Int

        #region ToEnum Bool

        [TestMethod]
        public void ToEnum_Passing_Valid_Bool_Value_Returns_Enumeracion()
        {
            var value = false;
            var expected = StatusEnum.Inactive;

            var actual = StatusEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Null_Bool_Value_Returns_Inactive_Status()
        {
            string? value = null;
            var expected = StatusEnum.Inactive;

            var actual = StatusEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum Bool

        #region ToBool

        [TestMethod]
        public void ToBool_Passing_Valid_String_Active_Value_Returns_True()
        {
            var value = "Active";
            var expected = true;

            var actual = StatusEnum.ToBool(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToBool_Passing_Valid_Status_Inactive_Returns_False()
        {
            var value = StatusEnum.Inactive;
            var expected = false;

            var actual = StatusEnum.ToBool(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToBool

        #region GetAll

        [TestMethod]
        public void GetAll_Passing_Nothing_Returns_Enumeracion_List()
        {
            var expected = 2;

            var actual = StatusEnum.GetAll();

            Assert.AreEqual(expected, actual.Count());
            CollectionAssert.Contains(actual, StatusEnum.Inactive);
            CollectionAssert.Contains(actual, StatusEnum.Active);
        }

        #endregion GetAll

        #region Is

        [TestMethod]
        public void IsActive_Passing_Nothing_Returns_True()
        {
            var enumeracion = StatusEnum.Active;
            var expected = true;

            var actual = enumeracion.IsActive;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsInactive_Passing_Nothing_Returns_True()
        {
            var enumeracion = StatusEnum.Inactive;
            var expected = true;

            var actual = enumeracion.IsInactive;

            Assert.AreEqual(expected, actual);
        }

        #endregion Is
    }
}
