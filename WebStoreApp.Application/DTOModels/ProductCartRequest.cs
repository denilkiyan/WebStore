using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Application.DTOModels
{
    public class ProductCartRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int id { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int quantity { get; set; }
    }
}
