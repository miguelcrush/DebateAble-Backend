using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
	public class Debate : BaseTrackableModel
	{
		public string Title { get; set; } = default!;
		public string? Description { get; set; }
		public Guid CreatedByAppUserId { get; set; }

		public virtual AppUser? CreatedBy { get; set; }

		public virtual ICollection<ResponseRequest> ResponseRequests { get; set; }
		public virtual ICollection<DebateParticipant> Participants { get; set; }
	}
}
