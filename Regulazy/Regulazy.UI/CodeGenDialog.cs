using System.Windows.Forms;
using RegexWizard.TemplateEngine;
using Regulazy.UI.Properties;

namespace Regulazy.UI
{
    public partial class CodeGenDialog : Form
    {
        private TemplateInput templateInput;

        public CodeGenDialog(TemplateInput input)
        {
            InitializeComponent();
            templateInput = input;

            runEngine(input, Resources.Template_CS);
        }

        private void runEngine(TemplateInput input, string template)
        {
            TemplateEngine engine = new TemplateEngine();
            engine.Template = template;
            string output = engine.Process(input);
            txtOutput.Text = output;
        }

        private void cmdVB_Click(object sender, System.EventArgs e)
        {
            runEngine(templateInput, Resources.Template_VB);
        }

        private void cmdCSharp_Click(object sender, System.EventArgs e)
        {
            runEngine(templateInput, Resources.Template_CS);
        }

        private void cmdCopy_Click(object sender, System.EventArgs e)
        {
            txtOutput.HideSelection = false;
            txtOutput.SelectAll();
            txtOutput.Copy();
        }

    }
}