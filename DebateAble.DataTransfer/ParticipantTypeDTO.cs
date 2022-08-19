using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.DataTransfer
{
    public class ParticipantTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool CanComment { get; set; }
        public bool CanPost { get; set; }
        public bool CanView { get; set; }
    }
}
