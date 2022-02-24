using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMarket.Context;
using EMarket.Models;

namespace EMarket.Controllers
{
    public class CartsController : Controller
    {
        private sotreContext db = new sotreContext();

        public ActionResult ListCart()
        {
            var cart = db.carts.ToList();

            return PartialView(cart);
        }
        public ActionResult DeleteCart(int id)
        {
            var cart = db.carts.SingleOrDefault(c => c.id == id);
            db.carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index", "Products");

        }
    }
}
