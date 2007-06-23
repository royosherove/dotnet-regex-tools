using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace RegexWizard.Framework
{
    public class RegexAdvisor : IAdvisor
    {
        public int RuleCount
        {
            get { return possibleMatches.Count; }
        }

        public List<Suggestion> Suggest(Scope scope)
        {
            Thread.SpinWait(1);
            List<Suggestion> list = new List<Suggestion>();
            if (scope == null)
            {
                return list;
            }
            bool scopeAlreadyHasSuggestions = (scope.Suggestions.Count > 0);
            if (scopeAlreadyHasSuggestions && scope.IsFlat)
            {
                if (useGroupsForUnnamedScopes)
                {
                    useUnnamedGroupedSuggestion(scope.Suggestions, scope, scope.Suggestions[0]);
                }
                useGroupedSuggestionIfNeeded(scope.Suggestions, scope, scope.Suggestions[0]);
                return scope.Suggestions;
            }

            if (scope.IsFlat)
            {
                List<Suggestion> results = Suggest(scope.Text);
                if (useGroupsForUnnamedScopes)
                {
                    useUnnamedGroupedSuggestion(results, scope, results[0]);
                }
                useGroupedSuggestionIfNeeded(results, scope, results[0]);
                return results;
            }


            for (int i = 0; i < 2; i++)
            {
                StringBuilder combinedSuggestions = new StringBuilder();
                int suggestionIndex = i;
                processInnerScope(combinedSuggestions, scope.InnerLeftScope, suggestionIndex);
                processInnerScope(combinedSuggestions, scope.InnerMiddleScope, suggestionIndex);
                processInnerScope(combinedSuggestions, scope.InnerRightScope, suggestionIndex);

                Suggestion suggestion = new Suggestion(combinedSuggestions.ToString(), "Combined");
                list.Add(suggestion);
                if (useGroupsForUnnamedScopes)
                {
                    useUnnamedGroupedSuggestion(list, scope, suggestion);
                }
                useGroupedSuggestionIfNeeded(list, scope, list[0]);
                scope.VisitedByGroup = false;
            }


            scope.Suggestions = list;
            return list;
        }
        private bool useGroupsForUnnamedScopes = false;

        
        public bool UseGroupsForUnnamedScopes
        {
            [DebuggerStepThrough]
            get { return useGroupsForUnnamedScopes; }
            [DebuggerStepThrough]
            set { useGroupsForUnnamedScopes = value; }
        }

        public int MaxSuggestionLength
        {
            get
            {
                return maxSuggestionLength;
            }
            set { maxSuggestionLength = value; }
        }

        private void useGroupedSuggestionIfNeeded(List<Suggestion> results, Scope scope, Suggestion toReplace)
        {
            
            if (scope.VisitedByGroup)
            {
                Console.Write("break here");
            }
            if (/*!scope.VisitedByGroup && */scope.Name != string.Empty && !(toReplace is GroupedSuggestionDecorator))
            {
                scope.VisitedByGroup = true; 
                GroupedSuggestionDecorator groupedSug = null;
                groupedSug = new GroupedSuggestionDecorator(toReplace, scope);


                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i] == toReplace)
                    {
                        results.Remove(toReplace);
                        results.Insert(i, groupedSug);
                    }
                    return;
                }
            }
            if ((toReplace is GroupedSuggestionDecorator))
            {
                ((GroupedSuggestionDecorator)toReplace).GroupName = scope.Name;
            }
        }

        private void useUnnamedGroupedSuggestion(List<Suggestion> results, Scope scope, Suggestion toReplace)
        {
            if (!(toReplace is UnNamedGroupedSuggestionDecorator))
            {
                UnNamedGroupedSuggestionDecorator groupedSug = new UnNamedGroupedSuggestionDecorator(toReplace, scope);

                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i] == toReplace)
                    {
                        results.Remove(toReplace);
                        results.Insert(i, groupedSug);
                    }
                    return;
                }
            }
        }


        public void LearnFromFile(string FileName)
        {
            Learn(File.ReadAllText(FileName));
        }

        private void processInnerScope(StringBuilder combinedSuggestions, Scope inner, int suggestionIndex)
        {
            List<Suggestion> innerResults = Suggest(inner);
            if (innerResults.Count > 0)
            {
                if (innerResults.Count <= suggestionIndex)
                {
                    //last one
                    suggestionIndex = innerResults.Count - 1;
                }
                combinedSuggestions.Append(innerResults[suggestionIndex].RegexText);
            }
        }

        List<Suggestion> possibleMatches = new List<Suggestion>();
        private int maxSuggestionLength=1000;


        public List<Suggestion> Suggest(string input)
        {
            List<Suggestion> matches = new List<Suggestion>();
            foreach (Suggestion possibility in possibleMatches)
            {
                if (isMatch(possibility, input))
                {
                    matches.Add(possibility);
                    inferMultipleMatchesSuggestions(input, matches, possibility);
                }

                else if (isMultilineMatch(possibility, input))
                {
                    matches.Add(new MultilineSuggestionDecorator(possibility));
                    inferMultipleMatchesSuggestions(input, matches, possibility);
                }

            }
            string escaped = Regex.Escape(input);
            if (matches.Count == 0 || matches[0].RegexText != escaped)
            {
                string trimmedInput = trimExactInputToMaxLength(input);
                string suggestionRegex = string.Format("^{0}$", escaped);
                string suggestionText = string.Format("Exactly '{0}'", trimmedInput);
                matches.Insert(0, new Suggestion(suggestionRegex, suggestionText, 900));
            }
            return matches;
        }

        private string trimExactInputToMaxLength(string input)
        {
            if(input.Length<=maxSuggestionLength)
            {
                return input;
            }
            return input.Substring(0,maxSuggestionLength) + "...";
        }

        private void inferMultipleMatchesSuggestions(string input, List<Suggestion> matches, Suggestion possibility)
        {
            bool IsMatchingOneOrMore =
                possibility.RegexText.EndsWith(@"+$") ||
                possibility.RegexText.EndsWith(@"+?$");

            if (!(IsMatchingOneOrMore))
            {
                return;
            }
            int howManyChars = input.Length;
            string[] descWords = possibility.Description.Split(' ');
            string lastWord = descWords[descWords.Length - 1];

            inferMultiple(matches, possibility, howManyChars, lastWord);
            inferMultipleExplicitChar(input, matches, howManyChars, lastWord);
        }


        private void inferMultiple(List<Suggestion> matches, Suggestion possibility, int howManyChars, string lastWord)
        {
            string newMatchSuffix = "{" + howManyChars + "}$";
            string newRegex = possibility.RegexText + newMatchSuffix;
            newRegex = newRegex.Replace("+$" + newMatchSuffix, newMatchSuffix);
            newRegex = newRegex.Replace("+?$" + newMatchSuffix, newMatchSuffix);

            matches.Add(new Suggestion(newRegex, howManyChars + " " + lastWord));
        }

        private void inferMultipleExplicitChar(string input, List<Suggestion> matches, int howManyChars, string lastWord)
        {
            string firstChar = input[0].ToString();
            string regex = "^" + firstChar + "{" + howManyChars + "}$";
            try
            {
                if (Regex.IsMatch(input, regex))
                {
                    string description = howManyChars + " '" + firstChar + "' " + lastWord;
                    matches.Add(new Suggestion(regex, description));
                }
            }
            catch (Exception)
            {
            }
        }
        private bool isMultilineMatch(Suggestion possibility, string input)
        {
            try
            {
                Regex r = new Regex(possibility.RegexText, RegexOptions.Singleline);
                Match m = r.Match(input);
                return m.Success;

            }
            catch (Exception)
            {


                return false;

            }
        }

        private bool isMatch(Suggestion possibility, string input)
        {
            try
            {
                bool success = Regex.IsMatch(input, possibility.RegexText, RegexOptions.None);
                return success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Learn(Suggestion suggestion)
        {
            if (possibleMatches.Count == 0)
            {
                possibleMatches.Add(suggestion);
                return;
            }
            if (matchAlreadyExists(suggestion))
            {
                string text = "A suggestion with the regex '"
                                + suggestion.RegexText
                                + "' already exists";
                throw new InvalidOperationException(text);
            }

            if (AutoLineStartEndSymbolOnSuggestions)
            {
                if (!suggestion.RegexText.StartsWith("^"))
                {
                    suggestion.RegexText = "^" + suggestion.RegexText;
                }

                if (!suggestion.RegexText.EndsWith("$"))
                {
                    suggestion.RegexText += "$";
                }
            }

            addSuggestionSortedByHighestProbability(suggestion);

        }

        private void addSuggestionSortedByHighestProbability(Suggestion suggestion)
        {
            for (int i = 0; i < possibleMatches.Count; i++)
            {
                Suggestion curSuggestion = possibleMatches[i];
                if (curSuggestion.Probability < suggestion.Probability)
                {
                    possibleMatches.Insert(i, suggestion);
                    return;
                }
            }
            possibleMatches.Add(suggestion);
        }
        private bool matchAlreadyExists(Suggestion newSuggestion)
        {
            foreach (Suggestion existingSuggestions in possibleMatches)
            {
                if (existingSuggestions.RegexText == newSuggestion.RegexText)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Learn(string suggestionsList)
        {
            if (suggestionsList == string.Empty)
            {
                return;
            }

            string[] lines = Regex.Split(suggestionsList, "\n");
            foreach (string line in lines)
            {
                if (line == String.Empty)
                {
                    continue;
                }
                string[] splitted = Regex.Split(line, "\t\t");
                if (splitted.Length < 2)
                {
                    continue;
                }
                Suggestion suggestion = new Suggestion(splitted[0], splitted[1]);
                if (splitted.Length >= 3)
                {
                    try
                    {
                        suggestion.Probability = int.Parse(splitted[2]);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                if (splitted.Length == 4)
                {
                    try
                    {
                        suggestion.Arity = splitted[3].Replace(Environment.NewLine, "");
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                try
                {
                    Learn(suggestion);
                }
                catch (Exception)
                {
                }
            }

        }
        public void Learn(params Suggestion[] suggestions)
        {
            for (int i = 0; i < suggestions.Length; i++)
            {
                Learn(suggestions[i]);
            }
        }

        public List<Suggestion> GetPrototypes()
        {
            List<Suggestion> results = new List<Suggestion>();
            foreach (Suggestion sug in possibleMatches)
            {
                if (sug.Arity != string.Empty)
                {
                    results.Add(sug);
                }
            }

            return results;
        }

        public void AutoScope(Scope root)
        {
            if(autoAdvisor==null)
            {
                return;
            }
            foreach (Suggestion possibility in autoAdvisor.possibleMatches)
            {
                Match match = Regex.Match(root.Text, possibility.RegexText);
                if (match.Success)
                {
                    Scope innerScope = root.FindInnerScope(match.Index, match.Length);
                    
                    if (innerScope == null || innerScope.Length!=match.Length)
                    {
                        List<SplitPoint> points = new ScopeSplitter().GetSplitPoints(root, match.Index, match.Length);
                        bool willNewScopeBeInsdeAScopeWithSuggestions = false;
                        if (points.Count>0)
                        {
                            foreach (SplitPoint point in points)
                            {
                                Scope target = root.FindInnerScope(point.StartIndex, point.Length);
                                if(target!=null && target.Suggestions.Count>0)
                                {
                                    willNewScopeBeInsdeAScopeWithSuggestions = true;
                                    break;
                                }
                            }
                            
                        }
                        if(!willNewScopeBeInsdeAScopeWithSuggestions)
                            innerScope = root.DefineInnerScope(match.Index, match.Length);
                    }
                    if (innerScope!=null)
                    {
                        innerScope.Suggestions.Add(possibility);
                        innerScope.IsExplicit = true;
                    }
                }
            }
        }

        private bool autoLineStartEndSymbolOnSuggestions = true;

        public bool AutoLineStartEndSymbolOnSuggestions
        {
            get { return autoLineStartEndSymbolOnSuggestions; }
            set { autoLineStartEndSymbolOnSuggestions = value; }
        }

        RegexAdvisor autoAdvisor;
        public void LearnAutomaticRules(Suggestion suggestion)
        {
            initAutoAdvisor();
            autoAdvisor.Learn(suggestion);
        }
        
        public void LearnAutomaticRules(string text)
        {
            initAutoAdvisor();
            autoAdvisor.Learn(text);
        }

        private void initAutoAdvisor()
        {
            if(autoAdvisor==null)
            {
                autoAdvisor = new RegexAdvisor();
                autoAdvisor.autoLineStartEndSymbolOnSuggestions = false;
            }
        }
    }
}
