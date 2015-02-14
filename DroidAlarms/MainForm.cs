using System;
using Eto.Forms;
using Eto.Drawing;
using DroidAlarms.Interface;

using DroidAlarms.Repositories;
using DroidAlarms.Models.ADB;

namespace DroidAlarms
{
	/// <summary>
	/// Your application's main form
	/// </summary>
	public class MainForm : Form
	{
		DevicesPanel devicesPanel = new DevicesPanel();
		AlarmsPanel alarmsPanel = new AlarmsPanel();

		public MainForm ()
		{
//			ADBExecuter.DebugMode = true;
			ADBExecuter.ExecutablePath = "/Users/umurgedik/Applications/adt/sdk/platform-tools/adb";

			Title = "My Eto Form";
			ClientSize = new Size (800, 600);

			// scrollable region as the main content
			Content = new Splitter() {
				Panel1 = devicesPanel,
				Panel2 = alarmsPanel,
				Position = 160
			};

			devicesPanel.ApplicationActivated += (sender, e) => {
				alarmsPanel.SetAlarms (e.Alarms);
			};

			// create a few commands that can be used for the menu and toolbar
			var clickMe = new Command {
				MenuText = "Refresh!",
				ToolBarText = "Refresh!",
				Image = Icon.FromResource(@"refreshIcon")
			};
			clickMe.Executed += (sender, e) => DeviceRepository.Instance.Refresh ();

			var quitCommand = new Command {
				MenuText = "Quit",
				Shortcut = Application.Instance.CommonModifier | Keys.Q
			};
			quitCommand.Executed += (sender, e) => Application.Instance.Quit ();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => MessageBox.Show (this, "About my app...");

			// create menu
			Menu = new MenuBar {
				Items = {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { clickMe } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
				ApplicationItems = {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};

			// create toolbar			
			ToolBar = new ToolBar { Items = { clickMe } };

			DeviceRepository.Instance.Refresh ();
		}
	}
}
