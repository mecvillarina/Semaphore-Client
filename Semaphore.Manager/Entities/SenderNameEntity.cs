using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.Manager.Entities
{
	public class SenderNamesResponseEntity : EntityBase
	{
		public List<SenderNameEntity> SenderNames { get; set; }
	}


	public class SenderNameEntity
	{
		public string Name { get; set; }
		public string Status { get; set; }
		public string Created { get; set; }
	}
}
