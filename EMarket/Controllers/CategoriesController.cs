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
    public class CategoriesController : Controller
    {
        private sotreContext db = new sotreContext();

        
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,number_of_products")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public Category Edit(int? id)
        {
            if (id == null)
            {
                return null ;
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return null ;
            }
            return category;
        }

        
        public bool update([Bind(Include = "Id,name,number_of_products")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
