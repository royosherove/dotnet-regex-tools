using System;
using System.Collections.Generic;
using System.Text;

namespace Regulator2005.Core
{
	/// <summary>
	/// represents a generic regex option
	/// such as "ignore whitespace"
	/// </summary>
	public class ExpressionOption
	{
		public ExpressionOption() { }

		public ExpressionOption(bool selected,string name)
		{
			Selected = selected;
			Name = name;
		}
		private string m_name=string.Empty;

		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}


		private bool m_Selected=false;

		public bool Selected
		{
			get { return m_Selected; }
			set { m_Selected = value; }
		}

	}
}
