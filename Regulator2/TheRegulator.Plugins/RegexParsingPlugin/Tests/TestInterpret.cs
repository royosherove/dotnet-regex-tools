/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */

using System;
using NUnit.Framework;

namespace RegexTest
{
	/// <summary>
	/// Summary description for TestInterpret.
	/// </summary>
	[TestFixture]
	public class TestInterpret
	{
		public TestInterpret()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		string Interpret(string regex)
		{
			return RegexExpression.Interpret(regex);
		}

		[Test]
		public void TestNormalChars()
		{
			string output = Interpret("Test");
			Assertion.AssertEquals("Test\r\n", output);
		}


		[Test]
		public void TestCharacterShortcuts()
		{
			string output = Interpret(@"\a");
			Assertion.AssertEquals("A bell (alarm) \\u0007 \r\n", output);

			output = Interpret(@"\t");
			Assertion.AssertEquals("A tab \\u0009 \r\n", output);
			
			output = Interpret(@"\r");
			Assertion.AssertEquals("A carriage return \\u000D \r\n", output);
			
			output = Interpret(@"\v");
			Assertion.AssertEquals("A vertical tab \\u000B \r\n", output);
			
			output = Interpret(@"\f");
			Assertion.AssertEquals("A form feed \\u000C \r\n", output);
			
			output = Interpret(@"\n");
			Assertion.AssertEquals("A new line \\u000A \r\n", output);

			output = Interpret(@"\e");
			Assertion.AssertEquals("An escape \\u001B \r\n", output);

			output = Interpret(@"\xFF");
			Assertion.AssertEquals("Hex FF\r\n", output);

			output = Interpret(@"\cC");
			Assertion.AssertEquals("CTRL-C\r\n", output);

			output = Interpret(@"\u1234");
			Assertion.AssertEquals("Unicode 1234\r\n", output);
		}

		[Test]
		public void TestCharacterGroup()
		{
			string output = Interpret("[abcdef]");
			Assertion.AssertEquals("Any character in \"abcdef\"\r\n", output);
		}

		[Test]
		public void TestCharacterGroupNegated()
		{
			string output = Interpret("[^abcdef]");
			Assertion.AssertEquals("Any character not in \"abcdef\"\r\n", output);
		}

		[Test]
		public void TestCharacterPeriod()
		{
			string output = Interpret(".");
			Assertion.AssertEquals(". (any character)\r\n", output);
		}

		[Test]
		public void TestCharacterWord()
		{
			string output = Interpret(@"\w");
			Assertion.AssertEquals("Any word character \r\n", output);
		}

		[Test]
		public void TestCharacterNonWord()
		{
			string output = Interpret(@"\W");
			Assertion.AssertEquals("Any non-word character \r\n", output);
		}
		
		[Test]
		public void TestCharacterWhitespace()
		{
			string output = Interpret(@"\s");
			Assertion.AssertEquals("Any whitespace character \r\n", output);
		}

		
		[Test]
		public void TestCharacterNonWhitespace()
		{
			string output = Interpret(@"\S");
			Assertion.AssertEquals("Any non-whitespace character \r\n", output);
		}
		
		[Test]
		public void TestCharacterDigit()
		{
			string output = Interpret(@"\d");
			Assertion.AssertEquals("Any digit \r\n", output);
		}
		
		[Test]
		public void TestCharacterNonDigit()
		{
			string output = Interpret(@"\D");
			Assertion.AssertEquals("Any non-digit \r\n", output);
		}

		[Test]
		public void TestQuantifierPlus()
		{
			string output = Interpret(@"+");
			Assertion.AssertEquals("+ (one or more times)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierStar()
		{
			string output = Interpret(@"*");
			Assertion.AssertEquals("* (zero or more times)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierQuestion()
		{
			string output = Interpret(@"?");
			Assertion.AssertEquals("? (zero or one time)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierFromNToM()
		{
			string output = Interpret(@"{1,2}");
			Assertion.AssertEquals("At least 1, but not more than 2 times\r\n", output);
		}
		
		[Test]
		public void TestQuantifierAtLeastN()
		{
			string output = Interpret(@"{5,}");
			Assertion.AssertEquals("At least 5 times\r\n", output);
		}		

		[Test]
		public void TestQuantifierExactlyN()
		{
			string output = Interpret(@"{12}");
			Assertion.AssertEquals("Exactly 12 times\r\n", output);
		}

		[Test]
		public void TestQuantifierPlusNonGreedy()
		{
			string output = Interpret(@"+?");
			Assertion.AssertEquals("+ (one or more times) (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierStarNonGreedy()
		{
			string output = Interpret(@"*?");
			Assertion.AssertEquals("* (zero or more times) (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierQuestionNonGreedy()
		{
			string output = Interpret(@"??");
			Assertion.AssertEquals("? (zero or one time) (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierFromNToMNonGreedy()
		{
			string output = Interpret(@"{1,2}?");
			Assertion.AssertEquals("At least 1, but not more than 2 times (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierAtLeastNNonGreedy()
		{
			string output = Interpret(@"{5,}?");
			Assertion.AssertEquals("At least 5 times (non-greedy)\r\n", output);
		}		

		[Test]
		public void TestQuantifierExactlyNNonGreedy()
		{
			string output = Interpret(@"{12}?");
			Assertion.AssertEquals("Exactly 12 times (non-greedy)\r\n", output);
		}
	}
}

#if fred


= "Beginning of string - ^";
= "Beginning, multiline - \\A";
= "End of string - $";
= "End, multiline - \\Z";
= "End, multiline -  \\z";
= "Word boundary - \\b";
= "Non-word boundary - \\B";
= "Grouping";
= "Capture - (<exp>)";
= "Named capture - (?<<name>>x)";
= "Non-capture - (?:<exp>)";
= "Alternation - (<x>|<y>)";
= "Zero-Width";
= "Positive Lookahead - (?=<x>)";
= "Negative Lookahead - (?!<x>)";
= "Positive Lookbehind - (?<=<x>)";
= "Negative Lookbehind - (?<!<x>)";
= "Conditionals";
= "Expression - (?(<exp>)yes|no)";
= "Named - (?(<name>)yes|no)";
= "Options";
= "Ignore Case - (?i)";
= "Ignore Case off - (?-i)";
= "Multline - (?m)";
= "Multiline off - (?-m)";
= "Explicit Capture - (?c)";
= "Explicit Capture off - (?-c)";
= "Singleline - (?s)";
= "Singleline off - (?-s)";
= "Ignore Whitespace - (?x)";
= "Ignore Whitespace off - (?-x)";


#endif
