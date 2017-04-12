using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zebra.Models;

namespace Zebra.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index(int Id)
        {
            CommentModelsVM vm = new CommentModelsVM()
            {
                Music = Id,
                Comments = new List<CommentModels>(),
            };
            foreach (var c in db.Comments)
            {
                if (c.Music == Id)
                {
                    vm.Comments.Add(c);
                }
            }
            return View(vm);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModels commentModels = db.Comments.Find(id);
            if (commentModels == null)
            {
                return HttpNotFound();
            }
            return View(commentModels);
        }

        // GET: Musics/Create
        public ActionResult Create(int Id)
        {
            CommentModelsVM commentvm = new CommentModelsVM()
            {
                Comment = new CommentModels(),
                Music = Id,
            };
            return View(commentvm);
        }

        // POST: Musics/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,ID_Music,Content")] CommentModels comment)
        {
            comment.ID_User = User.Identity.Name;
            comment.Music = int.Parse(Request.Form["Id"]);
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = comment.Music });
        }

        // GET: Comments/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModels commentModels = db.Comments.Find(id);
            if (commentModels == null)
            {
                return HttpNotFound();
            }
            return View(commentModels);
        }

        // POST: Comments/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_User,ID_Music,Content")] CommentModels commentModels)
        {
            if (ModelState.IsValid)
            {
                string aux = commentModels.Content;
                CommentModels comment = db.Comments.Find(commentModels.ID);
                comment.Content = aux;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = comment.Music });
            }
            return View(commentModels);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModels commentModels = db.Comments.Find(id);
            if (commentModels == null)
            {
                return HttpNotFound();
            }
            return View(commentModels);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModels commentModels = db.Comments.Find(id);
            int aux = commentModels.Music;
            db.Comments.Remove(commentModels);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = aux });
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
