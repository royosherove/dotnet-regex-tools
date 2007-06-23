using System;
using System.Net;

namespace Regulator.SDK.Proxy
{

	/// <summary>
	///     Creates a custom initialized Proxy object 
	///     based on requested settings
	/// </summary>
	public class ProxyFactory
	{
		private ProxyFactory()
		{
		}

		private static  void SetCredentials(WebProxy proxy,string user,string password,string domain,string uri)
		{
			CredentialCache myCache = new CredentialCache();
			NetworkCredential myCred =null;

			if(domain==null || domain.Length==0)
			{ myCred = new NetworkCredential(user,password);}
			else
			{myCred = new NetworkCredential(user,password,domain);}
		
			myCache.Add(new Uri(uri), "Negotiate", myCred);
			proxy.Credentials = myCred;
		}



		public static WebProxy GetDefaultProxy()
		{
			return WebProxy.GetDefaultProxy();
		}

		public static WebProxy Create(string proxyName,int proxyPort,bool bypassLocal)
		{
			WebProxy proxy = new System.Net.WebProxy(proxyName,proxyPort);
			proxy.BypassProxyOnLocal=bypassLocal;
			return proxy;

		}



		public static WebProxy Create(string domain,
										string userId,
										string password,
										string proxyName,
										int proxyPort,
										bool bypassLocal, 
										string uri)
		{
			WebProxy proxy = Create(proxyName,proxyPort,bypassLocal);
			SetCredentials(proxy,userId,password,domain,uri);
			
			return proxy;
		}

	
	

		public static WebProxy Create(ProxyInfo settings,string uri)
		{

			if(!settings.OverrideDefaultProxy)
			{
				return GetDefaultProxy();
			}

			if(!settings.RequiresLogin)
			{
				return Create(settings.ProxyName,settings.ProxyPort,settings.BypassLocal);
			}

			if(settings.Domain.Trim().Length==0)
			{
				//return using all the settings without domain
				return Create("",
					settings.UserId,
					settings.Password,
					settings.ProxyName,
					settings.ProxyPort,
					settings.BypassLocal,
					uri);
			}

			//return using all the settings
			return Create(settings.Domain,
				settings.UserId,
				settings.Password,
				settings.ProxyName,
				settings.ProxyPort,
				settings.BypassLocal,
				uri);
			

		}

	}
}
