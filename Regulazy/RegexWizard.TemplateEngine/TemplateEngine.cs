using System;
using System.Text.RegularExpressions;

namespace RegexWizard.TemplateEngine
{
    public class TemplateEngine
    {
    
        public string Process(TemplateInput input)
        {
         if( template!=string.Empty)
         {
             string output;
             output =template.Replace("$REGEX$",input.Pattern);
             output =output.Replace("$INPUT$",input.Input);
             
             string regexOptionsString = GetRegexOptionsStringForCSharp(input);
             output = output.Replace("$REGEXOPTIONS_CS$", regexOptionsString);
             
             string regexOptionsStringVB = GetRegexOptionsStringForVB(input);
             output = output.Replace("$REGEXOPTIONS_VB$", regexOptionsStringVB);

             output = ProcessForEachGroup(input, output);

             output = output.Replace("$FOREACH-GROUP$", string.Empty);
             output = output.Replace("$END-FOREACH-GROUP$", string.Empty);
             output = output.Replace("$GROUPNAME$", string.Empty);
             
             return output;
         }
            return string.Empty;
        }

        private  string ProcessForEachGroup(TemplateInput input, string output)
        {
            //regex for group name: \(\?<(?<Groupname>\w+?)>
            //regex for FOREACH_GROUP body: \$FOREACH-GROUP\$(?<middle>.+?)\$END-FOREACH-GROUP\$

            const string FOREACH_GROUP_REGEX = @"\$FOREACH-GROUP\$(?<body>.+?)\$END-FOREACH-GROUP\$";
            const string GROUPNAME_REGEX = @"\(\?<(?<GroupName>\w+?)>";
            MatchCollection groupName = Regex.Matches(input.Pattern,GROUPNAME_REGEX,RegexOptions.Singleline);
            
            string foreachGroupBody =
                Regex.Match(template, FOREACH_GROUP_REGEX,RegexOptions.Singleline).Groups["body"].Value;
            if (groupName.Count > 0)
            {
                string forEachGroupBodyInnerTemplate = foreachGroupBody;
                string tempBody = string.Empty;
                foreach (Match match in groupName)
                {
                    tempBody += forEachGroupBodyInnerTemplate.Replace("$GROUPNAME$", match.Groups["GroupName"].Value);
                }
                output = Regex.Replace(output, FOREACH_GROUP_REGEX, tempBody, RegexOptions.Singleline);
            }
            else
            {
                output = Regex.Replace(output, foreachGroupBody, string.Empty, RegexOptions.Singleline);
            }
            return output;
        }

        private string GetRegexOptionsStringForCSharp(TemplateInput input)
        {
            string regexOptionsString = input.RegexOptions.ToString();
            regexOptionsString = regexOptionsString.Replace(", ", " | RegexOptions.");
            regexOptionsString = "RegexOptions." + regexOptionsString;
            return regexOptionsString;
        }
        
        private string GetRegexOptionsStringForVB(TemplateInput input)
        {
            string regexOptionsString = input.RegexOptions.ToString();
            regexOptionsString = regexOptionsString.Replace(", ", " Or RegexOptions.");
            regexOptionsString = "RegexOptions." + regexOptionsString;
            return regexOptionsString;
        }

        private string template=String.Empty;

        public string Template
        {
            get { return template; }
            set { template = value; }
        }
    }
}
