using System;

namespace RegexWizard.Framework
{
    [Serializable]
    public class RegexWizardProjectData:SelfSerializer
    {
        private Scope rootScope = new Scope("");
        private string sampleText = string.Empty;

        public string SampleText
        {
            get { return sampleText; }
            set { sampleText = value; }
        }

        public Scope RootScope
        {
            get { return rootScope; }
            set { rootScope = value; }
        }

        public override bool Equals(object obj)
        {
            RegexWizardProjectData temp = obj as RegexWizardProjectData;
            if(obj is RegexWizardProjectData)
            {
                if(!temp.RootScope.Equals(rootScope))
                {
                    return false;
                }
                
                return true;
            }

            return false;
        }
    }
}
