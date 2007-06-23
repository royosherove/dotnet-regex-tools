/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Text.RegularExpressions;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexCharClass.
	/// </summary>
	public class RegexCharClass: RegexItem
	{
				//RegexExpression expression;
		string description;

		public RegexCharClass(RegexBuffer buffer)
		{
			int startLoc = buffer.Offset;

			buffer.MoveNext();

			Regex regex;
			Match match;

			regex = new Regex(@"(?<Negated>\^?)(?<Class>.+?)\]");

			match = regex.Match(buffer.String);
			if (match.Success)
			{
				if (match.Groups["Negated"].ToString() == "^")
				{
					description = String.Format("Any character not in \"{0}\"", 
						match.Groups["Class"]);
				}
				else
				{
					description = String.Format("Any character in \"{0}\"", 
						match.Groups["Class"]);
				}
				buffer.Offset += match.Groups[0].Length;
			}
			else
			{
				description = "missing ']' in character class";
			}
			buffer.AddLookup(this, startLoc, buffer.Offset - 1);
		}

		public override string ToString(int offset)
		{
			return(description);
		}	
	}
}
