using System;
using System.Collections.Generic;
using System.Text;
using RegexWizard.Framework;

namespace Regulazy.UISupport
{
    public interface ISuggestionProvider
    {
        
        List<Suggestion> GetSuggestions(Scope s);
    }
}
