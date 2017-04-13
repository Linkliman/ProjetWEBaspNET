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
    public class AlbumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        SpotifyWebAPI _spotify = new SpotifyWebAPI()
        {
            UseAuth = false, //This will disable Authentication.
        };

        // GET: Albums
        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }

        // GET: MyAlbums
        [HttpGet]
        public ActionResult MyAlbums()
        {
            List<AlbumModels> item = new List<AlbumModels>();
            foreach (var m in db.Albums)
            {
                if (m.Created_by == User.Identity.Name)
                {
                    item.Add(m);
                }
            }
            return View(item);
        }

        // GET: Albums/Details/5
        public ActionResult Details(string id)
        {
            int albumID = int.Parse(id);
            AlbumModels alb = db.Albums.Include(a => a.Musics).Where(a => a.ID == albumID).First();
            return View(alb);
            FullAlbum album = _spotify.GetAlbum(id);
            AlbumModels v = new AlbumModels
            {
                Title = album.Name,
                Genre = album.Genres,
                ID_User = album.Artists,
                Note = album.Popularity
            };
            return View(v);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,prix,Note,ID_User")] AlbumModels albumModels)
        {
            if (ModelState.IsValid)
            {
                albumModels.Created_by = User.Identity.Name;
                db.Albums.Add(albumModels);
                db.SaveChanges();
                return RedirectToAction("MyAlbums");
            }

            return View(albumModels);
        }


        // GET: Albums/Musics
        public ActionResult Musics(int id)
        {
            AlbumModelsVM res = new AlbumModelsVM()
            {
                ID_album = id,
                music = new MusicModels()
            };
            return View(res);
        }

        // POST: Albums/Musics
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Musics([Bind(Include = "music")] AlbumModelsVM albumModels)
        {
            int id = int.Parse(Request.Form["Id"]);
            AlbumModels album = db.Albums.Include(a => a.Musics).Where(a => a.ID == id).First();
            var musics = db.Musics.ToList();
            foreach (var m in musics) {
                if (m.Title == albumModels.music.Title) {
                    m.ID_Album = album;
                    db.Entry(m).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MyAlbums");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumModels albumModels = db.Albums.Find(id);
            if (albumModels == null)
            {
                return HttpNotFound();
            }
            return View(albumModels);
        }

        // POST: Albums/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,prix,Note,ID_User")] AlbumModels albumModels)
        {
            if (ModelState.IsValid)
            {
                albumModels.Created_by = User.Identity.Name;
                db.Entry(albumModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAlbums");
            }
            return View(albumModels);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumModels albumModels = db.Albums.Find(id);
            if (albumModels == null)
            {
                return HttpNotFound();
            }
            return View(albumModels);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumModels albumModels = db.Albums.Find(id);
            db.Albums.Remove(albumModels);
            db.SaveChanges();
            return RedirectToAction("MyAlbums");
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
