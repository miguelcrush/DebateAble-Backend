using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class DebateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        
        public string? StartedByEmailAddress { get; set; }
        public string? StartedByFirstName { get; set; }
        public string? StartedByLastName { get; set; }
    }
}
