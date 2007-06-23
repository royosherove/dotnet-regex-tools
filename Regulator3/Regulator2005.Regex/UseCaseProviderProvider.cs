using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Interfaces;
using Regulator2005.Core.DotNet;

namespace Regulator2005.Core
{
	public class UseCaseProviderProvider
	{
		public List<IUseCaseProvider> GetAvailableProviders()
		{
			List<IUseCaseProvider> list = new List<IUseCaseProvider>();
			list.Add(new DotNetUseCaseProvider());
			return list;
		}
	}
}
