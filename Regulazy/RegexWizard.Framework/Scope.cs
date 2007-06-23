using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RegexWizard.Framework
{
    public enum ScopeLocation
    {
        Left,
        Middle,
        Right
    }

    [Serializable]
    public class Scope : SelfSerializer
    {
        private ScopeLocation location;

        public ScopeLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        public Scope()
        {
        }

        private string name = String.Empty;

        public string Name
        {
            [DebuggerStepThrough]
            get { return name; }
            [DebuggerStepThrough]
            set { name = value; }
        }

        public override string ToString()
        {
            return Text;
        }

        private int startPosInRootScope;

        public int StartPosInRootScope
        {
            [DebuggerStepThrough]
            get { return startPosInRootScope; }
            [DebuggerStepThrough]
            set { startPosInRootScope = value; }
        }



        public int Length
        {
            get { return text.Length; }
        }

        private Scope innerLeftScope;

        public Scope InnerLeftScope
        {
            [DebuggerStepThrough]
            get { return innerLeftScope; }
            [DebuggerStepThrough]
            set { innerLeftScope = value; }

        }

        public Scope(string text, string name)
            : this(text)
        {
            this.name = name;
        }

        public Scope(string text, int startPos)
            : this(text)
        {
            startPosInRootScope = startPos;
            isImplicit = false;
        }

        public Scope(string Text, int StartPos, bool IsExplicitScope)
            : this(Text, StartPos)
        {
            IsExplicit = IsExplicitScope;
        }

        private Scope innerMiddleScope = null;

        public Scope InnerMiddleScope
        {
            [DebuggerStepThrough]
            get { return innerMiddleScope; }
            [DebuggerStepThrough]
            set { innerMiddleScope = value; }
        }

        private bool isImplicit = true;

        public bool IsImplicit
        {
            [DebuggerStepThrough]
            get { return isImplicit; }
            [DebuggerStepThrough]
            set { isImplicit = value; }
        }

        public bool IsExplicit
        {
            get { return !isImplicit; }
            set { isImplicit = !value; }
        }

        private string text;

        public string Text
        {
            [DebuggerStepThrough]
            get { return text; }
            [DebuggerStepThrough]
            set { text = value; }
        }

        private ScopeMetaData metaData = new ScopeMetaData();

        public ScopeMetaData MetaData
        {
            [DebuggerStepThrough]
            get { return metaData; }
            [DebuggerStepThrough]
            set { metaData = value; }
        }

        public Scope(string text)
        {
            this.text = text;
        }

        public bool IsFlat
        {
            get { return innerMiddleScope == null && innerRightScope == null; }
        }

        private Scope innerRightScope;

        public Scope InnerRightScope
        {
            [DebuggerStepThrough]
            get { return innerRightScope; }
            [DebuggerStepThrough]
            set { innerRightScope = value; }
        }

        private Scope parentScope = null;

        public Scope ParentScope
        {
            [DebuggerStepThrough]
            get { return parentScope; }
        }


        public Scope DefineInnerScope(int startPosInParent, int length)
        {
            if (!IsFlat)
            {
                Scope possibleDefinedScope = TryDefineScopeInInnerScope(length, startPosInParent);
                if (possibleDefinedScope!=null)
                {
                    return possibleDefinedScope;
                }
            }

            bool isInnerScopeSameAsParentScope =
                (startPosInParent == startPosInRootScope && length == this.Length);
            if (isInnerScopeSameAsParentScope)
            {
                throw new InvalidOperationException(
                    "You can't set an inner scope with the same length of the parent scope");
            }


            string parentFullText = getParentFullText();
            string scopeText = parentFullText.Substring(startPosInParent, length);
            Scope newScope = new Scope(scopeText, startPosInParent);
            newScope.parentScope = this;
            newScope.Root = this.Root;

            InhaleScopesInsideSelection(length, startPosInParent, newScope);
            addNewScopeAndImplicitScopes(newScope);

            return newScope;
        }

        private Scope TryDefineScopeInInnerScope(int length, int startPosInParent)
        {
            if (innerMiddleScope != null && innerMiddleScope.EncapsulatesTextPart(startPosInParent, length))
            {
                return innerMiddleScope.DefineInnerScope(startPosInParent, length);
            }
            if (innerLeftScope.EncapsulatesTextPart(startPosInParent, length))
            {
                return innerLeftScope.DefineInnerScope(startPosInParent, length);
            }
            if (innerRightScope.EncapsulatesTextPart(startPosInParent, length))
            {
                return innerRightScope.DefineInnerScope(startPosInParent, length);
            }
            return null;
        }

        public void InhaleScopesInsideSelection(int length, int startPosInParent, Scope inhaleTo)
        {
            ScopeSplitter splitter = new ScopeSplitter();
            splitter.AutoSplit(this, startPosInParent, length,true);
            List<Scope> scopesToInhale = FindScopesInRange(startPosInParent, length);
            if (scopesToInhale.Count > 0)
            {
                inhaleTo.innerLeftScope = scopesToInhale[0];
                if (scopesToInhale.Count == 3)
                {
                    inhaleTo.innerMiddleScope = scopesToInhale[1];
                    inhaleTo.innerMiddleScope.parentScope = inhaleTo;
                    
                    inhaleTo.innerRightScope = scopesToInhale[2];
                }
                else
                {
                    inhaleTo.innerRightScope = scopesToInhale[1];
                }

                inhaleTo.innerLeftScope.parentScope = inhaleTo;
                inhaleTo.innerRightScope.parentScope = inhaleTo;
                

            }
        }

        private Scope root = null;

        public Scope Root
        {
            get
            {
                if (this.IsRoot)
                {
                    return this;
                }
                return root;
            }
            set { root = value; }
        }

        public bool HasInnerScopeWithSameEndPos(int endPosIndex)
        {
            bool found = false;
            if (innerLeftScope != null && innerLeftScope.EndPosInRootScope == endPosIndex)
                found = true;
            if (innerRightScope != null && innerRightScope.EndPosInRootScope == endPosIndex)
                found = true;
            if (innerMiddleScope != null && innerMiddleScope.EndPosInRootScope == endPosIndex)
                found = true;

            return found;
        }

        public bool HasInnerScopeWithSameStartPos(int startPosIndex)
        {
            bool found = false;
            if (innerLeftScope != null && innerLeftScope.startPosInRootScope == startPosIndex)
                found = true;
            if (innerRightScope != null && innerRightScope.startPosInRootScope == startPosIndex)
                found = true;
            if (innerMiddleScope != null && innerMiddleScope.startPosInRootScope == startPosIndex)
                found = true;

            return found;
        }

        public bool EncapsulatesTextPart(int startPosInParent, int length)
        {
            int myEndPos = EndPosInRootScope;
            int suggestedEndPos = startPosInParent + length - 1;

            return (startPosInRootScope <= startPosInParent &&
                    this.Length >= length &&
                    myEndPos >= suggestedEndPos);
        }

        private string getParentFullText()
        {
            Scope upperScope = this;
            while (upperScope.ParentScope != null)
            {
                upperScope = upperScope.ParentScope;
            }
            return upperScope.text;
        }

        private Scope getRoot()
        {
            Scope upperScope = this;
            while (!upperScope.IsRoot)
            {
                upperScope = upperScope.ParentScope;
            }
            return upperScope;
        }

        private void addNewScopeAndImplicitScopes(Scope newScope)
        {
            int newScopeEndPos = newScope.EndPosInRootScope;
            bool isNewScopeRightAligned = (newScopeEndPos == Length - 1 + startPosInRootScope);
            bool isNewScopeLeftAligned = (newScope.startPosInRootScope == startPosInRootScope);
            innerMiddleScope = null;
            if (isNewScopeLeftAligned)
            {
                newScope.location = ScopeLocation.Left;
                innerLeftScope = newScope;
                if (innerRightScope == null)
                {
                    createRightScope(newScope);
                }
                else
                {
                    innerRightScope.AdjustForInhaledScopeToTheLeft(newScope);
                }

            }
            else if (isNewScopeRightAligned)
            {
                newScope.location = ScopeLocation.Right;
                if(innerLeftScope==null)
                {
                    createLeftScope(newScope);
                }
                else
                {
                    innerLeftScope.AdjustForInhaledScopeToTheRight(newScope);
                }
                innerRightScope = newScope;
            }
            else
            {
                newScope.location = ScopeLocation.Middle;
                createLeftScope(newScope);
                innerMiddleScope = newScope;
                createRightScope(innerMiddleScope);
            }
        }

        private void AdjustForInhaledScopeToTheRight(Scope newScope)
        {
            AdjustTextToEndOfScopeToTheRight(newScope);
            RemoveInnerScopesWithWrongParent();
        }
         private void AdjustForInhaledScopeToTheLeft(Scope newScope)
        {
            AdjustTextToEndOfScopeToTheLeft(newScope);
            RemoveInnerScopesWithWrongParent();
        }

        private void AdjustTextToEndOfScopeToTheRight(Scope newScope)
        {
            bool textChanged = false;

            while (newScope.StartPosInRootScope <= EndPosInRootScope)
            {
                textChanged = true;

                Text = Text.Substring(0, Text.Length - 1);
                if (textChanged)
                {
                    Suggestions.Clear();
                }
            }
            if (textChanged)
            {
                Suggestions.Clear();
            }
        }
        
        private void AdjustTextToEndOfScopeToTheLeft(Scope newScope)
        {
            bool textChanged = false;
            while (newScope.EndPosInRootScope>=StartPosInRootScope)
            {
                textChanged = true;
                Text = Text.Substring(1);
                startPosInRootScope++;
            }
            if(textChanged)
            {
                Suggestions.Clear();
            }
        }

        private void createRightScope(Scope middleScope)
        {
            int beforeScopeEndPosInParent = middleScope.EndPosInRootScope + 1;
            string parentText = getParentFullText();
            string newText =
                parentText.Substring(beforeScopeEndPosInParent,
                                     EndPosInRootScope - middleScope.EndPosInRootScope);

            Scope afterScope = new Scope(newText, beforeScopeEndPosInParent);
            afterScope.parentScope = this;
            afterScope.isImplicit = true;
            afterScope.location = ScopeLocation.Right;
            innerRightScope = afterScope;
        }


        private string arity = string.Empty;

        public string Arity
        {
            get { return arity; }
            set { arity = value; }
        }

        private string prototype = string.Empty;

        public string Prototype
        {
            get { return prototype; }
            set { prototype = value; }
        }

        private void createLeftScope(Scope afterScope)
        {
            string newScopeText =
                getParentFullText().Substring(startPosInRootScope, afterScope.startPosInRootScope - startPosInRootScope);
            Scope beforeScope =
                new Scope(newScopeText, startPosInRootScope);
            beforeScope.isImplicit = true;
            beforeScope.parentScope = this;

            beforeScope.location = ScopeLocation.Left;
            innerLeftScope = beforeScope;
        }


        public void RemoveInnerScope(Scope inner)
        {
            RemoveInnerScope(inner,false);
        }

        private void RemoveInnerScope(Scope inner, bool force)
        {
            if (inner.IsImplicit && !force)
            {
                throw new InvalidOperationException(
                    "You can't remove an implicit Scope, you can only remove an Explicit one");
            }
            innerMiddleScope = null;
            innerLeftScope = null;
            innerRightScope = null;
        }



        public Scope[] GetInnerScopes()
        {
            if (innerMiddleScope == null && innerLeftScope == null && innerRightScope == null)
            {
                return new Scope[] { };
            }

            if (innerMiddleScope != null)
            {
                return new Scope[] { innerLeftScope, innerMiddleScope, innerRightScope };
            }
            else
            {
                return new Scope[] { innerLeftScope, innerRightScope };
            }
        }

        private List<Suggestion> suggestions = new List<Suggestion>();
        private bool visitedByGroup = false;

        public List<Suggestion> Suggestions
        {
            get { return suggestions; }
            set { suggestions = value; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Scope))
            {
                return false;
            }

            Scope compared = obj as Scope;
            if (compared.startPosInRootScope == startPosInRootScope
                && compared.Length == Length
                && compared.text == text
                && compared.IsImplicit == IsImplicit)
            {
                return true;
            }

            return false;
        }

        public int EndPosInRootScope
        {
            get { return startPosInRootScope + Length - 1; }
        }

        public bool IsRoot
        {
            get { return (ParentScope == null); }
        }

        public bool VisitedByGroup
        {
            get
            {
                return visitedByGroup;
            }
            set { visitedByGroup = value; }
        }

        public Scope FindInnerScope(int startPos, int length)
        {
            int requestedEndPos = startPos + length - 1;

            if (EndPosInRootScope < requestedEndPos ||
                startPos < startPosInRootScope)
            {
                throw new InvalidOperationException("Invalid start position requested");
            }
            if (IsFlat)
            {
                return this;
            }

            if (innerMiddleScope != null &&
                innerMiddleScope.EncapsulatesTextPart(startPos, length))
            {
                return innerMiddleScope.FindInnerScope(startPos, length);
            }

            if (innerRightScope.EncapsulatesTextPart(startPos, length))
            {
                return innerRightScope.FindInnerScope(startPos, length);
            }
            if (innerLeftScope.EncapsulatesTextPart(startPos, length))
            {
                return innerLeftScope.FindInnerScope(startPos, length);
            }
            return null;
        }

        public List<Scope> FindScopesInRange(int startIndex, int length)
        {
            bool foundLeft = false, foundRight = false, foundMiddle = false;

            List<Scope> found = new List<Scope>();

            if (this.FitsExactlyTo(startIndex, length))
            {
                found.Add(this);
                return found;
            }
            if (innerLeftScope != null && innerLeftScope.IsEncapsulatedIn(startIndex, length))
            {
                found.Add(innerLeftScope);
                foundLeft = true;
            }
            if (innerRightScope != null && innerRightScope.IsEncapsulatedIn(startIndex, length))
            {
                found.Add(innerRightScope);
                foundRight = true;
            }
            if (innerMiddleScope != null && innerMiddleScope.IsEncapsulatedIn(startIndex, length))
            {
                found.Add(innerMiddleScope);
                foundMiddle = true;
            }

            if (!foundLeft && innerLeftScope != null && innerLeftScope.IsFlat == false)
            {
                foreach (Scope scope in innerLeftScope.FindScopesInRange(startIndex, length))
                {
                    found.Add(scope);
                }
            }

            if (!foundMiddle && innerMiddleScope != null && innerMiddleScope.IsFlat == false)
            {
                foreach (Scope scope in innerMiddleScope.FindScopesInRange(startIndex, length))
                {
                    found.Add(scope);
                }
            }

            if (!foundRight && innerRightScope != null && innerRightScope.IsFlat == false)
            {
                foreach (Scope scope in innerRightScope.FindScopesInRange(startIndex, length))
                {
                    found.Add(scope);
                }
            }

            found.Sort(Scope.CompareScopesByLocation);
            return found;
        }

        public static int CompareScopesByLocation(Scope x, Scope y)
        {

            if (x.startPosInRootScope < y.startPosInRootScope)
            {
                return -1;
            }

            if (x.startPosInRootScope > y.startPosInRootScope)
            {
                return 1;
            }

            return 0;
        }


        private bool FitsExactlyTo(int startIndex, int length)
        {
            return StartPosInRootScope == startIndex && Length == length;
        }

        private bool IsEncapsulatedIn(int startIndex, int length)
        {
            int requestedEndPos = startIndex + length - 1;
            return this.StartPosInRootScope >= startIndex && this.EndPosInRootScope <= requestedEndPos;
        }

        private void RemoveInnerScopesWithWrongParent()
        {
            foreach (Scope innerScope in GetInnerScopes())
            {
                if (innerScope.parentScope != this)
                {
                    RemoveInnerScope(innerScope,true);
                }
            }
        }
    }
}