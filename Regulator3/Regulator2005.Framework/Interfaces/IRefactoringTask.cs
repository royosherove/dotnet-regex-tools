using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Args;

namespace Regulator2005.Core.Interfaces
{
	public interface IRefactoringTask
	{
		string Name { get;}
		string Description { get;}
		void Refactor(RefactoringArgs args);
	}
}
