
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
	/// Summary description for TestInterpretOptions
	/// </summary>
	[TestFixture]
	public class TestInterpretOptions
	{
		public TestInterpretOptions()
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
		public void TestIgnoreCase()
		{
			string output = Interpret("(?i:)");
			Assertion.AssertEquals("Set options to Ignore Case\r\n", output);
		}

		public void TestIgnoreCaseOff()
		{
			string output = Interpret("(?-i:)");
			Assertion.AssertEquals("Set options to Ignore Case Off\r\n", output);
		}

		public void TestMultiline()
		{
			string output = Interpret("(?m:)");
			Assertion.AssertEquals("Set options to Multiline\r\n", output);
		}

		public void TestMultilineOff()
		{
			string output = Interpret("(?-m:)");
			Assertion.AssertEquals("Set options to Multiline Off\r\n", output);
		}

		public void TestExplicitCapture()
		{
			string output = Interpret("(?n:)");
			Assertion.AssertEquals("Set options to Explicit Capture\r\n", output);
		}

		public void TestExplicitCaptureOff()
		{
			string output = Interpret("(?-n:)");
			Assertion.AssertEquals("Set options to Explicit Capture Off\r\n", output);
		}

		public void TestSingleline()
		{
			string output = Interpret("(?s:)");
			Assertion.AssertEquals("Set options to Singleline\r\n", output);
		}

		public void TestSinglelineOff()
		{
			string output = Interpret("(?-s:)");
			Assertion.AssertEquals("Set options to Singleline Off\r\n", output);
		}

		public void TestIgnoreWhitespace()
		{
			string output = Interpret("(?x:)");
			Assertion.AssertEquals("Set options to Ignore Whitespace\r\n", output);
		}

		public void TestIgnoreWhitespaceOff()
		{
			string output = Interpret("(?-x:)");
			Assertion.AssertEquals("Set options to Ignore Whitespace Off\r\n", output);
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
