using System;
using System.Collections.Generic;
using System.Text;

namespace RegexWizard.Framework
{
    public class ScopeParser
    {
        private Dictionary<string, IAdvisor> advisorsPool = new Dictionary<string, IAdvisor>();
        public void AddAdvisor(IAdvisor advise, string key)
        {
            advisorsPool.Add(key, advise);
        }

        public void Visit(Scope scope)
        {
            if (scope == null)
            {
                return;
            }
            Visit(scope.InnerLeftScope);
            Visit(scope.InnerMiddleScope);
            Visit(scope.InnerRightScope);

            scope.Suggestions.Clear();
            foreach (string category in advisorsPool.Keys)
            {
                if (category == scope.MetaData.Category)
                {
                    IAdvisor advisor = advisorsPool[category];
                    foreach (Suggestion advice in advisor.Suggest(scope))
                    {
                        scope.Suggestions.Add(advice);
                    }
                }
            }
        }
    }
}
