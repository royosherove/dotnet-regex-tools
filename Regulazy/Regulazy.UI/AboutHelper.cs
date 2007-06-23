using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SysMonitor11
{
	/// <summary>
	/// Summary description for AboutHelper.
	/// </summary>
	public class AboutHelper
	{
		public static void NavigateTo(string url)
		{
			try
			{
				Process.Start(url);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		
		public static void NavTeamAgileHomepage()
		{NavigateTo("http://www.TeamAgile.com");}
		
		public static void NavBlog()
		{NavigateTo("http://www.ISerializable.com");}
		
		public static void NavTools()
		{NavigateTo("http://tools.osherove.com");}
		
		public static void NavBugz()
		{NavigateTo("http://bugz.osherove.com");}
		
		public static void EmailAuthor()
		{NavigateTo("mailto:Roy@Osherove.com");}
		
		
		
		
		
	}
}
