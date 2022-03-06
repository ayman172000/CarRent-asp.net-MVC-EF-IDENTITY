using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentt.Models;

namespace CarRentt.Areas.Admin.Controllers
{
    [Authorize(Roles = "admins")]
    public class LigneDeReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/LigneDeReservations
        public ActionResult Index(int? id)
        {
            var ligneDeReservations = db.LigneDeReservations.Include(l => l.Reservation).Include(l => l.Voiture).Where(l=>l.ReservationID==id);
            return View(ligneDeReservations.ToList());
        }

        // GET: Admin/LigneDeReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LigneDeReservation ligneDeReservation = db.LigneDeReservations.Find(id);
            if (ligneDeReservation == null)
            {
                return HttpNotFound();
            }
            return View(ligneDeReservation);
        }

        // GET: Admin/LigneDeReservations/Create
        public ActionResult Create()
        {
            ViewBag.ReservationID = new SelectList(db.Reservations, "ReservationID", "UserID");
            ViewBag.VoitureID = new SelectList(db.Voitures, "VoitureID", "Matricule");
            return View();
        }

        // POST: Admin/LigneDeReservations/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,VoitureID,Duree,PrixTTC")] LigneDeReservation ligneDeReservation)
        {
            if (ModelState.IsValid)
            {
                db.LigneDeReservations.Add(ligneDeReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationID = new SelectList(db.Reservations, "ReservationID", "UserID", ligneDeReservation.ReservationID);
            ViewBag.VoitureID = new SelectList(db.Voitures, "VoitureID", "Matricule", ligneDeReservation.VoitureID);
            return View(ligneDeReservation);
        }

        // GET: Admin/LigneDeReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LigneDeReservation ligneDeReservation = db.LigneDeReservations.Find(id);
            if (ligneDeReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationID = new SelectList(db.Reservations, "ReservationID", "UserID", ligneDeReservation.ReservationID);
            ViewBag.VoitureID = new SelectList(db.Voitures, "VoitureID", "Matricule", ligneDeReservation.VoitureID);
            return View(ligneDeReservation);
        }

        // POST: Admin/LigneDeReservations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,VoitureID,Duree,PrixTTC")] LigneDeReservation ligneDeReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ligneDeReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationID = new SelectList(db.Reservations, "ReservationID", "UserID", ligneDeReservation.ReservationID);
            ViewBag.VoitureID = new SelectList(db.Voitures, "VoitureID", "Matricule", ligneDeReservation.VoitureID);
            return View(ligneDeReservation);
        }

        // GET: Admin/LigneDeReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LigneDeReservation ligneDeReservation = db.LigneDeReservations.Find(id);
            if (ligneDeReservation == null)
            {
                return HttpNotFound();
            }
            return View(ligneDeReservation);
        }

        // POST: Admin/LigneDeReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LigneDeReservation ligneDeReservation = db.LigneDeReservations.Find(id);
            db.LigneDeReservations.Remove(ligneDeReservation);
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
