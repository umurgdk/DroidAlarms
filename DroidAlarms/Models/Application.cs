using System;
using System.Collections.Generic;
using Eto.Forms;

namespace DroidAlarms.Models
{
	public class Application : ITreeItem, IEquatable<Application>
	{
		public string Name { get; private set; }
		public Device Device { get; set; }
		public List<Alarm> Alarms { get; private set; }

		public Application (string name)
		{
			Name = name;
			Alarms = new List<Alarm> ();
		}

		#region IEquatable<Application> implementation

		public bool Equals (Application obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Application))
				return false;
			Application other = (Application)obj;
			return Device == other.Device && Name == other.Name;
		}
		

		public override int GetHashCode ()
		{
			unchecked {
				return (Device != null ? Device.GetHashCode () : 0) ^ (Name != null ? Name.GetHashCode () : 0);
			}
		}

		#endregion

		#region ITreeItem implementation

		public bool Expanded { get; set; }

		public bool Expandable { get; } = false;

		public ITreeItem Parent {
			get {
				return Device;
			}
			set { }
		}

		#endregion

		#region IDataStore implementation

		public int Count { get; } = 0;

		public ITreeItem this [int index] {
			get { return null; }
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

