using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDapper.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "The Product Name field is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Product Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Product Price must be a positive value.")]
        public decimal ProductPrice { get; set; }
    }

}
