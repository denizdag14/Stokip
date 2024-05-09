using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POStock.Models.Entity;

namespace POStock.Controllers
{
    public class MusteriController : Controller
    {
        DbStockEntities db = new DbStockEntities();
        public ActionResult MusteriIndex()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("UserIndex", "User");
            }
            short userID = (short)Session["userID"];
            var musteriList = db.MUSTERI.Where(u => u.MusteriUser == userID).ToList();
            return View(musteriList);
        }

        [HttpPost]
        public ActionResult MusteriOlustur(MUSTERI musteri)
        {
            musteri.IsActive = true;
            musteri.MusteriUser = (short)Session["userID"];
            db.MUSTERI.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("MusteriIndex");
        }

        public ActionResult MusteriSil(int id)
        {
            db.MUSTERI.Find(id).IsActive = false;
            db.SaveChanges();
            return RedirectToAction("MusteriIndex");
        }

        [HttpGet]
        public ActionResult MusteriBilgisiniGetir(int id)
        {
            var musteri = db.MUSTERI.Find(id);
            var model = new
            {
                MusteriID = musteri.MusteriID,
                MusteriAd = musteri.MusteriAd,
                MusteriSoyad = musteri.MusteriSoyad,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MusteriGuncelle(MUSTERI updated)
        {
            var musteri = db.MUSTERI.Find(updated.MusteriID);
            musteri.MusteriAd = updated.MusteriAd;
            musteri.MusteriSoyad = updated.MusteriSoyad;
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}