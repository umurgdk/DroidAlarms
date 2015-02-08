using System;
using System.Collections.Generic;

namespace DroidAlarms.Models
{
	public class Device
	{
		public string Name { get; private set; }

		public List<Application> Applications { get; private set; }

		public Device (string name)
		{
			Name = name;
			Applications = new List<Application> ();
		}
	}
}

