using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Args;
using Regulator2005.Framework.Args;

namespace Regulator2005.Core.Interfaces
{
	public interface IRegexView
	{
		void OnSplit(SplitEventArgsBase args);
		void OnMatches(MatchesEventArgsBase args);
		void OnReplace(ReplaceEventArgsBase args);
		void Init(IRegexEngine engine);
		
	}


}
