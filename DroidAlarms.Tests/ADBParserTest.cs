using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

using DroidAlarms.Models.ADB;

namespace DroidAlarms.Tests
{
	[TestFixture]
	public class ADBParserTest
	{
		const string DEVICES_OUTPUT = "List of devices attached\nSH46MWR01205\tdevice\nHEBELEK\tdevice";

		[Test]
		public void TestDevicesParser ()
		{
			ADBParser parser = new ADBParser ();
			List<string> devices = parser.ParseDevices (DEVICES_OUTPUT);

			Assert.Contains ("SH46MWR01205", devices);
			Assert.Contains ("HEBELEK", devices);
		}

		[Test]
		public void TestAlarmsParser ()
		{
			ADBParser parser = new ADBParser ();
			List<ADBAlarmParseResult> results = parser.ParseAlarms (ReadAlarmsFile());

			Assert.IsNotEmpty (results);
			Assert.Greater (results.Count, 1);

			ADBAlarmParseResult first = results [0];

			Assert.AreEqual ("ELAPSED", first.Type);
			Assert.AreEqual ("4251dc70", first.Id);
			Assert.AreEqual ("android", first.Package);
			Assert.AreEqual ("+5s435ms", first.When);
			Assert.AreEqual ("0", first.Interval);
		}

		private string ReadAlarmsFile ()
		{
			return File.ReadAllText ("Fixtures/alarms.txt");
		}
	}
}

