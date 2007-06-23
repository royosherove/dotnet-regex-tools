//using System;
//using System.Collections.Generic;
//using NUnit.Framework;
//using RegexWizard.Framework;
//using Regulazy.UISupport.UserActions;
//
//namespace Regulazy.UISupport.Tests
//{
//    [TestFixture]
//    public class UIActionHelperTests
//    {
//         FakeSugProvider sugProvider = new FakeSugProvider();
//        Scope root=null;
//        [SetUp]
//        public void SetUp()
//        {
//            sugProvider = new FakeSugProvider();
//            ActionHelper.SetSuggestionProvider(sugProvider);
//            root = new Scope("abc");
//        }
//        
//        [Test]
//        public void GetRelevantScopeActions_NoSuggestions_HasNoRuleSuggestions()
//        {
//            ScopeActionsInfo actionsForScope = ActionHelper.GetActions(root);
//            ActionList actions = actionsForScope.RuleSuggestions;
//            Assert.AreEqual(0, actions.Count);
//        }
//        
//        [Test]
//        public void GetRelevantScopeActions_1Sugestion_Has1Suggestion()
//        {
//            sugProvider.SuggestionsToReturn.Add(new Suggestion("abc",""));
//            
//            ScopeActionsInfo actionsForScope = ActionHelper.GetActions(root);
//            ActionList actions = actionsForScope.RuleSuggestions;
//            Assert.AreEqual(1, actions.Count);
//        }
//        
//        [Test]
//        public void GetRelevantScopeActions_1Sugestion_HasActionToMatchSuggestion()
//        {
//            Suggestion realSug = new Suggestion("abc","");
//            sugProvider.SuggestionsToReturn.Add(realSug);
//            
//            ScopeActionsInfo actionsForScope = ActionHelper.GetActions(root);
//            ActionList actions = actionsForScope.RuleSuggestions;
//            SuggestionAction sug = actions[0] as SuggestionAction;
//            
//            Assert.AreEqual(realSug, sug.Suggestion);
//        }
//        
//        [Test]
//        public void GetRelevantScopeActions_SendsRelevantScopeToSugProvider()
//        {
//            ActionHelper.GetActions(root);
//            Assert.AreEqual(root, sugProvider.SentInScope);
//            
//        }
//        [Test]
//        public void GetRelevantScopeActions_ScopeWithNoName_StanardActions()
//        {
//            ActionList expectedUserActions = new ActionList();
//            expectedUserActions.Add(new ScopeRenameAction(root,null));
//            ScopeActionsInfo actions = ActionHelper.GetActions(root);
//            
//        }
//    }
//
//    class FakeSugProvider:ISuggestionProvider
//    {
//        public List<Suggestion> SuggestionsToReturn= new List<Suggestion>();
//        public Scope SentInScope;
//
//        public List<Suggestion> GetSuggestions(Scope scopeToCheck)
//        {
//            SentInScope=scopeToCheck;
//            return SuggestionsToReturn;
//        }
//    }
//    
//}
