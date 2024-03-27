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
            var urunList = db.URUN.ToList();
            var musteriList = db.MUSTERI.ToList();
            ViewBag.urunList = urunList;
            ViewBag.musteriList = musteriList;
            var satisList = db.SATIS.ToList();
            ViewBag.satisList = satisList;
            return View();
        }

        [HttpPost]
        public ActionResult SatisOlustur(short Urun, short Musteri, int Adet, string ToplamFiyat)
        {
            SATIS satis = new SATIS();
            satis.ToplamFiyat = Convert.ToDecimal(ToplamFiyat.Replace(",", "_").Replace(".", ",").Replace("_", "."));
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
            satis.URUN.Stok = (short?)(satis.URUN.Stok - (short)satis.Adet);
            db.SaveChanges();
            return Json(new { success = true, message = "Satış başarıyla oluşturuldu"});
        }

        public ActionResult SatisSil(int id)
        {
            SATIS satis = db.SATIS.Find(id);
            satis.URUN.Stok += (short)satis.Adet;
            db.SATIS.Remove(satis);
            db.SaveChanges();
            return RedirectToAction("SatisIndex");
        }
    }
}