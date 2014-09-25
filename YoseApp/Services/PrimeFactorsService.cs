using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoseApp.Services
{
    public class PrimeFactorsService
    {
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