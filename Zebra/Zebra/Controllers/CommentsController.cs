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
            List<CommentModels> item = new List<CommentModels>();
            foreach (var c in db.Comments)
            {
                if (c.Music == Id)
                {
                    item.Add(c);
                }
            }
            return View(item);
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
            return View();
        }

        // POST: Musics/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,Music,ID_Album,ID_Music,Content")] CommentModels comment)
        {
            comment.ID_User = User.Identity.Name;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
                db.Entry(commentModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            db.Comments.Remove(commentModels);
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

        private List<CommentModels> GetComments()
        {
            List<CommentModels> comments = new List<CommentModels>();
            comments.Add(new CommentModels { ID = 1, Music = 1, ID_User = "Pierre", Content = "Master1" });
            comments.Add(new CommentModels { ID = 2, Music = 1, ID_User = "Paul", Content = "Paul" });
            comments.Add(new CommentModels { ID = 3, Music = 1, ID_User = "Jacques", Content = "Lucien" });
            comments.Add(new CommentModels { ID = 4, Music = 1, ID_User = "Franck", Content = "Nadine" });
            return comments;
        }
    }
}
