using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoseApp.Models;

namespace YoseApp.Services
{
    public class PrimeFactorsService
    {
        public BasePrimeFactorsResult GetPrimeFactors(string number)
        {
            int multiple;
            if (!int.TryParse(number, out multiple))
            {
                return new PrimeFactorsError { Number = number, Error = "not a number" };
            }
            else if (multiple > 1000000)
            {
                return new PrimeFactorsError { Number = number, Error = "too big number (>1e6)" };
            }

            var primeFactors = GetPrimeFactors(multiple);
            return new PrimeFactorsDecomposition { Number = number, Decomposition = primeFactors };
        }

        public IList<BasePrimeFactorsResult> GetPrimeFactors(string[] numbers)
        {
            var result = new List<BasePrimeFactorsResult>(numbers.Length);
            
            foreach (var number in numbers)
            {
                result.Add(GetPrimeFactors(number));
            }

            return result;
        }

        public IList<int> GetPrimeFactors(int number)
        {
            var primeFactors = new List<int>();
            var divisor = 2;

            while (number > 1 )
            {
                while (number % divisor == 0)
                {
                    number = number / divisor;
                    primeFactors.Add(divisor);
                }
                divisor++;
            }
            
            return primeFactors;
        }

        
    }
}