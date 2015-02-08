using System;
using Eto.Forms;

namespace DroidAlarms.Interface
{
	public class DevicesPanel : Panel
	{
		TreeView treeView;
		TreeItemCollection devices = new TreeItemCollection();

		public DevicesPanel ()
		{
			treeView = new TreeView { Style = "devicelist" };
			treeView.DataStore = devices;
			treeView.SelectionChanged += OnDeviceSelectionChanged;
			treeView.Activated += OnItemActivated;
		}

		protected virtual void OnDeviceSelectionChanged (object sender, EventArgs e) 
		{

		}

		protected virtual void OnItemActivated (object sender, TreeViewItemEventArgs e)
		{

		}
	}
}

