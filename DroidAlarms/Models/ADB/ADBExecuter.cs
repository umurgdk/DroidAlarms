using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DroidAlarms.Models.ADB
{
	public class ADBExecuter
	{
		private static ADBExecuter instance;
		public static ADBExecuter Instance {
			get {
				if (instance == null) {
					instance = new ADBExecuter ();
				}

				return instance;
			}
		}

		public static string ExecutablePath { get; set; }

		private ADBExecuter ()
		{
		}

		private string Run (params string[] parameters)
		{
			Process process = new Process ();

			try {
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.FileName = "adb";
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.Arguments = string.Join(" ", parameters);
				process.Start();

				string output = process.StandardOutput.ReadToEnd();

				process.WaitForExit();

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
			return Run ("shell", "-s", deviceId, "getprop", propName);
		}

		public string Alarms (string deviceId)
		{
			return Run ("shell", "-s", deviceId, "dumpsys", "alarm");
		}
	}
}

