using System;
using Regulator.SDK.Plugins;
namespace Regulator.SDK
{
	/// <summary>
	/// Summary description for IRegexDisplay.
	/// </summary>
	public interface IRegexDisplay
	{

		void DisplayPlugin(IPlugin plugin);
		void InsertTextIntoCurrentRegex(string text);
		void InsertTextIntoCurrentRegex(string text,bool clearFirst);
		void InsertTextIntoCurrentInput(string text);
		void InsertTextIntoCurrentRegex(string regex,string input,string description);
		void CreateNewDocument(string regex,string input,string description);
		void RefreshSettings();
		RegexProject CurrentDocument{get;}

	}
}
