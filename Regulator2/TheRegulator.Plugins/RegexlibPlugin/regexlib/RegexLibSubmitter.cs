using System;
using Regulator.SDK;
using Regulator.SDK.ApplicationSettings;
using Regulator.SDK.Proxy;
using Regulator.SDK.Plugins.RegexLib.Services;

namespace Regulator.SDK.Plugins.RegexLib
{
	/// <summary>
	/// Summary description for RegexLibSubmitter.
	/// </summary>
	public class RegexLibSubmitter
	{
		private UserInfo m_useUserDetailsInfo;

		public delegate void SubmitterStatusDelegate(string status);
		public event SubmitterStatusDelegate Status;
		public RegexLibSubmitter()
		{
		 
			
		}

		private void ShowStatus(string text)
		{
			if(Status!=null)
			{
				Status(text);
			}
		}
		public delegate void FinishedDelegate(RegexResult result);
		public event FinishedDelegate Finished;

		private void RaiseFinishedEvent(RegexLib.Services.RegexResult result)
		{
			if(Finished!=null)
			{
				Finished(result);
			}
		}

		public RegexLib.Services.RegexResult Submit(RegexLib.Services.PatternInfo info)
		{
			try
			{
				ShowStatus("Getting ready to submit...");
				RegexLib.Services.Webservices ws = new RegexLib.Services.Webservices();
				ws.Proxy= ProxyFactory.Create( AppContext.Instance.Settings.ProxySettings,ws.Url);
			
				//Apply user authorication settings if exist in settings
				//the ticket allows us to update patterns as well
				if(info.UserInfo.Ticket==null ||
					info.UserInfo.Ticket=="")
				{
					if(UserDetailsInfo!=null &&
						UserDetailsInfo.Ticket!="")
					{
						info.UserInfo= UserDetailsInfo;
					}
				}
			

				ShowStatus("Submitting...");
				
				RegexLib.Services.RegexResult result = ws.Save(info);


				ShowStatus(result.Message);
				if(result.Status==RegexLib.Services.RegexActionStatus.Inserted )
				{
					//we have gotten a ticket!
					UserDetailsInfo= result.PatternInformation.UserInfo;
					ShowStatus("Your expression was posted successfully!");
				}
				if(result.Status==RegexLib.Services.RegexActionStatus.Updated)
				{
					//we have gotten a ticket!
					UserDetailsInfo= result.PatternInformation.UserInfo;
					ShowStatus("Your expression was updated successfully!");
				}

				RaiseFinishedEvent(result);
				return result;
			}
			catch(Exception e)
			{
				RaiseFinishedEvent(null);
				throw new Exception("There was a problem submitting to Regexlib.com\n" + e.Message,e);
			}
		}





		public UserInfo UserDetailsInfo
		{
			get
			{
				return m_useUserDetailsInfo;
			}
			set
			{
				m_useUserDetailsInfo = value;
				//save current info into application settings
				new RegexLibSettings(m_useUserDetailsInfo).Save();;
			}
		}

		

	}
}
