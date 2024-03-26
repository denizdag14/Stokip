using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POStock.Models.Entity;

namespace POStock.Controllers
{
    public class AlisController : Controller
    {
        DbStockEntities db = new DbStockEntities();
        public ActionResult AlisIndex()
        {
            var alisList = db.ALIS.ToList();
            return View(alisList);
        }

        [HttpGet]
        public ActionResult AlisOlustur()
        {
            var urunList = db.URUN.ToList();
            return View(urunList);
        }

        [HttpPost]
        public ActionResult AlisOlustur(ALIS alis)
        {
            alis.BirimFiyat = alis.ToplamFiyat / alis.Adet;
            alis.IsActive = true;
            db.ALIS.Add(alis);
            db.SaveChanges();
            return RedirectToAction("AlisIndex");
        }
    }
}