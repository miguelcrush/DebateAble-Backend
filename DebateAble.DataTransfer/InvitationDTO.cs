using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class GetInvitationDTO
    {
        public Guid InviterAppUserId { get; set; }
        public Guid InviteeAppUserId { get; set; }
        public DateTime? InvitationSentUtc { get; set; }
        public string InvitationToken { get; set; }

        public GetAppUserDTO Inviter { get; set; }
        public GetAppUserDTO Invitee { get; set; }
    }
}
