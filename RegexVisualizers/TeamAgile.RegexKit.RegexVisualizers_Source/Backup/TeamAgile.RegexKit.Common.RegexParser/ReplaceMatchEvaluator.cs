namespace TeamAgile.RegexKit.Common.RegexParser
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class ReplaceMatchEvaluator
    {
        private System.Text.RegularExpressions.MatchEvaluator matchEvaluator;
        private string matchEvaluatorString;
        private Regex regex;
        private static int serial;

        public ReplaceMatchEvaluator(Regex regex, string matchEvaluatorString)
        {
            this.regex = regex;
            this.matchEvaluatorString = matchEvaluatorString;
        }

        public string CreateAndLoadClass()
        {
            serial++;
            string path = string.Format("{0}match{1}.cs", Path.GetTempPath(), serial);
            StreamWriter writer = File.CreateText(path);
            string text2 = string.Format("MatchEvaluator{0}", serial);
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Text.RegularExpressions;");
            writer.WriteLine("class {0} {{", text2);
            writer.WriteLine(this.matchEvaluatorString);
            writer.WriteLine("}");
            writer.Close();
            Version version = Environment.Version;
            string text3 = string.Format(@"c:\windows\microsoft.net\framework\v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            string text4 = Path.GetTempPath() + "BuildMatch.bat";
            writer = File.CreateText(text4);
            writer.WriteLine("set path=%path%;{0}", text3);
            writer.WriteLine("csc /nologo /t:library {0}", path);
            writer.Close();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = text4;
            info.WorkingDirectory = Path.GetTempPath();
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            Process process = new Process();
            process.StartInfo = info;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                string[] textArray = process.StandardOutput.ReadToEnd().Split(new char[] { '\n' });
                string text5 = "";
                for (int i = 4; i < textArray.Length; i++)
                {
                    text5 = text5 + textArray[i];
                }
                return text5;
            }
            try
            {
                MethodInfo method = Assembly.LoadFrom(path.Replace(".cs", ".dll")).GetType(text2).GetMethod("Evaluator");
                this.matchEvaluator = (System.Text.RegularExpressions.MatchEvaluator) Delegate.CreateDelegate(typeof(System.Text.RegularExpressions.MatchEvaluator), method);
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
            return null;
        }

        public System.Text.RegularExpressions.MatchEvaluator MatchEvaluator
        {
            get
            {
                return this.matchEvaluator;
            }
        }
    }
}

