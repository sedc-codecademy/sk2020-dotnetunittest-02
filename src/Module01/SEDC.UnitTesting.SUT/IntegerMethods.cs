using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.UnitTesting.SUT
{
    public class IntegerMethods
    {
        public int CalculateNthFibonacciNumber(int n)
        {
            //if (n < 0)
            //    throw new ArgumentException("Value must be positive number");
            if (n < 2)
                return 1;

            int n_1 = 1;
            int n_2 = 1;

            for (int i = 2; i <= n; i += 1)
            {
                int n_new = n_1 + n_2;
                n_1 = n_2;
                n_2 = n_new;
            }

            return n_2;
        }


        public int FindNthLargestNumber(List<int> numbers, int nthLargestNumber)
        {
            if (!numbers.Any())
                throw new Exception("SEDC ex");
            var orderByDescending = numbers.OrderByDescending(x => x);

            var result = orderByDescending.ElementAt(nthLargestNumber - 1);

            return result;
        }

        public async Task<int> AddAsync(int x, int y)
        {
            await Task.Delay(30000);

            return x + y;
        }
    }
}
