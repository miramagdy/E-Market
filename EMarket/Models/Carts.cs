using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace emarket.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("product")]
        public int product_id {get; set;}

        public DateTime added_at { get; set; }

        public virtual Product product { get; set; }
    }
}