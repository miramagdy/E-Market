using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EMarket.Models;

namespace EMarket.Context
{
    public class sotreContext : DbContext
    {
        public DbSet <Category> categories { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Cart> carts { get; set; }
    }
}