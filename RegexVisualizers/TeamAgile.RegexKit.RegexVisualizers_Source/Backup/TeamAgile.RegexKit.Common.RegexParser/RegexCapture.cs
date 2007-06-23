namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;

    public class RegexCapture : RegexItem
    {
        private string description = "Capture";
        private RegexItem expression;
        private static Hashtable optionNames = new Hashtable();
        private int startLocation;

        static RegexCapture()
        {
            optionNames.Add("i", "Ignore Case");
            optionNames.Add("-i", "Ignore Case Off");
            optionNames.Add("m", "Multiline");
            optionNames.Add("-m", "Multiline Off");
            optionNames.Add("n", "Explicit Capture");
            optionNames.Add("-n", "Explicit Capture Off");
            optionNames.Add("s", "Singleline");
            optionNames.Add("-s", "Singleline Off");
            optionNames.Add("x", "Ignore Whitespace");
            optionNames.Add("-x", "Ignore Whitespace Off");
        }

        public RegexCapture(RegexBuffer buffer)
        {
            this.startLocation = buffer.Offset;
            buffer.MoveNext();
            buffer.ClearInSeries();
            if (buffer.Current == '?')
            {
                bool flag = this.CheckNamed(buffer);
                if (!flag)
                {
                    flag = this.CheckBalancedGroup(buffer);
                }
                if (!flag)
                {
                    flag = this.CheckNonCapturing(buffer);
                }
                if (!flag)
                {
                    flag = this.CheckOptions(buffer);
                }
                if (!flag)
                {
                    flag = this.CheckLookahead(buffer);
                }
                if (!flag)
                {
                    flag = this.CheckNonBacktracking(buffer);
                }
                if (!flag)
                {
                    flag = this.CheckConditional(buffer);
                }
            }
            else if (!this.HandlePlainOldCapture(buffer))
            {
                throw new Exception(string.Format("Unrecognized capture: {0}", buffer.String));
            }
            buffer.AddLookup(this, this.startLocation, buffer.Offset - 1);
        }

        private bool CheckBalancedGroup(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?[\\<|']                  # ?< or ?'\r\n\t\t\t\t\t\t(?<Name1>[a-zA-Z]+?)       # Capture name1\r\n\t\t\t\t\t\t-\r\n\t\t\t\t\t\t(?<Name2>[a-zA-Z]+?)       # Capture name2\r\n\t\t\t\t\t\t[\\>|']                    # ?> or ?'\r\n\t\t\t\t\t\t(?<Rest>.+)               # The rest of the expression\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                this.description = string.Format("Balancing Group <{0}>-<{1}>", match.Groups["Name1"], match.Groups["Name2"]);
                buffer.Offset += match.Groups["Rest"].Index;
                this.expression = new RegexExpression(buffer);
                this.CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private void CheckClosingParen(RegexBuffer buffer)
        {
            char current = ' ';
            try
            {
                current = buffer.Current;
            }
            catch (Exception exception)
            {
                buffer.ErrorLocation = this.startLocation;
                buffer.ErrorLength = 1;
                throw new Exception(string.Format("Missing closing ')' in capture", new object[0]), exception);
            }
            if (current != ')')
            {
                throw new Exception(string.Format("Unterminated closure at offset {0}", buffer.Offset));
            }
            buffer.Offset++;
        }

        private bool CheckConditional(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?\\(\r\n\t\t\t\t\t\t(?<Rest>.+)             # The rest of the expression\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                this.description = string.Format("Conditional Subexpression", new object[0]);
                buffer.Offset += match.Groups["Rest"].Index;
                this.expression = new RegexConditional(buffer);
                return true;
            }
            return false;
        }

        private bool CheckLookahead(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?\r\n\t\t\t\t\t\t(?<Assertion><=|<!|=|!)   # assertion char\r\n\t\t\t\t\t\t(?<Rest>.+)               # The rest of the expression\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (!match.Success)
            {
                return false;
            }
            switch (match.Groups["Assertion"].Value)
            {
                case "=":
                    this.description = "zero-width positive lookahead";
                    break;

                case "!":
                    this.description = "zero-width negative lookahead";
                    break;

                case "<=":
                    this.description = "zero-width positive lookbehind";
                    break;

                case "<!":
                    this.description = "zero-width negative lookbehind";
                    break;
            }
            buffer.Offset += match.Groups["Rest"].Index;
            this.expression = new RegexExpression(buffer);
            this.CheckClosingParen(buffer);
            return true;
        }

        private bool CheckNamed(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?(\\<|')                  # ?< or ?'\r\n\t\t\t\t\t\t(?<Name>[a-zA-Z0-9]+?)    # Capture name\r\n\t\t\t\t\t\t(\\>|')                    # ?> or ?'\r\n\t\t\t\t\t\t(?<Rest>.+)               # The rest of the string\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                this.description = string.Format("Capture to <{0}>", match.Groups["Name"]);
                buffer.Offset += match.Groups["Rest"].Index;
                this.expression = new RegexExpression(buffer);
                this.CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private bool CheckNonBacktracking(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?\\>\r\n\t\t\t\t\t\t(?<Rest>.+)             # The rest of the expression\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                this.description = string.Format("Non-backtracking subexpressio", new object[0]);
                buffer.Offset += match.Groups["Rest"].Index;
                this.expression = new RegexExpression(buffer);
                this.CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private bool CheckNonCapturing(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?:\r\n\t\t\t\t\t\t(?<Rest>.+)             # The rest of the expression\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                this.description = string.Format("Non-capturing Group", new object[0]);
                buffer.Offset += match.Groups["Rest"].Index;
                this.expression = new RegexExpression(buffer);
                this.CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private bool CheckOptions(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t        ^                         # anchor to start of string\r\n\t\t\t\t\t\t\\?(?<Options>[imnsx-]+):\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                string text = match.Groups["Options"].Value;
                this.description = string.Format("Set options to {0}", optionNames[text]);
                this.expression = null;
                buffer.Offset += match.Groups[0].Length;
                return true;
            }
            return false;
        }

        private bool HandlePlainOldCapture(RegexBuffer buffer)
        {
            if (buffer.ExplicitCapture)
            {
                this.description = string.Format("Non-capturing Group", new object[0]);
            }
            this.expression = new RegexExpression(buffer);
            this.CheckClosingParen(buffer);
            return true;
        }

        public override string ToString(int offset)
        {
            string description = this.description;
            if (this.expression != null)
            {
                string text2 = description;
                description = text2 + "\r\n" + this.expression.ToString(offset + 2) + new string(' ', offset) + "End Capture";
            }
            return description;
        }
    }
}

