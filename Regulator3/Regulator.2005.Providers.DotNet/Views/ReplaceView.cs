using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Regulator2005.Core.Interfaces;
using Regulator2005.Framework.Args;
using Regulator2005.Core.Args.DotNet;
using Regulator2005.Core.Args;
using Regulator2005.Framework.Controls;

namespace Regulator._2005.Providers.DotNet.Views
{
	public partial class ReplaceView : ViewFormBase,IRegexView
	{
		public ReplaceView()
		{
			InitializeComponent();
		}

		#region IRegexView Members

		void IRegexView.OnSplit(Regulator2005.Core.Args.SplitEventArgsBase args)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		void IRegexView.OnMatches(Regulator2005.Core.Args.MatchesEventArgsBase args)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		void IRegexView.OnReplace(ReplaceEventArgsBase args)
		{
			NReplaceEventArgs nargs = (NReplaceEventArgs)args;
			txtOutput.Text = nargs.OutputText;
		}

		void IRegexView.Init(IRegexEngine engine)
		{
			Engine = engine;
		}

		#endregion

		private void cmdGo_Click(object sender, EventArgs e)
		{
			Go();
		}

		private void Go()
		{
			txtOutput.Visible = true;
			Engine.Pattern = ptPattern.PatternText;
			Engine.ReplaceWithText = ptReplaceWith.PatternText;
			Engine.InputText = txtInput.Text;
			Engine.ExecuteReplace();
		}

		private IRegexEngine m_engine;

		private IRegexEngine Engine
		{
			get { return m_engine; }
			set { m_engine = value; }
		}

		private void extendedTextBox1_Load(object sender, EventArgs e)
		{

		}

		private void cmdGo_Click_1(object sender, EventArgs e)
		{
			Go();
		}

	}
}