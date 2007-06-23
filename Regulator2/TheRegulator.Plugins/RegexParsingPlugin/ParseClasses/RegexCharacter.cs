/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace RegexTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class RegexCharacter: RegexItem
	{
		string character;
		bool special;

			// used to coalesce single-character items together
		public RegexCharacter(string characters)
		{
			this.character = characters;
		}

		public RegexCharacter(RegexBuffer buffer)
		{
			int startLoc = buffer.Offset;
			bool quantifier = false;

			switch (buffer.Current)
			{
				case '.':
					character = ". (any character)";
					buffer.MoveNext();
					special = true;
					break;

				case '+':
					character = "+ (one or more times)";
					buffer.MoveNext();
					special = true;
					quantifier = true;
					break;

				case '*':
					character = "* (zero or more times)";
					buffer.MoveNext();
					special = true;
					quantifier = true;
					break;

				case '?':
					character = "? (zero or one time)";
					buffer.MoveNext();
					special = true;
					quantifier = true;
					break;

				case '^':
					character = "^ (anchor to start of string)";
					buffer.MoveNext();
					break;

				case '$':
					character = "$ (anchor to end of string)";
					buffer.MoveNext();
					break;

				case ' ':
					character = "' ' (space)";
					buffer.MoveNext();
					break;

				case '\\':
					DecodeEscape(buffer);
					break;

				default:
					character = buffer.Current.ToString();
					buffer.MoveNext();
					special = false;
					break;
			}
			if (quantifier)
			{
				if (!buffer.AtEnd && buffer.Current == '?')
				{
					character += " (non-greedy)";
					buffer.MoveNext();
				}
			}
			buffer.AddLookup(this, startLoc, buffer.Offset - 1, (character.Length == 1));
		}

		static Hashtable escaped = new Hashtable();
		static RegexCharacter()
		{
				// character escapes
			escaped.Add('a', @"A bell (alarm) \u0007 ");
			escaped.Add('b', @"Word boundary between //w and //W");
			escaped.Add('B', @"Not at a word boundary between //w and //W");
			escaped.Add('t', @"A tab \u0009 ");
			escaped.Add('r', @"A carriage return \u000D ");
			escaped.Add('v', @"A vertical tab \u000B ");
			escaped.Add('f', @"A form feed \u000C ");
			escaped.Add('n', @"A new line \u000A ");
			escaped.Add('e', @"An escape \u001B ");

				// character classes
			escaped.Add('w', "Any word character ");
			escaped.Add('W', "Any non-word character ");
			escaped.Add('s', "Any whitespace character ");
			escaped.Add('S', "Any non-whitespace character ");
			escaped.Add('d', "Any digit ");
			escaped.Add('D', "Any non-digit ");

				// anchors
			escaped.Add('A', "Anchor to start of string (ignore multiline)");
			escaped.Add('Z', "Anchor to end of string or before \\n (ignore multiline)");
			escaped.Add('z', "Anchor to end of string (ignore multiline)");
		}

		void DecodeEscape(RegexBuffer buffer)
		{
			buffer.MoveNext();

			character = (string) escaped[buffer.Current];
			if (character == null)
			{
				bool decoded = false;

				decoded = CheckBackReference(buffer);

				if (!decoded)
				{
						// TODO: Handle other items below:
					switch (buffer.Current)
					{
						case 'u':
							buffer.MoveNext();
							string unicode = buffer.String.Substring(0, 4);
							character = "Unicode " + unicode;
							buffer.Offset += 4;
							break;

						case ' ':
							character = "' ' (space)";
							special = false;
							buffer.MoveNext();
							break;

						case 'c':
							buffer.MoveNext();
							character = "CTRL-" + buffer.Current;
							buffer.MoveNext();
							break;

						case 'x':
							buffer.MoveNext();
							string number = buffer.String.Substring(0, 2);
							character = "Hex " + number;
							buffer.Offset += 2;
							break;

						default:
							character = new String(buffer.Current, 1);
							special = false;
							buffer.MoveNext();
							break;
					}
				}
			}
			else
			{
				special = true;
				buffer.MoveNext();
			}
		}

		bool CheckBackReference(RegexBuffer buffer)
		{
				// look for \k<name>
			Regex regex = new Regex(@"
						k\<(?<Name>.+?)\>
						",
				RegexOptions.IgnorePatternWhitespace);

			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				special = true;
				this.character = String.Format("Backreference to match: {0}", match.Groups["Name"]);
				buffer.Offset += match.Groups[0].Length;
				return true;
			}
			return false;
		}

#if fred
\040 An ASCII character as octal (up to three digits); numbers with no leading zero are backreferences if they have only one digit or if they correspond to a capturing group number. (See Backreferences.) The character \040 represents a space. 
\x20 An ASCII character using hexadecimal representation (exactly two digits). 
\cC An ASCII control character; for example, \cC is control-C. 
\u0020 A Unicode character using hexadecimal representation (exactly four digits). 
\ When followed by a character that is not recognized as an escaped character, matches that character. For example, \* is the same as \x2A. 

#endif

		public override string ToString(int offset)
		{
			return character;
		}

			// true if not a normal character...
		public bool Special
		{
			get
			{
				return special;
			}
		}

	}
}
