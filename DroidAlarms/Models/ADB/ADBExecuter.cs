using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace DroidAlarms.Models.ADB
{
	public class ADBExecuter
	{
		public static string ExecutablePath { get; set; }
		public static bool DebugMode = false;

		public ADBExecuter ()
		{
		}

		private string Run (params string[] parameters)
		{
			Process process = new Process ();

			try {
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.FileName = ExecutablePath;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.Arguments = string.Join(" ", parameters);
				process.Start();

				string output = process.StandardOutput.ReadToEnd();

				process.WaitForExit();

				if (DebugMode) {
					System.Console.WriteLine("adb " + string.Join(" ", parameters));
					System.Console.WriteLine(output);
				}

				return output;
			} catch (Exception ex) {
				return "Error: " + ex.Message;
			}
		}

		public string Devices ()
		{
			return Run ("devices");
		}

		public string DeviceProp (string deviceId, string propName)
		{
			return Run ("-s", deviceId, "shell", "getprop", propName).Trim ();
		}

		public List<string> DeviceProps (string deviceId, params string[] propNames)
		{
			return propNames.Select (prop => {
				return DeviceProp (deviceId, prop);
			}).ToList();
		}

		public string Alarms (string deviceId)
		{
			return Run ("-s", deviceId, "shell", "dumpsys", "alarm");
		}
	}
}

