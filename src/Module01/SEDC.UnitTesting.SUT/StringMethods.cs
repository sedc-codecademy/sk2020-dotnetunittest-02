using System;
using System.Threading;

namespace SEDC.UnitTesting.SUT
{
    public class StringMethods
    {
        public string Reverse(string value)
        {
            Thread.Sleep(30000);
            var arr = value.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
