using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class PostDebateDTO
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public PostResponseRequestDTO ResponseRequest { get; set; }
    }

    public class GetDebateDTO
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public string StartedByEmailAddress { get; set; }
        public string StartedByFirstName { get; set; }
        public string StartedByLastName { get; set; }

        public List<GetResponseRequestDTO> ResponseRequests { get; set; }
    }
}
