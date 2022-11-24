using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
    public class Invitation : BaseTrackableModel
    {
        public Guid InviterAppUserId { get; set; }
        public Guid InviteeAppUserId { get; set; }
        public DateTime? InvitationSentUtc { get; set; }
        public string InvitationToken { get; set; }

        public virtual AppUser Inviter { get; set; }
        public virtual AppUser Invitee { get; set; }
    }
}
