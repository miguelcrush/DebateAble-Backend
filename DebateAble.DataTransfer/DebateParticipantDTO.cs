using DebateAble.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class GetDebateParticipantDTO
    {
        public Guid DebateId { get; set; }
        public Guid AppUserId { get; set; }
        public ParticipantTypeDTO ParticpantType { get; set; }
    }

    public class PostDebateParticipantDTO
    {
        public Guid DebateId { get; set; }
        public Guid AppUserId { get; set; }
        public ParticipantTypeEnum ParticipantTypeEnum { get; set; }
    }
}
