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
    public class GestionModelesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Modeles
        public ActionResult Index()
        {
            var modeles = db.Modeles.Include(m => m.Categorie);
            return View(modeles.ToList());
        }

        // GET: Admin/Modeles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // GET: Admin/Modeles/Create
        public ActionResult Create()
        {
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "NomCategorie");
            return View();
        }

        // POST: Admin/Modeles/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModeleID,SerieVoiture,MarqueVoiture,CategorieID")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Modeles.Add(modele);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "NomCategorie", modele.CategorieID);
            return View(modele);
        }

        // GET: Admin/Modeles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "NomCategorie", modele.CategorieID);
            return View(modele);
        }

        // POST: Admin/Modeles/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModeleID,SerieVoiture,MarqueVoiture,CategorieID")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modele).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "NomCategorie", modele.CategorieID);
            return View(modele);
        }

        // GET: Admin/Modeles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // POST: Admin/Modeles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modele modele = db.Modeles.Find(id);
            db.Modeles.Remove(modele);
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
