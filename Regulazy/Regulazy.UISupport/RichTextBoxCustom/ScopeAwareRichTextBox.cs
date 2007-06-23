using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;
using RegexWizard.Framework;
using Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers;
using Regulazy.UI.RichTextBoxCustom;
using Regulazy.UISupport;
using Regulazy.UISupport.Properties;
using Regulazy.UISupport.UserActions;

namespace Regulazy.UI
{
    public enum InputModes
    {
        RegexManipulation,
        StandardEdit
    }

    [Designer(typeof(RTBDesigner))]
    public class ScopeAwareRichTextBox : CustomDrawRTB
    {
        #region Properties

        #region Properties
        private ScopeAwareRichTextBox rtb = null;

        public ScopeAwareRichTextBox Rtb
        {
            get { return rtb; }
        }
        private Color implicitScopeBorderColor = Color.Gray;

        public Color ImplicitScopeBorderColor
        {
            get { return implicitScopeBorderColor; }
            set { implicitScopeBorderColor = value; }
        }


        public Color NonActiveScopeFillColor
        {
            get { return nonActiveScopeFillColor; }
            set { nonActiveScopeFillColor = value; }
        }

        private Color nonActiveScopeFillColor = Color.DarkGreen;
        private Color activeScopeFillColor = Color.Red;
        private Color activeScopeBorderColor = Color.Blue;
        private Color nonActiveScopeBorderColor = Color.Red;
        private int nonActiveScopeBorderWidth = 1;
        private int activeScopeBorderWidth = 2;

        public Color ActiveScopeFillColor
        {
            get { return activeScopeFillColor; }
            set { activeScopeFillColor = value; }
        }

        public Color ActiveScopeBorderColor
        {
            get { return activeScopeBorderColor; }
            set { activeScopeBorderColor = value; }
        }

        public int ActiveScopeBorderWidth
        {
            get { return activeScopeBorderWidth; }
            set { activeScopeBorderWidth = value; }
        }

        public Color NonActiveScopeBorderColor
        {
            get { return nonActiveScopeBorderColor; }
            set { nonActiveScopeBorderColor = value; }
        }

        public int NonActiveScopeBorderWidth
        {
            get { return nonActiveScopeBorderWidth; }
            set { nonActiveScopeBorderWidth = value; }
        }




        #endregion

        #endregion



        private DrawingHelper drawingHelper = null;

        public List<Rectangle> GetSurroundingRects(Scope target)
        {
            if(target==null)
            {
                return new List<Rectangle>();
            }
            return GetSurroundingRects(target.StartPosInRootScope, target.Length);
        }
        
        public ScopeAwareRichTextBox()
            : base()
        {
            DoubleBuffered = true;
            EnableAutoDragDrop = false;
            KeyDown += new KeyEventHandler(ScopeAwareRichTextBox_KeyDown);
            drawingHelper = new DrawingHelper(this);
            WordWrap = false;
            AutoWordSelection = false;
            AcceptsTab = true;
            DetectUrls = false;
            MouseUp += txt_MouseUp;
            MouseMove += OnMouseMove;

            MouseLeave += OnCtlMouseLeave;
            ResetRootScope();
            uiHelper = new SelectionHelper(this);
        }

        private SelectionHelper uiHelper;

        private void adjustSelection()
        {
            while (SelectedText.EndsWith(" ") && SelectionLength > 0)
            {
                SelectionLength -= 1;
            }
        }

        public void OnCtlMouseLeave(object sender, EventArgs e)
        {
            if (lastVisualScopeMarker != null)
            {
                lastVisualScopeMarker.OnDeactivate();
            }
        }

        void ScopeAwareRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (alwaysPasteNonRTF && e.Control && e.KeyCode == Keys.V)
            {
                Paste(DataFormats.GetFormat(DataFormats.Text));
                e.Handled = true;
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (!CanDrawScopes)
            {
                return;
            }
            currentGraphics = e.Graphics;
            DrawScope(RootScope);

        }


        private Scope lastScope = null;
        private bool canDrawScopes = true;

        public bool AlwaysPasteNonRTF
        {
            get { return alwaysPasteNonRTF; }
            set { alwaysPasteNonRTF = value; }
        }

        private bool alwaysPasteNonRTF = true;
        protected Scope rootScope = null;
        private Scope activeScope;
        private InputModes inputMode = InputModes.RegexManipulation;
        private Color regexModeBackColor = Color.LightSteelBlue;
        private Color editModeBackColor = Color.White;
        private bool isControlPressed = false;

        public DrawingHelper DrawingHelper
        {
            get { return drawingHelper; }
        }

        private void DrawScope(Scope scopeToDraw)
        {
            if (!CanDrawScopes || scopeToDraw == null)
                return;

            VisualScopeMarker visualScopeMarker = GetVisualScopeMarker(scopeToDraw);
            if (visualScopeMarker!=null)
            {
                visualScopeMarker.Redraw(currentGraphics);
            }
            // makeDebuggerMarker(scopeToDraw).DrawCustomForSingleSurroundingRect(Rectangle.Empty, scopeToDraw);

            foreach (Scope innerScope in scopeToDraw.GetInnerScopes())
            {
                if (innerScope == null)
                {
                    continue;
                }

                if (!innerScope.IsFlat)
                {
                    DrawScope(innerScope);
                }
                VisualScopeMarker innerVisual = GetVisualScopeMarker(innerScope);
                if(innerVisual!=null)
                {
                    innerVisual.Redraw(currentGraphics);
                }
            }
        }


        public bool CanDrawScopes
        {
            get { return canDrawScopes; }
            set { canDrawScopes = value; }
        }

        public Color RegexModeBackColor
        {
            get { return regexModeBackColor; }
            set { regexModeBackColor = value; }
        }

        public Color EditModeBackColor
        {
            get { return editModeBackColor; }
            set { editModeBackColor = value; }
        }

        public Scope ActiveScope
        {
            get
            {
                return activeScope;
            }
            set
            {
                activeScope = value;
                OnActiveScopeChanged();
                Refresh();
            }
        }

        public Scope RootScope
        {
            get { return rootScope; }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                rootScope = new Scope(Text);
                Refresh();
            }
        }


        public InputModes InputMode
        {
            get
            {
                return inputMode;
            }
            set
            {
                inputMode = value;
                setInputMode(value);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            needsRefreshingAfterScrollOrResize = true;
        }

        protected override void OnVScroll(EventArgs e)
        {
            base.OnVScroll(e);
            needsRefreshingAfterScrollOrResize = true;
        }

        protected override void OnHScroll(EventArgs e)
        {
            base.OnHScroll(e);
            needsRefreshingAfterScrollOrResize = true;
        }

        //private Dictionary<Scope, VisualScopeMarker> visualScopes = new Dictionary<Scope, VisualScopeMarker>();
        private Hashtable visualScopes = new Hashtable();
        private VisualScopeMarker lastVisualScopeMarker = null;
        private Graphics currentGraphics = null;

        private bool needsRefreshingAfterScrollOrResize = false;

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!CanDrawScopes || DesignMode || Text == string.Empty)
                return;
            //            LockWindowUpdate(Handle);
            Point cursorPos = new Point(e.X, e.Y);

            int cursorCharPos = GetCharIndexFromPosition(cursorPos);
            int cursorLine = GetLineFromCharIndex(cursorCharPos);


            Scope innerScope = RootScope.FindInnerScope(cursorCharPos, 1);
            VisualScopeMarker visualScopeMarker = GetVisualScopeMarker(innerScope);
            if (visualScopeMarker == null)
            {
                return;
            }
            bool intersectsWithCursor = visualScopeMarker.IntersectsWithCursor(cursorPos);

            if (!intersectsWithCursor)
            {
                if (lastVisualScopeMarker != null)
                {
                    lastVisualScopeMarker.OnDeactivate();
                }

                if (ActiveScope == null)
                    return;

                lastScope = null;
                ActiveScope = null;
                return;
            }

            if (lastVisualScopeMarker != null &&
                lastVisualScopeMarker != visualScopeMarker)
            {
                lastVisualScopeMarker.OnDeactivate();
            }

            lastVisualScopeMarker = visualScopeMarker;
            if (visualScopeMarker.IsActive)
            {
                visualScopeMarker.OnMouseMove(e);
            }
            else
            {
                visualScopeMarker.OnActivate();

            }

            if (lastScope == null && innerScope != null)
            {
                lastScope = innerScope;
                ActiveScope = innerScope;
            }
            else
            {
                if (lastScope == innerScope)
                {
                    return;
                }
                else
                {
                    lastScope = innerScope;
                    ActiveScope = innerScope;
                }
            }

        }
        private VisualScopeMarker makeDebuggerMarker(Scope s)
        {
            return new DebuggingMarker(CreateGraphics(), s, this);
        }

        private VisualScopeMarker GetVisualScopeMarker(Scope scopeToDraw)
        {
            try
            {
                if (scopeToDraw == null)
                {
                    return null;
                }
                if (needsRefreshingAfterScrollOrResize)
                {
                    visualScopes.Clear();
                    needsRefreshingAfterScrollOrResize = false;
                }
                VisualScopeMarker visualScopeMarker = visualScopes[scopeToDraw] as VisualScopeMarker;
                if (visualScopeMarker == null)
                {
                    visualScopeMarker = makeRelevantVisualScope(scopeToDraw);
                    visualScopes[scopeToDraw] = visualScopeMarker;
                }
                visualScopeMarker = visualScopes[scopeToDraw] as VisualScopeMarker;

                return visualScopeMarker;
            }
            catch (Exception )
            {
                return null;
            }
        }

        private VisualScopeMarker makeRelevantVisualScope(Scope scopeToDraw)
        {
            return new VisualScopeDrawingLogic(CreateGraphics(), scopeToDraw, this);
        }



        public class ScopeEventArgs : EventArgs
        {
            private Scope scope;

            public Scope Scope
            {
                get { return scope; }
            }

            public ScopeEventArgs(Scope scope)
            {
                this.scope = scope;
            }
        }

        [Browsable(true)]
        public event EventHandler<ScopeEventArgs> ActiveScopeChanged = delegate { };

        public event EventHandler ExpressionChanged = delegate { };

        private void OnActiveScopeChanged()
        {
            ActiveScopeChanged(this, new ScopeEventArgs(activeScope));
        }

        protected void ResetRootScope()
        {
            rootScope = new Scope(Text);
            ActiveScope = rootScope;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Control)
            {
                isControlPressed = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Control)
            {
                isControlPressed = false;
            }
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (isControlPressed)
            {
                IncreaseFont(e.Delta);
            }
        }

        public event EventHandler InputModeChanged;

        public void IncreaseFont(int by)
        {
            Font = new Font(Font.FontFamily, Font.Size + by);
        }

        public void DecreaseFont(int by)
        {
            IncreaseFont(0 - by);
        }

        public void IncreaseFont()
        {
            IncreaseFont(1);
        }

        public void DecreaseFont()
        {
            IncreaseFont(-1);
        }

        private string lastTextBeforeInputModeChange = string.Empty;
        private void setInputMode(InputModes value)
        {
            visualScopes.Clear();
            lastVisualScopeMarker = null;

            switch (value)
            {
                case InputModes.RegexManipulation:
                    if (lastTextBeforeInputModeChange!=Text)
                    {
                        ResetRootScope();
                        lastTextBeforeInputModeChange = Text;
                    }
                    CanDrawScopes = true;
                    ReadOnly = true;
                    BackColor = regexModeBackColor;
                    CacheSpecialMarks = true;
                    raiseInputModeChangedEvent();
                    break;
                case InputModes.StandardEdit:
                    ShowSPaces = false;
                    ShowTabs = false;
                    ShowNewLines = false;
                    CacheSpecialMarks = false;
                    CanDrawScopes = false;
                    ReadOnly = false;
                    BackColor = editModeBackColor;
                    raiseInputModeChangedEvent();
                    break;
                default:
                    break;
            }
        }

        private void raiseInputModeChangedEvent()
        {
            if (InputModeChanged != null)
            {
                InputModeChanged(this, EventArgs.Empty);
            }
        }

        protected void txt_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && inputMode == InputModes.RegexManipulation && Text != string.Empty)
            {
                try
                {
                    ShowRelevantSuggestionMenu(e);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
        }

        private void ShowRelevantSuggestionMenu(MouseEventArgs e)
        {
            
            if (SelectionLength > 0)
            {
                SmartContextMenu menu = GetSuggestionMenu(null);
                menu.Show(this, e.Location);
            }
            else
            {
                if (ActiveScope != null)
                {
                    SmartContextMenu menu= GetSuggestionMenu(ActiveScope);
                    menu.ShowForScope(this, e.Location);

                }
            }
        }


       

        private SmartContextMenu GetSuggestionMenu(Scope target)
        {
            ActionHelper.Txt = this;
            ActionHelper.SetSuggestionProvider(new UIActionSuggestionProvider(this));

            ScopeActionsInfo actions = ActionHelper.GetActions(target);
            SmartContextMenu menu = new SmartContextMenu(target, this);

            AddActionsToMenu(menu, actions.UserActions);
            
            if (actions.UserActions.Count > 0)
            {
                menu.MenuItems.Add(new MenuItem("-"));
            }
            AddActionsToMenu(menu, actions.RuleSuggestions);
            return menu;
        }


        private void AddActionsToMenu(ContextMenu menu, ActionList suggestions)
        {
            foreach (UserAction rule in suggestions)
            {
                MenuItem mi = new MenuItem(rule.Title);
                mi.Click += rule.UserCallback;
                mi.Select += rule.Highlight;
                mi.Tag = rule;
                menu.MenuItems.Add(mi);
            }
        }


        public void Invalidate(Scope scope)
        {
            List<Rectangle> rects = GetSurroundingRects(scope.StartPosInRootScope, scope.Length);
            foreach (Rectangle rect in rects)
            {
                Invalidate(rect);
            }
        }

        internal void TriggerExpressionChanged()
        {
            ExpressionChanged(this, EventArgs.Empty);
        }
    }
}