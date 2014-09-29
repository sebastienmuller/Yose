using System;
using NUnit.Framework;
using YoseApp.Controllers;
using System.Web.Mvc;
using System.Linq;
using YoseApp.Services;
using System.Collections.Generic;
using Newtonsoft.Json;
using YoseApp.Models;

namespace Tests
{
    public class PrimeFactorsTest
    {
        [Test]
        public void CheckPowerOfTwoDecomposition()
        {
            var controller = new PrimeFactorsController();
            var result = controller.Index(new string[] { "16" }) as ContentResult;

            var decomposition = JsonConvert.DeserializeObject<PrimeFactorsDecomposition>(result.Content);

            StringAssert.AreEqualIgnoringCase("application/json", result.ContentType);
            Assert.AreEqual(decomposition.Number, "16");
            Assert.AreEqual(decomposition.Decomposition, Enumerable.Repeat<int>(2, 4));
        }

        [Test]
        public void CheckNotANumber()
        {
            var controller = new PrimeFactorsController();
            var result = controller.Index(new string[] { "YO" }) as ContentResult;

            var decomposition = JsonConvert.DeserializeObject<PrimeFactorsError>(result.Content);

            StringAssert.AreEqualIgnoringCase("application/json", result.ContentType);
            Assert.AreEqual(decomposition.Number, "YO");
            Assert.AreEqual(decomposition.Error, "not a number");
        }

        [Test]
        public void CheckTooBigNumber()
        {
            var numberStr = "1000001";
            var controller = new PrimeFactorsController();
            var result = controller.Index(new string[] { numberStr }) as ContentResult;

            var decomposition = JsonConvert.DeserializeObject<PrimeFactorsError>(result.Content);

            StringAssert.AreEqualIgnoringCase("application/json", result.ContentType);
            Assert.AreEqual(decomposition.Number, numberStr);
            Assert.AreEqual(decomposition.Error, "too big number (>1e6)");
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

        [Test]
        public void CheckSeveralNumbers()
        {
            var controller = new PrimeFactorsController();
            var result = controller.Index(new string[] { "300", "120", "hello" }) as ContentResult;

            var decomposition = JsonConvert.DeserializeObject<IList<PrimeFactorsError>>(result.Content);

            StringAssert.AreEqualIgnoringCase("application/json", result.ContentType);
            Assert.AreEqual(3, decomposition.Count);
        }
    }
}
