using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitpymes.Core.Entities.Enumerations.Tests
{
    [TestClass]
    public class GenderTypeEnumTests
    {
        #region ToEnum String

        [TestMethod]
        public void ToEnum_Passing_Valid_String_Name_Enumeracion_Returns_Enumeracion()
        {
            var name = "Female";
            var expected = GenderTypeEnum.Female;

            var actual = GenderTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Valid_String_ShortName_Enumeracion_Returns_Enumeracion()
        {
            var name = "M";
            var expected = GenderTypeEnum.Male;

            var actual = GenderTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_String_Name_Enumeracion_Returns_Null()
        {
            var name = Guid.NewGuid().ToString();
            GenderTypeEnum? expected = null;

            var actual = GenderTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum String

        #region ToEnum Int

        [TestMethod]
        public void ToEnum_Passing_Valid_IntName_Enumeracion_Returns_Enumeracion()
        {
            var value = 2;
            var expected = GenderTypeEnum.Female;

            var actual = GenderTypeEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_Int_Name_Enumeracion_Returns_Null()
        {
            var value = new Random().Next();
            GenderTypeEnum? expected = null;

            var actual = GenderTypeEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum Int

        #region GetAll

        [TestMethod]
        public void GetAll_Passing_Nothing_Returns_Enumeracion_List()
        {
            var expected = 2;

            var actual = GenderTypeEnum.GetAll();

            Assert.AreEqual(expected, actual.Count());
            CollectionAssert.Contains(actual, GenderTypeEnum.Female);
            CollectionAssert.Contains(actual, GenderTypeEnum.Male);
        }

        #endregion GetAll

        #region Is

        [TestMethod]
        public void IsMale_Passing_Nothing_Returns_True()
        {
            var enumeracion = GenderTypeEnum.Male;
            var expected = true;

            var actual = enumeracion.IsMale;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsFemale_Passing_Nothing_Returns_True()
        {
            var enumeracion = GenderTypeEnum.Female;
            var expected = true;

            var actual = enumeracion.IsFemale;

            Assert.AreEqual(expected, actual);
        }

        #endregion Is
    }
}
