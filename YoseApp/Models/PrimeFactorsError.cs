using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoseApp.Models
{
    public class PrimeFactorsError : BasePrimeFactorsResult
    {
        public string Error { get; set; }
    }
}