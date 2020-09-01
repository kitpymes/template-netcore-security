using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitpymes.Core.Entities.Enumerations.Tests
{
    [TestClass]
    public class LogTypeEnumTests
    {
        #region ToEnum String

        [TestMethod]
        public void ToEnum_Passing_Valid_String_Name_Enumeracion_Returns_Enumeracion()
        {
            var name = "Changed";
            var expected = LogTypeEnum.Changed;

            var actual = LogTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_String_Name_Enumeracion_Returns_Null()
        {
            var name = Guid.NewGuid().ToString();
            LogTypeEnum? expected = null;

            var actual = LogTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum String

        #region ToEnum Int

        [TestMethod]
        public void ToEnum_Passing_Valid_IntName_Enumeracion_Returns_Enumeracion()
        {
            var value = 2;
            var expected = LogTypeEnum.Created;

            var actual = LogTypeEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_Int_Name_Enumeracion_Returns_Null()
        {
            var value = new Random().Next();
            LogTypeEnum? expected = null;

            var actual = LogTypeEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum Int

        #region GetAll

        [TestMethod]
        public void GetAll_Passing_Nothing_Returns_Enumeracion_List()
        {
            var expected = 5;

            var actual = LogTypeEnum.GetAll();

            Assert.AreEqual(expected, actual.Count());
            CollectionAssert.Contains(actual, LogTypeEnum.Changed);
            CollectionAssert.Contains(actual, LogTypeEnum.Created);
            CollectionAssert.Contains(actual, LogTypeEnum.Deleted);
            CollectionAssert.Contains(actual, LogTypeEnum.None);
            CollectionAssert.Contains(actual, LogTypeEnum.Updated);
        }

        #endregion GetAll
    }
}
