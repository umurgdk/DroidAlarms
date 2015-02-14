﻿using System;
using Eto.Forms;

namespace DroidAlarms.Models
{
	public class Alarm : IEquatable<Alarm>
	{
		public enum AlarmType
		{
			RTC,
			RTC_WAKEUP,
			INTERVAL,
			INTERVAL_WAKEUP
		}

		public long Id { get; private set; }
		public DateTime Time { get; private set; }
		public AlarmType Type { get; private set; }

		public Alarm (long id, DateTime time, AlarmType type)
		{
			Id = id;
			Time = time;
			Type = type;
		}

		#region IEquatable implementation

		public bool Equals (Alarm obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Alarm))
				return false;
			Alarm other = (Alarm)obj;
			return Id == other.Id;
		}
		

		public override int GetHashCode ()
		{
			unchecked {
				return Id.GetHashCode ();
			}
		}
		

		#endregion
	}
}

