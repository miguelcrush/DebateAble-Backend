using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class PostResponseRequestDTO
    {
        public Guid DebateId { get; set; }
        public Guid ParticipantId { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
    }

    public class GetResponseRequestDTO
    {
        public Guid DebateId { get; set; }
        public string ParticipantFirstName { get; set; }
        public string ParticipantLastName { get; set; }
    }
}
