using System;
using System.Collections.Generic;
using DroidAlarms.Models;

namespace DroidAlarms.Repositories
{
	public class DeviceRepository
	{
		public List<Device> Devices { get; private set; }
		public event EventHandler<DeviceRepositoryEventArgs> Added;
		public event EventHandler<DeviceRepositoryEventArgs> Removed;
		public event EventHandler<EventArgs> Resetted;

		private static DeviceRepository _instance;

		public static DeviceRepository Instance {
			get {
				if (_instance == null) {
					_instance = new DeviceRepository ();
				}

				return _instance;
			}
		}

		private DeviceRepository ()
		{
			Devices = new List<Device> ();
		}

		public void Add(Device device)
		{
			Devices.Add (device);

			if (Added != null) {
				Added (this, new DeviceRepositoryEventArgs (device));
			}
		}

		public void Remove(Device device)
		{
			Devices.Remove (device);

			if (Removed != null) {
				Removed (this, new DeviceRepositoryEventArgs (device));
			}
		}

		public void Reset(IEnumerable<Device> devices)
		{
			Devices.Clear ();
			Devices.AddRange (devices);

			if (Resetted != null) {
				Resetted (this, EventArgs.Empty);
			}
		}
	}
}

