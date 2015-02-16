using System;
using System.Collections.ObjectModel;
using Eto;
using Eto.Forms;
using System.Collections.Generic;
using DroidAlarms.Models;

namespace DroidAlarms.Interface
{
	public class AlarmsPanel : Panel
	{
		ObservableCollection<Alarm> Alarms { get; set; }
		GridView GridView { get; set; }

		public AlarmsPanel ()
		{
			Alarms = new ObservableCollection<Alarm> ();

			GridView = new GridView {
				DataStore = Alarms,
				Style = "alarmsgrid"
			};

			GridView.Columns.Add (new GridColumn {
				DataCell = new TextBoxCell { Binding = Binding.Property<Alarm, string>(a => a.Id) },
				HeaderText = "ID"
			});

			GridView.Columns.Add (new GridColumn {
				DataCell = new TextBoxCell { Binding = Binding.Property<Alarm, string>(a => a.TypeText) },
				HeaderText = "Type"
			});

			GridView.Columns.Add (new GridColumn {
				DataCell = new TextBoxCell { Binding = Binding.Property<Alarm, string> (a => a.TimeText) },
				HeaderText = "Date"
			});

			Content = GridView;
		}
		

		public void SetAlarms (IEnumerable<Alarm> newAlarms)
		{
			Alarms.Clear ();

			foreach (var alarm in newAlarms) {
				Alarms.Add (alarm);
			}
		}
	}
}

