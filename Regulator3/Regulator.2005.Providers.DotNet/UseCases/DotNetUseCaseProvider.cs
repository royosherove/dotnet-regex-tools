using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Interfaces;
using Regulator2005.Core;
using Regulator2005.Core.Args;
using Regulator._2005.Providers.DotNet.UseCases;

namespace Regulator2005.Core.DotNet
{
	public class DotNetUseCaseProvider:IUseCaseProvider
	{
		#region IUseCaseProvider Members

		public List<IUseCase> GetAvailableUseCases()
		{
			List<IUseCase> cases = new List<IUseCase>();
			cases.Add(new ReplaceUseCase());
			return cases;
		}

		#endregion
	}
}
