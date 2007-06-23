using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class HTMLInputFinderTests
    {
        HTMLInputFinder finder;

        [SetUp]
        public void Setup()
        {
            finder  = new HTMLInputFinder();
        }

        [Test]
        public void FindWithQuotesNewLinesAndExtraSpaces()
        {
            string fullHTML = @"<h4 class=""blogpostheader""><a id=""_ctl0__ctl1_bcr_aggposts___posts___posts__ctl1_titlelink"" href=""/erobillard/archive/2006/09/21/_2200_develop-without-borders_2c002200_-and-parkdale-legal-clinic.aspx"">&quot;develop without borders,&quot; and parkdale legal clinic</a></h4>
			<div class=""blogpostcontent"">
				at last night&#39;s tspug meeting,one of our new members was kevin smith of the parkdale legal clinic here in toronto. kevin told us apretty cool contest going on, i&#39;d love to see someone take him up on it.i&#39;ll let kevin describe what it&#39;s...
			</div>";
            
            
            string partialSearchHTML = @"<h4 class=blogpostheader><a id=_ctl0__ctl1_bcr_aggposts___posts___posts__ctl1_titlelink 
href=""/erobillard/archive/2006/09/21/_2200_develop-without-borders_2c002200_-and-parkdale-legal-clinic.aspx"">""develop 
without borders,"" and parkdale legal clinic</a></h4>
<div class=blogpostcontent>at last night's tspug meeting,one of our new members 
was kevin smith of the parkdale legal clinic here in toronto. kevin told us 
apretty cool contest going on, i'd love to see someone take him up on it.i'll 
let kevin describe what it's... </div>";

            finder.SearchTargetHTML = fullHTML;
            finder.SearchFor = partialSearchHTML;
            Assert.IsTrue(finder.Find());
            Assert.AreEqual(0,finder.StartIndex);
            Assert.AreEqual(fullHTML.Length,finder.Length);
        }
        
        [Test]
        public void FindWithQuotesNewLinesAndExtraSpaces_CaseInsensitive()
        {
            string fullHTML = @"<H4 class=""blogpostheader""><a id=""_ctl0__ctl1_bcr_aggposts___posts___posts__ctl1_titlelink"" href=""/erobillard/archive/2006/09/21/_2200_develop-without-borders_2c002200_-and-parkdale-legal-clinic.aspx"">&quot;develop without borders,&quot; and parkdale legal clinic</a></h4>
			<div class=""blogpostcontent"">
				at last night&#39;s tspug meeting,one of our new members was kevin smith of the parkdale legal clinic here in toronto. kevin told us apretty cool contest going on, i&#39;d love to see someone take him up on it.i&#39;ll let kevin describe what it&#39;s...
			</div>";
            
            
            string partialSearchHTML = @"<H4 CLASS=blogpostheader><a id=_ctl0__ctl1_bcr_aggposts___posts___posts__ctl1_titlelink 
href=""/erobillard/archive/2006/09/21/_2200_develop-without-borders_2c002200_-and-parkdale-legal-clinic.aspx"">""develop 
without borders,"" and parkdale legal clinic</a></h4>
<div class=blogpostcontent>at last night's tspug meeting,one of our new members 
was kevin smith of the parkdale legal clinic here in toronto. kevin told us 
apretty cool contest going on, i'd love to see someone take him up on it.i'll 
let kevin describe what it's... </div>";

            finder.SearchTargetHTML = fullHTML;
            finder.SearchFor = partialSearchHTML;
            Assert.IsTrue(finder.Find());
            Assert.AreEqual(0,finder.StartIndex);
            Assert.AreEqual(fullHTML.Length,finder.Length);
            Assert.AreEqual(fullHTML,finder.FoundHTML);
        }
        
        [Test]
        public void FindWithQuotesNewLinesAndExtraSpaces_NotFoundOnWrongString()
        {
            string fullHTML = @"<h4 class=""blogpostheader""><a id=""_ctl0__ctl1_bcr_aggposts___posts___posts__ctl1_titlelink"" href=""/erobillard/archive/2006/09/21/_2200_develop-without-borders_2c002200_-and-parkdale-legal-clinic.aspx"">&quot;develop without borders,&quot; and parkdale legal clinic</a></h4>
			<div class=""blogpostcontent"">
				at last night&#39;s tspug meeting,one of our new members was kevin smith of the parkdale legal clinic here in toronto. kevin told us apretty cool contest going on, i&#39;d love to see someone take him up on it.i&#39;ll let kevin describe what it&#39;s...
			</div>";
            
            
            string partialSearchHTML = @"<h3 whatever";

            finder.SearchTargetHTML = fullHTML;
            finder.SearchFor = partialSearchHTML;
            Assert.IsFalse(finder.Find());
            }

    }
}
