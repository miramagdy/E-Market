using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emarket.Models;

namespace emarket.ViewModels
{
    public class ProductCartViewModel
    {
        public List<Product>  product { get; set; }
        public List<Cart> cart { get; set; }
    }
}
