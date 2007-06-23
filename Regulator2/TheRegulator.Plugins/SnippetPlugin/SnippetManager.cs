using System;
using System.Windows.Forms;
using System.IO;
using System.Data;
using Regulator.SDK;

namespace Regulator.GUI
{
	/// <summary>
	/// Summary description for SnippetManager.
	/// </summary>
	public class SnippetManager
	{
		private  SnippetManager()
		{
		}

		public static DataSet GetSnippets()
		{
			try
			{
				DataSet ds = new DataSet("Snippets");
				string filename = Path.Combine( AppContext.Instance.Settings.BaseDir,"Snippets.xml");
				ds.ReadXml(filename,XmlReadMode.InferSchema);
				return ds;
			}
			catch(Exception e)
			{
				return null;
			}

		}

		public static void SaveSnippets(DataSet ds)
		{
			try
			{
				ds.WriteXml(AppContext.Instance.Settings.BaseDir + @"\Snippets.xml");
			}
			catch(Exception e)
			{
				MessageBox.Show("Could not save snippets file snippets.xml!");
			}
		}
	}
}
