using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Travel.Service.Tests._03
{
    public class PricingTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //yield return new object[] { new DateTime(2020, 01, 15), new DateTime(2020, 01, 25), 100, 110 };
            //yield return new object[] { new DateTime(2020, 03, 25), new DateTime(2020, 04, 15), 100, 112 };
            //yield return new object[] { new DateTime(2020, 12, 25), new DateTime(2021, 01, 05), 100, 113 };

            yield return new object[] { new PriceTestCases { CheckIn= new DateTime(2020, 01, 15) , CheckOut= new DateTime(2020, 01, 25), Price=100, ExpPrice=110 } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
