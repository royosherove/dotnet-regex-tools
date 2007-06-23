namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;

    public class RegexConditional : RegexItem
    {
        private RegexExpression expression;
        private int startLocation;
        private RegexExpression yesNo;

        public RegexConditional(RegexBuffer buffer)
        {
            this.startLocation = buffer.Offset;
            this.expression = new RegexExpression(buffer);
            this.CheckClosingParen(buffer);
            this.yesNo = new RegexExpression(buffer);
            this.CheckClosingParen(buffer);
            buffer.AddLookup(this, this.startLocation, buffer.Offset - 1);
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

        public override string ToString(int offset)
        {
            string text = new string(' ', offset);
            string text2 = (text + "if: " + this.expression.ToString(0)) + text + "match: ";
            foreach (RegexItem item in this.yesNo.Items)
            {
                if (item is RegexAlternate)
                {
                    text2 = text2 + "\r\n" + text + "else match: ";
                    continue;
                }
                text2 = text2 + item.ToString(offset);
            }
            return (text2 + "\r\n");
        }
    }
}

