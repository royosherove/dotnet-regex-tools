using System.Collections.Generic;
using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class ScopeSplitterTests
    {
        ScopeSplitter splitter = new ScopeSplitter();
        [SetUp]
        public void setup()
        {
            splitter = new ScopeSplitter();
        }
        
        
        [Test]
        public void Split_1()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(0, 2);//ab
            splitter.Split(root, new SplitPoint(1,1), true);
            Assert.IsNotNull(root.InnerLeftScope.InnerRightScope);
            Assert.AreEqual("b", root.InnerLeftScope.InnerRightScope.Text);
        }
        
        
        
        [Test]
        public void Split_InnerRight_AddsCorrectLocationToNewScope()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(0, 2);//ab
            splitter.Split(root, new SplitPoint(1,1), true);
            Assert.AreEqual(ScopeLocation.Right, root.InnerLeftScope.InnerRightScope.Location);
        }
        
        [Test]
        public void Split_InnerLeft_AddsCorrectLocationToNewScope()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 2);//ab
            splitter.Split(root, new SplitPoint(1,1), true);
            Assert.AreEqual(ScopeLocation.Left, root.InnerRightScope.InnerLeftScope.Location);
            Assert.AreEqual(ScopeLocation.Right, root.InnerRightScope.InnerRightScope.Location);
            Assert.AreEqual(ScopeLocation.Left, root.InnerLeftScope.Location);
        }

        [Test]
        public void Split_SetsExplicitCorrectly_False()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(0, 2);//ab
            splitter.Split(root, new SplitPoint(1,1), false);
            Assert.AreEqual(false, root.InnerLeftScope.InnerRightScope.IsImplicit);
        }

        [Test]
        public void Split_SetsExplicitCorrectly_True()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(0, 2);//ab
            splitter.Split(root, new SplitPoint(1,1), true);
            Assert.AreEqual(true, root.InnerLeftScope.InnerRightScope.IsImplicit);
        }

        [Test]
        public void Split_2()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 2);//bc
            splitter.Split(root, new SplitPoint(1,1), true);
            Assert.IsNotNull(root.InnerRightScope.InnerRightScope);
            Assert.AreEqual("c", root.InnerRightScope.InnerRightScope.Text);
        }

        [Test]
        public void Split_4()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(1, 3);//bcd
            splitter.Split(root, new SplitPoint(2,2), true);
            Assert.AreEqual("cd", root.InnerRightScope.InnerRightScope.Text);
        }

        [Test]
        public void Split_5()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(0, 3);//bcd
            splitter.Split(root, new SplitPoint(1,2), true);
            Assert.AreEqual("bc", root.InnerLeftScope.InnerRightScope.Text);
        }


        [Test]
        public void GetSplitPoints_OnTwoScopesOnBothSides_ReturnsTwo()
        {
            Scope root = new Scope("abcde");
            root.DefineInnerScope(2, 1);//c

            List<SplitPoint> points = splitter.GetSplitPoints(root, 1, 3);///bcd
            Assert.AreEqual(2, points.Count);
            Assert.AreEqual(1, points[0].StartIndex);
            Assert.AreEqual(3, points[1].StartIndex);
        }
        
        [Test]
        public void GetSplitPoints_OnTwoScopesOnBothSides_ReturnsTwo2()
        {
            Scope root = new Scope("abcdef");
            root.DefineInnerScope(3, 1);//d

            List<SplitPoint> points = splitter.GetSplitPoints(root, 2, 3);//cde
            Assert.AreEqual(2, points.Count);
            Assert.AreEqual(2, points[0].StartIndex);
            Assert.AreEqual(4, points[1].StartIndex);
        }
        
        
        [Test]
        public void GetSplitPoints_2ndSplitIsNotAtStartOfRightScope_ReturnsCorrectIndexOf2ndSplit()
        {
            Scope root = new Scope("0123456");
            root.DefineInnerScope(3, 3);//345

            List<SplitPoint> points = splitter.GetSplitPoints(root, 2, 3);//234
            Assert.AreEqual(2, points.Count);
            Assert.AreEqual(2, points[0].StartIndex);
            Assert.AreEqual(3, points[1].StartIndex);
            Assert.AreEqual(2, points[1].Length);
        }
        
        [Test]
        public void GetSplitPoints_OnlySpillsLeft_GetsOnlyLeftSplitPoint()
        {
            Scope root = new Scope("0123456");
            root.DefineInnerScope(3, 3);//012|345|6

            List<SplitPoint> points = splitter.GetSplitPoints(root, 2, 4);//01(|2|345)|6
            Assert.AreEqual(1, points.Count);
            Assert.AreEqual(2, points[0].StartIndex);
            Assert.AreEqual(1, points[0].Length);
        }
        
        [Test]
        public void GetSplitPoints_OnlySpillsLeft_SplitPointLengthIsCorrect()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(3, 3);//012|345

            List<SplitPoint> points = splitter.GetSplitPoints(root, 1, 5);//0(12|345)
            Assert.AreEqual(1, points.Count);
            SplitPoint firstPoint = points[0];
            
            Assert.AreEqual(1, firstPoint.StartIndex);
            Assert.AreEqual(2, firstPoint.Length);
        }
        
        [Test]
        public void GetSplitPoints_OnlySpillsRight_SplitPointLengthIsCorrect()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(3, 3);//012|345

            List<SplitPoint> points = splitter.GetSplitPoints(root, 0, 5);//(012|34)5)
            Assert.AreEqual(1, points.Count);
            SplitPoint firstPoint = points[0];
            
            Assert.AreEqual(3, firstPoint.StartIndex);
            Assert.AreEqual(2, firstPoint.Length);
        }

        [Test]
        public void GetSplitPoints_1stSplitIsNotAtStartOfleftScope_ReturnsCorrectIndexOf1stSplit()
        {
            Scope root = new Scope("0123456");
            root.DefineInnerScope(3, 3);//345

            List<SplitPoint> points = splitter.GetSplitPoints(root, 1, 4);//1234
            Assert.AreEqual(2, points.Count);
            Assert.AreEqual(1, points[0].StartIndex);
            Assert.AreEqual(3, points[1].StartIndex);
            Assert.AreEqual(2, points[1].Length);
        }

        [Test]
        public void Split_MultiplePoints()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(3, 1);//012|3|45
            
            splitter.AutoSplit(root, 2,3,true);//01|(2|3|4)|5
            Assert.AreEqual("2",root.InnerLeftScope.InnerRightScope.Text);
            Assert.AreEqual("3", root.InnerMiddleScope.Text);
            Assert.AreEqual("4", root.InnerRightScope.InnerLeftScope.Text);
            
        }
        
        [Test]
        public void Split_MultiplePoints_ReturnsNewScopes()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(3, 1);//012|3|45
            
            List<Scope> scopes= splitter.AutoSplit(root, 2,3,true);//01|(2|3|4)|5
            Assert.AreEqual("2",scopes[0].Text);
            Assert.AreEqual("4",scopes[1].Text);
        }
        
        [Test]
        public void Split_MultiplePoints_ReturnsNewScopes2()
        {
            Scope root = new Scope("012345");
            root.DefineInnerScope(2, 1);//01|2|345
            
            List<Scope> scopes= splitter.AutoSplit(root, 1,3,true);//0|(1|2|3|)45
            Assert.AreEqual("1",scopes[0].Text);
            Assert.AreEqual("3",scopes[1].Text);
        }
        
        [Test]
        public void Split_OnFirstCharOf2CharScopeOnRight()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(0, 1);//0|12
            splitter.Split(root, new SplitPoint(2,1), true);//0|(1|2)
            Assert.AreEqual("1", root.InnerRightScope.InnerLeftScope.Text);
        }
        
        [Test]
        public void IsSplitNeededForNewScopeToLive_NewScopeSmallerThanInner_False()
        {
            Scope root = new Scope("abc");
            Assert.IsFalse(splitter.IsSplitNeededForNewScopeToLive(root, 1, 1));
        }

        [Test]
        public void IsSplitNeededForNewScopeToLive_NewScopeExactlyLikeInner_False()
        {
            Scope root = new Scope("abc");
            Assert.IsFalse(splitter.IsSplitNeededForNewScopeToLive(root, 0, 3));
        }

        [Test]
        public void IsSplitNeededForNewScopeToLive_NewScopeExactlyLikeInner_False2()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 1);
            Assert.IsFalse(splitter.IsSplitNeededForNewScopeToLive(root, 1, 1));
        }

        [Test]
        public void IsSplitNeededForNewScopeToLive_NewScopeCoversPartiallyRightScope_True()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(0, 1);
            bool result = splitter.IsSplitNeededForNewScopeToLive(root, 0, 2);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSplitNeededForNewScopeToLive_NewScopeCoversPartiallyRightScope_True2()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(2, 1);
            bool result = splitter.IsSplitNeededForNewScopeToLive(root, 1, 2);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetRequiredSplitStart_NewScopeSpillsToTheRight()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(1, 2);//bc
            int splitIndex = splitter.GetRequiredSplitStartForNewScopeIn(root, 0, 2);//for new scope(ab)
            Assert.AreEqual(1, splitIndex);

        }

        [Test]
        public void GetRequiredSplitStart_NewScopeSpillsToTheRight2()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(2, 2);//cd
            int splitIndex = splitter.GetRequiredSplitStartForNewScopeIn(root, 0, 3);//for new scope(abc)
            Assert.AreEqual(2, splitIndex);

        }

        [Test]
        public void GetRequiredSplitStart_NewScopeSpillsToTheLeft()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(0, 2);//ab
            int splitIndex = splitter.GetRequiredSplitStartForNewScopeIn(root, 1, 2);//for new scope(bc)
            Assert.AreEqual(1, splitIndex);

        }

        [Test]
        public void GetRequiredSplitStart_NewScopeSpillsToTheLeft2()
        {
            Scope root = new Scope("abcd");
            root.DefineInnerScope(0, 2);//ab
            int splitIndex = splitter.GetRequiredSplitStartForNewScopeIn(root, 1, 3);//for new scope(bcd)
            Assert.AreEqual(1, splitIndex);

        }

        [Test]
        public void GetRequiredSplitStart_NewScopeSpillsToBothSides_GetsLeftSideSplitStart()
        {
            Scope root = new Scope("abcde");
            root.DefineInnerScope(2, 1);//c
            int splitIndex = splitter.GetRequiredSplitStartForNewScopeIn(root, 1, 3);//for new scope(bcd)
            Assert.AreEqual(1, splitIndex);
        }

        [Test]
        public void IsMultipleSplitsNeeded_ScopeSpillsOnBothSides_yes()
        {
            Scope root = new Scope("abcde");
            root.DefineInnerScope(2, 1);//c
            Assert.IsTrue(splitter.IsMultipleSplittingNeededFor(root, 1, 3));//bcd
        }

        [Test]
        public void IsMultipleSplitsNeeded_ScopeSpillsOnLeftOnly_No()
        {
            Scope root = new Scope("abc");
            root.DefineInnerScope(2, 1);//c
            Assert.IsFalse(splitter.IsMultipleSplittingNeededFor(root, 1, 2));//bc
        }
        
        
        [Test]
        public void NotTooManySplits()
        {
            Scope root = new Scope("012");
            root.DefineInnerScope(2, 1);//(01)(2)
            root.DefineInnerScope(1, 1);//(0|1)(2)
            List<Scope> list = splitter.AutoSplit(root, 1, 2, false);
            Assert.AreEqual(0,list.Count);
        }

    }
}
