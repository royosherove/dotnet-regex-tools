using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Regulator2005.Core.Args.DotNet
{
	public class NMatchesEventArgs:MatchesEventArgsBase
	{
		private MatchCollection m_matches = null;

		public MatchCollection Matches
		{
			get { return m_matches; }
			set { m_matches = value; }
		}

		private ExtendedRegex m_regexInstance;

		public ExtendedRegex RegexInstance
		{
			get { return m_regexInstance; }
			set { m_regexInstance = value; }
		}


	}
}
