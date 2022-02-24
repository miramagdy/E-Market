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
using System.IO;
using System.Data.Entity.Migrations;

namespace EMarket.Controllers
{
    public class ProductsController : Controller
    {
        private sotreContext db = new sotreContext();

        // GET: Products
        public ActionResult Index(string search)
        {
            // get products from database

            var products = from s in db.product select s;

            // check if search is empty 
            if (!string.IsNullOrEmpty(search))
            {
                products = db.product.Where(s => s.Category.name.Contains(search));
            }
            else
            {
                products = db.product.Include(p => p.Category);
            }
            //var product = db.product.Include(p => p.Category);
            
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Image , ImageFile ,Description,CategoryId")] Product product)
        {
            
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string exe = Path.GetExtension(product.ImageFile.FileName);
                string fileNameToStore = filename + DateTime.Now.ToString("ddmmyy") + exe;
                product.Image = "~/productImages/" + fileNameToStore;
                fileNameToStore = Path.Combine(Server.MapPath("~/productImages/"), fileNameToStore);
                product.ImageFile.SaveAs(fileNameToStore);

                db.product.Add(product);
                
                //CategoriesController c = new CategoriesController();
                Category category = db.categories.Find(product.CategoryId);
                category.number_of_products += 1;
                db.categories.AddOrUpdate(category);             
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "name", product.CategoryId);
            return View(product);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Image,Description,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = db.product.Find(id);
            db.product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddToCart(int id)
        {

            var ccart = db.carts.ToList();
            Cart cart = new Cart();
            Product prod = db.product.SingleOrDefault(p => p.Id == id);
            cart.product_id = id;
        //    cart.product = prod;
            cart.added_at = DateTime.Now;
            db.carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
            /* foreach (var item in ccart)
             {
                 if (id != item.product.Id)
                 {
                     Cart cart = new Cart();
                     Product prod = db.product.SingleOrDefault(p => p.Id == id);
                     cart.product = prod;
                     cart.added_at = DateTime.Now;
                     db.carts.Add(cart);
                     db.SaveChanges();
                     return RedirectToAction("Index");
                 }

                 else if (id == item.product.Id)
                     return PartialView();
             }
             return null;
         }*/
        }

    }
}
