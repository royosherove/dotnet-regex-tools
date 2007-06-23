using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexWizard.Framework
{
    public class HTMLInputFinder
    {
        private string searchTargetHTML = string.Empty;
        private string searchFor = string.Empty;
        private int startIndex = 0;
        private int length = 0;

        public string SearchTargetHTML
        {
            get { return searchTargetHTML; }
            set
            {
                searchTargetHTML = value;
            }
        }

        public string SearchFor
        {
            get { return searchFor; }
            set
            {
                normalizedSearchString = value;
                searchFor = value;
            }
        }

        private string foundHTML = string.Empty;

        public string FoundHTML
        {
            get { return foundHTML; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }

        public int Length
        {
            get { return length; }
        }

        private string normalizedSearchString = string.Empty;

        public string NormalizedSearchString
        {
            get { return normalizedSearchString; }
        }

        public bool Find()
        {
            normalizeSearchString();
            Regex matcher = new Regex(normalizedSearchString,RegexOptions.ExplicitCapture|RegexOptions.IgnoreCase|RegexOptions.Singleline);
            Match match = matcher.Match(searchTargetHTML);
            if(match.Success)
            {
                startIndex = match.Index;
                length = match.Length;
                foundHTML = searchTargetHTML.Substring(startIndex, length);
                return true;
            }
            return false;
        }

        private void normalizeSearchString()
        {
            ReplaceAllDoubleQuotesWithPossibleHTMLQuotes();
            AddPossibleDoubleQuotesToAllAssignments();
            ReplaceAllSingleQuotesWithPossibleHtmlQuotes();
            ReplaceNewLinesWithNewLineCharsAndSpacesAfter();
            ReplacesRealSpacesWithSpacePatterns();
            ReplaceEndTagsWithEndTagAndSpaces();
        }

        private void AddPossibleDoubleQuotesToAllAssignments()
        {
            normalizedSearchString = Regex.Replace(normalizedSearchString, @"=(?<SOMETHING>\w+)", @"=""?${SOMETHING}""?");
        }
        
        private void ReplaceNewLinesWithNewLineCharsAndSpacesAfter()
        {
            normalizedSearchString = Regex.Replace(normalizedSearchString, @"\n", @"\n?\s*");
        }
        private void ReplacesRealSpacesWithSpacePatterns()
        {
            normalizedSearchString = Regex.Replace(normalizedSearchString, @"\s+", @"\s+");
        }
        
        private void ReplaceEndTagsWithEndTagAndSpaces()
        {
            normalizedSearchString = Regex.Replace(normalizedSearchString, @">", @">\s*");
        }

        private void ReplaceAllSingleQuotesWithPossibleHtmlQuotes()
        {
            normalizedSearchString = Regex.Replace(normalizedSearchString, @"'", @"(\'|(&#39;))");
        }

        private void ReplaceAllDoubleQuotesWithPossibleHTMLQuotes()
        {
            normalizedSearchString = Regex.Replace(normalizedSearchString, @"""", @"(""|(&quot;))");
        }
    }
}
