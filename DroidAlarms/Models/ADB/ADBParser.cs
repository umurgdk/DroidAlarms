using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DroidAlarms.Models.ADB
{
	public class ADBParser
	{
		public Regex DevicesRegex = new Regex(@"^([^\s]+)\s+device$", RegexOptions.Multiline);
		public Regex AlarmRegex = new Regex(@"(ELAPSED|ELAPSED_WAKEUP|RTC_WAKEUP|RTC)\s.*?Alarm\{([^\s]+)\stype\s\d+\s(.*?)\}\n\s+.*?when=([+-].*?ms).*?repeatInterval=(\d+)");

		public ADBParser ()
		{
		}

		public List<string> ParseDevices (string output)
		{
			MatchCollection matches = DevicesRegex.Matches (output);
			List<string> deviceNames = new List<string> ();

			foreach (Match match in matches) {
				deviceNames.Add(match.Groups [1].Value);
			}

			return deviceNames;
		}

		public List<ADBAlarmParseResult> ParseAlarms (string output) {
			MatchCollection matches = AlarmRegex.Matches (output);
			List<ADBAlarmParseResult> results = new List<ADBAlarmParseResult> ();

			foreach (Match match in matches) {
				results.Add (
					new ADBAlarmParseResult {
						Type = match.Groups [1].Value,
						Id = match.Groups [2].Value,
						Package = match.Groups [3].Value,
						When = match.Groups [4].Value,
						Interval = match.Groups [5].Value
					}
				);
			}

			return results;
		}
	}
}

