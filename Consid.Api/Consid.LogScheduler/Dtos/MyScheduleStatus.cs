﻿using System;

namespace Consid.LogScheduler.Dtos
{
	public class MyScheduleStatus
	{
		public DateTime Last { get; set; }

		public DateTime Next { get; set; }

		public DateTime LastUpdated { get; set; }
	}
}
