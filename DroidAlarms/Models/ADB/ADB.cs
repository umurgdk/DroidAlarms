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

		public List<Device> GetDevices()
		{
			string output = executer.Devices ();
			List<string> deviceIds = parser.ParseDevices (output);

			return deviceIds.Select (deviceId => {
				return new Device() {
					Name = string.Join(" ", executer.DeviceProps(deviceId, PROP_MODEL, PROP_BUILD))
				};
			}).ToList();
		}
	}
}

