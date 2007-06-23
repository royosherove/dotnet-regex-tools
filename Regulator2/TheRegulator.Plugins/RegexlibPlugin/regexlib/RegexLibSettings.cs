using System;
using Regulator.SDK.Plugins.RegexLib.Services;
using Regulator.SDK.Plugins;
using Regulator.SDK.Core;

namespace Regulator.SDK.Plugins.RegexLib
{
	[Serializable]
	public class RegexLibSettings:SDK.Core.SelfSerializer
	{
		public static readonly string FILENAME= "regexlig.config";
		public static RegexLibSettings Load()
		{
			try
			{
				return (RegexLibSettings)Load(typeof(RegexLibSettings),FILENAME);
			}
			catch(Exception )
			{
				return null;
			}
		}
		public void Save()
		{
			this.Save(RegexLibSettings.FILENAME);
		}
		public UserInfo userInfo;
		public RegexLibSettings(UserInfo info)
		{
			this.userInfo=info;			
		}
		public RegexLibSettings()
		{
		}

	}
}
