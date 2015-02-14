using System;

namespace DroidAlarms.Models.ADB
{
	public class ADBAlarmParseResult
	{
		public string Type     { get; set; }
		public string Id       { get; set; }
		public string Package  { get; set; }
		public string When     { get; set; }
		public string Interval { get; set; }
	}
}

