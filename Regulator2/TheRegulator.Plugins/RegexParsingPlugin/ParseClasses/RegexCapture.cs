/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexCapture.
	/// </summary>
	public class RegexCapture: RegexItem
	{
		RegexItem expression;
		string description = "Capture";
		int startLocation;
		static Hashtable optionNames = new Hashtable();

		static RegexCapture()
		{
			optionNames.Add("i", "Ignore Case");
			optionNames.Add("-i", "Ignore Case Off");
			optionNames.Add("m", "Multiline");
			optionNames.Add("-m", "Multiline Off");
			optionNames.Add("n", "Explicit Capture");
			optionNames.Add("-n", "Explicit Capture Off");
			optionNames.Add("s", "Singleline");
			optionNames.Add("-s", "Singleline Off");
			optionNames.Add("x", "Ignore Whitespace");
			optionNames.Add("-x", "Ignore Whitespace Off");
		}

		public RegexCapture(RegexBuffer buffer)
		{
			startLocation = buffer.Offset;
			buffer.MoveNext();

				// we're not in a series of normal characters, so clear
			buffer.ClearInSeries();

				// if the first character of the capture is a '?',
				// we need to decode what comes after it.
			if (buffer.Current == '?')
			{
				bool decoded = CheckNamed(buffer);

				if (!decoded)
				{
					decoded = CheckBalancedGroup(buffer);
				}

				if (!decoded)
				{
					decoded = CheckNonCapturing(buffer);
				}

				if (!decoded)
				{
					decoded = CheckOptions(buffer);
				}

				if (!decoded)
				{
					decoded = CheckLookahead(buffer);
				}

				if (!decoded)
				{
					decoded = CheckNonBacktracking(buffer);
				}
			
				if (!decoded)
				{
					decoded = CheckConditional(buffer);
				}
			
			}
			else
				// plain old capture...
			{
				if (!HandlePlainOldCapture(buffer))
				{
					throw new Exception(
						String.Format("Unrecognized capture: {0}", buffer.String));
				}
			}
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

		bool HandlePlainOldCapture(RegexBuffer buffer)
		{
				// we're already at the expression. Just create a new
				// expression, and make sure that we're at a ")" when 
				// we're done
			if (buffer.ExplicitCapture)
			{
				description = String.Format("Non-capturing Group");
			}

			expression = new RegexExpression(buffer);
			CheckClosingParen(buffer);
			return true;
		}

		bool CheckNamed(RegexBuffer buffer)
		{
			Regex regex;
			Match match;

			// look for ?<Name> or ?'Name' syntax...
			regex = new Regex(@"
				        ^                         # anchor to start of string
						\?(\<|')                  # ?< or ?'
						(?<Name>[a-zA-Z0-9]+?)    # Capture name
						(\>|')                    # ?> or ?'
						(?<Rest>.+)               # The rest of the string
						",
				RegexOptions.IgnorePatternWhitespace);

			match = regex.Match(buffer.String);
			if (match.Success)
			{
				description = String.Format("Capture to <{0}>", match.Groups["Name"]);
				
					// advance buffer to the rest of the expression
				buffer.Offset += match.Groups["Rest"].Index;
				expression = new RegexExpression(buffer);

				CheckClosingParen(buffer);
				return true;
			}
			return false;
		}

		bool CheckNonCapturing(RegexBuffer buffer)
		{
			// Look for non-capturing ?:

			Regex regex = new Regex(@"
				        ^                         # anchor to start of string
						\?:
						(?<Rest>.+)             # The rest of the expression
						",
				RegexOptions.IgnorePatternWhitespace);
			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				description = String.Format("Non-capturing Group");

				buffer.Offset += match.Groups["Rest"].Index;
				expression = new RegexExpression(buffer);

				this.CheckClosingParen(buffer);
				return true;
			}
			return false;
		}


		bool CheckBalancedGroup(RegexBuffer buffer)
		{
			// look for ?<Name1-Name2> or ?'Name1-Name2' syntax...
			// look for ?<Name> or ?'Name' syntax...
			Regex regex = new Regex(@"
				        ^                         # anchor to start of string
						\?[\<|']                  # ?< or ?'
						(?<Name1>[a-zA-Z]+?)       # Capture name1
						-
						(?<Name2>[a-zA-Z]+?)       # Capture name2
						[\>|']                    # ?> or ?'
						(?<Rest>.+)               # The rest of the expression
						",
				RegexOptions.IgnorePatternWhitespace);

			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				description = String.Format("Balancing Group <{0}>-<{1}>", match.Groups["Name1"], match.Groups["Name2"]);
				buffer.Offset += match.Groups["Rest"].Index;
				expression = new RegexExpression(buffer);
				CheckClosingParen(buffer);
				return true;
			}
			return false;
		}


		bool CheckOptions(RegexBuffer buffer)
		{
			// look for ?imnsx-imnsx:
			Regex regex = new Regex(@"
				        ^                         # anchor to start of string
						\?(?<Options>[imnsx-]+):
						",
				RegexOptions.IgnorePatternWhitespace);

			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				string option = match.Groups["Options"].Value;
				description = String.Format("Set options to {0}", 
					optionNames[option]);
				expression = null;
				buffer.Offset += match.Groups[0].Length;
				return true;
			}
			return false;
		}

		bool CheckLookahead(RegexBuffer buffer)
		{
			Regex regex = new Regex(@"
				        ^                         # anchor to start of string
						\?
						(?<Assertion><=|<!|=|!)   # assertion char
						(?<Rest>.+)               # The rest of the expression
						",
				RegexOptions.IgnorePatternWhitespace);

			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				switch (match.Groups["Assertion"].Value)
				{
					case "=":
						description = "zero-width positive lookahead";
						break;

					case "!":
						description = "zero-width negative lookahead";
						break;

					case "<=":
						description = "zero-width positive lookbehind";
						break;

					case "<!":
						description = "zero-width negative lookbehind";
						break;
				}
				buffer.Offset += match.Groups["Rest"].Index;
				expression = new RegexExpression(buffer);
				CheckClosingParen(buffer);
				return true;
			}
			return false;
		}

		bool CheckNonBacktracking(RegexBuffer buffer)
		{
			// Look for non-backtracking sub-expression ?>

			Regex regex = new Regex(@"
				        ^                         # anchor to start of string
						\?\>
						(?<Rest>.+)             # The rest of the expression
						",
				RegexOptions.IgnorePatternWhitespace);
			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				description = String.Format("Non-backtracking subexpressio");

				buffer.Offset += match.Groups["Rest"].Index;
				expression = new RegexExpression(buffer);

				this.CheckClosingParen(buffer);
				return true;
			}
			return false;
		}

		bool CheckConditional(RegexBuffer buffer)
		{
			// Look for conditional (?(name)yesmatch|nomatch)
			// (name can also be an expression)

			Regex regex = new Regex(@"
				        ^                         # anchor to start of string
						\?\(
						(?<Rest>.+)             # The rest of the expression
						",
				RegexOptions.IgnorePatternWhitespace);
			Match match = regex.Match(buffer.String);
			if (match.Success)
			{
				description = String.Format("Conditional Subexpression");

				buffer.Offset += match.Groups["Rest"].Index;
				expression = new RegexConditional(buffer);

				return true;
			}
			return false;
		}

		public override string ToString(int offset)
		{
			string result = description;
			if (expression != null)
			{
				result += "\r\n" + expression.ToString(offset + 2) + 
				new String(' ', offset) + "End Capture";
			}
			return result;
		}
	}
}
