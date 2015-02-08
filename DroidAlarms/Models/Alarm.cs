using System;

namespace DroidAlarms.Models
{
	public class Alarm
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
	}
}

