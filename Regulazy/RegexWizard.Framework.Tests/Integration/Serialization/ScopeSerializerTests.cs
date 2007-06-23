using NUnit.Framework;

namespace RegexWizard.Framework.Tests.Integration.Serialization
{
    [TestFixture,Ignore()]
    public class ScopeSerializerTests
    {
        private const string TEST_FILE_NAME = @"test.a";

        [Test]
        public void SimpleScope()
        {
            string filename = TEST_FILE_NAME;
            Scope s = new  Scope("a");
            s.Save<Scope>(filename);

            Scope loaded = Scope.Load<Scope>(filename);
            Assert.IsTrue(s.Equals(loaded));
        }
        
        [Test]
        public void NonFlatScope()
        {
            string filename = TEST_FILE_NAME;
            Scope s = new  Scope("abc");
            s.DefineInnerScope(1, 1);
            s.Save<Scope>(filename);

            Scope loaded = Scope.Load<Scope>(filename);
            Assert.AreEqual(3,loaded.GetInnerScopes().Length);
        }
        
        [Test]
        public void NonFlatScope2LevelsDeep()
        {
            string filename = TEST_FILE_NAME;
            Scope s = new  Scope("abcd");
            s.DefineInnerScope(1, 2).DefineInnerScope(2,1);
            s.Save<Scope>(filename);

            Scope loaded = Scope.Load<Scope>(filename);
            Assert.IsNotNull(loaded.InnerMiddleScope.InnerLeftScope);
            Assert.IsNotNull(loaded.InnerMiddleScope.InnerRightScope);
        }


        [Test]
        public void SimpleScopeWith1Suggestion()
        {
            string filename = TEST_FILE_NAME;
            Scope s = new Scope("a");
            Suggestion sug = new Suggestion("a","b");
            s.Suggestions.Add(sug);
            s.Save<Scope>(filename);

            Scope loaded = Scope.Load<Scope>(filename);
            Assert.AreEqual(sug, loaded.Suggestions[0]);
        }
        
        
        [Test]
        public void SimpleScopeWith1SuggestionInInner()
        {
            string filename = TEST_FILE_NAME;
            Scope s = new Scope("abc");
            Suggestion sug = new Suggestion("a","b");
            s.DefineInnerScope(1,1).Suggestions.Add(sug);
            s.Save<Scope>(filename);

            Scope loaded = Scope.Load<Scope>(filename);
            Assert.AreEqual(sug, loaded.InnerMiddleScope.Suggestions[0]);
        }
    }
}
