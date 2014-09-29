using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoseApp.Models
{
    public class PrimeFactorsDecomposition : BasePrimeFactorsResult
    {
        public IList<int> Decomposition { get; set; }
    }
}