using System;
using System.Collections.Generic;
using System.Text;
using NMock.Constraints;

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
		
		protected IConstraint withAnything()
		{
 			return new IsAnything();
		}
	}
}
