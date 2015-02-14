using System;
using Eto.Forms;

using DroidAlarms.Repositories;

namespace DroidAlarms.Interface
{
	public class DevicesPanel : Panel
	{
		TreeView treeView;
		TreeItemCollection devices = new TreeItemCollection();

		DeviceRepository deviceRepository = DeviceRepository.Instance;

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
			treeView.RefreshData ();
		}

		protected virtual void OnDeviceRemoved (object sender, DeviceRepositoryEventArgs args)
		{
			devices.Remove (args.Device);
			treeView.RefreshData ();
		}

		protected virtual void OnDeviceSelectionChanged (object sender, EventArgs e) 
		{

		}

		protected virtual void OnItemActivated (object sender, TreeViewItemEventArgs e)
		{

		}
	}
}

