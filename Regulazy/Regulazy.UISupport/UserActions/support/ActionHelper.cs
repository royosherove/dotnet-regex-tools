using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RegexWizard.Framework;
using Regulazy.UI;
using Regulazy.UISupport.UserActions;

namespace Regulazy.UISupport
{
    public class ActionHelper
    {
        private static ISuggestionProvider m_SuggestionProvider=null;
        private static ScopeAwareRichTextBox txt;

        public static ScopeAwareRichTextBox Txt
        {
            get { return txt; }
            set { txt = value; }
        }

       
	
        public static void SetSuggestionProvider(ISuggestionProvider suggestionProvider)
        {
            m_SuggestionProvider = suggestionProvider;
        }

        public static ScopeActionsInfo GetActions(Scope scope)
        {
            ScopeActionsInfo info = new ScopeActionsInfo();
            if (scope == null)
            {
                AddActionsOnSelectedText(info);
                AddUserActions(info, scope);
                AddRuleSuggestions(info, scope);
                
            }
            else
            {
                AddUserActions(info, scope);
                AddRuleSuggestions(info, scope);
            }
            
            return info ;
        }

        private static void AddActionsOnSelectedText(ScopeActionsInfo info)
        {
            if (txt.ActiveScope != null)
            {
                string curText = txt.ActiveScope.Text;
                string curSel = txt.SelectedText;
                bool isSelectionSameActiveScope = curSel.Length == 0 || curSel == curText;
                if (isSelectionSameActiveScope)
                {
                    ScopeRenameAction rename = new ScopeRenameAction(txt.ActiveScope,txt);
                    info.UserActions.Add(rename);
                }
                else
                {
                    AddDefineNewAreaWithNameAction(info);
                }
            }
            
            
            
           
        }

        private static void AddUserActions(ScopeActionsInfo info, Scope scope)
        {
            Scope target = scope;
            if (target == null)
            {
                target = txt.RootScope;
            }
            if(txt.SelectionLength==0)
            {
                AddRenameAction(info, target);
            }
        }

        private static void AddDefineNewAreaWithNameAction(ScopeActionsInfo info)
        {
            DefineScopeWithNameAction action = new DefineScopeWithNameAction(txt);
            info.UserActions.Add(action);
        }

//        private static void AddGroupheseAction(ScopeActionsInfo info)
//        {
//            if(txt.SelectedText.Length>0)
//            {
//                int start = txt.SelectionStart;
//                int length = txt.SelectionLength;
//
//                List<Scope> scopes = txt.RootScope.FindScopesInRange(start, length);
//                if(scopes.Count>1)
//                {
//                    DefineEncapsulatingScopeAction define = new DefineEncapsulatingScopeAction(txt, txt.RootScope);
//                    info.UserActions.Add(define);
//                }
//            }
//        }

        private static void AddRenameAction(ScopeActionsInfo info, Scope target)
        {
            ScopeRenameAction rename=new ScopeRenameAction(target, txt);
            info.UserActions.Add(rename);
            Scope parent = target.ParentScope;
            if(target.IsRoot)
            {
                    return;
            }

            string parentLevelPrefix = "-----";
            while (parent!=null)
            {
                ScopeRenameAction renameParentScope = new ScopeRenameAction(parent, txt);
                renameParentScope.TitlePrefix = parentLevelPrefix +  "| Rename Parent";
                info.UserActions.Add(renameParentScope);
                parent = parent.ParentScope;
                parentLevelPrefix += "-----";
            }
        }

        private static void AddRuleSuggestions(ScopeActionsInfo info, Scope scope)
        {
            List<Suggestion> suggestions = m_SuggestionProvider.GetSuggestions(scope);
            foreach (Suggestion sug in suggestions)
            {
                ApplySuggestionOnSelectionAction action = new ApplySuggestionOnSelectionAction(txt,sug);
                info.RuleSuggestions.Add(action);
            }
        }
    }
}
