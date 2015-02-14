using System;
using Eto.Forms;

using DroidAlarms.Repositories;
using DroidAlarms.Models;

namespace DroidAlarms.Interface
{
	public class DevicesPanel : Panel
	{
		TreeView treeView;
		TreeItemCollection devices = new TreeItemCollection();

		DeviceRepository deviceRepository = DeviceRepository.Instance;

		public DroidAlarms.Models.Application SelectedApplication {
			get {
				return treeView.SelectedItem as DroidAlarms.Models.Application;
			}
		}

		public event EventHandler<DroidAlarms.Models.Application> ApplicationActivated;

		public DevicesPanel ()
		{
			treeView = new TreeView { Style = "devicelist" };
			treeView.DataStore = devices;
			treeView.SelectionChanged += OnDeviceSelectionChanged;
			treeView.Activated += OnItemActivated;

			deviceRepository.Added += OnDeviceAdded;
			deviceRepository.Removed += OnDeviceRemoved;

			Content = treeView;
		}

		private void LoadDevices ()
		{
			devices.AddRange (deviceRepository.Devices);
		}

		protected virtual void OnDeviceAdded (object sender, DeviceRepositoryEventArgs args)
		{
			devices.Add (args.Device);
			args.Device.ApplicationsChanged += (_sender, e) => OnDeviceApplicationsChanged(_sender as Device);
			treeView.RefreshData ();
		}

		protected virtual void OnDeviceApplicationsChanged (Device device)
		{
			treeView.RefreshItem (device);
		}

		protected virtual void OnDeviceRemoved (object sender, DeviceRepositoryEventArgs args)
		{
			devices.Remove (args.Device);
			treeView.RefreshData ();
		}

		protected virtual void OnDeviceSelectionChanged (object sender, EventArgs e) 
		{
			if (SelectedApplication != null) {
				ApplicationActivated (this, SelectedApplication);
			}
		}

		protected virtual void OnItemActivated (object sender, TreeViewItemEventArgs e)
		{
			if (SelectedApplication != null) {
				ApplicationActivated (this, SelectedApplication);
			}
		}
	}
}

