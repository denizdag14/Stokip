using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POStock.Models.Entity;

namespace POStock.Controllers
{
    public class SatisController : Controller
    {
        DbStockEntities db = new DbStockEntities();
        public ActionResult SatisIndex()
        {
            var satisList = db.SATIS.ToList();
            ViewBag.satisList = satisList;
            return View();
        }

        [HttpGet]
        public ActionResult SatisOlustur()
        {
            var urunList = db.URUN.ToList();
            var musteriList = db.MUSTERI.ToList();
            ViewBag.urunList = urunList;
            ViewBag.musteriList = musteriList;
            return View();
        }

        [HttpPost]
        public ActionResult SatisOlustur(short Urun, short Musteri, int Adet, string ToplamFiyat)
        {
            SATIS satis = new SATIS();
            satis.ToplamFiyat = Convert.ToDecimal(ToplamFiyat);
            satis.Musteri = Musteri;
            satis.Urun = Urun;
            satis.MUSTERI = db.MUSTERI.Find(Musteri);
            satis.URUN = db.URUN.Find(Urun);
            satis.Adet = Adet;
            satis.BirimFiyat = satis.ToplamFiyat / satis.Adet;
            satis.IsActive = true;
            if(satis.URUN.Stok == 0 || satis.Adet > satis.URUN.Stok)
            {
                return Json(new { success = false, message = "Üzgünüz, bu ürünün stokları tükenmiş durumda ya da stok sayısı yetersiz.\nStok: " + satis.URUN.Stok.ToString() });
            }
            db.SATIS.Add(satis);
            satis.URUN.Stok--;
            db.SaveChanges();
            return Json(new { success = true, message = "Satış başarıyla oluşturuldu"});

            //satis.BirimFiyat = satis.ToplamFiyat / satis.Adet;
            //satis.SatisID = 5;
            //db.SATIS.Add(satis);
            //db.SaveChanges();
            //return RedirectToAction("SatisIndex");
        }
    }
}