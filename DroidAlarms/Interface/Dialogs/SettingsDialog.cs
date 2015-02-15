using System;
using Eto.Forms;
using Eto.Drawing;

namespace DroidAlarms.Interface.Dialogs
{
	public class SettingsDialog : Dialog
	{
		OpenFileDialog openFile;

		TextBox textbox = new TextBox { PlaceholderText = "/some/path/to/adb" };
		Button btnBrowse = new Button { Text = "Browse" };

		Button btnCancel = new Button { Text = "Cancel" };
		Button btnAccept = new Button { Text = "Accept" };

		bool quitOnCancel = true;

		public string Path { get; set; }

		public SettingsDialog (bool quitOnCancel)
		{
			this.quitOnCancel = quitOnCancel;

			if (quitOnCancel) {
				btnCancel.Text = "Quit";
			}

			Resizable = false;
			Size = new Size (400, 140);

			openFile = new OpenFileDialog ();

			btnCancel.Click += OnCancel;
			btnBrowse.Click += OnBrowse;
			btnAccept.Click += (sender, e) => Close();

			Content = new TableLayout {
				Padding = new Padding(10),
				Spacing = new Size(5, 5),
				Rows = {
					new TableLayout(new Label { Text = "ADB Executable path:" }),
					new TableLayout(new TableRow (new TableCell(new TableLayout(null, textbox, null), true), btnBrowse)),
					null,
					new TableLayout(new TableRow (null, btnCancel, btnAccept, null)),
				}
			};
		}

		public void OnBrowse (object sender, EventArgs e)
		{
			DialogResult res = openFile.ShowDialog (this);

			if (res == DialogResult.Ok) {
				textbox.Text = openFile.FileName;
				Path = openFile.FileName;
			}
		}

		public void OnCancel (object sender, EventArgs e)
		{
			if (this.quitOnCancel) {
				Close ();
				Application.Instance.Quit ();
			} else {
				Close();
			}
		}
	}
}

