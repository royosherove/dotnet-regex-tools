using System;
using System.Collections.Generic;
namespace Regulator2005.Core.Interfaces
{
	public interface IRegexEngine
	{
		void AttachView(IRegexView view);
		void DetachView(IRegexView view);
		void ExecuteMatches();
		void ExecuteReplace();
		void ExecuteSplit();
		string InputText { get; set; }
		string Pattern { get; set; }
		RegexParsingOptions GetOptions();
		void  SetOptions(RegexParsingOptions options);

		string ReplaceWithText { get; set; }
	}

	public class RegexParsingOptions : List<ExpressionOption>
	{
		public ExpressionOption GetByName(string name)
		{
			for (int i = 0; i < Count; i++)
			{
				if (this[i].Name == name)
					return this[i];
			}

			return null;
		}
	}

}
