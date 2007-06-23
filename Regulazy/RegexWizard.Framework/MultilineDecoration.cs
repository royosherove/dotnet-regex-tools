using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RegexWizard.Framework
{
    public class MultilineSuggestionDecorator : SuggestionDecorator
    {
        public MultilineSuggestionDecorator(Suggestion sug)
            : base(sug)
        {
            m_sug.RegexMatchFlags.Add(MatchFlags.Multiline);
        }

        public override string Description
        {
            get
            {
                return base.Description + ", Multiline";
            }
            set
            {
                base.Description = value;
            }
        }
    }

    public enum MatchFlags
    {
        Multiline,
        IgnoreWhitespaces
    }

    public class GroupedSuggestionDecorator : SuggestionDecorator
    {
        protected Type MyType
        {
            get { return GetType(); }
        }
        private string scopeName = String.Empty;
        private string baseRegexText=String.Empty;
        public GroupedSuggestionDecorator(Suggestion inner, Scope namedScope)
            : base(inner)
        {
            scopeName = namedScope.Name;
            baseRegexText = inner.RegexText;
        }

        public override string RegexText
        {
            get
            {
                if (scopeName != string.Empty)
                {
                    string groupRegex = string.Format(@"(?<{0}>{1})", scopeName, baseRegexText);
                    return groupRegex;
                }
                else
                {
                    return baseRegexText;
                }
            }
        }

        public string GroupName
        {
            get
            {
                return  scopeName;
            }
            set { scopeName = value; }
        }
    }
    
    public class UnNamedGroupedSuggestionDecorator : SuggestionDecorator
    {
        
        private string scopeName = String.Empty;

        public Suggestion Innersuggestion
        {
            get { return innersuggestion; }
        }

        private Suggestion innersuggestion;
        private string originalText=String.Empty;

        public UnNamedGroupedSuggestionDecorator(Suggestion inner, Scope namedScope)
            : base(inner)
        {
            scopeName = namedScope.Name;
            innersuggestion = inner;
            originalText = inner.RegexText;
        }


        public override string RegexText
        {
            get
            {
               
                
                if (scopeName == string.Empty)
                {
                    string groupRegex = string.Format(@"({0})", originalText);
                    return groupRegex;
                }
                else
                {
                    return base.RegexText;
                }
            }
        }
    }
    public class SuggestionDecorator : Suggestion
    {
        public override List<MatchFlags> RegexMatchFlags
        {
            get
            {
                return m_sug.RegexMatchFlags;
            }
            set
            {
                m_sug.RegexMatchFlags = value;
            }
        }
        protected Suggestion m_sug = null;
        public SuggestionDecorator(Suggestion sug)
        {
            m_sug = sug;
        }

        public override string ToString()
        {
            return Description;
        }
        public override string Description
        {
            get
            {
                return m_sug.Description;
            }
            set
            {
                m_sug.Description = value;
            }
        }

        public override string Arity
        {
            get
            {
                return m_sug.Arity;
            }
            set
            {
                m_sug.Arity = value;
            }
        }

        public override int Probability
        {
            get
            {
                return m_sug.Probability;
            }
            set
            {
                m_sug.Probability = value;
            }
        }


        public override string RegexText
        {
            get
            {
                return m_sug.RegexText;
            }
            set
            {
                m_sug.RegexText = value;
            }
        }
    }
}
