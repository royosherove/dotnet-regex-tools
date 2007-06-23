using System;

namespace Regulazy.UISupport
{
    public abstract class UserAction
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Exception lastExecption;

        public Exception LastExecption
        {
            get { return lastExecption; }
            set { lastExecption = value; }
        }

        private string title;
        protected object sender;
        protected EventArgs eventArgs;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public abstract bool Execute();

        public void UserCallback(object callbackSender, EventArgs e)
        {
            sender = callbackSender;
            eventArgs = e;
            Execute();
        }

        private static Action<object> BeforeNextActionSelectedCallback=delegate {};
        
        public virtual void Highlight(object senderObj, EventArgs e)
        {
            BeforeNextActionSelectedCallback.Invoke(this);
            BeforeNextActionSelectedCallback = delegate{OnHighlightOff(); }; 
        }
        
        public virtual void OnHighlightOff()
        {
            
        }
    }
}
