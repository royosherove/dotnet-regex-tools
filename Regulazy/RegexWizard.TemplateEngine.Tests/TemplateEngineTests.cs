using System.Text.RegularExpressions;
using NUnit.Framework;

namespace RegexWizard.TemplateEngine.Tests
{
    [TestFixture]
    public class TemplateEngineTests
    {
        TemplateEngine engine = new TemplateEngine();
        TemplateInput input = new TemplateInput();
        [SetUp]
        public void setup()
        {
            engine = new TemplateEngine();
            input = new TemplateInput();
        }
        [Test]
        public void Process_EmptyTemplate_returnsEmptyString()
        {
            string output = engine.Process(input);
            Assert.AreEqual(string.Empty, output);
        }


        [Test]
        public void Process_TemplatewithJustRegex_returnscorrect()
        {
            input.Pattern = "pattern";
            engine.Template = "$REGEX$";

            string output = engine.Process(input);
            Assert.AreEqual("pattern", output);
        }

        [Test]
        public void Process_TemplatewithRegexAndStaticText()
        {
            input.Pattern = "pattern";
            engine.Template = "STATIC TEXT $REGEX$";

            string output = engine.Process(input);
            Assert.AreEqual("STATIC TEXT pattern", output);
        }
        
        [Test]
        public void Process_TemplatewithInputAndStaticText()
        {
            input.Input = "input";
            engine.Template = "STATIC TEXT $INPUT$";

            string output = engine.Process(input);
            Assert.AreEqual("STATIC TEXT input", output);
        }
        
        [Test]
        public void Process_TemplatewithInputThatHasDoubleQuotes()
        {
            input.Input = @"input ""Roy"" ";
            engine.Template = "$INPUT$";

            string output = engine.Process(input);
            Assert.AreEqual(@"input """"Roy"""" ", output);
        }
        
        
        
        [Test]
        public void Process_TemplatewithInputAndStaticText1()
        {
            input.Input = "input1";
            engine.Template = "STATIC TEXT $INPUT$";

            string output = engine.Process(input);
            Assert.AreEqual("STATIC TEXT input1", output);
        }
        
        [Test]
        public void Process_TemplatewithRegexOptionsNone()
        {
            input.RegexOptions = RegexOptions.None;
            engine.Template = "$REGEXOPTIONS_CS$";

            string output = engine.Process(input);
            Assert.AreEqual("RegexOptions.None", output);
        }
        
        [Test]
        public void Process_TemplatewithRegexOptionsNone_VB()
        {
            input.RegexOptions = RegexOptions.None;
            engine.Template = "$REGEXOPTIONS_VB$";

            string output = engine.Process(input);
            Assert.AreEqual("RegexOptions.None", output);
        }
        
        [Test]
        public void Process_TemplatewithRegexOptionsIgnoreCase()
        {
            input.RegexOptions = RegexOptions.IgnoreCase;
            engine.Template = "$REGEXOPTIONS_CS$";

            string output = engine.Process(input);
            Assert.AreEqual("RegexOptions.IgnoreCase", output);
        }
        
        [Test]
        public void Process_Templatewith2RegexOptionsInCSharp()
        {
            input.RegexOptions = RegexOptions.IgnoreCase|RegexOptions.Multiline;
            engine.Template = "$REGEXOPTIONS_CS$";

            string output = engine.Process(input);
            Assert.AreEqual("RegexOptions.IgnoreCase | RegexOptions.Multiline", output);
        }
        
        [Test]
        public void Process_Templatewith2RegexOptionsInVB()
        {
            input.RegexOptions = RegexOptions.IgnoreCase|RegexOptions.Multiline;
            engine.Template = "$REGEXOPTIONS_VB$";

            string output = engine.Process(input);
            Assert.AreEqual("RegexOptions.IgnoreCase Or RegexOptions.Multiline", output);
        }
        
        
        [Test]
        public void Process_FOREACH_GROUP_NoGroups()
        {
            engine.Template = "$FOREACH-GROUP$$END-FOREACH-GROUP$";

            string output = engine.Process(input);
            Assert.AreEqual(string.Empty, output);
        }
        
        [Test]
        public void Process_FOREACH_GROUP_NoGroups2()
        {
            engine.Template = "$FOREACH-GROUP$ $GROUPNAME$ $END-FOREACH-GROUP$";

            string output = engine.Process(input);
            Assert.AreEqual("  ", output);
        }
        
        
        [Test]
        public void Process_FOREACH_GROUP_OneGroup()
        {
            input.Pattern = @"(?<MyGroup>\w+?)";
            engine.Template = "$FOREACH-GROUP$ $GROUPNAME$ $END-FOREACH-GROUP$";

            string output = engine.Process(input);
            Assert.AreEqual(" MyGroup ", output);
        }
        
        
        [Test]
        public void Process_FOREACH_GROUP_TwoGroups()
        {
            input.Pattern = @"(?<Group1>\w+?)(?<Group2>\w+?)";
            engine.Template = "$FOREACH-GROUP$ $GROUPNAME$ $END-FOREACH-GROUP$";

            string output = engine.Process(input);
            Assert.AreEqual(" Group1  Group2 ", output);
        }
        
        [Test]
        public void Process_FOREACH_GROUP_OneGroupMultiLine()
        {
            input.Pattern = @"(?<GroupNameTest>\w+?)";
            engine.Template = 
@"$FOREACH-GROUP$
$GROUPNAME$ 
$END-FOREACH-GROUP$";

            string output = engine.Process(input);
            Assert.AreEqual("\r\nGroupNameTest \r\n", output);
        }
           [Test]
        public void Process_FOREACH_GROUP_NoGroup()
        {
            input.Pattern = @"This is a pattern with no grouping";
            engine.Template = 
@"$FOREACH-GROUP$
$GROUPNAME$ 
$END-FOREACH-GROUP$";

            string output = engine.Process(input);
            Assert.AreEqual("\r\n \r\n", output);
        }
        
        
        [Test]
    public void Process_FullTemplate()
        {
            //all double quotes turned into single quotes
            string template =
                @"public void SampleRegexUsage()
{
	string regex=@'$REGEX$';
	RegexOptions options = $REGEXOPTIONS_CS$;
	string input= @'$INPUT$';
	
	MatchCollection matches = Regex.Matches(input,regex,options);
    foreach (Match match in matches)
	{
		Console.WriteLine(match.Value);
        $FOREACH-GROUP$
			Console.WriteLine('$GROUPNAME$:' + match.Groups['$GROUPNAME$'].Value);
		$END-FOREACH-GROUP$
	}

}";
            
            string expectedOutput=
                                @"public void SampleRegexUsage()
{
	string regex=@'(?<Group1>\w\s(?<Group2>\s+))';
	RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
	string input= @'MyInput';
	
	MatchCollection matches = Regex.Matches(input,regex,options);
    foreach (Match match in matches)
	{
		Console.WriteLine(match.Value);
            Console.WriteLine('Group1:' + match.Groups['Group1'].Value);
			Console.WriteLine('Group2:' + match.Groups['Group2'].Value);
        
	}

}";

            input.Pattern = @"(?<Group1>\w\s(?<Group2>\s+))";
            input.Input = "MyInput";
            input.RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            engine.Template = template;
            string output = engine.Process(input);
            Assert.AreEqual(expectedOutput,output);
        }
    }
}
