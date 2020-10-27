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
            return null;
            //return new object[] { new DateTime(2020, 01, 15), new DateTime(2020, 01, 25), 100, 110 };
            //return new object[] { new DateTime(2020, 03, 25), new DateTime(2020, 04, 15), 100, 112 };
            //return new object[] { new DateTime(2020, 12, 25), new DateTime(2021, 01, 05), 100, 113 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
