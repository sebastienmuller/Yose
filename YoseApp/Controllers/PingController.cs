using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YoseApp.Controllers
{
    public class PingController : Controller
    {
        public ActionResult Index()
        {
            return Json(new { alive = true });
        }
	}
}