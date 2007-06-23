namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Text.RegularExpressions;

    public class RegexCharClass : RegexItem
    {
        private string description;

        public RegexCharClass(RegexBuffer buffer)
        {
            int startLocation = buffer.Offset;
            buffer.MoveNext();
            Match match = new Regex(@"(?<Negated>\^?)(?<Class>.+?)\]").Match(buffer.String);
            if (match.Success)
            {
                if (match.Groups["Negated"].ToString() == "^")
                {
                    this.description = string.Format("Any character not in \"{0}\"", match.Groups["Class"]);
                }
                else
                {
                    this.description = string.Format("Any character in \"{0}\"", match.Groups["Class"]);
                }
                buffer.Offset += match.Groups[0].Length;
            }
            else
            {
                this.description = "missing ']' in character class";
            }
            buffer.AddLookup(this, startLocation, buffer.Offset - 1);
        }

        public override string ToString(int offset)
        {
            return this.description;
        }
    }
}

