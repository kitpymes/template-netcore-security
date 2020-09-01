using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitpymes.Core.Entities.Enumerations.Tests
{
    [TestClass]
    public class SubscriptionEnumTests
    {
        #region ToEnum String

        [TestMethod]
        public void ToEnum_Passing_Valid_String_Name_Returns_Enumeracion()
        {
            var name = "Free";
            var expected = SubscriptionEnum.Free;

            var actual = SubscriptionEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_String_Name_Enumeracion_Returns_Null()
        {
            var name = Guid.NewGuid().ToString();
            SubscriptionEnum? expected = null;

            var actual = SubscriptionEnum.ToEnum(name);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum String

        #region ToEnum Int

        [TestMethod]
        public void ToEnum_Passing_Valid_IntName_Enumeracion_Returns_Enumeracion()
        {
            var value = 3;
            var expected = SubscriptionEnum.Gold;

            var actual = SubscriptionEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToEnum_Passing_Invalid_Int_Name_Enumeracion_Returns_Null()
        {
            var value = new Random().Next();
            SubscriptionEnum? expected = null;

            var actual = SubscriptionEnum.ToEnum(value);

            Assert.AreEqual(expected, actual);
        }

        #endregion ToEnum Int

        #region GetAll

        [TestMethod]
        public void GetAll_Passing_Nothing_Returns_Enumeracion_List()
        {
            var expected = 3;

            var actual = SubscriptionEnum.GetAll();

            Assert.AreEqual(expected, actual.Count());
            CollectionAssert.Contains(actual, SubscriptionEnum.Free);
            CollectionAssert.Contains(actual, SubscriptionEnum.Silver);
            CollectionAssert.Contains(actual, SubscriptionEnum.Gold);
        }

        #endregion GetAll

        #region Is

        [TestMethod]
        public void IsFree_Passing_Nothing_Returns_True()
        {
            var enumeracion = SubscriptionEnum.Free;
            var expected = true;

            var actual = enumeracion.IsFree;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsSilver_Passing_Nothing_Returns_True()
        {
            var enumeracion = SubscriptionEnum.Silver;
            var expected = true;

            var actual = enumeracion.IsSilver;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsGold_Passing_Nothing_Returns_True()
        {
            var enumeracion = SubscriptionEnum.Gold;
            var expected = true;

            var actual = enumeracion.IsGold;

            Assert.AreEqual(expected, actual);
        }

        #endregion Is
    }
}
