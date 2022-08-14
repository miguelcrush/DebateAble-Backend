using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
	public class ParticipantType
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool CanComment { get; set; }
		public bool CanPost { get; set; }
		public bool CanView { get; set; }
	}
}
