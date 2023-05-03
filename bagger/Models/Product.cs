using System;
namespace bagger.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? Count { get; set; }
		public string? UoM { get; set; }

        public string? ActualPurchase { get; set; }
        public string? Explanation { get; set; }
        public string? LegacyDescription { get; set; }
        public string? LegacyItemNumber { get; set; }
        public string? UsageReport { get; set; }

    }
}

