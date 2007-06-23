using System;
using System.Collections.Generic;
using System.Text;
using NMock.Constraints;
using NUnit.Framework;

namespace Regulator2005.Tests.BaseClasses
{
	public class MockFixture
	{

		protected IConstraint withNotNull()
		{
			return new Not(new IsNull());
		}

		protected IConstraint withTypeOf(Type type)
		{
			return new IsTypeOf(type);
		}

		protected IConstraint withSameObjectAs(object objInstance)
		{
			return new IsSame(objInstance);
		}
		protected IConstraint withAnything()
		{
 			return new IsAnything();
		}
	}

	public class IsSame:IConstraint
	{
		private object m_expectedInstance;

		public object ExpectedInstance
		{
			get { return m_expectedInstance; }
			set { m_expectedInstance = value; }
		}

		public IsSame(object objInstance)
		{
			ExpectedInstance = objInstance;
		}
		#region IConstraint Members

		public bool Eval(object val)
		{
			try
			{
				Assert.AreSame(ExpectedInstance, val);
				return true;
			}
			catch (AssertionException)
			{

				return false;
			}
		}

		public object ExtractActualValue(object actual)
		{
			return actual;
		}

		public string Message
		{
			get { return "Object instances are not the same reference!"; }
		}

		#endregion
	}
}
