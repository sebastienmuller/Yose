using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            if (multiple % 2 == 0)
            {
                int twoCount = 0;
                int tmpNumber = multiple;
                while(tmpNumber >= 2)
                {
                    tmpNumber = tmpNumber / 2;
                    twoCount++;
                }

                return Json(new { number = multiple, decomposition = Enumerable.Repeat<int>(2, twoCount) }, "application/json", JsonRequestBehavior.AllowGet);
            }

            return Json(new { number = multiple, decomposition = new List<int>(0) }, "application/json", JsonRequestBehavior.AllowGet);
        }
	}
}