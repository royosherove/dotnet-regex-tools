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
	/// Summary description for TestInterpretAnchor.
	/// </summary>
	[TestFixture]
	public class TestInterpretGrouping
	{
		public TestInterpretGrouping()
		{
		}

		string Interpret(string regex)
		{
			RegexBuffer buffer = new RegexBuffer(regex);
			RegexExpression expression = new RegexExpression(buffer);
			string output = expression.ToString(0);
			return output;
		}

		[Test]
		public void TestCapture()
		{
			string output = Interpret("(abc)");
			Assertion.AssertEquals("Capture\r\n  abc\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestNamedCapture()
		{
			string output = Interpret("(?<L>abc)");
			Assertion.AssertEquals("Capture to <L>\r\n  abc\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestNonCapture()
		{
			string output = Interpret("(?:abc)");
			Assertion.AssertEquals("Non-capturing Group\r\n  abc\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestAlternation()
		{
			string output = Interpret("(a|b)");
			Assertion.AssertEquals("Capture\r\n  a\r\n    or\r\n  b\r\nEnd Capture\r\n", output);
		}

			// lookahead/lookbehind

		[Test]
		public void TestPositiveLookahead()
		{
			string output = Interpret("(?=a)");
			Assertion.AssertEquals("zero-width positive lookahead\r\n  a\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestNegativeLookahead()
		{
			string output = Interpret("(?!b)");
			Assertion.AssertEquals("zero-width negative lookahead\r\n  b\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestPositiveLookbehind()
		{
			string output = Interpret("(?<=c)");
			Assertion.AssertEquals("zero-width positive lookbehind\r\n  c\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestNegativeLookbehind()
		{
			string output = Interpret("(?<!d)");
			Assertion.AssertEquals("zero-width negative lookbehind\r\n  d\r\nEnd Capture\r\n", output);
		}

			// Conditionals
		[Test]
		public void TestConditionalExpression()
		{
			string output = Interpret("(?(abc)yes|no)");
			Assertion.AssertEquals("Conditional Subexpression\r\n  if: abc\r\n  match: yes\r\n  else match: no\r\nEnd Capture\r\n", output);
		}

		[Test]
		public void TestConditionalNamed()
		{
			string output = Interpret("(?(<V>)yes|no)");
			Assertion.AssertEquals("Conditional Subexpression\r\n  if: <V>\r\n  match: yes\r\n  else match: no\r\nEnd Capture\r\n", output);
		}
	}
}

#if fred

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
