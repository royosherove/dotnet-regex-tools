using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class ScopeParserTests
    {
            MockAdvisor mockAdvisor = null;
            ScopeParser parser = null;
            Scope scope = null;

        [SetUp]
        public void Setup()
        {
            mockAdvisor = new MockAdvisor();
            parser = new ScopeParser();
            Scope scope = new Scope("a");
        }


        [Test]
        public void Create()
        {
            ScopeParser s = new ScopeParser();
            Assert.IsNotNull(s);
        }

        [Test]
        public void Visit_OneSuggestionSameCategory_HasOneSuggestion()
        {
            Suggestion sug = makeSuggestion("a", "the letter a");
            mockAdvisor.Suggestions.Add(sug);
            parser.AddAdvisor(mockAdvisor, "SomeCategory");

            scope = makeScope("a");
            scope.MetaData.Category = "SomeCategory";

            parser.Visit(scope);

            Assert.AreSame(sug, scope.Suggestions[0]);
        }

        [Test]
        public void Visit_OneSuggestionSameCategory_VisitsInnerScopesAsWell()
        {
            Suggestion sug = makeSuggestion("a", "the letter a");
            mockAdvisor.Suggestions.Add(sug);
            parser.AddAdvisor(mockAdvisor, "cat");

            scope = makeScope("abc");
            scope.DefineInnerScope(1, 1);
            scope.MetaData.Category = "cat";
            scope.InnerLeftScope.MetaData.Category = "cat";
            scope.InnerMiddleScope.MetaData.Category = "cat";
            scope.InnerRightScope.MetaData.Category = "cat";

            parser.Visit(scope);

            Assert.AreSame(sug, scope.InnerLeftScope.Suggestions[0]);
            Assert.AreSame(sug, scope.InnerMiddleScope.Suggestions[0]);
            Assert.AreSame(sug, scope.InnerRightScope.Suggestions[0]);
        }
        
        [Test]
        public void Visit_ClearsPriorScopeSuggestions()
        {
            Suggestion sug = makeSuggestion("a", "the letter a");
            mockAdvisor.Suggestions.Add(sug);
            parser.AddAdvisor(mockAdvisor, "SomeCategory");

            scope = makeScope("a");
            scope.MetaData.Category = "SomeCategory";

            parser.Visit(scope);
            parser.Visit(scope);

            Assert.AreEqual(1, scope.Suggestions.Count);
        }
        
        [Test]
        public void Visit_OneSuggestionSameCategory_HasOneSuggestion2()
        {
            Suggestion sug = makeSuggestion("b", "the letter a");
            mockAdvisor.Suggestions.Add(sug);
            parser.AddAdvisor(mockAdvisor, "SomeCategory");

            scope = makeScope("b");
            scope.MetaData.Category = "SomeCategory";

            parser.Visit(scope);

            Assert.AreSame(sug, scope.Suggestions[0]);
        }


        [Test]
        public void Visit_OneSuggestionWrongCategoryCategory_NoSuggestions()
        {
            Suggestion sug = makeSuggestion("a", "the letter a");
            mockAdvisor.Suggestions.Add(sug);
            parser.AddAdvisor(mockAdvisor, "SomeCategory");

            scope = makeScope("a");
            scope.MetaData.Category = "OTHERCategory";

            parser.Visit(scope);

            Assert.AreEqual(0,scope.Suggestions.Count);
        }

        [Test]
        public void Visit_TwoSuggestsionOneWithOKCategory_HasOneSuggestion()
        {
            parser.AddAdvisor(mockAdvisor, "");
            parser.Visit(new Scope("text"));
            Assert.AreEqual("text", mockAdvisor.QueryStringSent);
        }

        
        private Scope makeScope(string text)
        {
            return new Scope(text);
        }

        private Suggestion makeSuggestion(string text,string desc)
        {
            return new Suggestion(text,desc);
        }


    }

    class MockAdvisor:IAdvisor
    {
        public string QueryStringSent = null;
        public List<Suggestion> Suggestions = new List<Suggestion>();

        public List<Suggestion> Suggest(Scope input)
        {
            QueryStringSent = input.Text;
            return Suggestions;
        }

        List<Suggestion> IAdvisor.Suggest(string input)
        {
            QueryStringSent = input;
            return Suggestions;
        }

        #region Not Implemented

        void IAdvisor.Learn(params Suggestion[] suggestions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IAdvisor.Learn(Suggestion suggestion)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #endregion
    }
}
