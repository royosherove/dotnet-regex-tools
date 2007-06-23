using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class AutoAdvisorTests
    {
        RegexAdvisor auto = null;

        [SetUp]
        public void Setup()
        {
            auto = new RegexAdvisor();
        }

        [Test]
        public void NoAutoSuggestions_NothingHappens()
        {
            Scope root = new Scope("abc");
            auto.AutoScope(root);
            Assert.AreEqual(0,root.GetInnerScopes().Length);
        }
        
        [Test]
        public void SingleValidSuggestion_AutoScoped()
        {
            Scope root = new Scope("abc");
            auto.LearnAutomaticRules(new Suggestion("b", "the letter b"));
            auto.AutoScope(root);
            
            Assert.AreEqual(3,root.GetInnerScopes().Length);
        }
        
        [Test]
        public void SingleValidSuggestion_SuggestionAddedToNewScope()
        {
            Scope root = new Scope("abc");
            Suggestion Bsuggestion = new Suggestion("b", "the letter b");
            auto.LearnAutomaticRules(Bsuggestion);
            auto.AutoScope(root);
            
            Assert.AreEqual(Bsuggestion,root.InnerMiddleScope.Suggestions[0]);
        }
        
        [Test]
        public void SingleValidSuggestion_SuggestionAddedToNewScopeImplicitly()
        {
            Scope root = new Scope("abc");
            Suggestion Bsuggestion = new Suggestion("b", "the letter b");
            auto.LearnAutomaticRules(Bsuggestion);
            auto.AutoScope(root);
            
            Assert.IsTrue(root.InnerMiddleScope.IsExplicit);
        }
        
        [Test]
        public void SingleValidSuggestion_SuggestionAddedToNewScopeOnLeft_usesMatchIndex()
        {
            Scope root = new Scope("abc");
            Suggestion Asuggestion = new Suggestion("a", "the letter a");
            auto.LearnAutomaticRules(Asuggestion);
            auto.AutoScope(root);
            
            Assert.AreEqual(Asuggestion,root.InnerLeftScope.Suggestions[0]);
        }
        
        [Test]
        public void SingleValidSuggestion_SuggestionAddedToNewScopeOnLeft_usesMatchLength()
        {
            Scope root = new Scope("abc");
            Suggestion Asuggestion = new Suggestion("ab", "the letters ab");
            auto.LearnAutomaticRules(Asuggestion);
            auto.AutoScope(root);
            
            Assert.AreEqual(2,root.InnerLeftScope.Length);
        }
        
        [Test]
        public void TwoValidMatches_AutoScoped_UsesExistingScopeIfExists()
        {
            Scope root = new Scope("abc");
            Suggestion Asuggestion = new Suggestion("ab", "the letters ab");
            Suggestion Csuggestion = new Suggestion("c", "the letter c");
            auto.LearnAutomaticRules(Asuggestion);
            auto.LearnAutomaticRules(Csuggestion);
            auto.AutoScope(root);
            
            Assert.AreEqual(Asuggestion, root.InnerLeftScope.Suggestions[0]);
            Assert.AreEqual(Csuggestion, root.InnerRightScope.Suggestions[0]);
        }
        
        [Test]
        public void TwoValidMatches_AutoScoped_TurnsExistingScopeToExplicit()
        {
            Scope root = new Scope("abc");
            Suggestion Asuggestion = new Suggestion("ab", "the letters ab");
            Suggestion Csuggestion = new Suggestion("c", "the letter c");
            auto.LearnAutomaticRules(Asuggestion);
            auto.LearnAutomaticRules(Csuggestion);
            auto.AutoScope(root);
            
            Assert.AreEqual(true, root.InnerRightScope.IsExplicit);
        }
        
        [Test]
        public void ThreeValidMatches_3rdOneMatchesBetween1And3_3rdIsIgnored()
        {
            Scope root = new Scope("abc");
            Suggestion ABsuggestion = new Suggestion("ab", "the letters ab");
            Suggestion Csuggestion = new Suggestion("c", "the letter c");
            Suggestion BCsuggestion = new Suggestion("bc", "the letters bc");
            auto.LearnAutomaticRules(ABsuggestion);
            auto.LearnAutomaticRules(Csuggestion);
            auto.LearnAutomaticRules(BCsuggestion);
            
            auto.AutoScope(root);
            
            Assert.AreEqual(ABsuggestion, root.InnerLeftScope.Suggestions[0]);
            Assert.AreEqual(Csuggestion, root.InnerRightScope.Suggestions[0]);
        }
        
        [Test]
        public void ThreeValidMatches_3rdOneMatchesBetween1And3_3rdIsIgnored2()
        {
            Scope root = new Scope("abc");
            Suggestion ABsuggestion = new Suggestion("ab", "the letters ab");
            Suggestion Csuggestion = new Suggestion("c", "the letter c");
            Suggestion BCsuggestion = new Suggestion("bc", "the letters bc");
            Suggestion Bsuggestion = new Suggestion("b", "the letters b");
            auto.LearnAutomaticRules(ABsuggestion);
            auto.LearnAutomaticRules(Csuggestion);
            auto.LearnAutomaticRules(BCsuggestion);
            auto.LearnAutomaticRules(Bsuggestion);
            
            auto.AutoScope(root);
            
            Assert.AreEqual(ABsuggestion, root.InnerLeftScope.Suggestions[0]);
            Assert.AreEqual(Csuggestion, root.InnerRightScope.Suggestions[0]);
        }

        
        [Test]
        public void TwoValidMatches_AutoScoped()
        {
            Scope root = new Scope("abc");
            Suggestion Asuggestion = new Suggestion("a", "the letters ab");
            Suggestion Csuggestion = new Suggestion("c", "the letter c");
            auto.LearnAutomaticRules(Asuggestion);
            auto.LearnAutomaticRules(Csuggestion);
            auto.AutoScope(root);
            
            Assert.AreEqual(Asuggestion, root.InnerLeftScope.Suggestions[0]);
            Assert.AreEqual(Csuggestion, root.InnerRightScope
                                                .InnerRightScope.Suggestions[0]);
        }
        
        [Test]
        public void SingleNONValidSuggestion_NothingHappens()
        {
            Scope root = new Scope("abc");
            auto.LearnAutomaticRules(new Suggestion("D", "the letter D"));
            auto.AutoScope(root);
            
            Assert.AreEqual(0,root.GetInnerScopes().Length);
        }

    }
}
