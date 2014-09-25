using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoseApp.Services;

namespace YoseApp.Controllers
{
    public class PrimeFactorsController : Controller
    {
        public ActionResult Index(string number)
        {
            int multiple;
            if (!int.TryParse(number, out multiple))
            {
                return Json(new { number = number, error = "not a number" }, "application/json", JsonRequestBehavior.AllowGet);
            }

            var primeFactors = new PrimeFactorsService().GetPrimeFactors(multiple);

            return Json(new { number = multiple, decomposition = primeFactors }, "application/json", JsonRequestBehavior.AllowGet);
        }
	}
}