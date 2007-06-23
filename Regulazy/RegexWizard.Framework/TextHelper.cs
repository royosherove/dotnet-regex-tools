using System.Text.RegularExpressions;

namespace RegexWizard.Framework
{
    public  class TextHelper
    {
       

        public static bool IsDigit(string s)
        {
            return Regex.IsMatch(s,@"^\d$");
        }
        
        public static bool IsAtLeastOneDigit(string s)
        {
            return Regex.IsMatch(s, @"^\d+$");
        }
        
        public static bool IsEmpty(string s)
        {
            return Regex.IsMatch(s, @"^$");
        }

        public static bool IsLetter(string s)
        {
            return Regex.IsMatch(s, @"^\w$") && (!IsAtLeastOneDigit(s));
        }
        
        public static bool IsAtLeastOneLetter(string s)
        {
            return Regex.IsMatch(s, @"^\w+$") && (!IsAtLeastOneDigit(s));
        }

    }
}
