using System;
using System.Collections.Generic;
using System.Text;

namespace RegexWizard.Framework
{
    [Serializable]
    public class ScopeMetaData
    {
        private string category=string.Empty;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

    }
}
