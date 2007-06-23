namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Text.RegularExpressions;

    public class RegexExpression : RegexItem
    {
        private ArrayList items = new ArrayList();

        public RegexExpression(RegexBuffer buffer)
        {
            this.Parse(buffer);
        }

        private void EatComment(RegexBuffer buffer)
        {
            while (buffer.Current != '\r')
            {
                buffer.MoveNext();
            }
        }

        public static string Interpret(string regex)
        {
            RegexBuffer buffer = new RegexBuffer(regex);
            RegexExpression expression = new RegexExpression(buffer);
            return expression.ToString(0);
        }

        private void Parse(RegexBuffer buffer)
        {
            while (!buffer.AtEnd)
            {
                if (buffer.IgnorePatternWhitespace && (((buffer.Current == ' ') || (buffer.Current == '\r')) || ((buffer.Current == '\n') || (buffer.Current == '\t'))))
                {
                    buffer.MoveNext();
                }
                else
                {
                    switch (buffer.Current)
                    {
                        case '(':
                        {
                            this.items.Add(new RegexCapture(buffer));
                            continue;
                        }
                        case ')':
                            return;

                        case '#':
                        {
                            if (buffer.IgnorePatternWhitespace)
                            {
                                this.EatComment(buffer);
                            }
                            else
                            {
                                this.items.Add(new RegexCharacter(buffer));
                            }
                            continue;
                        }
                        case '[':
                        {
                            this.items.Add(new TeamAgile.RegexKit.Common.RegexParser.RegexCharClass(buffer));
                            continue;
                        }
                        case '\\':
                        {
                            this.items.Add(new RegexCharacter(buffer));
                            continue;
                        }
                        case '{':
                        {
                            this.items.Add(new RegexQuantifier(buffer));
                            continue;
                        }
                        case '|':
                        {
                            this.items.Add(new RegexAlternate(buffer));
                            continue;
                        }
                    }
                    this.items.Add(new RegexCharacter(buffer));
                }
            }
        }

        public override string ToString(int indent)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            foreach (RegexItem item in this.items)
            {
                RegexCharacter character = item as RegexCharacter;
                if ((character != null) && !character.Special)
                {
                    builder2.Append(character.ToString(indent));
                    continue;
                }
                if (builder2.Length != 0)
                {
                    builder.Append(new string(' ', indent));
                    builder.Append(builder2.ToString() + "\r\n");
                    builder2 = new StringBuilder();
                }
                builder.Append(new string(' ', indent));
                string text = item.ToString(indent);
                if (text.Length != 0)
                {
                    builder.Append(text);
                    Regex regex = new Regex(@"\r\n$");
                    if (!regex.IsMatch(text))
                    {
                        builder.Append("\r\n");
                    }
                }
            }
            if (builder2.Length != 0)
            {
                builder.Append(new string(' ', indent));
                builder.Append(builder2.ToString() + "\r\n");
                builder2 = new StringBuilder();
            }
            return builder.ToString();
        }

        public ArrayList Items
        {
            get
            {
                return this.items;
            }
        }
    }
}

