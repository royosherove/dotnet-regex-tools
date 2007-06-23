/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace RegexTest
{
	/// <summary>
	/// do a replace with a MatchEvaluator
	/// </summary>
	public class ReplaceMatchEvaluator
	{
		Regex regex;
		string matchEvaluatorString;
		static int serial = 0;
		MatchEvaluator matchEvaluator = null;

		public ReplaceMatchEvaluator(Regex regex, string matchEvaluatorString )
		{
			this.regex = regex;
			this.matchEvaluatorString = matchEvaluatorString;
			
		}

		public MatchEvaluator MatchEvaluator
		{
			get
			{
				return matchEvaluator;
			}
		}

		public string CreateAndLoadClass()
		{
			serial++;
			string filename = 
				String.Format("{0}match{1}.cs", Path.GetTempPath(), serial);

			StreamWriter writer = File.CreateText(filename);
			
			string className = String.Format("MatchEvaluator{0}", serial);
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Text.RegularExpressions;");
			writer.WriteLine("class {0} {{", className);
			writer.WriteLine(matchEvaluatorString);
			writer.WriteLine("}");
			writer.Close();

			Version version = Environment.Version;

			string runtimePath =
				String.Format(@"c:\windows\microsoft.net\framework\v{0}.{1}.{2}",
					version.Major, version.Minor, version.Build);

			string batchFilename =
				Path.GetTempPath() + "BuildMatch.bat";
			writer = File.CreateText(batchFilename);
			writer.WriteLine("set path=%path%;{0}", runtimePath);
			writer.WriteLine("csc /nologo /t:library {0}", filename);
			writer.Close();

			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.FileName = batchFilename;
			startInfo.WorkingDirectory = Path.GetTempPath();
			startInfo.CreateNoWindow = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.UseShellExecute = false;
			Process process = new Process();
			process.StartInfo = startInfo;
			process.Start();
			process.WaitForExit();

			if (process.ExitCode != 0)
			{
				string output = process.StandardOutput.ReadToEnd();
				string[] lines = output.Split('\n');
				
				output = "";
				for (int j = 4; j < lines.Length; j++)
				{
					output += lines[j];
				}

				return output;
			}

			try
			{
				Assembly assembly = 
					Assembly.LoadFrom(filename.Replace(".cs", ".dll"));

				Type type = assembly.GetType(className);
				MethodInfo methodInfo = type.GetMethod("Evaluator");
				matchEvaluator = 
					(MatchEvaluator) Delegate.CreateDelegate(typeof(MatchEvaluator), methodInfo);
			}
			catch (Exception e)
			{
				return e.ToString();
			}
			return null;
		}
	}
}
