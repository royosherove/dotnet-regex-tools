/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexItem.
	/// </summary>
	public abstract class RegexItem
	{
		public RegexItem()
		{
		}

		public void Parse(string expression)
		{

		}

		public abstract string ToString(int indent);
	}
}
