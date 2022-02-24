using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Product Name")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Product Price")]
        public int Price { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Product Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}