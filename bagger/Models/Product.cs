using System;
using System.ComponentModel.DataAnnotations;

namespace bagger.Models
{
	public class Product
	{
		public int Id { get; set; }


        [Required(ErrorMessage = "Please enter the name of the product!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter the count of the product!")]
        public int? Count { get; set; }

        [Required(ErrorMessage = "Please enter the unit of measure of the product!")]
        public string? UoM { get; set; }

        [Required(ErrorMessage = "Please enter the actual purchase amount of the product!")]
        public string? ActualPurchase { get; set; }

        [Required(ErrorMessage = "Please enter the explanation of the products usage!")]
        public string? Explanation { get; set; }

        [Required(ErrorMessage = "Please enter the Legacy Description!")]
        public string? LegacyDescription { get; set; }

        [Required(ErrorMessage = "Please enter legacy item number!")]
        public string? LegacyItemNumber { get; set; }

        [Required(ErrorMessage = "Please enter the usage report")]
        public string? UsageReport { get; set; }

    }
}

