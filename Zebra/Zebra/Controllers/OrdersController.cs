using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zebra.Models;
using Microsoft.AspNet.Identity;

namespace Zebra.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            string strCurrentUserId = User.Identity.GetUserId();
            return View(db.Orders.Where(u => u.ID_User.ToString() == strCurrentUserId).ToList());
        }


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModels orderModels = db.Orders.Find(id);
            if (orderModels == null)
            {
                return HttpNotFound();
            }
            return View(orderModels);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View("~/Views/Orders/Index.cshtml");
        }

        

        // POST: Orders/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusicModels music)
        {
            if (ModelState.IsValid)
            {
                OrderModels order = new OrderModels()
                {
                    ID_Album = null,
                    ID_Music = music, //pas sur que ça soit cool qu'on est un id_music de type MusicModels cf plus haut
                    ID_User = Convert.ToInt32(User.Identity.GetUserId()),
                };
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Orders/Index.cshtml");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModels orderModels = db.Orders.Find(id);
            if (orderModels == null)
            {
                return HttpNotFound();
            }
            return View(orderModels);
        }

        // POST: Orders/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_User")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderModels);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModels orderModels = db.Orders.Find(id);
            if (orderModels == null)
            {
                return HttpNotFound();
            }
            return View(orderModels);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderModels orderModels = db.Orders.Find(id);
            db.Orders.Remove(orderModels);
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
    }
}
