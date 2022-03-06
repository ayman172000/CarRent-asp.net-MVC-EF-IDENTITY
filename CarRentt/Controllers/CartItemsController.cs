using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentt.Cart;
using CarRentt.Models;
using Microsoft.AspNet.Identity;

namespace CarRentt.Controllers
{
    public class CartItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ShoppingCartActions cart = new ShoppingCartActions();

        // GET: CartItems
        public ActionResult Index()
        {
            return View(cart.GetCartItems());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CartId,Duree,DateCreated,Idvoiture")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CartId,Duree,DateCreated,Idvoiture")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartItem);
        }

        public ActionResult Delete(string id)
        {
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            db.ShoppingCartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Buy(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                double prix = 0;
                Reservation R = new Reservation();
                LigneDeReservation LR = new LigneDeReservation();
                R.UserID = User.Identity.GetUserId();
                R.Etat = "non_validée";
                db.Reservations.Add(R);
                db.SaveChanges();
                foreach (var item in cart.GetCartItems())
                {
                    LR.ReservationID = R.ReservationID;
                    LR.VoitureID = item.Idvoiture;
                    LR.Duree = item.Duree;
                    if (LR.Duree >= 30)
                        LR.PrixTTC = item.Duree * (item.Voiture.PrixJournaliere - (item.Voiture.PrixJournaliere * 0.6));
                    else
                        LR.PrixTTC = item.Duree * item.Voiture.PrixJournaliere;
                    prix += LR.PrixTTC;/*
                    Voiture voiture = db.Voitures.Find(item.Idvoiture);
                    voiture.IsAvailable = 0;
                    db.Entry(voiture).State = EntityState.Modified;*/
                    CartItem cartItem = db.ShoppingCartItems.Find(item.ItemId);
                    db.ShoppingCartItems.Remove(cartItem);
                    db.LigneDeReservations.Add(LR);
                    db.SaveChanges();
                }
                R.Total = prix;
                db.Entry(R).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
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
