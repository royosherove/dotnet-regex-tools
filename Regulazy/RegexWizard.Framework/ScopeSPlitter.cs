using System.Collections.Generic;

namespace RegexWizard.Framework
{
    public struct SplitPoint
    {
        public SplitPoint(int startIndex, int length)
        {
            this.startIndex = startIndex;
            this.length = length;
        }

        private int startIndex;

        public int StartIndex
        {
            get { return startIndex; }
            set { startIndex = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private int length;
    }
    public class ScopeSplitter
    {
        public Scope Split(Scope root, SplitPoint splitPoint, bool isImplicit)
        {
            Scope inner = root.FindInnerScope(splitPoint.StartIndex, 1);
            int requiredLength = splitPoint.Length;
            if(requiredLength==inner.Length)
            {
                requiredLength -= 1;
            }
            Scope newScope = inner.DefineInnerScope(splitPoint.StartIndex, requiredLength);
            newScope.IsImplicit= isImplicit;
            return newScope;
        }

        public bool IsSplitNeededForNewScopeToLive(Scope root, int startIndex, int length)
        {
            return GetSplitPoints(root, startIndex, length).Count > 0;
        }

        public int GetRequiredSplitStartForNewScopeIn(Scope root, int startIndexOfWantedScope, int lengthOfWantedScope)
        {
            return GetSplitPoints(root, startIndexOfWantedScope, lengthOfWantedScope)[0].StartIndex;
        }

        public List<Scope> AutoSplit(Scope root, int startIndexForWantedScope, int lengthOfWantedScope, bool isImplicit)
        {
            List<Scope> newScopes = new List<Scope>();
            List<SplitPoint> splitPoints = GetSplitPoints(root, startIndexForWantedScope, lengthOfWantedScope);
            foreach (SplitPoint splitPoint in splitPoints)
            {
                Scope newInnerScope = Split(root, splitPoint, isImplicit);
                newScopes.Add(newInnerScope);
            }
            return newScopes;
        }

        public bool IsMultipleSplittingNeededFor(Scope root, int newScopeStartIndex, int newScopeLength)
        {
            int requestedEndPos = newScopeStartIndex + newScopeLength - 1;

            Scope innerLeft = root.FindInnerScope(newScopeStartIndex, 1);
            Scope innerRight = root.FindInnerScope(requestedEndPos, 1);

            bool isSpillingLeft = (innerLeft.StartPosInRootScope < newScopeStartIndex);
            bool isSpillingRight = (innerRight.EndPosInRootScope > requestedEndPos);
            return (isSpillingRight && isSpillingLeft);
        }

        public List<SplitPoint> GetSplitPoints(Scope root, int newScopeStartIndex, int newScopeLength)
        {
            List<SplitPoint> splitPoints = new List<SplitPoint>();
            if(root.IsFlat)
            {
                return splitPoints;
            }
            int requestedEndPos = newScopeStartIndex + newScopeLength - 1;
            Scope innerLeft = root.FindInnerScope(newScopeStartIndex, 1);
            Scope innerRight = root.FindInnerScope(requestedEndPos, 1);

            bool isSpillingLeft = (innerLeft.StartPosInRootScope < newScopeStartIndex);
            bool isSpillingRight = (innerRight.EndPosInRootScope > requestedEndPos);

            if (isSpillingLeft)
            {
                int splitLength = (innerLeft.EndPosInRootScope-newScopeStartIndex)+1;
                splitPoints.Add(new  SplitPoint(newScopeStartIndex, splitLength));
            }
            if (isSpillingRight)
            {
                int splitLength = (requestedEndPos-innerRight.StartPosInRootScope)+1;
                splitPoints.Add(new SplitPoint(innerRight.StartPosInRootScope, splitLength));
//                splitPoints.Add(new SplitPoint(requestedEndPos, splitLength));
            }
            return splitPoints;
        }
    }
}
