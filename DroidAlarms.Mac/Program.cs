using System;
using Eto.Forms;
using Eto;
using Eto.Mac.Forms;
using Eto.Mac.Forms.Controls;
using MonoMac.AppKit;

namespace DroidAlarms.Mac
{
	public class Program
	{
		[STAThread]
		public static void Main (string[] args)
		{
			Style.Add<TreeViewHandler> ("devicelist", handler => {
				handler.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
				handler.Scroll.BorderType = NSBorderType.NoBorder;
			});

			Style.Add<GridViewHandler> ("alarmsgrid", handler => {
				handler.ScrollView.BorderType = NSBorderType.NoBorder;
			});

			new Application (Eto.Platforms.Mac).Run (new MainForm ());
		}
	}
}

