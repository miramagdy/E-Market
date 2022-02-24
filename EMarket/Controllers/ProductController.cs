using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emarket.Context;
using emarket.Models;
using System.IO;
using emarket.ViewModels;
using System.Data.Entity;
using Newtonsoft.Json.Serialization;
using System.Web.Services.Description;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;
using System.Net.Http;
using Microsoft.Ajax.Utilities;
using emarket.Controllers;

namespace emarket.Controllers
{
    public class ProductController : Controller
    {
        MyContext db = new MyContext();
        
        // GET: Product
        public ActionResult Index(string search)
        {
            
            var ccart = db.cart.ToList(); 
            var products = from s in db.product select s;
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.category.name.Contains(search));  
            }
            var productcart = new ProductCartViewModel
            {
                product = products.ToList(),
                cart = ccart
            };
            return View(productcart);

        }
        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();
            ViewBag.category_id = new SelectList(db.category, "id", "name", product.category_id);

            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(product.Imagefile.FileName);
                string ext = Path.GetExtension(product.Imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + ext;
                product.image = "~/images/" + filename;
                filename = Path.Combine(Server.MapPath("~/images/"), filename);
                product.Imagefile.SaveAs(filename);
                db.product.Add(product);
                db.SaveChanges();
                var category = db.category.ToList();
                foreach(var cate in category)
                {
                    if (product.category_id == cate.id)
                    {
                        cate.number_of_products++;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var product = db.product.SingleOrDefault(c => c.id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var productId = db.product.SingleOrDefault(c => c.id == id);
            if (productId == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.category_id = new SelectList(db.category, "id", "name", productId.category_id); // ana momkin a3adel al category bta3 al product 3ady
            return View(productId); // 5od a3redly al product da 
        }
        [HttpPost]
        public ActionResult Edit(int id , Product product)
        {

            if (product.Imagefile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(product.Imagefile.FileName);
                string ext = Path.GetExtension(product.Imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + ext;
                product.image = "~/images/" + filename;
                filename = Path.Combine(Server.MapPath("~/images/"), filename);
                product.Imagefile.SaveAs(filename);
                Product prod1 = db.product.Where(p => p.id == id).FirstOrDefault();
                prod1.image = product.image;
                prod1.name = product.name;
                prod1.price = product.price;
                prod1.description = product.description;
                prod1.category_id = product.category_id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (product.image == null)
            {
                Product prod1 = db.product.Where(p => p.id == id).FirstOrDefault();
              
                prod1.name = product.name;
                prod1.price = product.price;
                prod1.description = product.description;
                prod1.category_id = product.category_id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var product = db.product.SingleOrDefault(c => c.id == id);
            db.product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            var ccart = db.cart.ToList();
            foreach (var item in ccart)
            {
                if (id != item.product.id)
                {
                    Cart cart = new Cart();
                    Product prod = db.product.SingleOrDefault(p => p.id == id);
                    cart.product = prod;
                    cart.added_at = DateTime.Now;
                    db.cart.Add(cart);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                else if (id == item.product.id)
                    return PartialView();
            }
            return null;
        }
    }

}