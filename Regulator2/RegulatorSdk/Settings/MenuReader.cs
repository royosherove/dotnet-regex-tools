using System;
using System.Windows.Forms;
using System.Xml;

namespace Regulator.SDK.ApplicationSettings
{
	/// <summary>
	/// Summary description for MenuReader.
	/// </summary>
	public class MenuReader
	{
		private  MenuReader()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static ContextMenu ReadContextMenu(string filename,EventHandler ClickHandler)
		{
			ContextMenu menu = new ContextMenu();
			FillQuickMenu(menu,filename,ClickHandler);
			return menu;
			
		}

		public static MenuItem ReadMenuItem(string filename,EventHandler ClickHandler)
		{
			MenuItem menu = new MenuItem("Quick &Add");
			FillQuickMenu(menu,filename,ClickHandler);
			return menu;
			
		}

		private static void FillQuickMenu(ContextMenu parentMenu,string filename,EventHandler ClickHandler)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			AddMenu(parentMenu,doc.DocumentElement,ClickHandler);
		}

		public static QuickMenu ReadQuickMenuData()
		{
			QuickMenu menu = new QuickMenu();
			menu.ReadXml(AppContext.Instance.Settings.QuickMenuFileName);
			return menu;
		}

		private static void FillQuickMenu(MenuItem parentMenu,string filename,EventHandler ClickHandler)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			AddMenu(parentMenu,doc.DocumentElement,ClickHandler);
		}
		private static void AddMenu(MenuItem parentMenu,XmlNode node,EventHandler ClickHandler)
		{
			MenuItem newMenu=null;
			if(node.Name=="MenuItem")
			{
				
				if(node.HasChildNodes)
				{
					newMenu= new MenuItem(node.Attributes["name"].Value );
					foreach (XmlNode child in node.ChildNodes)
					{
						AddMenu(newMenu,child,ClickHandler);
					}
				}
				else
				{
					if(node.Attributes["name"].Value=="-")
					{
						newMenu= new MenuItem(node.Attributes["name"].Value);
					}
					else
					{
						newMenu= new MenuItem(node.Attributes["name"].Value + " - " + node.Attributes["value"].Value);
					}
				}
			}
			else
			{
				foreach (XmlNode childMenuNode in node.SelectNodes(@"MenuItem"))
				{
					AddMenu(parentMenu,childMenuNode,ClickHandler);
				}
				return ;
			}

			newMenu.Click+=ClickHandler;
			parentMenu.MenuItems.Add(newMenu);

		}
		private static void AddMenu(ContextMenu parentMenu,XmlNode node,EventHandler ClickHandler)
		{
			MenuItem newMenu=null;
			if(node.Name=="MenuItem")
			{
				
				if(node.HasChildNodes)
				{
					newMenu= new MenuItem(node.Attributes["name"].Value);

					foreach (XmlNode child in node.ChildNodes)
					{
						AddMenu(newMenu,child,ClickHandler);
					}
				}
				else
				{
					newMenu= new MenuItem(node.Attributes["name"].Value + " - " + node.Attributes["value"].Value);
				}
			}
			else
			{
				foreach (XmlNode childMenuNode in node.SelectNodes(@"MenuItem"))
				{
					AddMenu(parentMenu,childMenuNode,ClickHandler);
				}
				return ;
			}

			newMenu.Click+=ClickHandler;
			parentMenu.MenuItems.Add(newMenu);

		}
	}
}
