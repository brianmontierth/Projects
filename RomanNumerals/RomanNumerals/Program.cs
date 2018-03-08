using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var result = File.ReadAllLines(@"C:\Users\brian\Documents\Visual Studio 2015\Projects\RomanNumerals\RomanNumerals\roman.txt");
           
            int oldLength = result.Sum(x => x.Length);
            int newLength = result.Sum(x => x.ConvertToDecimal().ConvertToNumeral().Length);
            Console.WriteLine(oldLength - newLength);

            var decimals = result.Select(x => x + " = " + x.ConvertToDecimal() + " = " + x.ConvertToDecimal().ConvertToNumeral());

            foreach (var item in decimals)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
