namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;

    public class RegexBuffer
    {
        private int errorLength = -1;
        private int errorLocation = -1;
        private string expression;
        private ArrayList expressionLookup = new ArrayList();
        private bool inSeries;
        private int offset;
        private System.Text.RegularExpressions.RegexOptions regexOptions;

        public RegexBuffer(string expression)
        {
            this.expression = expression;
        }

        public void AddLookup(RegexItem item, int startLocation, int endLocation)
        {
            this.AddLookup(item, startLocation, endLocation, false);
        }

        public void AddLookup(RegexItem item, int startLocation, int endLocation, bool canCoalesce)
        {
            if (this.inSeries)
            {
                if (canCoalesce)
                {
                    RegexRef ref2 = (RegexRef) this.expressionLookup[this.expressionLookup.Count - 1];
                    ref2.StringValue = ref2.StringValue + item.ToString(0);
                    ref2.Length += (endLocation - startLocation) + 1;
                }
                else
                {
                    this.expressionLookup.Add(new RegexRef(item, startLocation, endLocation));
                    this.inSeries = false;
                }
            }
            else
            {
                if (canCoalesce)
                {
                    this.inSeries = true;
                }
                this.expressionLookup.Add(new RegexRef(item, startLocation, endLocation));
            }
        }

        public void ClearInSeries()
        {
            this.inSeries = false;
        }

        public RegexRef MatchLocations(int spot)
        {
            ArrayList list = new ArrayList();
            foreach (RegexRef ref2 in this.expressionLookup)
            {
                if (ref2.InRange(spot))
                {
                    list.Add(ref2);
                }
            }
            list.Sort();
            if (list.Count != 0)
            {
                return (RegexRef) list[0];
            }
            return null;
        }

        public void MoveNext()
        {
            this.offset++;
        }

        public RegexBuffer Substring(int start, int end)
        {
            return new RegexBuffer(this.expression.Substring(start, (end - start) + 1));
        }

        public bool AtEnd
        {
            get
            {
                return (this.offset >= this.expression.Length);
            }
        }

        public char Current
        {
            get
            {
                if (this.offset >= this.expression.Length)
                {
                    throw new Exception("Beyond end of buffer");
                }
                return this.expression[this.offset];
            }
        }

        public int ErrorLength
        {
            get
            {
                return this.errorLength;
            }
            set
            {
                this.errorLength = value;
            }
        }

        public int ErrorLocation
        {
            get
            {
                return this.errorLocation;
            }
            set
            {
                this.errorLocation = value;
            }
        }

        public bool ExplicitCapture
        {
            get
            {
                return ((this.regexOptions & System.Text.RegularExpressions.RegexOptions.ExplicitCapture) != System.Text.RegularExpressions.RegexOptions.None);
            }
        }

        public bool IgnorePatternWhitespace
        {
            get
            {
                return ((this.regexOptions & System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace) != System.Text.RegularExpressions.RegexOptions.None);
            }
        }

        public int Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }

        public System.Text.RegularExpressions.RegexOptions RegexOptions
        {
            get
            {
                return this.regexOptions;
            }
            set
            {
                this.regexOptions = value;
            }
        }

        public string String
        {
            get
            {
                return this.expression.Substring(this.offset);
            }
        }
    }
}

