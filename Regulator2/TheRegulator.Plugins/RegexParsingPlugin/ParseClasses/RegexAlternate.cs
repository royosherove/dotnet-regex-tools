/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Text.RegularExpressions;

namespace RegexTest
{
	class RegexAlternate: RegexItem
	{
		public RegexAlternate(RegexBuffer buffer)
		{
			buffer.AddLookup(this, buffer.Offset, buffer.Offset);

			buffer.MoveNext();		// skip "|"
		}

		public override string ToString(int offset)
		{
			return(new String(' ', offset) + "or");
		}		
	}
}
