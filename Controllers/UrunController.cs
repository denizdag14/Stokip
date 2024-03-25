using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POStock.Models.Entity;

namespace POStock.Controllers
{
    public class UrunController : Controller
    {

        DbStockEntities db = new DbStockEntities();

        public ActionResult UrunIndex()
        {
            var urunList = db.URUN.ToList();
            ViewBag.katList = db.KATEGORI.ToList();
            return View(urunList);
        }

        [HttpGet]
        public ActionResult UrunOlustur()
        {
            var katList = db.KATEGORI.ToList();
            return View(katList);
        }

        [HttpPost]
        public ActionResult UrunOlustur(URUN urun)
        {
            urun.Stok = 0;
            urun.IsActive = true;
            db.URUN.Add(urun);
            db.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        public ActionResult UrunSil(int id)
        {
            db.URUN.Find(id).IsActive = false;
            db.URUN.Find(id).UrunAd += " (Silinmiş Ürün)";
            db.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        [HttpGet]
        public ActionResult UrunBilgisiniGetir(int id)
        {
            var urun = db.URUN.Find(id);
            var kategoriler = db.KATEGORI.ToList();
            var seciliKat = urun.UrunKategori;
            var model = new
            {
                UrunID = urun.UrunID,
                UrunAd = urun.UrunAd,
                kategoriler = kategoriler.Select(k => new { id = k.KategoriID, ad = k.KategoriAd }),
                seciliKat = seciliKat,
                Stok = urun.Stok
            };
            return Json(model ,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(URUN updated)
        {
            var urun = db.URUN.Find(updated.UrunID);
            urun.UrunAd = updated.UrunAd;
            urun.UrunKategori = updated.UrunKategori;
            urun.Stok = updated.Stok;
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}