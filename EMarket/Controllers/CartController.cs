using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emarket.Context;
using emarket.Models;
using System.IO;
using emarket.ViewModels;

namespace emarket.Controllers
{
    public class CartController : Controller
    {
        MyContext db = new MyContext();
        // GET: Cart
        public ActionResult ListCart()
        {
            var cart = db.cart.ToList();

            return PartialView(cart);
        }
        public ActionResult DeleteCart(int id)
        {
            var cart = db.cart.SingleOrDefault(c => c.product_id == id);
            db.cart.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index","Product");

        }
    }
}