using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Interfaces;
using System.Collections;
using Regulator2005.Core.Args;

namespace Regulator2005.Core
{
	public abstract class RegexEngineBase : IRegexEngine
	{
		private string m_replaceWithText = string.Empty;

		public virtual string ReplaceWithText
		{
			get { return m_replaceWithText; }
			set { m_replaceWithText = value; }
		}


		private string m_pattern = string.Empty;

		public virtual string Pattern
		{
			get { return m_pattern; }
			set { m_pattern = value; }
		}

		private string m_inputText = string.Empty;

		public virtual string InputText
		{
			get { return m_inputText; }
			set { m_inputText = value; }
		}

		protected List<IRegexView> m_views = new List<IRegexView>();

		public virtual void AttachView(IRegexView view)
		{
			m_views.Add(view);
		}

		public virtual void DetachView(IRegexView view)
		{
			m_views.Remove(view);
		}


		public abstract void ExecuteSplit();

		public abstract void ExecuteMatches();

		public abstract void ExecuteReplace();

		public abstract RegexParsingOptions GetOptions();

		public abstract void SetOptions(RegexParsingOptions options);

	}
}
