using System;
using NUnit.Framework;
using YoseApp.Controllers;
using System.Web.Mvc;
using System.Linq;
using YoseApp.Services;
using System.Collections.Generic;

namespace Tests
{
    public class PrimeFactorsTest
    {
        [Test]
        public void CheckPowerOfTwoDecomposition()
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

        [Test]
        public void CheckPrimeFactors()
        {
            var pfService = new PrimeFactorsService();
            var decomposition15 = pfService.GetPrimeFactors(15);
            var decomposition24 = pfService.GetPrimeFactors(24);
            
            CollectionAssert.AreEquivalent(new List<int> { 3, 5 }, decomposition15);
            CollectionAssert.AreEquivalent(new List<int> { 2, 2, 2, 3 }, decomposition24);
        }
    }
}
