using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    public static class NumeralConverter
    {
        private static Dictionary<int, string> _decimalToNumeralLookup;
        private static Dictionary<string, int> _numeralToDecimalLookup;

        static NumeralConverter()
        {
            _decimalToNumeralLookup = new Dictionary<int, string>();
            _decimalToNumeralLookup.Add(1000, "M");
            _decimalToNumeralLookup.Add(900, "CM");
            _decimalToNumeralLookup.Add(500,"D");
            _decimalToNumeralLookup.Add(400, "CD");
            _decimalToNumeralLookup.Add(100,"C");
            _decimalToNumeralLookup.Add(90, "XC");
            _decimalToNumeralLookup.Add(50,"L");
            _decimalToNumeralLookup.Add(40, "XL");
            _decimalToNumeralLookup.Add(10,"X");
            _decimalToNumeralLookup.Add(9, "IX");
            _decimalToNumeralLookup.Add(5,"V");
            _decimalToNumeralLookup.Add(4, "IV");
            _decimalToNumeralLookup.Add(1,"I");
            

            _numeralToDecimalLookup = new Dictionary<string, int>();
            
            foreach (var pair in _decimalToNumeralLookup)
            {
                _numeralToDecimalLookup.Add(pair.Value, pair.Key);
            }
        }

        public static int ConvertToDecimal(this string numerals)
        {
            var result = numerals
                .ToCharArray()
                .Reverse()
                .Select(c => _numeralToDecimalLookup[c.ToString()])
                .Aggregate(new { MaxValue = 0, RunningTotal = 0 }, (state, item) => new
                {
                    MaxValue = Math.Max(state.MaxValue, item),
                    RunningTotal = item >= state.MaxValue ? state.RunningTotal + item : state.RunningTotal - item
                },
                aggregate => aggregate.RunningTotal);
            
            return result;
        }


        public static string ConvertToNumeral(this int dec)
        {
            if (dec < 1)
                return "N";

            Func<dynamic,KeyValuePair<int, string>, dynamic> getNumeral = (state, numeral) =>
              {
                  var currentDecimal = numeral.Key;
                  int n = state.number;
                  StringBuilder s = state.sb;
                  while (n >= currentDecimal)
                  {
                      state.sb.Append(numeral.Value);
                      n = n - currentDecimal;
                  }
                  return new { number = n, sb = s } ;
              };

            var result = _decimalToNumeralLookup.Aggregate(new { number = dec, sb = new StringBuilder() }, (state, numeral) => getNumeral(state, numeral),
            completed => completed.sb.ToString()
            );
            
            return result;
        }

    }
}
