namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;

    public class RegexCharacter : RegexItem
    {
        private string character;
        private static Hashtable escaped = new Hashtable();
        private bool special;

        static RegexCharacter()
        {
            escaped.Add('a', @"A bell (alarm) \u0007 ");
            escaped.Add('b', "Word boundary between //w and //W");
            escaped.Add('B', "Not at a word boundary between //w and //W");
            escaped.Add('t', @"A tab \u0009 ");
            escaped.Add('r', @"A carriage return \u000D ");
            escaped.Add('v', @"A vertical tab \u000B ");
            escaped.Add('f', @"A form feed \u000C ");
            escaped.Add('n', @"A new line \u000A ");
            escaped.Add('e', @"An escape \u001B ");
            escaped.Add('w', "Any word character ");
            escaped.Add('W', "Any non-word character ");
            escaped.Add('s', "Any whitespace character ");
            escaped.Add('S', "Any non-whitespace character ");
            escaped.Add('d', "Any digit ");
            escaped.Add('D', "Any non-digit ");
            escaped.Add('A', "Anchor to start of string (ignore multiline)");
            escaped.Add('Z', @"Anchor to end of string or before \n (ignore multiline)");
            escaped.Add('z', "Anchor to end of string (ignore multiline)");
        }

        public RegexCharacter(string characters)
        {
            this.character = characters;
        }

        public RegexCharacter(RegexBuffer buffer)
        {
            int startLocation = buffer.Offset;
            bool flag = false;
            switch (buffer.Current)
            {
                case ' ':
                    this.character = "' ' (space)";
                    buffer.MoveNext();
                    break;

                case '$':
                    this.character = "$ (anchor to end of string)";
                    buffer.MoveNext();
                    break;

                case '*':
                    this.character = "* (zero or more times)";
                    buffer.MoveNext();
                    this.special = true;
                    flag = true;
                    break;

                case '+':
                    this.character = "+ (one or more times)";
                    buffer.MoveNext();
                    this.special = true;
                    flag = true;
                    break;

                case '.':
                    this.character = ". (any character)";
                    buffer.MoveNext();
                    this.special = true;
                    break;

                case '?':
                    this.character = "? (zero or one time)";
                    buffer.MoveNext();
                    this.special = true;
                    flag = true;
                    break;

                case '\\':
                    this.DecodeEscape(buffer);
                    break;

                case '^':
                    this.character = "^ (anchor to start of string)";
                    buffer.MoveNext();
                    break;

                default:
                    this.character = buffer.Current.ToString();
                    buffer.MoveNext();
                    this.special = false;
                    break;
            }
            if ((flag && !buffer.AtEnd) && (buffer.Current == '?'))
            {
                this.character = this.character + " (non-greedy)";
                buffer.MoveNext();
            }
            buffer.AddLookup(this, startLocation, buffer.Offset - 1, this.character.Length == 1);
        }

        private bool CheckBackReference(RegexBuffer buffer)
        {
            Match match = new Regex("\r\n\t\t\t\t\t\tk\\<(?<Name>.+?)\\>\r\n\t\t\t\t\t\t", RegexOptions.IgnorePatternWhitespace).Match(buffer.String);
            if (match.Success)
            {
                this.special = true;
                this.character = string.Format("Backreference to match: {0}", match.Groups["Name"]);
                buffer.Offset += match.Groups[0].Length;
                return true;
            }
            return false;
        }

        private void DecodeEscape(RegexBuffer buffer)
        {
            buffer.MoveNext();
            this.character = (string) escaped[buffer.Current];
            if (this.character == null)
            {
                if (!this.CheckBackReference(buffer))
                {
                    switch (buffer.Current)
                    {
                        case 'u':
                        {
                            buffer.MoveNext();
                            string text = buffer.String.Substring(0, 4);
                            this.character = "Unicode " + text;
                            buffer.Offset += 4;
                            return;
                        }
                        case 'x':
                        {
                            buffer.MoveNext();
                            string text2 = buffer.String.Substring(0, 2);
                            this.character = "Hex " + text2;
                            buffer.Offset += 2;
                            return;
                        }
                        case ' ':
                            this.character = "' ' (space)";
                            this.special = false;
                            buffer.MoveNext();
                            return;

                        case 'c':
                            buffer.MoveNext();
                            this.character = "CTRL-" + buffer.Current;
                            buffer.MoveNext();
                            return;
                    }
                    this.character = new string(buffer.Current, 1);
                    this.special = false;
                    buffer.MoveNext();
                }
            }
            else
            {
                this.special = true;
                buffer.MoveNext();
            }
        }

        public override string ToString(int offset)
        {
            return this.character;
        }

        public bool Special
        {
            get
            {
                return this.special;
            }
        }
    }
}

