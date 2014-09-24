using System;
using NUnit.Framework;
using YoseApp.Controllers;
using System.Web.Mvc;
using System.Linq;

namespace Tests
{
    public class PrimeFactorTest
    {
        [Test]
        public void CheckDecomposition()
        {
            var controller = new PrimeFactorsController();
            var result = controller.Index("16") as JsonResult;

            dynamic jsonObj = result.Data;

            StringAssert.AreEqualIgnoringCase("application/json", result.ContentType);
            Assert.AreEqual(JsonRequestBehavior.AllowGet, result.JsonRequestBehavior);
            Assert.AreEqual(jsonObj.number, 16);
            Assert.AreEqual(jsonObj.decomposition, Enumerable.Repeat<int>(2, 4));
        }

        [Test]
        public void CheckNotANumber()
        {
            var controller = new PrimeFactorsController();
            var result = controller.Index("YO") as JsonResult;

            dynamic jsonObj = result.Data;

            StringAssert.AreEqualIgnoringCase("application/json", result.ContentType);
            Assert.AreEqual(JsonRequestBehavior.AllowGet, result.JsonRequestBehavior);
            Assert.AreEqual(jsonObj.number, "YO");
            Assert.AreEqual(jsonObj.error, "not a number");
        }
    }
}
