using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
	public class DebatePost : BaseTrackableModel
	{
		public Guid AppUserId { get; set; }
		public string Post { get; set; }
	}
}
