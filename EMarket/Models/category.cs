using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Category Name")]
        [Display(Name = "Category Name")]
        public string name { get; set; }
        public int number_of_products { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}