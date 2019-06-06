using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.WebService.DataContracts
{
	public class SenderNamesResponseContract : ContractBase
	{
		public List<SenderNameContract> SenderNames { get; set; }
	}

	public class SenderNameContract
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("created")]
		public string Created { get; set; }
	}
}
