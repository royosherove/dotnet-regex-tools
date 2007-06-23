using System;
using System.IO;
using System.Windows.Forms;

namespace Regulator.GUI
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class MainStart:Form
	{
		[STAThread]
		public static void Main(string[] args)
		{
			if(HandleInputFile(args))
			{
				return ;
			}		
			Application.Run(new MainForm());
		}

		private static bool HandleInputFile(string[] args)
		{
			if(args.Length==1)
			{
				string filename = args[0];
				if(filename.ToLower().EndsWith(".express"))
				{
					if(File.Exists( filename))
					{
						Application.Run(new MainForm(filename));
						return true;
					}
					else
					{
						MessageBox.Show("Could not find the file " + filename);
						return false;
						//continue to open with an empty document
					}
				}
				else
				{
					MessageBox.Show("This file is not supported by The Regulator");
					return false;
					//continue to open with an empty document
				}
			}
			return false;
		}

	}
}
