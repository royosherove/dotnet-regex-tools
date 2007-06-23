using System;
using System.Collections.Generic;
using System.Text;
using Regulator2005.Core.Interfaces;
using System.Collections;
using Regulator2005.Core.Args;
using System.Text.RegularExpressions;
using Regulator2005.Core.Args.DotNet;

namespace Regulator2005.Core
{

	
	public class DotNetRegexEngine : RegexEngineBase
	{
		private RegexOptions m_regexOptions=RegexOptions.None;

		public virtual RegexOptions DotNetRegexOptions
		{
			get { return m_regexOptions; }
			set { m_regexOptions = value; }
		}
		
		public  override void ExecuteSplit()
		{
			string[] result = getRegex().Split(InputText);
			foreach (IRegexView view in m_views)
			{
				NSplitEventArgs args = new NSplitEventArgs();
				args.Results = result;

				view.OnSplit(args);
			}

		}

		public override void ExecuteMatches()
		{
			ExtendedRegex regex = getRegex();
			MatchCollection matches = regex.Matches(InputText);
			foreach (IRegexView view in m_views)
			{
				NMatchesEventArgs args = new NMatchesEventArgs();
				args.Matches = matches;
				args.RegexInstance = regex;
				view.OnMatches(args);
			}
		}

		private ExtendedRegex m_regex = null;

		private ExtendedRegex getRegex()
		{
			m_regex = new ExtendedRegex(Pattern,m_regexOptions);
			return m_regex;
		}

		public override void ExecuteReplace()
		{
			foreach (IRegexView view in m_views)
			{
				NReplaceEventArgs args = new NReplaceEventArgs();
				args.OutputText = getRegex().Replace(InputText,ReplaceWithText);
				view.OnReplace(args);
			}
		}


		

		public override RegexParsingOptions GetOptions()
		{
			RegexParsingOptions options = new RegexParsingOptions();
			ExpressionOption[] flags = new ExpressionOption[]
			{
				new ExpressionOption(getOptionFlag(RegexOptions.Compiled)			,"Compiled"),
				new ExpressionOption(getOptionFlag(RegexOptions.CultureInvariant)	,"CultureInvariant"),
				new ExpressionOption(getOptionFlag(RegexOptions.ExplicitCapture)	,"ExplicitCapture"),
				new ExpressionOption(getOptionFlag(RegexOptions.ECMAScript)			,"ECMAScript"),
				new ExpressionOption(getOptionFlag(RegexOptions.IgnoreCase)			,"IgnoreCase"),
				new ExpressionOption(getOptionFlag(RegexOptions.IgnorePatternWhitespace)	,"IgnorePatternWhitespace"),
				new ExpressionOption(getOptionFlag(RegexOptions.Multiline)			,"Multiline"),
				new ExpressionOption(getOptionFlag(RegexOptions.RightToLeft)		,"RightToLeft"),
				new ExpressionOption(getOptionFlag(RegexOptions.Singleline)			,"Singleline")
			};

			options.AddRange(flags);
			return options;
		}

		private bool getOptionFlag(RegexOptions regexOption)
		{
			return (this.DotNetRegexOptions | regexOption) == DotNetRegexOptions;
		}

		public override void SetOptions(RegexParsingOptions options)
		{
			setParsingOption(options, RegexOptions.IgnorePatternWhitespace);
			setParsingOption(options, RegexOptions.Compiled);
			setParsingOption(options, RegexOptions.CultureInvariant);
			setParsingOption(options, RegexOptions.ECMAScript);
			setParsingOption(options, RegexOptions.ExplicitCapture);
			setParsingOption(options, RegexOptions.IgnoreCase);
			setParsingOption(options, RegexOptions.Multiline);
			setParsingOption(options, RegexOptions.RightToLeft);
			setParsingOption(options, RegexOptions.Singleline);


		}

		private void setParsingOption(RegexParsingOptions options,RegexOptions flagToSet)
		{
			string parsingOptionName = Enum.GetName(typeof(RegexOptions), flagToSet);
			ExpressionOption option = options.GetByName(parsingOptionName);
			bool shouldBeSelected = option.Selected;
			bool isSelected = getOptionFlag(flagToSet);
			
			//for readability
			bool shouldNotBeSelected = !shouldBeSelected;
			bool isNotSelected = !isSelected;
			
			
			if(shouldBeSelected && isNotSelected) 
			{
				DotNetRegexOptions |= flagToSet;
				return;
			}

			if(shouldNotBeSelected && isSelected) 
			{
				DotNetRegexOptions ^= flagToSet;
			}
		}
	}
}
