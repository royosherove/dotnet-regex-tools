using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Regulator.SDK.Core;

namespace Regulator.SDK
{
	public enum RegexActionTypes
	{
		Match,
		Replace,
		Split,
		None
	}

	/// <summary>
	/// Summary description for RegexProject.
	/// </summary>
	/// 
	[Serializable]
	public class RegexProject:SelfSerializer//,ISerializable
	{
		private bool m_booIsLoading;

		private bool m_booUpdateEventsEnabled=true;

		public event EventHandler Updated;
		private string m_strInputFilename=string.Empty;


		private string m_strReplaceString=string.Empty;

		private string m_strFileName=string.Empty;

		private string m_strName=string.Empty;

		private RegexOptions m_regOptions = RegexOptions.None;

		private string m_strInput=string.Empty;

		private string m_strRegex=string.Empty;


		public event EventHandler RequestMatch;
		public event EventHandler RequestReplace;
		public event EventHandler RequestSplit;

		public bool ShouldSaveAs
		{
			get
			{
				return (FileName==null ||
					FileName.Trim()==string.Empty);
			}

		}

	
		public void RaiseUpdateEvent()
		{
			if(IsLoading)
			{
				return ;
			}
			if(Updated!=null && m_booUpdateEventsEnabled)
			{
				Updated(this,new EventArgs());
			}
		}

		public void RunMatch()
		{
			RaiseBasicEvent(RequestMatch);
		}

		public event RegexActionStartDelegate ActionStarted;
		public event RegexSplitEndedDelegate SplitEnded;
		public event RegexMatchEndedDelegate MatchEnded;
		public event RegexReplaceEndedDelegate ReplaceEnded;

		public delegate void RegexActionStartDelegate(RegexProject sender,RegexActionTypes action);
		public delegate void RegexSplitEndedDelegate(RegexProject sender,string[] results);
		public delegate void RegexReplaceEndedDelegate(RegexProject sender,string replaceResult);
		public delegate void RegexMatchEndedDelegate(RegexProject sender,MatchCollection matches);

		public void TriggerActionStart(Regulator.SDK.RegexActionTypes action)
		{
			if(ActionStarted!=null)
			{
				ActionStarted(this,action);
			}
		}
		

		public void TriggerActionEnd(Regulator.SDK.RegexActionTypes action,object result)
		{
			switch (action) 
			{
				case RegexActionTypes.Match:
					if(MatchEnded!=null)
					{
						MatchEnded(this,(MatchCollection)result);
					}
					break;
				case RegexActionTypes.Split:
					if(SplitEnded!=null)
					{
						SplitEnded(this,(string[])result);
					}
					break;
				case RegexActionTypes.Replace:
					if(ReplaceEnded!=null)
					{
						ReplaceEnded(this,(string)result);
					}
					break;

				default:
					;
					break;
			}	
		}

		private void RaiseBasicEvent(EventHandler Event)
		{
			if(Event!=null)
			{
				Event(this,new EventArgs());
			}
		}
		public void RunReplace()
		{
			RaiseBasicEvent(RequestReplace);

		}

		private void RunSplit()
		{
			RaiseBasicEvent(RequestSplit);
		}

		#region contructors
		public RegexProject(SerializationInfo info, StreamingContext context)
		{
			Input = info.GetString("Input");	
			Regex= info.GetString("Regex");
			ReplaceString = info.GetString("ReplaceString");
		}

		public RegexProject():this("")
		{
			
		}

		public RegexProject(string regex,string input)
			:this(regex,input,RegexOptions.None)
		{
			this.Options=GetDefaultOptions();
		}

		public RegexProject(string regex)
			:this(regex,"",RegexOptions.None)
		{
			this.Options=GetDefaultOptions();
		}

		private RegexOptions GetDefaultOptions()
		{
			return RegexOptions.Multiline |
				RegexOptions.IgnorePatternWhitespace |
				RegexOptions.IgnoreCase;
		}

		public RegexProject(string regex,string input,RegexOptions options)
		{
			
			this.Regex= regex;
			this.Input=input;
			this.Options= options;
		}

		#endregion

		public string Regex
		{
			get
			{
				return m_strRegex;
			}
			set
			{
				m_strRegex = value;
				RaiseUpdateEvent();
			}
		}



		public string Input
		{
			get
			{
				return m_strInput;
			}
			set
			{
				m_strInput = value;
				RaiseUpdateEvent();
			}
		}


		public RegexOptions Options
		{
			get
			{
				return m_regOptions;
			}
			set
			{
				m_regOptions = value;
				RaiseUpdateEvent();
			}
		}



		public void BeginLoad()
		{
			m_booIsLoading=true;
		}

		public void EndLoad()
		{
			m_booIsLoading=false;
		}
		public string FileNameNoPath
		{
			get
			{

				if(File.Exists(FileName))
				{
					return Path.GetFileName(FileName);
				}
				else
				{
					return "New Document";
				}
				
			}
		}

		public override void Save(string filename)
		{
			base.Save (filename);
			RaiseUpdateEvent();
		}

		public string FileName
		{
			get
			{
				return m_strFileName;
			}
			set
			{
				m_strFileName = value;
				//RaiseUpdateEvent();
			}
		}
		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Input",Input);
			info.AddValue("Regex",Regex);
			info.AddValue("ReplaceString",ReplaceString);
		}

		#endregion

		public string ReplaceString
		{
			get
			{
				return m_strReplaceString;
			}
			set
			{
				m_strReplaceString = value;
				RaiseUpdateEvent();
			}
		}




		public string InputFilename
		{
			get
			{
				return m_strInputFilename;
			}
			set
			{
				m_strInputFilename = value;
				RaiseUpdateEvent();
			}
		}


		public bool UpdateEventsEnabled
		{
			get
			{
				return m_booUpdateEventsEnabled;
			}
			set
			{
				m_booUpdateEventsEnabled = value;
			}
		}


		public bool IsLoading
		{
			get
			{
				return m_booIsLoading;
			}
		}

	}
}
