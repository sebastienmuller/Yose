using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        public ActionResult Index(string[] number)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            if (number.Length == 1)
            {
                return Content(JsonConvert.SerializeObject(new PrimeFactorsService().GetPrimeFactors(number[0]), settings), "application/json");
            }

            return Content(JsonConvert.SerializeObject(new PrimeFactorsService().GetPrimeFactors(number), settings), "application/json");
        }
	}
}