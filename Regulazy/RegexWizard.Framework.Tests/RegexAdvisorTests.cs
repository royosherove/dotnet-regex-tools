using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class RegexAdvisorTests
    {
        RegexAdvisor suggest = null;

        [SetUp]
        public void Setup()
        {

            suggest = new RegexAdvisor();
        }
        
        [Test]
        public void Create()
        {
            RegexAdvisor s = new RegexAdvisor();
            Assert.IsNotNull(s);
        }

        [Test]
        public void Suggest_NoSuggestions_SuggestsExactText()
        {
            RegexAdvisor s = new RegexAdvisor();
            List<Suggestion> suggestions = s.Suggest("a");
            Assert.AreEqual(new Suggestion("^a$", "Exactly 'a'", 900), suggestions[0]);
        }
        
        [Test]
        public void Suggest_InputTextLegthExceedsMaxSuggestionLength_SuggestsExactTrimmedText()
        {
            RegexAdvisor s = new RegexAdvisor();
            s.MaxSuggestionLength = 1;
            List<Suggestion> suggestions = s.Suggest("ab");
            Assert.AreEqual(new Suggestion("^ab$", "Exactly 'a...'", 900), suggestions[0]);
        }
        
        [Test]
        public void Suggest_InputTextLegthExceedsMaxSuggestionLength_SuggestsExactTrimmedText2()
        {
            RegexAdvisor s = new RegexAdvisor();
            s.MaxSuggestionLength = 2;
            List<Suggestion> suggestions = s.Suggest("abc");
            Assert.AreEqual(new Suggestion("^abc$", "Exactly 'ab...'", 900), suggestions[0]);
        }

        [Test]
        public void Suggest_NoSuggestionsImplicitScope_SuggestsExactText()
        {
            RegexAdvisor s = new RegexAdvisor();
            Scope scope = new Scope("abc");
            scope.DefineInnerScope(1, 1);

            List<Suggestion> suggestions = s.Suggest(scope.InnerRightScope);
            Assert.AreEqual(new Suggestion("^c$", "Exactly 'c'", 900), suggestions[0]);
        }

        [Test]
        public void Sugggest_NamedScopeWithInnerScopes_ReturnesCombinedInsideGroup()
        {
            Scope root = new Scope("abc","RootName");
            root.DefineInnerScope(1, 1);
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<RootName>^a$^b$^c$)",suggestion.RegexText);
        }
        
        [Test]
        public void UseGroupsForUnNamedScopes_UnNamedScopeWithInnerScopes_ReturnesCombinedInsideGroup()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"((^a$)(^b$)(^c$))",suggestion.RegexText);
        }
        
        [Test]
        public void UseGroupsForUnNamedScopes_NamedAndUnNamedScopeWithInnerScopes_ReturnesCombinedInsideGroup()
        {
            Scope root = new Scope("abc","Root");
            root.DefineInnerScope(1, 1);
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<Root>(^a$)(^b$)(^c$))",suggestion.RegexText);
        }
        
        
        [Test]
        public void UseGroupsForUnNamedScopes_NamedInsideNamedScopeWithInnerScopes_ReturnesCombinedInsideGroup()
        {
            Scope root = new Scope("abc","Root");
            root.DefineInnerScope(1, 1).Name="Inner";
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<Root>(^a$)(?<Inner>^b$)(^c$))",suggestion.RegexText);
        }
        
        [Test]
        public void UseGroupsForUnNamedScopes_LeftiNnerNameUsed()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);
            
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            
            root.InnerLeftScope.Name = "left";
            root.InnerLeftScope.Suggestions.Add(new Suggestion(".","anything"));
            Suggestion newsuggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"((?<left>.)(^b$)(^c$))", newsuggestion.RegexText);
        }
        
        
        [Test,Ignore("Feature not working for now")]
        public void UseGroupsForUnNamedScopes_GroupedSuggestionExists2ndPass_DoesNotNestGroupInItself()
        {
            Scope root = new Scope("abc","Root");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name = "Inner";
            Suggestion simple = new Suggestion(".", "anything");
            UnNamedGroupedSuggestionDecorator grouped = new UnNamedGroupedSuggestionDecorator(simple,inner);
            inner.Suggestions.Add(grouped);
            
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<Root>(^a$)(?<Inner>.)(^c$))", suggestion.RegexText);
            
            suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<Root>(^a$)(?<Inner>.)(^c$))",suggestion.RegexText);
        }


        [Test, Ignore("Feature not working for now")]
        public void UseGroupsForUnNamedScopes_GroupedSuggestionExists2ndPass_DoesNotNestGroupInItselfLeftNameworks()
        {
            Scope root = new Scope("abc");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name = "Inner";
            Suggestion simple = new Suggestion(".", "anything");
            UnNamedGroupedSuggestionDecorator grouped = new UnNamedGroupedSuggestionDecorator(simple,inner);
            inner.Suggestions.Add(grouped);
            
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];

            root.InnerLeftScope.Name = "left";
            suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<Root>(?<left>^a$)(?<Inner>.)(^c$))",suggestion.RegexText);
        }
        
        
        [Test]
        public void Suggest_RootSuggestionsAttachedCorrectly()
        {
            Scope root = new Scope("abc","Root");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name = "Inner";
            
            
            Suggestion simple = new Suggestion(".", "anything");
            UnNamedGroupedSuggestionDecorator grouped = new UnNamedGroupedSuggestionDecorator(simple,inner);
            inner.Suggestions.Add(grouped);
            
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.IsTrue(root.Suggestions.Count>0);
            
        }
        
        
        [Test]
        public void UseGroupsForUnNamedScopes_NamedInsideNamedScopeWithInnerScopes_ReturnesCombinedInsideGroup2()
        {
            Scope root = new Scope("abc","Root");
            root.DefineInnerScope(1, 1);
            root.InnerLeftScope.Name = "left";
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<Root>(?<left>^a$)(^b$)(^c$))",suggestion.RegexText);
        }
        
       
        
        [Test]
        public void Sugggest_NamedScopeWithInnerNamedScopes_ReturnesCombinedInsideGroup()
        {
            Scope root = new Scope("abc","RootName");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name="Inner";
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<RootName>^a$(?<Inner>^b$)^c$)",suggestion.RegexText);
        }
        
        
        [Test]
        public void Sugggest_InnerNamedScopesWithUnNamedRoot_ReturnesCombinedInsideGroup()
        {
            suggest.UseGroupsForUnnamedScopes = true;
            Scope root = new Scope("abc");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name="Inner";
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"((^a$)(?<Inner>^b$)(^c$))",suggestion.RegexText);
        }
         
        [Test]
        public void Sugggest_ScopeWithNameAndNonGroupedSuggestion_ConvertsToGrouped()
        {
            Suggestion nonGroupedSuggestions = new Suggestion(".", "anything", 1000);
            
            Scope root = new Scope("abc", "RootName");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name="Inner";
            inner.Suggestions.Add(nonGroupedSuggestions);
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<RootName>^a$(?<Inner>.)^c$)", suggestion.RegexText);
        }
        
        [Test]
        public void UseGroupsUnNamedScopes_ScopeWithNoNameAndNonGroupedSuggestion_ConvertsToGrouped()
        {
            suggest.UseGroupsForUnnamedScopes = true;
            Suggestion nonGroupedSuggestions = new Suggestion(".", "anything", 1000);
            
            Scope root = new Scope("abc");
            Scope inner = root.DefineInnerScope(1, 1);

            inner.Suggestions.Add(nonGroupedSuggestions);
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"((^a$)(.)(^c$))", suggestion.RegexText);
        }
        
        
        [Test]
        public void Sugggest_ScopeWithNameAndGroupedSuggestion_DoesNotConvertsUnneedlessly()
        {
            Suggestion nonGroupedSuggestions = new Suggestion(".", "anything", 1000);
            Scope root = new Scope("abc", "RootName");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name="Inner";

            GroupedSuggestionDecorator grouped = new GroupedSuggestionDecorator(nonGroupedSuggestions,inner);    
            inner.Suggestions.Add(grouped);
            
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<RootName>^a$(?<Inner>.)^c$)", suggestion.RegexText);
        }
        
        [Test]
        public void Sugggest_ScopeWithNameAndGroupedSuggestion_ChangesNameCorrectly()
        {   
            Suggestion nonGroupedSuggestions = new Suggestion(".", "anything", 1000);
            Scope root = new Scope("abc", "RootName");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name="Inner";

            GroupedSuggestionDecorator grouped = new GroupedSuggestionDecorator(nonGroupedSuggestions,inner);    
            inner.Suggestions.Add(grouped);
            
            inner.Name = "NameAfterSuggestion";
            
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<RootName>^a$(?<NameAfterSuggestion>.)^c$)", suggestion.RegexText);
        }
        
        [Test]
        public void Sugggest_NamedScopeWithInnerNamedScopesAndMultipleSuggestions_ReturnesCombinedInsideGroup()
        {
            //More than one alternative for the named Scopes besides the default one
            suggest.Learn(new Suggestion(@".", "anything"));
            
            Scope root = new Scope("abc", "RootName");
            Scope inner = root.DefineInnerScope(1, 1);
            inner.Name="Inner";
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<RootName>^a$(?<Inner>^b$)^c$)",suggestion.RegexText);
        }
        
        [Test]
        public void Sugggest_UnNamedScopeWithIneNamedInnerScopes_ReturnesCombinedCintainingGroup()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);
            root.InnerLeftScope.Name = "first";
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<first>^a$)^b$^c$",suggestion.RegexText);
        }
        
        [Test]
        public void Sugggest_UnNamedScopeWithIneRecentlyNamedInnerScope2ndSuggestion_ReturnesCombinedCintainingGroup()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);
            suggest.Suggest(root);
            
            root.InnerLeftScope.Name = "first";
            Suggestion suggestion = suggest.Suggest(root)[0];
            Assert.AreEqual(@"(?<first>^a$)^b$^c$",suggestion.RegexText);
        }

        [Test]
        public void Suggest_NoSuggestionsExplicitScope_SuggestsExactText()
        {
            RegexAdvisor s = new RegexAdvisor();
            Scope scope = new Scope("abc");
            scope.DefineInnerScope(0, 1);

            List<Suggestion> suggestions = s.Suggest(scope.InnerRightScope);
            Assert.AreEqual(new Suggestion("^bc$", "Exactly 'bc'", 900), suggestions[0]);
        }

        [Test]
        public void Suggest_NoSuggestions_SuggestsExactText2()
        {
            RegexAdvisor s = new RegexAdvisor();
            List<Suggestion> suggestions = s.Suggest("b");
            Assert.AreEqual(new Suggestion("^b$", "Exactly 'b'", 900), suggestions[0]);
        }


        [Test]
        public void Suggest_CombinedScopes_SuggestsExactWhenPossible()
        {
            RegexAdvisor s = new RegexAdvisor();
            s.Learn(new Suggestion(@"\w", "any letter or num", 100));

            Scope scope = new Scope("abc");
            scope.DefineInnerScope(1, 1);

            List<Suggestion> suggestions = s.Suggest(scope);
            Assert.AreEqual(new Suggestion("^a$^b$^c$", "Combined"), suggestions[0]);
        }

        [Test]
        public void Suggest_NoSuggestionsWithSpecialMarks_SuggestsExactTextEscaped()
        {
            RegexAdvisor s = new RegexAdvisor();
            List<Suggestion> suggestions = s.Suggest(@"\a");
            Assert.AreEqual(new Suggestion(@"^\\a$", @"Exactly '\a'", 900), suggestions[0]);
        }


        [Test]
        public void Suggest_ScopeWithName_SuggestsWithGroupName()
        {
            Scope s = new Scope("a", "ScopeName");
            List<Suggestion> suggestions = suggest.Suggest(s);
            Assert.AreEqual(@"(?<ScopeName>^a$)", suggestions[0].RegexText);
        }

        [Test]
        public void Sugggest_2Spaces_SuggestsRegularAndAlsoSpaceSuggestion()
        {
            Suggestion sug = new Suggestion(@"^\s+$", "1 or more spaces");
            Suggestion sugMulti = new Suggestion(@"^\s{2}$", "2 spaces");
            suggest.Learn(sug);
            //don't learn the other one..
            List<Suggestion> options = suggest.Suggest("  ");
            Assert.AreEqual(sugMulti, options[2]);
        }

        [Test]
        public void Sugggest_2As_SuggestsRegularAndAlso2LettersMatch()
        {
            Suggestion sug = new Suggestion(@"^\w+$", "1 or more letters");
            Suggestion sugMulti = new Suggestion(@"^\w{2}$", "2 letters");
            suggest.Learn(sug);
            //don't learn the other one..
            List<Suggestion> options = suggest.Suggest("aa");
            Assert.AreEqual(sugMulti, options[2]);
        }


        [Test]
        public void Sugggest_3As_SuggestsRegularAndAlso3LetterMatch()
        {
            Suggestion sug = new Suggestion(@"^\w+$", "1 or more letters");
            Suggestion sugMulti = new Suggestion(@"^\w{3}$", "3 letters");
            suggest.Learn(sug);
            //don't learn the other one..
            List<Suggestion> options = suggest.Suggest("aaa");
            Assert.AreEqual(sugMulti, options[2]);
        }

        [Test]
        public void Sugggest_2As_SuggestsRegularAndAlso2AMatch()
        {
            Suggestion sug = new Suggestion(@"^\w+$", "1 or more letters");
            Suggestion sugMulti = new Suggestion(@"^a{2}$", "2 'a' letters");
            suggest.Learn(sug);
            //don't learn the other one..
            List<Suggestion> options = suggest.Suggest("aa");
            Assert.AreEqual(sugMulti, options[3]);
        }

        [Test]
        public void Sugggest_2As_NonExplicitMultipleWorksWithNonGreedyMatches()
        {
            Suggestion sug = new Suggestion(@"^\w+?$", "1 or more letters");
            suggest.Learn(sug);
            List<Suggestion> options = suggest.Suggest("aa");

            Suggestion expected = new Suggestion(@"^\w{2}$", "2 letters");
            Suggestion expected2 = new Suggestion(@"^a{2}$", "2 'a' letters");
            Assert.AreEqual(expected, options[2]);
            Assert.AreEqual(expected2, options[3]);
        }

        [Test]
        public void Sugggest_2As_NonExplicitMultipleWorksWithNonGreedyMatches2()
        {
            Suggestion sug = new Suggestion(@"^.+?$", "1 or more anything");
            suggest.Learn(sug);
            List<Suggestion> options = suggest.Suggest("aa");

            Suggestion expected = new Suggestion(@"^.{2}$", "2 anything");
            Suggestion expected2 = new Suggestion(@"^a{2}$", "2 'a' anything");

            Assert.AreEqual(sug, options[1]);
            Assert.AreEqual(expected, options[2]);
            Assert.AreEqual(expected2, options[3]);
        }

        [Test]
        public void Sugggest_2As_ExplicitMultipleWorksWithNonGreedyMatches()
        {
            Suggestion sug = new Suggestion(@"^\w+?$", "1 or more letters");
            Suggestion sugMulti = new Suggestion(@"^a{2}$", "2 'a' letters");
            suggest.Learn(sug);
            //don't learn the other one..
            List<Suggestion> options = suggest.Suggest("aa");
            Assert.AreEqual(sugMulti, options[3]);
        }

        [Test]
        public void Sugggest_3As_SuggestsRegularAndAlso3AMatch()
        {
            Suggestion sug = new Suggestion(@"^\w+$", "1 or more letters");
            suggest.Learn(sug);
            List<Suggestion> options = suggest.Suggest("aaa");

            Suggestion sugMulti = new Suggestion(@"^a{3}$", "3 'a' letters");
            Assert.AreEqual(sugMulti, options[3]);
        }

        [Test]
        public void Sugggest_4spaces_SuggestsRegularAndAlso3SpaceMatch()
        {
            Suggestion sug = new Suggestion(@"^\s+$", "1 or more spaces");
            suggest.Learn(sug);
            List<Suggestion> options = suggest.Suggest("    ");

            Suggestion sugMulti = new Suggestion(@"^\s{4}$", "4 spaces");
            Assert.AreEqual(sugMulti, options[2]);
        }

        [Test]
        public void Sugggest_2Differentletters_SuggestsRegularAndMultipleLetters_ButNotSameLetterMultipleMatch()
        {
            Suggestion sug = new Suggestion(@"^\w+$", "1 or more letters");
            suggest.Learn(sug);
            List<Suggestion> options = suggest.Suggest("ab");
            Assert.AreEqual(3, options.Count);
        }

        [Test]
        public void Sugggest_MultipleLearns2Differentletters_SuggestsRegularAndMultipleLetters_ButNotSameLetterMultipleMatch()
        {
            Suggestion sug = new Suggestion(@"^\w+$", "1 or more letters");
            Suggestion sug2 = new Suggestion(@"^\d+$", "1 or more digitis");
            suggest.Learn(sug, sug2);
            List<Suggestion> options = suggest.Suggest("ab");
            Assert.AreEqual(3, options.Count);
        }

        [Test]
        public void Sugggest_31s_SuggestsRegularAndAlso31Match()
        {
            Suggestion sug = new Suggestion(@"^\d+$", "1 or more digits");
            suggest.Learn(sug);
            List<Suggestion> options = suggest.Suggest("111");

            Suggestion sugMulti = new Suggestion(@"^1{3}$", "3 '1' digits");
            Assert.AreEqual(sugMulti, options[3]);
        }

        [Test]
        public void Sugggest_LearnNotCalled_NoSuggestions()
        {
            List<Suggestion> options = suggest.Suggest("a");
            Assert.AreEqual(1, options.Count);
        }

        [Test]
        public void Sugggest_LearnCalledWithMatch_OneSuggestionFound()
        {
            Suggestion s = new Suggestion(@"a", "explicit match a");
            suggest.Learn(s);
            List<Suggestion> options = suggest.Suggest("a");
            Assert.AreSame(s, options[0]);
        }

        [Test]
        public void Sugggest_LearnCalledWithTwoMatches_TwoSuggestionsFound()
        {
            Suggestion s1 = new Suggestion(@"a", "explicit match a");
            Suggestion s2 = new Suggestion(@".", "anything");
            suggest.Learn(s1);
            suggest.Learn(s2);
            List<Suggestion> options = suggest.Suggest("a");
            Assert.AreSame(s2, options[1]);
        }

        [Test]
        public void Sugggest_LearnCalledWithTwoMatchesAndOneNoMatch_TwoSuggestionsFound()
        {
            Suggestion s1 = new Suggestion(@"a", "explicit match a");
            Suggestion s2 = new Suggestion(@".", "anything");
            Suggestion s3 = new Suggestion(@"b", "explicit match a");

            suggest.Learn(s1, s2, s3);
            List<Suggestion> options = suggest.Suggest("a");
            Assert.AreEqual(2, options.Count);
        }

        [Test]
        public void Suggest_HasOptionsWithHigerProbablity_ReturnsHighPropbabilityFirst()
        {
            Suggestion s1 = new Suggestion(@"a", "explicit match a");
            Suggestion s2WIthHigherProbability = new Suggestion(@".", "other one with same regex", 1);

            suggest.Learn(s1, s2WIthHigherProbability);
            List<Suggestion> options = suggest.Suggest("a");
            Assert.AreSame(s2WIthHigherProbability, options[1]);
        }

        [Test]
        public void Suggest_NonFlatScopeWithEmptySuggestions_SuggestsAllHierarchyNonDefault()
        {
            Suggestion sug = new Suggestion(".", "anything");
            suggest.Learn(sug);
            Scope root = new Scope("abc");

            root.DefineInnerScope(1, 1);

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("...", list[1].RegexText);
        }

        [Test]
        public void Suggest_NonFlatScopeWithEmptySuggestions_SuggestsAllHierarchyDefault()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("^a$^b$^c$", list[0].RegexText);
        }

        [Test]
        public void Suggest_NonFlatScopeWithInnerSuggestions_CombinesExistingSuggestions()
        {
            Scope root = new Scope("abc");

            root.DefineInnerScope(1, 1);
            root.InnerLeftScope.Suggestions.Add(new Suggestion("X", "anything"));
            root.InnerMiddleScope.Suggestions.Add(new Suggestion("Y", "anything"));
            root.InnerRightScope.Suggestions.Add(new Suggestion("Z", "anything"));

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("XYZ", list[0].RegexText);
        }
        [Test]
        public void Suggest_NonFlatScopeWithInnerAndOwnSuggestions_IgnoresOwnSuggestions()
        {
            Scope root = new Scope("abc");
            root.Suggestions.Add(new Suggestion("ROOT", "Something"));
            root.DefineInnerScope(1, 1);
            root.InnerLeftScope.Suggestions.Add(new Suggestion("X", "anything"));
            root.InnerMiddleScope.Suggestions.Add(new Suggestion("Y", "anything"));
            root.InnerRightScope.Suggestions.Add(new Suggestion("Z", "anything"));

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("XYZ", list[0].RegexText);
        }


        [Test]
        public void Suggest_NonFlatScopeWithInnerAndOwnPrototype_IgnoresOwnPrototype()
        {
            Suggestion protoype = new Suggestion("proto", "prototype", "single");
            Scope root = new Scope("abc");
            suggest.Learn(protoype);
            root.Prototype = "single";
            root.DefineInnerScope(1, 1);
            root.InnerLeftScope.Suggestions.Add(new Suggestion("X", "anything"));
            root.InnerMiddleScope.Suggestions.Add(new Suggestion("Y", "anything"));
            root.InnerRightScope.Suggestions.Add(new Suggestion("Z", "anything"));

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("XYZ", list[0].RegexText);
        }


        [Test]
        public void Suggest_EncapsulatingScope2mostRightInnerScopes_WorksOK()
        {
            Scope root = new     Scope("012");
            root.DefineInnerScope(2, 1);
            root.DefineInnerScope(1, 1);
            Scope encapsulator = root.DefineInnerScope(1, 2);
            
            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("^0$^1$^2$", list[0].RegexText);
        }

        [Test]
        public void DefineInnerScope_WithEncapsulator_ClearsOlderSuggestionsForLEftInner()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(2, 1);
            root.DefineInnerScope(1, 1);
            suggest.Suggest(root); 
            Scope encapsulator = root.DefineInnerScope(1, 2);

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("^0$^1$^2$", list[0].RegexText);
        }
        
        
        [Test]
        public void Suggest_EncapsulatingScope2mostRightInnerScopes_WorksOK_Right()
        {
            Scope root = new     Scope("012");
            root.DefineInnerScope(0, 1);
            root.DefineInnerScope(1, 1);
            Scope encapsulator = root.DefineInnerScope(0, 2);
            
            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("^0$^1$^2$", list[0].RegexText);
        }

        [Test]
        public void DefineInnerScope_WithEncapsulator_ClearsOlderSuggestionsForRightInner()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(0, 1);
            root.DefineInnerScope(1, 1);
            suggest.Suggest(root);
            Scope encapsulator = root.DefineInnerScope(0, 2);

            List<Suggestion> list = suggest.Suggest(root);
            Assert.AreEqual("^0$^1$^2$", list[0].RegexText);
        }

        #region LearnFile
        [Test]
        [ExpectedException(typeof(InvalidOperationException), "A suggestion with the regex 'a' already exists")]
        public void Learn_SuggestionMatchAlreadyExists_ThrowsException()
        {
            Suggestion s1 = new Suggestion(@"a", "explicit match a");
            Suggestion s2 = new Suggestion(@"a", "other one with same regex");

            suggest.Learn(s1, s2);
        }

        [Test]
        public void LearnFile_EmptyRules_HasNoSuggestionsExcepExactOne()
        {
            string suggestions = "";
            suggest.Learn(suggestions);
            Assert.AreEqual(1, suggest.Suggest("a").Count);
        }

        [Test]
        public void LearnFile_OneMatchingRule_OneSuggestions()
        {
            string suggestions = "a\t\ta";
            suggest.Learn(suggestions);
            Assert.AreEqual(1, suggest.Suggest("a").Count);
        }
        
        [Test]
        public void Learn_RuleWithEndingNewLineDesc_Trimmed()
        {
            Suggestion s = new Suggestion(".","desc with newline in end\n");
            Assert.AreEqual("desc with newline in end",s.Description);
        }

        [Test]
        public void LearnFile_OneMatchingRuleWithProbablity_OneSuggestionsWithProbablity()
        {
            string suggestions = "a\t\ta\t\t500";
            suggest.Learn(suggestions);
            Assert.AreEqual(500, suggest.Suggest("a")[0].Probability);
        }

        [Test]
        public void LearnFile_OneMatchingRuleWithProbablity_OneSuggestionsWithProbablity2()
        {
            string suggestions = "a\t\ta\t\t100";
            suggest.Learn(suggestions);
            Assert.AreEqual(100, suggest.Suggest("a")[0].Probability);
        }

        [Test]
        public void LearnFile_OneNonMatchingRule_NoSuggestions()
        {
            string suggestions = "b\t\tb";
            suggest.Learn(suggestions);
            Assert.AreEqual(1, suggest.Suggest("a").Count);
        }

        [Test]
        public void LearnFile_TwoMatchingRulse_TwoSuggestions()
        {
            string suggestions = "a\t\ta\n\\w\t\tb";
            suggest.Learn(suggestions);
            Assert.AreEqual(2, suggest.Suggest("a").Count);
        }

        [Test]
        public void LearnString_EmptyLastLine_IsIgnored()
        {
            string suggestions = "a\t\ta\n\\w\t\tb\n";
            suggest.Learn(suggestions);
            Assert.AreEqual(2, suggest.Suggest("a").Count);
        }

        [Test]
        public void LearnFromFile_EmptyFile_GetsEMptyFileString()
        {
            TestableRegexAdvisor visor = new TestableRegexAdvisor();
            visor.LearnFromFile("RuleFiles\\EmptyFile.txt");
            Assert.AreEqual(string.Empty, visor.FileTextToLearn);
        }

        [Test]
        public void LearnFromFile_SuggestionWithProbabilityAndArity()
        {
            string suggestions = "a\t\ta\t\t0\t\tSingle";
            suggest.Learn(suggestions);
            List<Suggestion> results = suggest.Suggest("a");
            Suggestion expected = new Suggestion("a", "a", "Single");
            Assert.AreEqual(expected, results[0]);
        }

        [Test]
        public void LearnFromFile_SuggestionWithProbabilityAndArity_ProbabilityIsCorrect()
        {
            string suggestions = "a\t\ta\t\t100\t\tSingle";
            suggest.Learn(suggestions);
            List<Suggestion> results = suggest.Suggest("a");
            Suggestion expected = new Suggestion("a", "a", "Single");
            expected.Probability = 100;
            Assert.AreEqual(expected, results[0]);
        }

        [Test]
        public void GetProtoTypes_SingleProtoType_ReturnsOne()
        {
            string suggestions = "a\t\ta\t\t100\t\tSingle";
            suggest.Learn(suggestions);
            List<Suggestion> results = suggest.GetPrototypes();
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void GetProtoTypes_SingleProtoTypeAndOneNonePrototype_ReturnsOne()
        {
            suggest.Learn(new Suggestion("a", "a"));
            suggest.Learn(new Suggestion("b", "b", "Single"));
            List<Suggestion> results = suggest.GetPrototypes();
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void LearnFromFile_NonEmptyFile_GetsNonEmptyFileString()
        {
            TestableRegexAdvisor visor = new TestableRegexAdvisor();
            visor.LearnFromFile("RuleFiles\\OneRule.txt");
            Assert.AreEqual("a\t\ta", visor.FileTextToLearn);
        }

        #endregion


        #region Suggest with GroupScopes



        public void MakeRegexTulesFile()
        {
            string filename = @"z:\regularExpressions.txt";
            string text = File.ReadAllText(filename);
            StringBuilder sb = new StringBuilder(text.Length / 2);
            MatchCollection matches = Regex.Matches(text, @"\d\t.+?\t\d\t(?<title>.+?)\t(?<regex>.+?)\t.+?\t.+?\t\d\t\d\t\s+(?<description>.+?)\t.+?\n");
            foreach (Match m in matches)
            {
                string title = m.Groups["title"].Value;
                string desc = m.Groups["description"].Value;
                string regex = m.Groups["regex"].Value;
                if (title == string.Empty || title == "Pattern Title")
                {
                    title = desc;
                }
                sb.Append(regex).Append("\t\t");
                sb.Append(title).Append("\t\t");
                sb.Append(300).Append('\n');
            }

            File.WriteAllText("newRules.txt", sb.ToString());
        }

        #endregion



    }

    class TestableRegexAdvisor : RegexAdvisor
    {
        public string FileTextToLearn = null;
        public override void Learn(string suggestions)
        {
            FileTextToLearn = suggestions;
        }

    }
}
