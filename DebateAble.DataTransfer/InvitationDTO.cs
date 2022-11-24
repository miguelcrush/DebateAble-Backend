using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class InvitationDTO
    {
        public Guid InviterAppUserId { get; set; }
        public Guid InviteeAppUserId { get; set; }
        public DateTime? InvitationSentUtc { get; set; }
        public string InvitationToken { get; set; }

        public AppUserDTO Inviter { get; set; }
        public AppUserDTO Invitee { get; set; }
    }
}
