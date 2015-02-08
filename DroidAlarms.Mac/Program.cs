using System;
using Eto.Forms;
using Eto;
using Eto.Mac.Forms;

namespace DroidAlarms.Mac
{
	public class Program
	{
		[STAThread]
		public static void Main (string[] args)
		{
			Style.Add<FormHandler> ("mainform", (handler) => {

			});



			new Application (Eto.Platforms.Mac).Run (new MainForm ());
		}
	}
}

