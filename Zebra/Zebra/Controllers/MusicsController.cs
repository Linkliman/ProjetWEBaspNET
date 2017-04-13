using Microsoft.AspNet.Identity;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        public bool IsBuy(string currentIdUser, string idMusic)
        {
            var isbuybycurrent = new List<OrderModels>();
            isbuybycurrent = db.Orders.Where(u => u.ID_User.ToString() == currentIdUser.ToString()).Where(u => u.ID_Music.ID.ToString() == idMusic.ToString()).ToList();
            if (isbuybycurrent.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: Musics/MusicDetails/5
        public ActionResult MusicDetails(string Id)
        {
            
            foreach (var m in db.Musics)
            {
                if (m.ID.ToString() == Id)
                {
                    return View(m);
                }
            }
            FullTrack track = _spotify.GetTrack(Id);
            FullAlbum album = null;
            ViewBag.Boolean = IsBuy(User.Identity.GetUserId().ToString(), Id);
            MusicModels v = new MusicModels
            {
                ID_Spotify = Id,
                Title = track.Name,
                prix = track.Popularity / 7,
                Note = track.Popularity,
                ID_User = track.Artists,
                PreviewUrl = track.PreviewUrl,
            };
            if (track.Album != null)
            {
                album = _spotify.GetAlbum(track.Album.Id);
                v.Album = album.Name;
                v.ReleaseDate = DateTime.Parse(album.ReleaseDate);
            }
            return View(v);
        }

        // GET: Musics/SearchMusic/5
        public ActionResult SearchMusic(string recherche)
        {
            SearchItem item = _spotify.SearchItems(recherche, SearchType.Track);
            List<MusicModels> liste = new List<MusicModels>();
            foreach (var m in db.Musics)
            {
                if ((m.Title != null) && (m.Title.IndexOf(recherche) == 0))
                {
                    liste.Add(m);
                }
                if ((m.Created_by != null) && (m.Created_by.IndexOf(recherche) == 0))
                {
                    liste.Add(m);
                }
            }
            ViewBag.Recherche=(liste as IEnumerable<MusicModels>);
            return View(item.Tracks);
        }

        // GET: MyMusics
        [HttpGet]
        public ActionResult MyMusics()
        {
            List<MusicModels> item = new List<MusicModels>();
            foreach (var m in db.Musics)
            {
                if (m.Created_by == User.Identity.Name)
                {
                    item.Add(m);
                }
            }
            return View(item);
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
        public ActionResult Create(MusicModels musicModels, HttpPostedFileBase MusicFile)
        {
            if (ModelState.IsValid && MusicFile != null)
            {
                musicModels.Created_by=User.Identity.Name;
                var path = Server.MapPath("~/Content/musique/");
                var path2 = Server.MapPath("~/Content/trailer/");
                int fileNumber = Directory.GetFiles(path).Length + 1;
                musicModels.numberMusic = fileNumber;
                db.Musics.Add(musicModels);
                db.SaveChanges();
                string filename = Path.GetFileName(fileNumber.ToString() + ".mp3");
                MusicFile.SaveAs(Path.Combine(path, filename));

                string ffPath = Server.MapPath("~/Content/ffmpeg-20170411-f1d80bc-win64-static/bin/ffmpeg.exe");
                string processString = "-t 20 -i " + Path.Combine(path, filename) + " " + Path.Combine(path2, filename);

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo = new System.Diagnostics.ProcessStartInfo(ffPath, processString);
                p.Start();
                p.WaitForExit();
                return RedirectToAction("MyMusics");
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
                musicModels.Created_by = User.Identity.Name;
                db.Entry(musicModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyMusics");
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
            return RedirectToAction("MyMusics");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Play(int musicID, string path)
        {
            var file = Server.MapPath("~/Content/"+ path + "/" + musicID.ToString() + ".mp3");
            return File(file, "audio/mp3");
        }
    }
}
