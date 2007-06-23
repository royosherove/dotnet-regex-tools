using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Regulator2005.Core;
using Regulator2005.Core.Interfaces;
using NMock;
using NMock.Constraints;
using Regulator2005.Tests.BaseClasses;

namespace Regulator2005.Tests.UseCases
{
	[TestFixture]
	public class ViewFactoryTests:MockFixture
	{
		[Test]
		public void Create()
		{
			ViewManager factory = new ViewManager();
			Assert.IsNotNull(factory);
		}

		[Test]
		public void ViewManagerCreateView()
		{
			#region Mock intialization

			DynamicMock useCase = new DynamicMock(typeof(IUseCase));
			DynamicMock engine = new DynamicMock(typeof(IRegexEngine));
			DynamicMock view = new DynamicMock(typeof(IRegexView));

			IRegexView viewMockInstance = (IRegexView)view.MockInstance;
			IUseCase useCaseMockInstance = (IUseCase)useCase.MockInstance;
			IRegexEngine engineMockInstance = (IRegexEngine)engine.MockInstance;

			#endregion
			
			
			useCase.ExpectAndReturn("CreateNewView", viewMockInstance);
			useCase.ExpectAndReturn("CreateNewEngine", engineMockInstance);
			engine.Expect("AttachView", withSameObjectAs(viewMockInstance));
			view.Expect("Init", withSameObjectAs(engineMockInstance));

			
			ViewManager factory = new ViewManager();
			IRegexView newView = factory.CreateView(useCaseMockInstance);

			useCase.Verify();
			engine.Verify();
			view.Verify();

			Assert.AreSame(newView, viewMockInstance,"Returned view is not the same instance as expected");
		}

	}
}
