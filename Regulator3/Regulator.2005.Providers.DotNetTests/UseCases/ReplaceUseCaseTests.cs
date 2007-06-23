using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Regulator._2005.Providers.DotNet.UseCases;
using Regulator2005.Core.Interfaces;
using Regulator._2005.Providers.DotNet.Views;
using Regulator2005.Core;
using System.Windows.Forms;
using NMock;

namespace Regulator._2005.Providers.DotNetTests.UseCases
{
	[TestFixture]
	public class ReplaceUseCaseTests
	{
		ReplaceUseCase useCase = new ReplaceUseCase();

		[SetUp]
		public void setup()
		{
			useCase = new ReplaceUseCase();
		}
		[Test]
		public void CreateViewReturnsNonNull()
		{
			Assert.IsNotNull(useCase.CreateNewView(),"useCase.CreateNewView returned null!");
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void CreateViewReturnsDifferentEveryTime()
		{
			IRegexView view1 = useCase.CreateNewView();
			IRegexView view2 = useCase.CreateNewView();
			//thse objects should NOT be the same instance!
			//we sould get an exception here
			Assert.AreSame(view1, view2);
		}

		public void ViewManagerCreateView()
		{
			DynamicMock useCase = new DynamicMock(typeof(IUseCase));
			IUseCase useCaseMockInstance = (IUseCase)useCase.MockInstance;
			
			IRegexEngine engine = new DotNetRegexEngine();
			IRegexView view = new ReplaceView();
			
			useCase.ExpectAndReturn("CreateNewView", view);
			useCase.ExpectAndReturn("CreateNewEngine", engine);


			ViewManager factory = new ViewManager();
			IRegexView newView = factory.CreateView(useCaseMockInstance);
			Form frm = (Form)newView;
			frm.ShowDialog();
		}

	}
}
