using System;
using System.Configuration;

namespace DroidAlarms
{
	public class Settings : ApplicationSettingsBase
	{
		[UserScopedSettingAttribute()]
		public String ADBPath {
			get { return this ["ADBPath"] as string; }
			set { this ["ADBPath"] = value; }
		}
	}
}

