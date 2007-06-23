using System;
using System.Collections.Generic;
using System.Text;

namespace Regulazy.UISupport.UserActions
{
    public class ActionUserInput
    {
        private object userInput;

        public object UserInput
        {
            get { return userInput; }
            set { userInput = value; }
        }
        private bool cancel=false;

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }
        
    }
}
