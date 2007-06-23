using System;
using System.Collections.Generic;
using System.Text;

namespace Regulator2005.Framework.Args
{
	public class DisplayArgs:EventArgs
	{
		private object m_container;

		public object Container
		{
			get { return m_container; }
			set { m_container = value; }
		}

	}
}
