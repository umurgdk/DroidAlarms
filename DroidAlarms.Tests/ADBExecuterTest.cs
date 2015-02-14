using NUnit.Framework;
using System;

using DroidAlarms.Models.ADB;

namespace Tests
{
	[TestFixture]
	public class ADBExecuterTest
	{
		[Test]
		public void TestDevices ()
		{
			ADBExecuter.ExecutablePath = "/Users/umurgedik/Applications/adt/sdk/platform-tools/adb";
			ADBExecuter executer = new ADBExecuter ();

			string devicesOutput = executer.Devices ();

			Assert.True (devicesOutput.Contains ("List of devices attached"));
		}
	}
}

