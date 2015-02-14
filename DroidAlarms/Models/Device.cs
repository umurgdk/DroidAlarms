using System;
using System.Collections.Generic;
using Eto.Forms;
using System.Linq;

namespace DroidAlarms.Models
{
	public class Device : ITreeItem
	{
		public string  			 Id   { get; set; } 
		public string 			 Name { get; set; }
		public List<Application> Applications { get; set; } = new List<Application>();

		public event EventHandler<EventArgs> ApplicationsChanged;

		public void ResetApplications (IEnumerable<Application> newApplications)
		{
			List<Application> willRemoved = Applications.Except (newApplications).ToList();
			List<Application> willAdded = newApplications.Except (Applications).ToList();
			List<Application> willUpdated = newApplications.Intersect (Applications).ToList();

			foreach (var app in willRemoved) {
				Applications.Remove (app);
			}

			Applications.AddRange (willAdded);

			foreach (var app in willUpdated) {
				var found = Applications.Where (a => a.Name == app.Name).FirstOrDefault ();

				if (found != null) {
					found.UpdateAlarms (app.Alarms);
				}
			}

			if (ApplicationsChanged != null) {
				ApplicationsChanged (this, EventArgs.Empty);
			}
		}

		#region IEquatable implementation
		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Device))
				return false;
			Device other = (Device)obj;
			return Name == other.Name;
		}
		

		public override int GetHashCode ()
		{
			unchecked {
				return (Name != null ? Name.GetHashCode () : 0);
			}
		}
		#endregion

		#region ITreeItem implementation
		public bool Expanded { get; set;}
		public bool Expandable { get; } = true;

		public ITreeItem Parent { get; set; }
		#endregion

		#region IDataStore implementation
		public int Count {
			get {
				return Applications.Count;
			}
		}
		public ITreeItem this [int index] {
			get {
				return Applications [index];
			}
		}
		#endregion

		#region IImageListItem implementation
		public Eto.Drawing.Image Image { get; } = null;
		#endregion

		#region IListItem implementation
		public string Text {
			get {
				return Name;
			}
			set {
				Name = value;
			}
		}
		public string Key {
			get {
				return Name;
			}
		}
		#endregion
	}
}

