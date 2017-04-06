using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
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
    public class MusicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        SpotifyWebAPI _spotify = new SpotifyWebAPI()
        {
            UseAuth = false, //This will disable Authentication.
        };

        // GET: Musics
        public ActionResult Index()
        {
            return View(db.Musics.ToList());
        }

        // GET: Musics/Id
        public ActionResult Album(string Id)
        {
            FullAlbum album = _spotify.GetAlbum("5O7V8l4SeXTymVp3IesT9C");
            AlbumModels v = new AlbumModels
            {
                ID = album.Id,
                Title = album.Name,
                Image = album.Images,
                ReleaseDate = album.ReleaseDate,
                Note = album.Popularity,
                ID_User = album.Artists,

            };
            return View(v);
    }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicModels musicModels = db.Musics.Find(id);
            if (musicModels == null)
            {
                return HttpNotFound();
            }
            return View(musicModels);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Musics/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,prix,ID_User,Note")] MusicModels musicModels)
        {
            if (ModelState.IsValid)
            {
                db.Musics.Add(musicModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musicModels);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicModels musicModels = db.Musics.Find(id);
            if (musicModels == null)
            {
                return HttpNotFound();
            }
            return View(musicModels);
        }

        // POST: Musics/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,prix,ID_User,Note")] MusicModels musicModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicModels);
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicModels musicModels = db.Musics.Find(id);
            if (musicModels == null)
            {
                return HttpNotFound();
            }
            return View(musicModels);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicModels musicModels = db.Musics.Find(id);
            db.Musics.Remove(musicModels);
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
