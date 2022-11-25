using DebateAble.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class GetDebateParticipantDTO
    {
        public Guid DebateId { get; set; }
        public Guid AppUserId { get; set; }
        public int ParticipantTypeId { get; set; }
        public ParticipantTypeDTO ParticpantType { get; set; }
        public GetAppUserDTO AppUser { get; set; }

    }

    public class PostDebateParticipantDTO
    {
        public Guid DebateId { get; set; }
        public Guid AppUserId { get; set; }
        public string? AppUserEmail { get; set; }

        public int ParticipantTypeId { get; set; }

        [JsonPropertyName("participantType")]
        public ParticipantTypeEnum ParticipantTypeEnum { get; set; }
    }
}
