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

namespace CarRentt.Controllers
{
    public class VoituresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ShoppingCartActions cart = new ShoppingCartActions();
        public ActionResult VoitureCategorie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var result = db.Voitures.Include(d => d.Modele.Categorie).Where(c => c.Modele.CategorieID == id).Where(c=>c.IsAvailable==1);
                return View(result.ToList());
            }
        }
        public ActionResult AjouterPanier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                cart.AddToCart((int)id);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Index()
        {
            var voitures = db.Voitures.Include(v => v.Modele).Where(c=>c.IsAvailable==1);
            return View(voitures.ToList());
        }


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
