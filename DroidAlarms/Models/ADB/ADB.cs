using System;
using System.Linq;
using System.Collections.Generic;

namespace DroidAlarms.Models.ADB
{
	public class ADB
	{
		ADBExecuter executer;
		ADBParser   parser;

		const string PROP_MANUFACTURER = "ro.product.manufacturer";
		const string PROP_MODEL = "ro.product.model";
		const string PROP_BUILD = "ro.build.version.release";

		public ADB ()
		{
			executer = new ADBExecuter ();
			parser = new ADBParser ();
		}

		public List<Application> GetApplicationsWithAlarms (Device device) 
		{
			string output = executer.Alarms (device.Id);
			var parser = new ADBParser ();

			List<Application> applications = new List<Application> ();
			var results = parser.ParseAlarms (output).GroupBy (result => result.Package);

			foreach (var resultGroup in results) {
				var app = new Application () { Name = resultGroup.Key };

				foreach (var result in resultGroup) {
					DateTime onDate;
					Alarm.AlarmType alarmType;

					if (result.Interval == "0") {
						onDate = parser.CalculateDateFromWhen (result.When);
					} else {
						onDate = parser.CalculateDateFromInterval (result.When, result.Interval);
					}

					Enum.TryParse<Alarm.AlarmType> (result.Type, out alarmType);

					app.Alarms.Add (new Alarm (result.Id, onDate, alarmType));
				}

				applications.Add (app);
			}

			return applications;
		}

		public List<Device> GetDevices()
		{
			string output = executer.Devices ();
			List<string> deviceIds = parser.ParseDevices (output);

			return deviceIds.Select (deviceId => {
				return new Device() {
					Id = deviceId,
					Name = string.Join(" ", executer.DeviceProps(deviceId, PROP_MODEL, PROP_BUILD))
				};
			}).ToList();
		}
	}
}

