using System;

using DroidAlarms.Models;

namespace DroidAlarms.Repositories
{
	public class DeviceRepositoryEventArgs
	{
		public Device Device { get; private set; }

		public DeviceRepositoryEventArgs (Device device)
		{
			Device = device;
		}
	}
}

