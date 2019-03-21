using System;
using System.Collections.Generic;

namespace Api.Models.Db.Entities
{
	public partial class Country
	{
		public Country()
		{
			City = new HashSet<City>();
		}

		public short CountryId { get; set; }
		public string CountryName { get; set; }
		public DateTimeOffset LastUpdate { get; set; }

		public virtual ICollection<City> City { get; set; }
	}
}
