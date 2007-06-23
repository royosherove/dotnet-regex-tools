using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Xml;
using Regulator.SDK.Core;
namespace Regulator.SDK.ApplicationSettings
{
	
	[Serializable]
	[XmlRoot("MRU")]
	public class MRUSettings:SelfSerializer
	{
		[XmlArray("RecentFiles")]
		[XmlArrayItem("File",typeof(string))]
		public ArrayList MRUList = new ArrayList();

		private int capacity=10;
		private StringCollection _MRUList  = new StringCollection();
			
		public string this [int index]
		{
			get 
			{
				if(index>=0 && index<MRUList.Count)return (string)MRUList[index];
				else return null;
			}
		}

		public int Count
		{
			get{return MRUList.Count;}
		}

		public int Capacity
		{
			get{return capacity;}
			set
			{  
				capacity=value;
				if(MRUList.Count>capacity)MRUList.RemoveRange(capacity,MRUList.Count-capacity);
			}
		}
		public MRUSettings()
		{
			MRUList = new  ArrayList();
		}

		public void Add(string FileName)
		{
			if(MRUList.Contains(FileName))
			{
				MRUList.Remove(FileName);
				MRUList.Insert(0,FileName);
			}
			else 
			{
				MRUList.Insert(0,FileName);
				if(MRUList.Count>Capacity)
				{
					MRUList.RemoveAt(MRUList.Count-1);
				}
			}
		}

		public void AddRange(string[] files)
		{
			foreach(string s in files)
			{
				Add(s);
			}
		}

		public void Clear()
		{
			MRUList.Clear();
		}

		public void Remove(string FileName)
		{
			if(MRUList.Contains(FileName))MRUList.Remove(FileName);
		}
			
		public string[] GetFileNames()
		{
			string[] files = new string[MRUList.Count];
			for(int i=0;i<MRUList.Count;i++)
			{
				files[i]=(string)MRUList[i];
			}
			return files;
		}
	}
	

}
