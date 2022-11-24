namespace DebateAble.Models
{
	public class AppUser : BaseTrackableModel
	{
		public string? Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public string? InvitationToken { get; set; }

		public virtual ICollection<Debate>? StartedDebates { get; set; }
		public virtual ICollection<DebateComment> DebateComments { get; set; }
		public virtual ICollection<DebatePost> DebatePosts { get; set; }
		public virtual ICollection<DebateParticipant> ParticipantOf { get; set; }
		public virtual ICollection<Invitation> SentInvitations { get; set; }
		public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
	}
}