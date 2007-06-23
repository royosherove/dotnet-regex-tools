namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Text.RegularExpressions;

    public class RegexQuantifier : RegexItem
    {
        private string description;

        public RegexQuantifier(RegexBuffer buffer)
        {
            int startLocation = buffer.Offset;
            buffer.MoveNext();
            Match match = new Regex(@"(?<n>\d+)(?<Comma>,?)(?<m>\d*)\}").Match(buffer.String);
            if (match.Success)
            {
                if (match.Groups["m"].Length != 0)
                {
                    this.description = string.Format("At least {0}, but not more than {1} times", match.Groups["n"], match.Groups["m"]);
                }
                else if (match.Groups["Comma"].Length != 0)
                {
                    this.description = string.Format("At least {0} times", match.Groups["n"]);
                }
                else
                {
                    this.description = string.Format("Exactly {0} times", match.Groups["n"]);
                }
                buffer.Offset += match.Groups[0].Length;
                if (!buffer.AtEnd && (buffer.Current == '?'))
                {
                    this.description = this.description + " (non-greedy)";
                    buffer.MoveNext();
                }
            }
            else
            {
                this.description = "missing '}' in quantifier";
            }
            buffer.AddLookup(this, startLocation, buffer.Offset - 1);
        }

        public override string ToString(int offset)
        {
            return this.description;
        }
    }
}

