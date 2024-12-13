using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Application.DTOModels
{
    public class ProductDTO
    {
        [MinLength(1)]
        [Required]
        public string Name { get; set; } = null!;

        [Range(0.01,double.MaxValue)]
        [Required]
        public decimal Price { get; set; }

        [MinLength(1)]
        [Required]
        public string Description { get; set; } = null!;

        [MinLength(1)]
        [Required]
        public string Category { get; set; } = null!;
    }
}
