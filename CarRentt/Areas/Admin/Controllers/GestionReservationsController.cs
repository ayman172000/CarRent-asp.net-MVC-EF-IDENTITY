using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentt.Models;

namespace CarRentt.Areas.Admin.Controllers
{
    [Authorize(Roles = "admins")]
    public class GestionReservationsController : Controller
    {
        ApplicationDbContext db= new ApplicationDbContext();
        // GET: Admin/Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.User);
            return View(reservations.ToList());
        }

        // GET: Admin/Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(db.LigneDeReservations.Include(r => r.Reservation).Where(r => r.ReservationID == id).ToList());
        }


        // GET: Admin/Reservations/Edit/5
        /*public ActionResult valider(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }*/

        // POST: Admin/Reservations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult valider(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            //LigneDeReservation lr = new LigneDeReservation();
            foreach (var item in db.LigneDeReservations.Where(l=>l.ReservationID==id).ToList())
            {
                item.Voiture.IsAvailable = 0;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            reservation.Etat = "Validée";
            db.Entry(reservation).State = EntityState.Modified;
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