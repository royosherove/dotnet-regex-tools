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
	/// Summary description for RegexQuantifier.
	/// </summary>
	public class RegexQuantifier: RegexItem
	{
		string description;

		public RegexQuantifier(RegexBuffer buffer)
		{
			int startLoc = buffer.Offset;
			buffer.MoveNext();

			Regex regex;
			Match match;

				// look for "n}", "n,}", or "n,m}"
			regex = new Regex(@"(?<n>\d+)(?<Comma>,?)(?<m>\d*)\}");

			match = regex.Match(buffer.String);
			if (match.Success)
			{
				if (match.Groups["m"].Length != 0)
				{
					description = String.Format("At least {0}, but not more than {1} times", 
						match.Groups["n"], match.Groups["m"]);
				}
				else if (match.Groups["Comma"].Length != 0)
				{
					description = String.Format("At least {0} times", 
						match.Groups["n"]);
				}
				else
				{
					description = String.Format("Exactly {0} times", 
						match.Groups["n"]);
				}
				buffer.Offset += match.Groups[0].Length;

				if (!buffer.AtEnd && buffer.Current == '?')
				{
					description += " (non-greedy)";
					buffer.MoveNext();
				}
			}
			else
			{
				description = "missing '}' in quantifier";
			}
			buffer.AddLookup(this, startLoc, buffer.Offset - 1);
		}

		public override string ToString(int offset)
		{
			return(description);
		}		
	}
}
