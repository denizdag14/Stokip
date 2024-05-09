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
            if (Session["userID"] == null)
            {
                return RedirectToAction("UserIndex", "User");
            }
            short userID = (short)Session["userID"];
            var urunList = db.URUN.Where(u => u.UrunUser == userID).ToList();
            var musteriList = db.MUSTERI.Where(u => u.MusteriUser == userID).ToList();
            var satisList = db.SATIS.Where(u => u.SatisUser == userID).ToList();
            ViewBag.urunList = urunList;
            ViewBag.musteriList = musteriList;
            ViewBag.satisList = satisList;
            return View();
        }

        [HttpPost]
        public ActionResult SatisOlustur(short Urun, short Musteri, string Adet, string ToplamFiyat)
        {
            SATIS satis = new SATIS();
            satis.ToplamFiyat = Convert.ToDecimal(ToplamFiyat.Replace(",", "_").Replace(".", ",").Replace("_", "."));
            satis.Musteri = Musteri;
            satis.Urun = Urun;
            satis.MUSTERI = db.MUSTERI.Find(Musteri);
            satis.URUN = db.URUN.Find(Urun);
            satis.Adet = Convert.ToInt32(Adet);
            satis.BirimFiyat = satis.ToplamFiyat / satis.Adet;
            satis.SatisUser = (short)Session["userID"];
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