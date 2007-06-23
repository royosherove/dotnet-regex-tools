using System;
using System.Collections.Generic;
using System.Text;

namespace Regulazy.UISupport
{
    public class ActionList:List<UserAction>
    {
        
    }
    public class ScopeActionsInfo
    {
        
        private ActionList ruleSuggestionActions=new  ActionList();

        public ActionList RuleSuggestions
        {
            get { return ruleSuggestionActions; }
        }
        
        private ActionList userActions=new  ActionList();

        public ActionList UserActions
        {
            get { return userActions; }
        }
        
    }
}
