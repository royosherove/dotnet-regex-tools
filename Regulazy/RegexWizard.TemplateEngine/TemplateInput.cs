using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexWizard.TemplateEngine
{
    public class TemplateInput
    {
        private string pattern=String.Empty;
        private string input=String.Empty;
        private RegexOptions regexOptions;

        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        public string Input
        {
            get
            {
                return input;
            }
            set { input = value; }
        }

        public RegexOptions RegexOptions
        {
            get
            {
                return regexOptions;
            }
            set { regexOptions = value; }
        }
    }
}
