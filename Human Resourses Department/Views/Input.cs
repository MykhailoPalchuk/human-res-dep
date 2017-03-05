using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Views
{
    public static class Input
    {
        public  static bool IsName(string input)
        {
            string pattern = @"^[A-Z][a-z]+$";
            if (Regex.IsMatch(input, pattern, RegexOptions.Compiled))
                return true;
            else
                return false;
        }

        public static bool IsAccountNumber(string input)
        {
            string pattern = @"^[0-9]{16}$";
            if (Regex.IsMatch(input, pattern, RegexOptions.Compiled))
                return true;
            else
                return false;
        }

        public static bool IsNumber(string input)
        {
            string pattern = @"^[1-9][0-9]*$";
            if (Regex.IsMatch(input, pattern, RegexOptions.Compiled))
                return true;
            else
                return false;
        }

        public static bool IsDouble(string input)
        {
            string pattern = @"^[0-9]+,[0-9]+$";
            if (Regex.IsMatch(input, pattern, RegexOptions.Compiled))
                return true;
            else
                return false;
        }
    }
}
