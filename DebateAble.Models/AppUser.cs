﻿namespace DebateAble.Models
{
	public class AppUser : BaseTrackableModel
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}