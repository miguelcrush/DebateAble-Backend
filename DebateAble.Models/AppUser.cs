namespace DebateAble.Models
{
	public class AppUser : BaseTrackableModel
	{
		public string Email { get; set; } = default!;
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public virtual ICollection<Debate>? StartedDebates { get; set; }
	}
}