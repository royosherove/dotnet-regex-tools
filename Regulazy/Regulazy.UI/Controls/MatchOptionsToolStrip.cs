using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Regulazy.UI.Properties;

namespace Regulazy.UI
{
    public partial class MatchOptionsToolStrip : UserControl
    {
        private RegexOptions regexOptions=RegexOptions.None;
        private bool isSettingFromProgram=false;

        public class RegexOptionsEventArgs : EventArgs
        {
            private RegexOptions options;

            public RegexOptions Options
            {
                get { return options; }
            }

            public RegexOptionsEventArgs(RegexOptions options)
            {
                this.options = options;
            }
        }
        public event EventHandler<RegexOptionsEventArgs> RegexOptionsChanged;

        
        private void SetRegexOption(RegexOptions option, bool enabled)
        {
            if (enabled)
            {
                RegXOptions |= option;

            }
            else
            {

                RegXOptions ^= option;
            }

        }

        private bool GetOptionState(RegexOptions enumeratedObject, RegexOptions wantedEnum)
        {
            return (enumeratedObject & wantedEnum) == wantedEnum;
        }
		
        public bool IsSet(RegexOptions option)
        {
            return GetOptionState(RegXOptions, option);
        }
		
        



        private void OnRegexOptionsChanged()
        {
            if (RegexOptionsChanged != null)
            {
                RegexOptionsChanged(this, new RegexOptionsEventArgs(RegXOptions));
            }
        }
        public RegexOptions RegXOptions
        {
            get
            {
                return regexOptions;
            }
            set
            {
                regexOptions = value;
                ApplyRegexOptionsToButtonStates();
            }
        }

        private void ApplyRegexOptionsToButtonStates()
        {
            isSettingFromProgram = true;
            toolCheckSingleLine.Checked = GetOptionState(regexOptions, RegexOptions.Singleline);
            toolCheckRTL.Checked = GetOptionState(regexOptions, RegexOptions.RightToLeft);
            toolCheckMultiLine.Checked = GetOptionState(regexOptions, RegexOptions.Multiline);
            toolCheckIgnoreWhiteSpace.Checked = GetOptionState(regexOptions, RegexOptions.IgnorePatternWhitespace);
            toolCheckIgnoreCase.Checked = GetOptionState(regexOptions, RegexOptions.IgnoreCase);
            toolCheckECMA.Checked = GetOptionState(regexOptions, RegexOptions.ECMAScript);
            toolCheckCultureInvariance.Checked = GetOptionState(regexOptions, RegexOptions.CultureInvariant);
            isSettingFromProgram = false;
        }


        public MatchOptionsToolStrip()
        {
            InitializeComponent();
            toolCheckRTL.Tag = RegexOptions.RightToLeft;
            toolCheckMultiLine.Tag = RegexOptions.Multiline;
            toolCheckIgnoreWhiteSpace.Tag = RegexOptions.IgnorePatternWhitespace;
            toolCheckECMA.Tag = RegexOptions.ECMAScript;
            toolCheckIgnoreCase.Tag = RegexOptions.IgnoreCase;
            toolCheckCultureInvariance.Tag = RegexOptions.CultureInvariant;
            toolCheckSingleLine.Tag = RegexOptions.Singleline;
            
            toolCheckSingleLine.Click+=ButtonCheckChanged;
            toolCheckRTL.Click += ButtonCheckChanged;
            toolCheckMultiLine.Click += ButtonCheckChanged;
            toolCheckIgnoreWhiteSpace.Click += ButtonCheckChanged;
            toolCheckIgnoreCase.Click += ButtonCheckChanged;
            toolCheckECMA.Click += ButtonCheckChanged;
            toolCheckCultureInvariance.Click += ButtonCheckChanged;
        }

        private void ButtonCheckChanged(object sender, EventArgs e)
        {
            if(isSettingFromProgram)
                return;
            
            ToolStripButton button = sender as ToolStripButton;
            RegexOptions option = (RegexOptions) button.Tag;
            SetRegexOption(option, button.Checked);
            OnRegexOptionsChanged();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            new QuickDemoForm(Resources.Huh_RegexOptionsPane).ShowDialog(this.ParentForm);

        }
    }
}
