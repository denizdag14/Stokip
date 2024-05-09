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
            if (Session["userID"] == null)
            {
                return RedirectToAction("UserIndex", "User");
            }
            short userID = (short)Session["userID"];
            var alisList = db.ALIS.Where(u => u.AlisUser == userID).ToList();
            var urunList = db.URUN.Where(u => u.UrunUser == userID).ToList();
            ViewBag.urunList = urunList;
            return View(alisList);
        }

        [HttpPost]
        public ActionResult AlisOlustur(short Urun, string Adet, string ToplamFiyat)
        {
            ALIS alis = new ALIS();
            alis.Adet = Convert.ToInt32(Adet);
            alis.ToplamFiyat = Convert.ToDecimal(ToplamFiyat.Replace(",", "_").Replace(".", ",").Replace("_", "."));
            alis.URUN = db.URUN.Find(Urun);
            alis.BirimFiyat = alis.ToplamFiyat / alis.Adet;
            alis.AlisUser = (short)Session["userID"];
            alis.IsActive = true;
            db.ALIS.Add(alis);
            alis.URUN.Stok = (short?)(alis.URUN.Stok + (short)alis.Adet);
            db.SaveChanges();
            return Json(new { success = true, message = "Alış başarıyla oluşturuldu" });
        }

        public ActionResult AlisSil(int id)
        {
            ALIS alis = db.ALIS.Find(id);
            if(alis.Adet >= alis.URUN.Stok) alis.URUN.Stok = 0;
            else alis.URUN.Stok -= (short)alis.Adet;
            db.ALIS.Remove(alis);
            db.SaveChanges();
            return RedirectToAction("AlisIndex");
        }
    }
}