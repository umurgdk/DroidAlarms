using System;
using System.Linq;
using System.Collections.Generic;
using DroidAlarms.Models;
using DroidAlarms.Models.ADB;

namespace DroidAlarms.Repositories
{
	public class DeviceRepository
	{
		public List<Device> Devices { get; private set; }
		public event EventHandler<DeviceRepositoryEventArgs> Added;
		public event EventHandler<DeviceRepositoryEventArgs> Removed;

		private static DeviceRepository _instance;

		public static DeviceRepository Instance {
			get {
				if (_instance == null) {
					_instance = new DeviceRepository ();
				}

				return _instance;
			}
		}

		public void Refresh ()
		{
			ADB adb = new ADB ();
			Reset(adb.GetDevices ());
			RefreshAlarms ();
		}

		public void RefreshAlarms ()
		{
			ADB adb = new ADB ();

			foreach (var device in Devices) {
				List<Application> applications = adb.GetApplicationsWithAlarms (device);
				device.ResetApplications(applications);
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

		public void Reset(IEnumerable<Device> newDevices)
		{
			IEnumerable<Device> willRemoved = Devices.Except (newDevices);
			IEnumerable<Device> willAdded = newDevices.Except (Devices);

			foreach (var device in willRemoved) {
				Remove (device);
			}

			foreach (var device in willAdded) {
				Add (device);
			}
		}
	}
}

