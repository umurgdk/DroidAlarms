using System;
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
			} catch (Exception ex) {
				
			}
		}

		public string Devices ()
		{

		}
	}
}

