using System;

namespace Regulator.SDK.Proxy
{
	/// <summary>
	/// Summary description for ProxyInfo.
	/// </summary>
	/// 

	
	[Serializable]
	public class ProxyInfo
	{
		private bool m_booBypassLocal;
		private bool m_booRequiresLogin=false;
		private bool m_booOverrideDefaultProxy=false;
		private int  m_strProxyPort=80;
		private string m_strProxyName="";
		private string m_strPassword="";
		private string m_strDomain="";
		private string m_strUserId="";
	
		public ProxyInfo()
		{
		}

		public string UserId
		{
			get
			{return m_strUserId;}
			set
			{m_strUserId = value;}
		}


		public string Domain
		{
			get
			{return m_strDomain;}
			set
			{m_strDomain = value;}
		}


		public string Password
		{
			get
			{return m_strPassword;}
			set
			{m_strPassword = value;}
		}


		public string ProxyName
		{
			get
			{return m_strProxyName;}
			set
			{m_strProxyName = value;}
		}


		public int ProxyPort
		{
			get
			{return m_strProxyPort;}
			set
			{m_strProxyPort = value;}
		}


		public bool OverrideDefaultProxy
		{
			get
			{return m_booOverrideDefaultProxy;}
			set
			{m_booOverrideDefaultProxy = value;}
		}


		public bool RequiresLogin
		{
			get
			{return m_booRequiresLogin;}
			set
			{m_booRequiresLogin = value;}
		}


		public bool BypassLocal
		{
			get
			{return m_booBypassLocal;}
			set
			{m_booBypassLocal = value;}
		}

	}
}
