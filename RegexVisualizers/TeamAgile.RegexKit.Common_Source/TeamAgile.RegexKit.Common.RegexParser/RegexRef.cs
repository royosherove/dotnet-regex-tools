namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;

    public class RegexRef : IComparable
    {
        private int end;
        private int start;
        private string stringValue;

        public RegexRef(RegexItem regexItem, int start, int end)
        {
            this.stringValue = regexItem.ToString(0);
            this.start = start;
            this.end = end;
        }

        public int CompareTo(object o2)
        {
            RegexRef ref2 = (RegexRef) o2;
            if (this.Length < ref2.Length)
            {
                return -1;
            }
            if (this.Length > ref2.Length)
            {
                return 1;
            }
            return 0;
        }

        public bool InRange(int location)
        {
            return ((location >= this.start) && (location <= this.end));
        }

        public int End
        {
            get
            {
                return this.end;
            }
        }

        public int Length
        {
            get
            {
                return ((this.end - this.start) + 1);
            }
            set
            {
                this.end = (this.start + value) - 1;
            }
        }

        public int Start
        {
            get
            {
                return this.start;
            }
        }

        public string StringValue
        {
            get
            {
                return this.stringValue;
            }
            set
            {
                this.stringValue = value;
            }
        }
    }
}

