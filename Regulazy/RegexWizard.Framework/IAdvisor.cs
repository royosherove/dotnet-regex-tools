using System;
namespace RegexWizard.Framework
{
    public interface IAdvisor
    {
        void Learn(params Suggestion[] suggestions);
        void Learn(Suggestion suggestion);
        System.Collections.Generic.List<Suggestion> Suggest(string input);
        System.Collections.Generic.List<Suggestion> Suggest(Scope input);
    }
}
