using CarRentt.Cart;
using CarRentt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarRentt.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ShoppingCartActions cart = new ShoppingCartActions();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult recherche(int id, DateTime date)
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}