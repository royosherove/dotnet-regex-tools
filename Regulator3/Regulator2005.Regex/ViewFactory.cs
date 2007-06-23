using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Interfaces;

namespace Regulator2005.Core
{
	public class ViewManager
	{
		public IRegexView CreateView(IUseCase useCaseInstance)
		{
			IRegexView newView = useCaseInstance.CreateNewView();
			IRegexEngine engine = useCaseInstance.CreateNewEngine();
			engine.AttachView(newView);
			newView.Init(engine);

			return newView;	
		}
	}
}
