using System;
using System.Collections.Generic;
using System.Text;

namespace Regulator2005.Core.Interfaces
{
	public interface IUseCaseProvider
	{
		List<IUseCase> GetAvailableUseCases();
	}
}
