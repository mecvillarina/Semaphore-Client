using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.Manager.Entities
{
	public class EntityBase
	{
		public bool IsSuccess { get; set; } = false;
		public int StatusCode { get; set; }
		public string Message { get; set; } = "";
	}
}
