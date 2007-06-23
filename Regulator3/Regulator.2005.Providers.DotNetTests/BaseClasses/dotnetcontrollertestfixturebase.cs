using System;
using System.Collections.Generic;
using System.Text;
using NMock;
using Regulator2005.Core;
using Regulator2005.Core.Interfaces;
using NUnit.Framework;
using NMock.Constraints;
using Regulator2005.Core.Args;

namespace Regulator2005.Tests.BaseClasses
{
	public abstract class DotNetControllerTestFixtureBase
		:MockFixture
		,IRegexView
	{
		protected DotNetRegexEngine m_controller = null;
		[SetUp]
		public void InitForEachTest()
		{
			m_controller = new DotNetRegexEngine();
		}



		[Test]
		public void TestedMethodIsCalled()
		{
			DynamicMock viewMock =
				setup_createAndAttachViewExpectingtestedMethod();

			executeTestedMethod();
			viewMock.Verify();
		}

		protected abstract void executeTestedMethod();

		[Test]
		public void TestedMethodIsPassedNotNull()
		{
			DynamicMock viewMock = setup_createEmptyViewMock();
			viewMock.Expect(getTestedMethodName(), withNotNull());

			setup_attachMockViewToController(viewMock, m_controller);


			executeTestedMethod();
			viewMock.Verify();
		}

		
		[Test]
		public void TestedMethodIsCalledOnMultipleViews()
		{
			DynamicMock viewMock1 =
				setup_createAndAttachViewExpectingtestedMethod();

			DynamicMock viewMock2 =
				setup_createAndAttachViewExpectingtestedMethod();

			executeTestedMethod();
			viewMock1.Verify();
			viewMock2.Verify();
		}

		protected abstract string getTestedMethodName();

		protected void setup_initControllerWithViewInputAndPattern(IRegexView view, string inputText, string pattern)
		{
			m_controller.AttachView(view);
			m_controller.InputText = inputText;
			m_controller.Pattern = pattern;
		}

		protected DynamicMock setup_createAndAttachViewExpectingtestedMethod()
		{
			DynamicMock viewMock = setup_createViewMockExpectingtestedMethod();
			setup_attachMockViewToController(viewMock, m_controller);
			return viewMock;
		}

		protected DynamicMock setup_createViewMockExpectingtestedMethod()
		{
			DynamicMock viewMock = setup_createEmptyViewMock();
			viewMock.Expect(getTestedMethodName(), new IsAnything());
			return viewMock;
		}

		protected void setup_attachMockViewToController(DynamicMock viewMock,DotNetRegexEngine controller)
		{
			controller.AttachView((IRegexView)viewMock.MockInstance);
		}

		protected DynamicMock setup_createEmptyViewMock()
		{
			DynamicMock viewMock = new DynamicMock(typeof(IRegexView));
			return viewMock;
		}


		#region IRegexView Members

		protected SplitEventArgsBase passedInSplitArgs = null;
		public virtual void OnSplit(SplitEventArgsBase args)
		{
			passedInSplitArgs = args;
			
		}

		protected MatchesEventArgsBase passedInMatchesArgs = null;
		public virtual void OnMatches(MatchesEventArgsBase args)
		{
			passedInMatchesArgs = args;
			
		}

		protected ReplaceEventArgsBase passedInReplaceArgs = null;
		public virtual void OnReplace(ReplaceEventArgsBase args)
		{
			passedInReplaceArgs = args;
		}

		public virtual void Init(IRegexEngine controller)
		{
		}

		

		public void Display(Regulator2005.Framework.Args.DisplayArgs args)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
