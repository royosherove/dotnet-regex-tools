using System;
using System.Collections.Generic;

namespace RegexWizard.Framework
{
    [Serializable]
    public class Suggestion
    {

        public override string ToString()
        {
            return Description+ ", " + RegexText ;
        }
        private List<MatchFlags> m_MatchFlags=new List<MatchFlags>();

        
        public virtual List<MatchFlags> RegexMatchFlags
        {
            get { return m_MatchFlags; }
            set { m_MatchFlags = value; }
        }
	    
        public Suggestion()
        {

        }
        private string regex;

        public virtual string RegexText
        {
            get { return regex; }
            set { regex = value; }
        }

        private string description;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public  override bool Equals(object obj)
        {
            if (!(obj is Suggestion))
            {
                return false;
            }
            
            Suggestion test = obj as Suggestion;
            return (test.arity == this.arity
                && test.description == this.description
                && test.probability == this.probability
                && test.regex==this.regex);
        }

        public Suggestion(string matchText, string desc)
        {
            description = desc.Trim();
            regex = matchText;
        }

        private int probability;

        public virtual int Probability
        {
            get { return probability; }
            set { probability = value; }
        }

        public Suggestion(string matchText, string desc, int probabilityOfOccuring)
            : this(matchText, desc)
        {
            probability = probabilityOfOccuring;
        }
        
        public Suggestion(string matchText, string desc, string arity)
            : this(matchText, desc)
        {
            this.arity = arity;    
        }

        private string arity=string.Empty;
        private bool visitedByGroup;

        public virtual string Arity
        {
            get { return arity; }
            set { arity = value; }
        }

        public bool VisitedByGroup
        {
            get { return visitedByGroup; }
            set { visitedByGroup=value; }
        }
    }
}
