using System;
using System.Collections.Generic;
using System.Text;

namespace Regulator2005.Core.Args
{
	public class ReplaceEventArgsBase:RegexEventArgs
	{
		private string m_outputText=string.Empty;

		public string OutputText
		{
			get { return m_outputText; }
			set { m_outputText = value; }
		}

	}
}
