using System.Text.RegularExpressions;

namespace Regulator2005.Core
{
	/// <summary>
	/// This class encapsulates all of the non static Regex class functionality
	/// but aqlso adds various regex events into the mix
	/// </summary>
	public class ExtendedRegex
	{

		private Regex m_regexEngine;
		private string m_pattern;

		public ExtendedRegex()
			: this("", RegexOptions.None)
		{
		}
		public string Pattern
		{
			get { return m_pattern; }
			set { m_pattern = value; }
		}
		public override string ToString()
		{
			return m_regexEngine.ToString();
		}

		#region Delegated members and methods
		public bool RightToLeft
		{
			get { return m_regexEngine.RightToLeft; }
		}



		public RegexOptions Options
		{
			get { return m_regexEngine.Options; }
		}


		public string[] GetGroupNames()
		{
			return m_regexEngine.GetGroupNames();
		}

		public int[] GetGroupNumbers()
		{
			return m_regexEngine.GetGroupNumbers();
		}

		public string GroupNameFromNumber(int i)
		{
			return m_regexEngine.GroupNameFromNumber(i);
		}

		public int GroupNumberFromName(string name)
		{
			return m_regexEngine.GroupNumberFromName(name);
		}

		public bool IsMatch(string input)
		{
			return m_regexEngine.IsMatch(input);
		}

		public bool IsMatch(string input, int startat)
		{
			return m_regexEngine.IsMatch(input, startat);
		}

		public Match Match(string input)
		{
			return m_regexEngine.Match(input);
		}

		public Match Match(string input, int startat)
		{
			return m_regexEngine.Match(input, startat);
		}

		public Match Match(string input, int beginning, int length)
		{
			return m_regexEngine.Match(input, beginning, length);
		}

		public MatchCollection Matches(string input)
		{
			return m_regexEngine.Matches(input);
		}

		public MatchCollection Matches(string input, int startat)
		{
			return m_regexEngine.Matches(input, startat);
		}

		public string Replace(string input, string replacement)
		{
			return m_regexEngine.Replace(input, replacement);
		}

		public string Replace(string input, string replacement, int count)
		{
			return m_regexEngine.Replace(input, replacement, count);
		}

		public string Replace(string input, string replacement, int count, int startat)
		{
			return m_regexEngine.Replace(input, replacement, count, startat);
		}

		public string Replace(string input, MatchEvaluator evaluator)
		{
			return m_regexEngine.Replace(input, evaluator);
		}

		public string Replace(string input, MatchEvaluator evaluator, int count)
		{
			return m_regexEngine.Replace(input, evaluator, count);
		}

		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			return m_regexEngine.Replace(input, evaluator, count, startat);
		}

		public string[] Split(string input)
		{
			return m_regexEngine.Split(input);
		}

		public string[] Split(string input, int count)
		{
			return m_regexEngine.Split(input, count);
		}

		public string[] Split(string input, int count, int startat)
		{
			return m_regexEngine.Split(input, count, startat);
		}

		#endregion

		#region Contructors
		public ExtendedRegex(string pattern, RegexOptions options)
		{
			m_pattern = pattern;
			m_regexEngine = new Regex(pattern, options);
		}
		public ExtendedRegex(string pattern)
		{
			m_pattern = pattern;
			m_regexEngine = new Regex(pattern);
		} 
		#endregion
	}
}
