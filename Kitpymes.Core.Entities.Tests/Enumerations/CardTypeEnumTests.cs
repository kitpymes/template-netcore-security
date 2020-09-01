using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitpymes.Core.Entities.Enumerations.Tests
{
    [TestClass]
    public class CardTypeEnumTests
    {
        #region ToEnum String

        [TestMethod]
        public void ToEnum_Passing_Valid_String_Name_Enumeracion_Returns_Enumeracion()
        {
            var name = "American Express";
            var expected = CardTypeEnum.Amex;

            var actual = CardTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Valid_String_ShortName_Enumeracion_Returns_Enumeracion()
        {
            var name = "Visa";
            var expected = CardTypeEnum.Visa;

            var actual = CardTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_String_Name_Enumeracion_Returns_Null()
        {
            var name = Guid.NewGuid().ToString();
            CardTypeEnum? expected = null;

            var actual = CardTypeEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum String

        #region ToEnum Int

        [TestMethod]
        public void ToEnum_Passing_Valid_IntName_Enumeracion_Returns_Enumeracion()
        {
            var value = 3;
            var expected = CardTypeEnum.MasterCard;

            var actual = CardTypeEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_Int_Name_Enumeracion_Returns_Null()
        {
            var value = new Random().Next();
            CardTypeEnum? expected = null;

            var actual = CardTypeEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum Int

        #region GetAll

        [TestMethod]
        public void GetAll_Passing_Nothing_Returns_Enumeracion_List()
        {
            var expected = 3;

            var actual = CardTypeEnum.GetAll();

            Assert.AreEqual(expected, actual.Count());
            CollectionAssert.Contains(actual, CardTypeEnum.Amex);
            CollectionAssert.Contains(actual, CardTypeEnum.MasterCard);
            CollectionAssert.Contains(actual, CardTypeEnum.Visa);
        }

        #endregion GetAll

        #region Is

        [TestMethod]
        public void IsVisa_Passing_Nothing_Returns_True()
        {
            var enumeracion = CardTypeEnum.Visa;
            var expected = true;

            var actual = enumeracion.IsVisa;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsAmex_Passing_Nothing_Returns_True()
        {
            var enumeracion = CardTypeEnum.Amex;
            var expected = true;

            var actual = enumeracion.IsAmex;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsMasterCard_Passing_Nothing_Returns_True()
        {
            var enumeracion = CardTypeEnum.MasterCard;
            var expected = true;

            var actual = enumeracion.IsMasterCard;

            Assert.AreEqual(expected, actual);
        }

        #endregion Is
    }
}
