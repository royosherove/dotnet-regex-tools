using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using RegexWizard.Framework;
namespace RegexWizard.Framework.Tests
{




    [TestFixture]
    public class ScopeTests
    {

        Scope scope = null;

        [SetUp]
        public void setup()
        {
            scope = new Scope("abc");
        }

        [Test]
        public void Create()
        {
            Scope s = new Scope("a");
            Assert.IsNotNull(s);
        }
        [Test]
        public void IsRoot_OnlyScope_IsTrue()
        {
            Scope s = new Scope("a");
            Assert.IsTrue(s.IsRoot);
        }

        #region DefineInnerScope

        [Test]
        public void SetInnerScope_EncapsulatingInnerScopes_CreatesOuterScopeWithInnerScopes()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(1, 1);//b
            root.DefineInnerScope(2, 1);//'c' as inner of 'cd'
            Scope encapsulating = root.DefineInnerScope(1, 2);//'b'+'c' splitting 'd' to an implicit scope
            Assert.AreEqual("bc", encapsulating.Text);
        }

        [Test]
        public void SetInnerScope_EncapsulatingInnerScopes_CreatesRightImplicitScopeSplitted()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(1, 1);//b
            Assert.AreEqual("cd", root.InnerRightScope.Text);

            Scope encapsulating = root.DefineInnerScope(1, 2);//'b'+'c' splitting 'd' to an implicit scope

            Assert.AreEqual("d", root.InnerRightScope.Text);
            Assert.AreEqual("bc", root.InnerMiddleScope.Text);
            Assert.AreEqual(true, root.InnerRightScope.IsImplicit);
            Assert.AreEqual("b", encapsulating.InnerLeftScope.Text);
            Assert.AreEqual("c", encapsulating.InnerRightScope.Text);
        }

        [Test]
        public void DefineInnerScope_WithleftMiddleandRightScopesasInner()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(3, 1);//012|3|45
            ScopeSplitter splitter = new ScopeSplitter();
            splitter.AutoSplit(root, 2, 3, true);//01|(2|3|4)|5

            Scope encapsulator = root.DefineInnerScope(2, 3);
            Assert.AreEqual("2", encapsulator.InnerLeftScope.Text);
            Assert.AreEqual("3", encapsulator.InnerMiddleScope.Text);
            Assert.AreEqual("4", encapsulator.InnerRightScope.Text);
        }
        
        [Test]
        public void DefineInnerScope_WithleftMiddleandRightScopesasInner_RemovesOriginalScopesFromDirectRootAndIntoNewScope()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(1, 1);//1
            root.InnerRightScope.Suggestions.Add(new Suggestion("a","b"));
            root.InnerMiddleScope.Suggestions.Add(new Suggestion("a","b"));

            Scope encapsulator = root.DefineInnerScope(0, 2);//01
            
            Assert.AreEqual("0", encapsulator.InnerLeftScope.Text);
            Assert.AreEqual(null, encapsulator.InnerMiddleScope);
            Assert.AreEqual("1", encapsulator.InnerRightScope.Text);
            
            Assert.AreEqual("2", root.InnerRightScope.Text);
            Assert.AreEqual("01", root.InnerLeftScope.Text);
            Assert.AreEqual(null, root.InnerMiddleScope);
            
            Assert.AreEqual(1, root.InnerRightScope.Suggestions.Count);
            Assert.AreEqual(1, encapsulator.InnerRightScope.Suggestions.Count);
            Assert.AreEqual(encapsulator, encapsulator.InnerRightScope.ParentScope);
            Assert.AreEqual(encapsulator, encapsulator.InnerLeftScope.ParentScope);
            
        }

        [Test]
        public void DefineInnerScope_InCombinedParentOfTwoInners_SetsRootScopeOnInner()
        {
            Scope root = new     Scope("0123456789");
            Scope parent = root.DefineInnerScope(5, 4);
            //5678
            Scope combinedParent = root.DefineInnerScope(5, 5);//5678+9
            Scope innerChild = root.DefineInnerScope(6, 2);//67
            Assert.AreEqual(combinedParent.InnerLeftScope, innerChild.ParentScope);
            Assert.AreEqual(combinedParent, combinedParent.InnerLeftScope.ParentScope);
            Assert.AreEqual(combinedParent, combinedParent.InnerRightScope.ParentScope);
        }
        
        [Test]
        public void DefineInnerScope_WithleftMiddleandRightScopesasInner_RemovesOriginalScopesFromDirectRootAndIntoNewScope2()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(1, 1);//1
            root.InnerLeftScope.Suggestions.Add(new Suggestion("a","b"));
            root.InnerMiddleScope.Suggestions.Add(new Suggestion("a","b"));

            Scope encapsulator = root.DefineInnerScope(1, 2);//12
            Assert.AreEqual("1", encapsulator.InnerLeftScope.Text);
            Assert.AreEqual(null, encapsulator.InnerMiddleScope);
            Assert.AreEqual("2", encapsulator.InnerRightScope.Text);
            
            Assert.AreEqual("0", root.InnerLeftScope.Text);
            Assert.AreEqual("12", root.InnerRightScope.Text);
            Assert.AreEqual(null, root.InnerMiddleScope);
            Assert.AreEqual(1, root.InnerLeftScope.Suggestions.Count);
            Assert.AreEqual(1, encapsulator.InnerLeftScope.Suggestions.Count);
            
        }
        
        [Test]
        public void DefineInnerScope_WithleftMiddleandRightScopesasInner_RemovesOriginalScopesFromDirectRootAndIntoNewScope1()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(1, 1);//1
            Scope encapsulator = root.DefineInnerScope(1, 2);//12
            Assert.AreEqual("0", root.InnerLeftScope.Text);
            Assert.AreEqual(null, root.InnerMiddleScope);
            
        }

        [Test]
        public void SetInnerScope_ParentIsNoLongerFlat()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(1, 1);//b
            Assert.IsFalse(root.IsFlat);
        }

        [Test]
        public void SetInnerScope_EncapsulatingInnerScopes_CreatesOuterScopeWithInnerScopes2()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(1, 1);//b
            root.DefineInnerScope(2, 1);//'c' as inner of 'cd'
            Scope encapsulating = root.DefineInnerScope(1, 3);//'bcd'
            Assert.AreEqual("bcd", encapsulating.Text);
        }


        [Test]
        public void SetInnerScope_CrossingOnStartingImplicitScope_splitsImplicitScopeAnsUsesItInEncapsulator()
        {
            Scope root = new Scope("implicitEXPLICIT");
            Scope explicitScope = root.DefineInnerScope(8, 8);//EXPLICIT
            Scope implicitScope = root.InnerLeftScope;//implicit

            Scope encapsulating = root.DefineInnerScope(7, 9);//tEXPLICIT
            Assert.AreEqual("tEXPLICIT", encapsulating.Text);
        }


        [Test]
        public void SetInnerScope_CrossingOnStartingImplicitScope_splitsImplicitScopeAnsUsesItInEncapsulator2()
        {
            Scope root = new Scope("EXPimp");
            Scope explicitScope = root.DefineInnerScope(0, 3);//EXP
            Scope implicitScope = root.InnerRightScope;//imp

            Scope encapsulating = root.DefineInnerScope(0, 4);//EXPi
            Assert.AreEqual("i", root.InnerLeftScope.InnerRightScope.Text);
        }


        #endregion

        #region FindScopesInRange

        [Test]
        public void FindScopesInRange_SingleScope_ReturnsIt()
        {
            Scope root = new Scope("012345");
            List<Scope> found = root.FindScopesInRange(0, 6);
            Assert.AreEqual(found[0],root);
        }
        
        [Test]
        public void FindScopesInRange_SingleScopeInnerLeft_ReturnsOnlyFullyEncapsulatedScope()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(0, 1);
            List<Scope> found = root.FindScopesInRange(0, 3);
            Assert.AreEqual(found[0],root.InnerLeftScope);
        }
        
        [Test]
        public void FindScopesInRange_SomeNestedScopesToFindAndOneDirectScopeToFind()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(3, 1);//middle scope
            root.DefineInnerScope(2, 1);//inside left scope
            root.DefineInnerScope(4, 1);//inside right scope
            List<Scope> found = root.FindScopesInRange(2, 3);
            Assert.AreEqual("2", found[0].Text);
            Assert.AreEqual("3", found[1].Text);
            Assert.AreEqual("4", found[2].Text);
            
        }
        
        [Test]
        public void FindScopesInRange_SingleScopeInnerMiddle_ReturnsOnlyFullyEncapsulatedScope()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(1, 1);
            List<Scope> found = root.FindScopesInRange(1, 3);
            Assert.AreEqual(found[0],root.InnerMiddleScope);
        }
        
        [Test]
        public void FindScopesInRange_SingleScopeInsideInnerLeft_ReturnsOnlyFullyEncapsulatedScope()
        {
            Scope root =      new Scope("012345");
            Scope inner = root.DefineInnerScope(0, 5);//01234
            inner.DefineInnerScope(2, 2);// 23
            
            List<Scope> found = root.FindScopesInRange(1, 3);//123
            Assert.AreEqual(found[0],root.InnerLeftScope.InnerMiddleScope);
        }
        
        [Test]
        public void FindScopesInRange_SingleScopeInsideInnerMiddle_ReturnsOnlyFullyEncapsulatedScope()
        {
            Scope root =      new Scope("0123456789");
            Scope inner = root.DefineInnerScope(1, 7);//1234567
            inner.DefineInnerScope(4, 2);// 45
            
            List<Scope> found = root.FindScopesInRange(3, 3);//3(45)
            Assert.AreEqual(found[0],root.InnerMiddleScope.InnerMiddleScope);
        }
        
        [Test]
        public void FindScopesInRange_SingleScopeInsideInnerRight_ReturnsOnlyFullyEncapsulatedScope()
        {
            Scope root =      new Scope("0123456789");
            Scope inner = root.DefineInnerScope(0, 3);//012
            root.DefineInnerScope(7, 2);//78 which is inside 3456789
            
            List<Scope> found = root.FindScopesInRange(6, 3);//6(78)
            Assert.AreEqual(found[0],root.InnerRightScope.InnerMiddleScope);
        }
        
        [Test]
        public void FindScopesInRange_TwoScopes_ReturnsOnlyFullyEncapsulatedScopes()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(1, 1);
            List<Scope> found = root.FindScopesInRange(0, 3);
            Assert.AreEqual(2,found.Count);
            Assert.AreEqual(root.InnerLeftScope, found[0]);//0
            Assert.AreEqual(root.InnerMiddleScope, found[1]);//1
        }

        [Test]
        public void FindScopesInRange_SingleScopeInnerRight_ReturnsOnlyFullyEncapsulatedScope()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(5, 1);
            List<Scope> found = root.FindScopesInRange(3, 3);
            Assert.AreEqual(found[0], root.InnerRightScope);
        }

        #endregion

        #region Compare

        [Test]
        public void ScopeCompareByLocation_1()
        {
            List<Scope> scopes = new List<Scope>();
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);

            scopes.Add(root.InnerLeftScope);
            scopes.Add(root.InnerMiddleScope);
            scopes.Add(root.InnerRightScope);
            scopes.Sort(Scope.CompareScopesByLocation);

            Assert.AreEqual(root.InnerLeftScope, scopes[0]);
            Assert.AreEqual(root.InnerMiddleScope, scopes[1]);
            Assert.AreEqual(root.InnerRightScope, scopes[2]);
        }

        [Test]
        public void ScopeCompareByLocation_2()
        {
            List<Scope> scopes = new List<Scope>();
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);

            scopes.Add(root.InnerMiddleScope);
            scopes.Add(root.InnerLeftScope);
            scopes.Add(root.InnerRightScope);

            scopes.Sort(Scope.CompareScopesByLocation);

            Assert.AreEqual(root.InnerLeftScope, scopes[0]);
            Assert.AreEqual(root.InnerMiddleScope, scopes[1]);
            Assert.AreEqual(root.InnerRightScope, scopes[2]);
        }

        [Test]
        public void ScopeCompareByLocation_3()
        {
            List<Scope> scopes = new List<Scope>();
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);

            scopes.Add(root.InnerRightScope);
            scopes.Add(root.InnerLeftScope);
            scopes.Add(root.InnerMiddleScope);

            scopes.Sort(Scope.CompareScopesByLocation);

            Assert.AreEqual(root.InnerLeftScope, scopes[0]);
            Assert.AreEqual(root.InnerMiddleScope, scopes[1]);
            Assert.AreEqual(root.InnerRightScope, scopes[2]);
        }

        #endregion


        [Test]
        public void Scope_StartAndEndPosAreCorrect()
        {
            Scope s = new Scope("a");
            Assert.AreEqual(0, s.StartPosInRootScope);
            Assert.AreEqual(0, s.EndPosInRootScope);
        }


        [Test]
        public void IsRoot_HasParent_IsFalse()
        {
            Scope s = new Scope("ab");
            Scope inner = s.DefineInnerScope(1, 1);
            Assert.IsFalse(inner.IsRoot);
        }

        [Test]
        public void GroupedSuggestion_Create_ContainsGoodRegex()
        {
            scope = new Scope("a", "Name");
            Suggestion s = new Suggestion(@"a", "desc");
            GroupedSuggestionDecorator decorator = new GroupedSuggestionDecorator(s, scope);
            Assert.AreEqual(@"(?<Name>a)", decorator.RegexText);
        }

        [Test]
        public void GroupedSuggestion_CreateWithNoScopeName_ReturnsOriginalRegex()
        {
            Scope unnamedScope = new Scope("a");
            Suggestion s = new Suggestion(@"a", "desc");
            GroupedSuggestionDecorator decorator = new GroupedSuggestionDecorator(s, unnamedScope);
            Assert.AreEqual(@"a", decorator.RegexText);
        }

        [Test]
        public void GroupedSuggestion_Create_ContainsGoodRegex2()
        {
            Scope scope = new Scope("a", "Name");
            Suggestion s = new Suggestion(@"b", "desc");
            GroupedSuggestionDecorator decorator = new GroupedSuggestionDecorator(s, scope);
            Assert.AreEqual(@"(?<Name>b)", decorator.RegexText);
        }

        [Test]
        public void GetInnetScopes_Returns0ByDefault()
        {
            Scope s = new Scope("1");
            Assert.AreEqual(0, s.GetInnerScopes().Length);
        }

        [Test]
        public void IsFaltByDefault()
        {
            scope = new Scope("abc");
            Assert.IsTrue(scope.IsFlat);
        }

        [Test]
        public void IsFlat_HasInnerScopes_NotFlat()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(1, 1);
            Assert.IsFalse(scope.IsFlat);
        }

        [Test]
        public void IsFlat_InnerScopeAddedAndRemoved_Flat()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(1, 1);
            scope.RemoveInnerScope(inner);
            Assert.IsTrue(scope.IsFlat);
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException), "You can't remove an implicit Scope, you can only remove an Explicit one")]
        public void RemoveInnerScope_TryRemoveImplicitScope_ThrowsException()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(1, 1);
            scope.RemoveInnerScope(scope.InnerLeftScope);

        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), "You can't remove an implicit Scope, you can only remove an Explicit one")]
        public void RemoveInnerScope_TryRemoveImplicitScopeRight_ThrowsException()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(1, 1);
            scope.RemoveInnerScope(scope.InnerRightScope);

        }
        
        [Test]
        public void RemoveInnerScope_leftIsTheOnlyExplicit_AllScopesAreRemoved()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(0, 1);
            scope.RemoveInnerScope(scope.InnerLeftScope);
            Assert.AreEqual(0,scope.GetInnerScopes().Length);
        }

        [Test]
        public void SetInnerScope_IfInnerScopeExists_DoesNotThrowsException()
        {
            scope = new Scope("abc");
            scope.DefineInnerScope(0, 1);
            scope.DefineInnerScope(2, 1);
        }

        [Test]
        public void SetInnerScope_WithScopeInThebeginning_SetsInnerLEftScopeAsNewScope()
        {
            scope = new Scope("abc");
            Scope newone = scope.DefineInnerScope(0, 1);
            Assert.AreSame(newone, scope.InnerLeftScope);
        }

        [Test]
        public void SetInnerScope_WithScopeInTheEnd_SetsInnerRightScopeAsNewScope()
        {
            scope = new Scope("abc");
            Scope newone = scope.DefineInnerScope(2, 1);
            Assert.AreSame(newone, scope.InnerRightScope);
        }


        [Test]
        public void SetInnerScope_WithScopeInThebeginning_DoesntSetInnerMiddleScope()
        {
            scope = new Scope("abc");
            Scope newone = scope.DefineInnerScope(0, 1);
            Assert.IsNull(scope.InnerMiddleScope);

        }

        [Test]
        public void SetInnerScope_WithScopeInTheEnd_DoesntSetInnerMiddleScope()
        {
            scope = new Scope("abc");
            Scope newone = scope.DefineInnerScope(2, 1);
            Assert.IsNull(scope.InnerMiddleScope);
        }

        [Test]
        public void SetInnerScope_CreatesNonImplicitScope()
        {
            scope = new Scope("abc");
            Scope newone = scope.DefineInnerScope(2, 1);
            Assert.IsFalse(newone.IsImplicit);
        }

        [Test]
        public void SetInnerScope_CreatesImplicitScopOnLeft()
        {
            scope = new Scope("abc");
            scope.DefineInnerScope(2, 1);
            Assert.IsTrue(scope.InnerLeftScope.IsImplicit);
        }

        [Test]
        public void SetInnerScope_CreatesImplicitScopOnRight()
        {
            scope = new Scope("abc");
            scope.DefineInnerScope(0, 1);
            Assert.IsTrue(scope.InnerRightScope.IsImplicit);
        }

        [Test]
        public void IsFlat_WithLeftAndRightScopes_ReturnsFalse()
        {
            scope = new Scope("abc");
            scope.DefineInnerScope(0, 1);
            Assert.IsFalse(scope.IsFlat);
        }
        [Test]
        public void SetInnerScope_CreatesScopeWithCorrectText()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(1, 1);
            Assert.AreEqual("b", inner.Text);
        }

        [Test]
        public void SetInnerScope_CreatesScopeWithCorrectText2()
        {
            scope = new Scope("abcd");
            Scope inner = scope.DefineInnerScope(1, 2);
            Assert.AreEqual("bc", inner.Text);
        }


        #region AutoSplit into several Scopes

        [Test]
        public void Create_ParentIsNull()
        {
            Scope rootScope = new Scope("abc");
            Assert.IsNull(rootScope.ParentScope);
        }


        [Test]
        public void SetInnerScope_SetsParentCorrectly()
        {
            Scope rootScope = new Scope("abc");
            Scope inner = rootScope.DefineInnerScope(1, 1);
            Assert.AreSame(rootScope, inner.ParentScope);
        }

        [Test]
        public void SetInnerScope_SetsParentCorrectlyLeftScope()
        {
            Scope rootScope = new Scope("abc");
            rootScope.DefineInnerScope(1, 1);
            Assert.AreSame(rootScope, rootScope.InnerLeftScope.ParentScope);
        }

        [Test]
        public void SetInnerScope_SetsParentCorrectlyRightScope()
        {
            Scope rootScope = new Scope("abc");
            rootScope.DefineInnerScope(1, 1);
            Assert.AreSame(rootScope, rootScope.InnerRightScope.ParentScope);
        }
        [Test]
        public void SetInnerScope_IsFlat_CreatesTwoExtraScopesIfScopeIsInTheMiddle()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(1, 1);
            Scope[] innerScopes = scope.GetInnerScopes();
            Assert.AreEqual(3, innerScopes.Length);
        }

        [Test]
        public void SetInnerScope_IsFlat_CreatesTwoExtraScopesIfScopeIsInTheMiddle2()
        {
            scope = new Scope("abcd");
            Scope inner = scope.DefineInnerScope(1, 2);
            Scope[] innerScopes = scope.GetInnerScopes();
            Assert.AreEqual(3, innerScopes.Length);
        }

        [Test]
        public void SetInnerScope_IsFlat_CreatesOneExtraScopesIfScopeIsInTheBeginning()
        {
            scope = new Scope("abc");
            scope.DefineInnerScope(0, 1);
            Scope[] innerScopes = scope.GetInnerScopes();
            Assert.AreEqual(2, innerScopes.Length);
        }

        [Test]
        public void SetInnerScope_IsFlat_CreatesOneExtraScopesIfScopeIsInTheEnd()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(2, 1);
            Scope[] innerScopes = scope.GetInnerScopes();
            Assert.AreEqual(2, innerScopes.Length);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), "You can't set an inner scope with the same length of the parent scope")]
        public void SetInnerScope_SameScopeAsParent_ThrowsExcpetion()
        {
            scope = new Scope("abc");
            Scope inner = scope.DefineInnerScope(0, 3);
        }


        [Test]
        public void SetInnerScope_SetsInnerMiddleScopePropertyToNewScope()
        {
            scope = new Scope("abc");
            Scope middle = scope.DefineInnerScope(1, 1);
            Assert.AreSame(middle, scope.InnerMiddleScope);
        }

        [Test]
        public void SetInnerScope_SetsInnerLeftScopePropertyToLeftScope()
        {
            scope = new Scope("abc", 0, true);
            scope.DefineInnerScope(1, 1);
            Scope leftScopeWithCorrectProperties = new Scope("a", 0, false);
            Assert.IsTrue(leftScopeWithCorrectProperties.Equals(scope.InnerLeftScope));
        }

        [Test]
        public void SetInnerScope_SetsInnerRightScopePropertyToRightScope()
        {
            scope = new Scope("abc");
            scope.DefineInnerScope(1, 1);
            Scope rightScopeWithCorrectProperties = new Scope("c", 2, false);
            Assert.IsTrue(rightScopeWithCorrectProperties.Equals(scope.InnerRightScope));
        }

        [Test]
        public void SetInnerScope_SetsInnerLeftScopePropertyToLeftScope2()
        {
            scope = new Scope("defg");
            scope.DefineInnerScope(2, 1);
            Scope leftScopeWithCorrectProperties = new Scope("de", 0);
            leftScopeWithCorrectProperties.IsExplicit = false;
            Assert.IsTrue(leftScopeWithCorrectProperties.Equals(scope.InnerLeftScope));
        }

        [Test]
        public void SetInnerScope_SetsInnerRightScopePropertyToRightScope2()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(2, 1);
            Scope rightScopeWithCorrectProperties = new Scope("ge", 3);
            rightScopeWithCorrectProperties.IsExplicit = false;
            Assert.IsTrue(rightScopeWithCorrectProperties.Equals(scope.InnerRightScope));
        }



        #endregion


        [Test]
        public void GetInnerScope_ByCharPos_GetsCorrectScope()
        {
            scope = new Scope("defge");
            scope.IsExplicit = true;
            scope.DefineInnerScope(2, 2);//'fg'
            Assert.AreSame(scope.InnerMiddleScope, scope.FindInnerScope(2, 1));
        }

        [Test]
        public void GetInnerScope_ByCharPos_GetsCorrectScope4()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(2, 2);//'fg'
            scope.IsExplicit = true;
            Assert.AreSame(scope.InnerMiddleScope,
                scope.FindInnerScope(3, 1));
        }

        [Test]
        public void GetInnerScope_IsFlat_ReturnsThis()
        {
            scope = new Scope("defge");//flat
            Assert.AreSame(scope,
                scope.FindInnerScope(2, 1));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), "Invalid start position requested")]
        public void GetInnerScope_NegativeStartPos_ThrowsException()
        {
            scope = new Scope("defge");//flat
            scope.FindInnerScope(-1, 1);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), "Invalid start position requested")]
        public void GetInnerScope_overflowingLength_THrowsException()
        {
            scope = new Scope("defge");//flat
            scope.FindInnerScope(0, scope.Length + 1);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), "Invalid start position requested")]
        public void GetInnerScope_overflowingLengthFromMiddle_THrowsException()
        {
            scope = new Scope("defge");//flat
            scope.FindInnerScope(3, 3);//one char overflow
        }

        [Test]
        public void GetInnerScope_ByCharPos_GetsCorrectScope2()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(2, 2);//'fg'
            Assert.AreSame(scope.InnerRightScope,
                scope.FindInnerScope(4, 1));//e
        }

        [Test]
        public void GetInnerScope_ByCharPos_GetsCorrectScope5()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(1, 1);//'e'
            Scope found = scope.FindInnerScope(4, 1);//'fge'
            Assert.AreSame(scope.InnerRightScope, found);//e
        }

        [Test]
        public void GetInnerScope_ByCharPos_GetsCorrectScope3()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(2, 2);//'fg'
            Assert.AreSame(scope.InnerLeftScope,
                scope.FindInnerScope(1, 1));
        }

        [Test]
        public void GetInnerScope_ByCharPos_GetsCorrectScope6()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(2, 2);//'fg'
            Assert.AreSame(scope.InnerMiddleScope,
                scope.FindInnerScope(3, 1));//g
        }

        [Test]
        public void GetInnerScope_InMiddleInnerScope_GetsCorrectScope()
        {
            scope = new Scope("abcde");
            Scope inner = scope.DefineInnerScope(1, 3);//'bcd'
            Scope deepInner = inner.DefineInnerScope(2, 1);//'c'
            Assert.AreSame(deepInner,
                scope.FindInnerScope(2, 1));//c
        }

        [Test]
        public void GetInnerScope_InLeftInnerScope_GetsCorrectScope()
        {
            scope = new Scope("012345");
            Scope inner = scope.DefineInnerScope(0, 3);//'012'
            Scope deepInner = inner.DefineInnerScope(2, 1);//'2'
            Assert.AreSame(deepInner,
                scope.FindInnerScope(2, 1));//2
        }

        [Test]
        public void GetInnerScope_InRightInnerScope_GetsCorrectScope()
        {
            scope = new Scope("012345");
            Scope inner = scope.DefineInnerScope(3, 3);//'345'
            Scope deepInner = inner.DefineInnerScope(3, 1);//'3'
            Assert.AreSame(deepInner,
                scope.FindInnerScope(3, 1));//3
        }

        [Test]
        public void GetInnerScope_MiddleIsNullByCharPos_GetsCorrectScope()
        {
            scope = new Scope("defge");
            scope.DefineInnerScope(0, 2);//'de'
            Assert.AreSame(scope.InnerLeftScope,
                scope.FindInnerScope(1, 1));//g
        }


//        [Test]
//        [ExpectedException(typeof(InvalidOperationException), "Your selection spans more than one scope")]
//        public void GetInnerScope_PositionsSPanTwoScopes_ThrowsException()
//        {
//            scope = new Scope("defge");
//            scope.DefineInnerScope(2, 2);//'fg'
//            scope.FindInnerScope(0, scope.Length);
//        }
//
//        [Test]
//        [ExpectedException(typeof(InvalidOperationException), "Your selection spans more than one scope")]
//        public void GetInnerScope_PositionsSPanTwoScopes_ThrowsException2()
//        {
//            scope = new Scope("defge");
//            scope.DefineInnerScope(2, 2);//'fg'
//            scope.FindInnerScope(0, 3);
//        }
//
//        [Test]
//        [ExpectedException(typeof(InvalidOperationException), "Your selection spans more than one scope")]
//        public void GetInnerScope_PositionsSPanTwoScopes_ThrowsException3()
//        {
//            scope = new Scope("abcdefgehi");
//            scope.DefineInnerScope(5, 2);//'fg'
//            scope.FindInnerScope(5, 3);//fge'
//        }
//
//        [Test]
//        [ExpectedException(typeof(InvalidOperationException), "Your selection spans more than one scope")]
//        public void GetInnerScope_PositionsSPanTwoExplicitScopes_ThrowsException4()
//        {
//            scope = new Scope("abcdefgehi");
//            scope.DefineInnerScope(5, 2);//'fg'
//            scope.FindInnerScope(4, 2);//ef'
//        }

        [Test]
        public void GetInnerScope_GetsCorrectScopeWithInnerLeftLongerThanLength()
        {
            scope = new Scope("a    bcdefgehi");
            scope.DefineInnerScope(5, 2);//'fg'
            Assert.AreSame(scope.InnerMiddleScope,
                scope.FindInnerScope(5, 2));//fg'
        }

        [Test]
        public void GetInnerScope_ForWholeRootScope_ReturnsRoot()
        {
            scope = new Scope("defge");
            Assert.AreSame(scope,
                scope.FindInnerScope(0, scope.Length));
        }


        [Test]
        public void SetInnerScope_OnInnerScope_IsCorrect()
        {
            scope = new Scope("abcde");
            scope.DefineInnerScope(1, 3);//bcd
            scope.InnerMiddleScope.DefineInnerScope(2, 1);//'c'
            Assert.AreEqual("c", scope.InnerMiddleScope.InnerMiddleScope.Text);

        }

        [Test]
        public void SetInnerScope_OnInnerScope_HasCorrectLeftScopeText()
        {
            scope = new Scope("abcde");
            scope.DefineInnerScope(1, 3);//bcd
            scope.DefineInnerScope(2, 1);//'c'
            Assert.AreEqual("b", scope.InnerMiddleScope.InnerLeftScope.Text);

        }

        [Test]
        public void SetInnerScope_OnFirstChar_InnerMiddleScopeIsNull()
        {
            scope = new Scope(" text");
            scope.DefineInnerScope(0, 1);//' '
            Assert.IsNull(scope.InnerMiddleScope);
        }

        [Test]
        public void SetInnerScope_OnFirstCharInInnerScope_InnerInnerMiddleScopeIsNull()
        {
            scope = new Scope("a text b");
            scope.DefineInnerScope(1, 5);//' text'
            scope.DefineInnerScope(1, 1);//' ' inside inner middle scope
            Assert.IsNull(scope.InnerMiddleScope.InnerMiddleScope);
        }

        [Test]
        public void SetInnerScope_OnLastCharInInnerScope_InnerInnerMiddleScopeIsNull()
        {
            scope = new Scope("a text b");
            scope.DefineInnerScope(1, 6);//' text '
            scope.DefineInnerScope(6, 1);//' ' in the end of inner middle scope
            Assert.IsNull(scope.InnerMiddleScope.InnerMiddleScope);
        }

        [Test]
        public void SetInnerScope_OnFirstCharInInnerScope_InnerInnerLeftScopeTextIsOK()
        {
            scope = new Scope("a text b");
            scope.DefineInnerScope(1, 5);//' text'
            scope.DefineInnerScope(1, 1);//' ' inside inner middle scope
            Assert.AreEqual(" ", scope.InnerMiddleScope.InnerLeftScope.Text);
        }

        [Test]
        public void SetInnerScope_2LevelsDeep_NoException()
        {
            scope = new Scope("abcde");
            scope.DefineInnerScope(1, 3);//'bcd'
            scope.DefineInnerScope(1, 2);//'bc'
            scope.DefineInnerScope(1, 1);//'bb'
        }

        [Test]
        public void SetInnerScope_2LevelsDeep_NoException2()
        {
            scope = new Scope("this is some text");
            Scope textScope = scope.DefineInnerScope(8, 5);//'some '
            Scope spaceScope = scope.DefineInnerScope(12, 1);//' ' before 'text'
            Scope exScope = scope.DefineInnerScope(9, 2);//'om'
        }

        [Test]
        public void SetInnerScope_OnInnerScope_SetsLeftScopeLengthCorrectly()
        {
            scope = new Scope("this is some text");
            Scope textScope = scope.DefineInnerScope(8, 5);//'some '
            Scope exScope = scope.DefineInnerScope(9, 2);//'om'
            Assert.AreEqual(1, textScope.InnerLeftScope.Length); //'s'
        }

        [Test]
        public void SetInnerScope_OnInnerScope_SetsRightScopeLengthCorrectly()
        {
            scope = new Scope("this is some text");
            Scope textScope = scope.DefineInnerScope(8, 4);//'some'
            Scope exScope = scope.DefineInnerScope(9, 2);//'om'
            Assert.AreEqual(1, textScope.InnerRightScope.Length); //'e'
        }

        [Test]
        public void SetInnerScope_OnInnerScope_SetsMiddleScopeLengthCorrectly()
        {
            scope = new Scope("this is some text");
            Scope textScope = scope.DefineInnerScope(8, 5);//'some '
            Scope exScope = scope.DefineInnerScope(9, 2);//'om'
            Assert.AreEqual(2, textScope.InnerMiddleScope.Length); //'s'
        }


        [Test]
        public void SetInnerScope_OnLastCharInInnerScope_InnerInnerRightScopeTextIsOK()
        {
            scope = new Scope("a text b");
            scope.DefineInnerScope(1, 6);//' text '
            scope.DefineInnerScope(6, 1);//' ' in the end of inner middle scope
            Assert.AreEqual(" ", scope.InnerMiddleScope.InnerRightScope.Text);
        }

        [Test]
        public void SetInnerScope_OnInnerScope_HasCorrectRightScopeText()
        {
            scope = new Scope("abcde");
            scope.DefineInnerScope(1, 3);//bcd
            scope.DefineInnerScope(2, 1);//'c'
            Assert.AreEqual("d", scope.InnerMiddleScope.InnerRightScope.Text);

        }

        [Test]
        public void SetInnerScope_OnInnerScope_WorksFromParent()
        {
            scope = new Scope("abcde");
            scope.DefineInnerScope(1, 3);//bcd
            scope.DefineInnerScope(2, 1);//'c'
            Assert.AreEqual("c", scope.InnerMiddleScope.InnerMiddleScope.Text);

        }

        [Test]
        public void SetInnerInnerScope_IsNotImplicit()
        {
            scope = new Scope("abcde");
            scope.DefineInnerScope(1, 3);//bcd
            Scope innerInnerScope = scope.DefineInnerScope(2, 1);//'c'
            Assert.IsFalse(innerInnerScope.IsImplicit);
        }

        [Test]
        public void SetInnerInnerScope_leftAndRightBecomeNonExplicit()
        {
            scope = new Scope("abcde");
            Scope innterOuterScope = scope.DefineInnerScope(1, 3);//bcd
            Scope innerInnerScope = scope.DefineInnerScope(2, 1);//'c'
            Assert.IsTrue(innterOuterScope.InnerLeftScope.IsImplicit);
            Assert.IsFalse(innterOuterScope.InnerMiddleScope.IsImplicit);
            Assert.IsTrue(innterOuterScope.InnerRightScope.IsImplicit);
        }

        [Test]
        public void Create_AsString_IsImplicit()
        {
            scope = new Scope("a");
            Assert.IsTrue(scope.IsImplicit);
        }

        [Test]
        public void Create_WithIndexes_IsNotImplicit()
        {
            scope = new Scope("a", 1);
            Assert.IsFalse(scope.IsImplicit);
        }

        [Test]
        public void Create_LengthIsCorrect()
        {
            scope = new Scope("a");
            Assert.AreEqual(1, scope.Length);
        }

        [Test]
        public void Create_LengthIsCorrect2()
        {
            scope = new Scope("abc");
            Assert.AreEqual(3, scope.Length);
        }
        ///TODO:
        ///- test that scope length and scope text corropond to right values when initialized fromopposite directions
        /// meaning: create scope with text has correct lenght
        /// and create inner scope has correct text



    }


}
