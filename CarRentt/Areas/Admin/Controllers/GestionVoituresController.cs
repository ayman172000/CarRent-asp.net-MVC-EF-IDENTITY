using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentt.Models;

namespace CarRentt.Areas.Admin.Controllers
{
    [Authorize(Roles = "admins")]
    public class GestionVoituresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Voitures
        public ActionResult Index()
        {
            var voitures = db.Voitures.Include(v => v.Modele);
            return View(voitures.ToList());
        }

        // GET: Admin/Voitures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // GET: Admin/Voitures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Voitures/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Voiture voiture, HttpPostedFileBase img, int MID)
        {
            string path = Path.Combine(Server.MapPath("~/Image/voitures"), img.FileName);
            img.SaveAs(path);
            voiture.Image = img.FileName;
            voiture.IsAvailable = 1;
            voiture.ModeleID = MID;
            db.Voitures.Add(voiture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Voitures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModeleID = new SelectList(db.Modeles, "ModeleID", "SerieVoiture", voiture.ModeleID);
            return View(voiture);
        }

        // POST: Admin/Voitures/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoitureID,Matricule,DateDeMiseEnCirculation,TypeCarburant,PrixJournaliere,Image,ModeleID,IsAvailable")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModeleID = new SelectList(db.Modeles, "ModeleID", "SerieVoiture", voiture.ModeleID);
            return View(voiture);
        }

        // GET: Admin/Voitures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // POST: Admin/Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voiture voiture = db.Voitures.Find(id);
            db.Voitures.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         public ActionResult Stocker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            voiture.IsAvailable = 1;
            db.Entry(voiture).State = EntityState.Modified;
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
