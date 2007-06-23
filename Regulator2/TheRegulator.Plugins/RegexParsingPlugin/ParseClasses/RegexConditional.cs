/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexConditional.
	/// </summary>
	public class RegexConditional: RegexItem
	{
		RegexExpression expression;
		RegexExpression yesNo;
		int startLocation;

			// Handle (?(expression)yes|no)
			// when we get called, we're pointing to the first character of the expression
		public RegexConditional(RegexBuffer buffer)
		{
			startLocation = buffer.Offset;

			expression = new RegexExpression(buffer);
			CheckClosingParen(buffer);

			yesNo = new RegexExpression(buffer);
			CheckClosingParen(buffer);

			buffer.AddLookup(this, startLocation, buffer.Offset - 1);
		}

		void CheckClosingParen(RegexBuffer buffer)
		{
			// check for closing ")"
			char current = ' ';
			try
			{
				current = buffer.Current;
			}
				// no closing brace. Set highlight for this capture...
			catch (Exception e)
			{
				buffer.ErrorLocation = startLocation;
				buffer.ErrorLength = 1;
				throw new Exception(
					String.Format("Missing closing ')' in capture"), e);
			}
			if (current != ')')
			{
				throw new Exception(
					String.Format("Unterminated closure at offset {0}",
					              buffer.Offset));
			}
			buffer.Offset++;	// eat closing parenthesis
		}

		public override string ToString(int offset)
		{
			string indent = new String(' ', offset);
			string result;
			result = indent + "if: " + expression.ToString(0);

			result += indent + "match: ";

				// walk through until we find an alternation
			foreach (RegexItem item in yesNo.Items)
			{
				if (item is RegexAlternate)
				{
					result += "\r\n" + indent + "else match: ";				
				}
				else
				{
					result += item.ToString(offset);
				}
			}
			result += "\r\n";
			return result;
		}
	}
}
