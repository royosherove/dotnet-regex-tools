using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Regulator2005.Core.Args
{
	public class SplitEventArgsBase:RegexEventArgs	
	{

		public override string ToString()
		{
			return Results.Length.ToString();
		}
		private string[] m_results = new string[] { };

		public string[] Results
		{
			get { return m_results; }
			set { m_results = value; }
		}

	}
}
	