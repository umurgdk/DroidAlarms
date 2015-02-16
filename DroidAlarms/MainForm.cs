using System;
using Eto.Forms;
using Eto.Drawing;

using DroidAlarms.Interface;
using DroidAlarms.Interface.Dialogs;
using DroidAlarms.Repositories;
using DroidAlarms.Models.ADB;
using System.Threading.Tasks;

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
			Title = "Droid Alarms";
			ClientSize = new Size (800, 600);

            HideEmptyPanel ();		

			devicesPanel.ApplicationActivated += (sender, e) => {
				alarmsPanel.SetAlarms (e.Alarms);
			};

			// create a few commands that can be used for the menu and toolbar
			var refresh = new Command {
				MenuText = "Refresh!",
				ToolBarText = "Refresh!",
				Image = Icon.FromResource(@"refreshIcon")
			};
            refresh.Executed += (sender, e) => Refresh();

			var quitCommand = new Command {
				MenuText = "Quit",
				Shortcut = Application.Instance.CommonModifier | Keys.Q
			};
			quitCommand.Executed += (sender, e) => Application.Instance.Quit ();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => MessageBox.Show (this, "About my app...");

			var preferencesCommand = new Command { MenuText = "&Preferences..." };
			preferencesCommand.Executed += (sender, e) => ShowSettings(true);

			// create menu
			Menu = new MenuBar {
				Items = {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { refresh } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
				ApplicationItems = {
					// application (OS X) or file menu (others)
					preferencesCommand,
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};

			// create toolbar			
			ToolBar = new ToolBar { Items = { refresh } };

			ShowSettings ();
		}

        public void ShowEmptyPanel ()
        {
            var layout = new DynamicLayout ();

            layout.Add (new Label {
                Text = "No Devices!", 
                VerticalAlign = VerticalAlign.Middle,
                HorizontalAlign = HorizontalAlign.Center,
                Font = new Font(SystemFont.Default, 30)
            }, true, true);

            Content = layout;
        }

        public void HideEmptyPanel ()
        {
            Content = new Splitter() {
                Panel1 = devicesPanel,
                Panel2 = alarmsPanel,
                Position = 160
            };
        }

        public void Refresh ()
        {
            bool shouldHideEmptyPanel = DeviceRepository.Instance.Devices.Count == 0;

            DeviceRepository.Instance.Refresh ();

            if (DeviceRepository.Instance.Devices.Count == 0) {
                ShowEmptyPanel ();
            } else if (shouldHideEmptyPanel) {
                HideEmptyPanel ();
            }
        }

		public async void ShowSettings (bool force = false)
		{
			await Task.Delay (100);

			Settings settings = new Settings ();

			if (settings.ADBPath != null && !force) {
				ADBExecuter.ExecutablePath = settings.ADBPath;
            } else {
                var settingsDialog = new SettingsDialog (true);
                settingsDialog.DisplayMode = DialogDisplayMode.Attached;
                settingsDialog.ShowModal (this);

                if (settingsDialog.Path != null) {
                    settings.ADBPath = settingsDialog.Path;
                    settings.Save ();

                    ADBExecuter.ExecutablePath = settingsDialog.Path;
                }
            }

            Refresh ();
		}
	}
}
