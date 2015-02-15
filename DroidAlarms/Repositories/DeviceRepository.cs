using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DroidAlarms.Models;
using DroidAlarms.Models.ADB;
using System.Threading.Tasks;

namespace DroidAlarms.Repositories
{
	public class DeviceRepository
	{
		public ObservableCollection<Device> Devices { get; private set; }
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
			Devices = new ObservableCollection<Device> ();
			Devices.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
				if (e.NewItems != null && Added != null) {
					Added (this, new DeviceRepositoryEventArgs(e.NewItems[0] as Device));
				} else if (e.OldItems != null && Removed != null) {
					Removed (this, new DeviceRepositoryEventArgs(e.OldItems[0] as Device));
				}
			};
		}


		public void Reset(IEnumerable<Device> newDevices)
		{
			IEnumerable<Device> willRemoved = Devices.Except (newDevices);
			IEnumerable<Device> willAdded = newDevices.Except (Devices);

			foreach (var device in willRemoved.ToList()) {
				Devices.Remove (device);
			}

			foreach (var device in willAdded) {
				Devices.Add (device);
			}
		}
	}
}

