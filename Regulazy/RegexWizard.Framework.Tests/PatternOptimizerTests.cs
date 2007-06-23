using NUnit.Framework;
using RegexWizard.Framework;

[TestFixture]
public class PatternOptimizerTests
{
    PatternOptimizer opt;

    [SetUp]
    public void Setup()
    {
        opt = new PatternOptimizer();
    }
    
    [Test]
    public void NoPattern()
    {
        string output = opt.Optimize("");
        Assert.AreEqual("",output);
    }
    
    [Test]
    public void LineStartMark_NotInStart_Removed()
    {
        string output = opt.Optimize(@"a^");
        Assert.AreEqual("a",output);
    }
    
    [Test]
    public void LineStartMark_NotInStart_Removed2()
    {
        string output = opt.Optimize(@"a^bc");
        Assert.AreEqual("abc",output);
    }
    
    [Test]
    public void LineEndMark_NotInEnd_Removed()
    {
        string output = opt.Optimize(@"$a");
        Assert.AreEqual("a",output);
    }
    
    [Test]
    public void LineEndMark_NotInEnd_Removed2()
    {
        string output = opt.Optimize(@"ab$c");
        Assert.AreEqual("abc",output);
    }
    
    [Test]
    public void LineEndAndStartMarks_opposite_Removed()
    {
        string output = opt.Optimize(@"$^");
        Assert.AreEqual("",output);
    }
    
    [Test]
    public void QuestionMarkBeforeEndLineInTheMiddle_Removed()
    {
        string output = opt.Optimize(@"a?$b");
        Assert.AreEqual("a?b",output);
    }
    
    [Test]
    public void Multiple1OrMoreTogether_OnlyOneRemains()
    {
        string output = opt.Optimize(@"a++");
        Assert.AreEqual("a+",output);
    }
    
    [Test]
    public void Multiple1OrMoreTogether_OnlyOneRemains2()
    {
        string output = opt.Optimize(@"a+++");
        Assert.AreEqual("a+",output);
    }
    
    [Test]
    public void QuestionBeforePlus_QuestionRemains()
    {
        string output = opt.Optimize(@"a?+");
        Assert.AreEqual("a?",output);
    }
    
    
    [Test]
    public void EndLienBeforeCurlyBrace_OnlyBrace()
    {
        string output = opt.Optimize(@"a${");
        Assert.AreEqual("a{",output);
    }
    
    [Test]
    public void StartLineBeofreQuestion_StartRemains()
    {
        string output = opt.Optimize(@"^?");
        Assert.AreEqual("^",output);
    }
    
    [Test]
    public void EndLineMark_NotThere_AddedToEnd()
    {
        opt.AutoAddLineEndMarks = true;
        string output = opt.Optimize(@"^abc");
        Assert.AreEqual("^abc$",output);
    }
    
    [Test]
    public void StartLineMark_NotThere_AddedToStart()
    {
        opt.AutoAddLineEndMarks = true;
        string output = opt.Optimize(@"abc$");
        Assert.AreEqual("^abc$",output);
    }
    
    
    [Test]
    public void MultipleStarts_RemovedToOne()
    {
        opt.AutoAddLineEndMarks = true;
        string output = opt.Optimize(@"^^a$$");
        Assert.AreEqual("^a$",output);
    }
    
    
    
    
}