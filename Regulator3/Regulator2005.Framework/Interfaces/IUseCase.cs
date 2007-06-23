using System;
using System.Collections.Generic;
using System.Text;

namespace Regulator2005.Core.Interfaces
{
	public interface IUseCase
	{
		string Name { get;}
		string Description { get;}

		IRegexView CreateNewView();
		IRegexEngine CreateNewEngine();
	}
}
