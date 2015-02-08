using System;
using System.Collections.Generic;

namespace DroidAlarms.Models
{
	public class Application
	{
		public string Name { get; private set; }

		public List<Alarm> Alarms { get; private set; }

		public Application (string name)
		{
			Name = name;
			Alarms = new List<Alarm> ();
		}
	}
}

