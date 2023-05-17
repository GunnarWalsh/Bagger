using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace bagger.Models
{
	public class Warehouse
	{
        
        public int WarehouseId { get; set; }

        public string Location { get; set; }
		public int Tier { get; set; }
        public ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();
    }
}

