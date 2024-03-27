using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using POStock.Models.Entity;
using WebGrease.Css.Extensions;

namespace POStock.Controllers
{
    public class KategoriController : Controller
    {
        DbStockEntities db = new DbStockEntities();
        public ActionResult KategoriIndex()
        {
            var kategoriList = db.KATEGORI.ToList();
            return View(kategoriList);
        }

        [HttpPost]
        public ActionResult KategoriOlustur(KATEGORI kat)
        {
            kat.IsActive = true;
            db.KATEGORI.Add(kat);
            db.SaveChanges();
            return RedirectToAction("KategoriIndex");
        }

        [HttpPost]
        public ActionResult KategoriOlustur2(KATEGORI kat)
        {
            kat.IsActive = true;
            db.KATEGORI.Add(kat);
            db.SaveChanges();
            return RedirectToAction("UrunOlustur", "Urun");
        }

        public ActionResult KategoriSil(int id)
        {
            db.KATEGORI.Find(id).IsActive = false;
            db.KATEGORI.Find(id).KategoriAd += " (Silinmiş Kategori)";
            db.SaveChanges();
            return RedirectToAction("KategoriIndex");
        }

        [HttpGet]
        public ActionResult KategoriBilgisiniGetir(int id)
        {
            var kat = db.KATEGORI.Find(id);
            var model = new
            {
                KategoriID = kat.KategoriID,
                KategoriAd = kat.KategoriAd,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult KategoriGuncelle(KATEGORI updated)
        {
            var kat = db.KATEGORI.Find(updated.KategoriID);
            kat.KategoriAd = updated.KategoriAd;
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}