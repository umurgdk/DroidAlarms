using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DroidAlarms.Models.ADB
{
	public class ADBParser
	{
		public Regex DevicesRegex 		= new Regex(@"^([^\s]+)\s+device$", RegexOptions.Multiline);
		public Regex WhenRegex    		= new Regex(@"(\d+)(ms|m|s|h|d)");
		public Regex AlarmRegex   		= new Regex(@"(ELAPSED|ELAPSED_WAKEUP|RTC_WAKEUP|RTC)\s.*?Alarm\{([^\s]+)\stype\s\d+\s(.*?)\}\s+.*?when=([+-].*?ms).*?repeatInterval=(\d+)");
		public Regex AlarmRegexLollipop = new Regex(@"(ELAPSED|ELAPSED_WAKEUP|RTC_WAKEUP|RTC)\s.*?Alarm\{([^\s]+)\s\S+\s\S+\s\S+\s\S+\s(.*?)\}.*?when=([^\s]+).*?repeatInterval=(\d+)", RegexOptions.Singleline);

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

		public DateTime CalculateDateFromWhen (string when)
		{
			var modifier = when [0];
			var dateString = when.Substring (1);

			MatchCollection matches = WhenRegex.Matches (dateString);

			var now = DateTime.Now;

			foreach (Match match in matches) {
				var number = int.Parse(match.Groups [1].Value);
				var timeType = match.Groups [2].Value;

				if (modifier == '-') {
					number = number * -1;
				}

				switch (timeType) {
				case "d":
					now = now.AddDays (number);
					break;
				case "h":
					now = now.AddHours (number);
					break;
				case "m":
					now = now.AddMinutes (number);
					break;
				case "s":
					now = now.AddSeconds (number);
					break;
				case "ms":
					now = now.AddMilliseconds (number);
					break;
				default:
					break;
				}
			}

			return now;
		}

		public DateTime CalculateDateFromInterval (string when, string interval)
		{
			var whenDate = CalculateDateFromWhen (when);
			return whenDate.AddMilliseconds (double.Parse (interval));
		}

		public List<ADBAlarmParseResult> ParseAlarms (string output, int version) {
			MatchCollection matches;

			if (version == 5) {
				matches = AlarmRegexLollipop.Matches (output);
			} else {
				matches = AlarmRegex.Matches (output);
			}

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

