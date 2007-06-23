namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;

    public abstract class RegexItem
    {
        public void Parse(string expression)
        {
        }

        public abstract string ToString(int indent);
    }
}

