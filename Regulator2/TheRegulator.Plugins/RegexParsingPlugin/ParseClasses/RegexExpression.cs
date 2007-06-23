/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexExpression.
	/// </summary>
	public class RegexExpression: RegexItem
	{
		ArrayList	items = new ArrayList();
		public static string Interpret(string regex)
		{
			RegexBuffer buffer = new RegexBuffer(regex);
			RegexExpression expression = new RegexExpression(buffer);
			string output = expression.ToString(0);
			return output;
		}

		public RegexExpression(RegexBuffer buffer)
		{
			Parse(buffer);
		}
	
		public ArrayList Items
		{
			get
			{
				return items;
			}
		}

		public override string ToString(int indent)
		{
			StringBuilder buf = new StringBuilder();
			StringBuilder bufChar = new StringBuilder();

			foreach (RegexItem item in items)
			{
				RegexCharacter regexChar = item as RegexCharacter;
				if (regexChar != null && !regexChar.Special)
				{
					bufChar.Append(regexChar.ToString(indent));
				}
				else
				{
					// add any buffered chars...
					if (bufChar.Length != 0)
					{
						buf.Append(new String(' ', indent));
						buf.Append(bufChar.ToString() + "\r\n");
						bufChar = new StringBuilder();
					}
					buf.Append(new String(' ', indent));
					string itemString = item.ToString(indent);
					if (itemString.Length != 0)
					{
						buf.Append(itemString);
						Regex newLineAlready = new Regex(@"\r\n$");
						if (!newLineAlready.IsMatch(itemString))
						{
							buf.Append("\r\n");
						}
					}
				}
			}
			if (bufChar.Length != 0)
			{
				buf.Append(new String(' ', indent));
				buf.Append(bufChar.ToString() + "\r\n");
				bufChar = new StringBuilder();
			}
			return buf.ToString();
		}
        
			// eat the whole comment until the end of line...
		void EatComment(RegexBuffer buffer)
		{
			while (buffer.Current != '\r')
			{
				buffer.MoveNext();
			}
		}

		void Parse(RegexBuffer buffer)
		{
			while (!buffer.AtEnd)
			{
					// if this regex ignores whitespace, we need to ignore these
				if (buffer.IgnorePatternWhitespace &&
					((buffer.Current == ' ') ||
					(buffer.Current == '\r') ||
					(buffer.Current == '\n') ||
					(buffer.Current == '\t')))
				{
					buffer.MoveNext();
				}
				else
				{
					switch (buffer.Current)
					{
						case '(':
							items.Add(new RegexCapture(buffer));
							break;

						case ')':
							// end of closure; just return.
							return;

						case '[':
							items.Add(new RegexCharClass(buffer));
							break;

						case '{':
							items.Add(new RegexQuantifier(buffer));
							break;

						case '|':
							items.Add(new RegexAlternate(buffer));
							break;

						case '\\':
							items.Add(new RegexCharacter(buffer));
							break;

						case '#':
							if (buffer.IgnorePatternWhitespace)
							{
								EatComment(buffer);
							}
							else
							{
								items.Add(new RegexCharacter(buffer));
							}
							break;

						default:
							items.Add(new RegexCharacter(buffer));
							break;
					}
				}
			}
		}
	}
}
