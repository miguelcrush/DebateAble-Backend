using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
    public class ResponseRequest : BaseTrackableModel
    {
        public Guid DebateId { get; set; }
        public Guid ParticipantId { get; set; }
        public bool Satisified { get; set; }
        public DateTime ExpiresOnUtc { get; set; }

        public Debate Debate { get; set; }
        public DebateParticipant Participant { get; set; }
    }
}
