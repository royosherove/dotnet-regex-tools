namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;

    internal class RegexAlternate : RegexItem
    {
        public RegexAlternate(RegexBuffer buffer)
        {
            buffer.AddLookup(this, buffer.Offset, buffer.Offset);
            buffer.MoveNext();
        }

        public override string ToString(int offset)
        {
            return (new string(' ', offset) + "or");
        }
    }
}

