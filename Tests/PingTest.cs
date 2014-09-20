using System;
using NUnit.Framework;
using YoseApp.Controllers;
using System.Web.Mvc;

namespace Tests
{
    public class PingTest
    {
        [Test]
        public void Index()
        {
            var pingCtrl = new PingController();

            var result = pingCtrl.Index();

            //assert
            Assert.IsInstanceOf<JsonResult>(result);
            var data = ((JsonResult)result).Data;
            var type = data.GetType();
            var alivePropertyInfo = type.GetProperty("alive");
            var actual = alivePropertyInfo.GetValue(data, null);

            Assert.AreEqual(true, actual);
        }
    }
}
