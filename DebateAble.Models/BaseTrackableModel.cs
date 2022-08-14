using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
	public abstract class BaseTrackableModel
	{
		public Guid Id { get; set; }
		public DateTime CreatedOnUtc { get; set; }
		public DateTime ModifiedOnUtc { get; set; }
	}
}
