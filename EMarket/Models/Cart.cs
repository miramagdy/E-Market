using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EMarket.Models
{
    public class Cart
    {

        [Key]
        public int id { get; set; }
        [ForeignKey("product")]
        public int product_id { get; set; }

        public virtual Product product { get; set; }
        public DateTime added_at { get; set; }

    }
}