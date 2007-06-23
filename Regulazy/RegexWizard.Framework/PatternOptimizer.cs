using System.Text.RegularExpressions;

namespace RegexWizard.Framework
{
    public class PatternOptimizer
    {
        public string Optimize(string regex)
        {
            if(regex=="")
            {
                return string.Empty;
            }
            
            string optimized = regex;
            optimized = Regex.Replace(optimized, @"\$\^", "");
            optimized = Regex.Replace(optimized, @"(?<before>.)\^", "${before}");
            optimized = Regex.Replace(optimized, @"\$(?<after>.)", "${after}");
            
            optimized = Regex.Replace(optimized, @"\++", Regex.Unescape(@"\+"));
            optimized = optimized.Replace("?+", "?");
            optimized = Regex.Replace(optimized, escape("^?"), "^");
            if (AutoAddLineEndMarks)
            {
                if (!optimized.EndsWith("$"))
                {
                    optimized += "$";
                }
                if (!optimized.StartsWith("^"))
                {
                    optimized = "^" + optimized;
                }

            }
            return optimized;
        }

        public bool AutoAddLineEndMarks
        {
            get { return autoAddLineEndMarks; }
            set { autoAddLineEndMarks = value; }
        }

        private bool autoAddLineEndMarks=false;
        
//        private string optimizeOLD(string regex)
//        {
//            string opt = regex;
//            opt = Regex.Replace(opt, @"(?<before>.)\\^", "${before}");
//            opt = Regex.Replace(opt, @"\\$(?<after>.)", "${after}");
//            opt = Regex.Replace(opt, @"\$\^", "");
//            opt = Regex.Replace(opt, @"\\?\\$", "?");
//            opt = Regex.Replace(opt, @"\++", Regex.Unescape(@"\+"));
//            opt = opt.Replace("?+", "?");
//            opt = Regex.Replace(opt, escape("${"), "{");
//            opt = Regex.Replace(opt, escape("$["), "[");
//            opt = Regex.Replace(opt, escape("^?"), "^");
//
//            if (!opt.EndsWith("$"))
//            {
//                opt += "$";
//            }
//            if (opt.StartsWith("^["))
//            {
//                opt = "^" + opt;
//            }
//
//            opt = dontAllowIntheMiddle("^", opt);
//            opt = dontAllowIntheMiddle("$", opt);
//
//            if (regex.StartsWith("^") && !opt.StartsWith("^"))
//            {
//                opt = "^" + opt;
//
//            }
//            return opt;
//        }
//
//        private string dontAllowIntheMiddle(string disallow, string input)
//        {
//            string format = string.Format(@"(?<before>.){0}(?<after>.)", Regex.Escape(disallow));
//            string replaced = Regex.Replace(input, format, "${before}${after}");
//            return replaced;
//        }

        private string escape(string what)
        {
            return Regex.Escape(what);
        }

    }
}
