using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Interfaces;
using Regulator._2005.Providers.DotNet.Views;
using Regulator2005.Core;

namespace Regulator._2005.Providers.DotNet.UseCases
{
	public class ReplaceUseCase:IUseCase
	{
		#region IUseCase Members

		public string Name
		{
			get { return "Replace Text"; }
		}

		public string Description
		{
			get { return "search and replace text within a string using a regex pattern and a given replacement pattern."; }
			
		}

		public IRegexView CreateNewView()
		{
			return new ReplaceView();
		}

		public IRegexEngine CreateNewEngine()
		{
			return new DotNetRegexEngine();
		}

		
		#endregion
	}
}
