using POStock.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POStock.Controllers
{
    public class MainController : Controller
    {

        DbStockEntities db = new DbStockEntities();

        public ActionResult MainIndex()
        {
            var urunList = db.URUN.ToList();
            var musteriList = db.MUSTERI.ToList();
            ViewBag.urunList = urunList;
            ViewBag.musteriList = musteriList;
            var satisList = db.SATIS.ToList();
            ViewBag.satisList = satisList;
            var alisList = db.ALIS.ToList();
            return View(alisList);
        }

        public JsonResult GetSalesData()
        {
            var salesData = db.SATIS
                .Where(s => s.URUN.IsActive == true)
                .GroupBy(a => new { a.Urun, a.URUN.UrunAd })
                .Select(g => new { ProductName = g.Key.UrunAd, TotalQuantity = g.Sum(a => a.Adet) })
                .ToList();

            return Json(salesData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchasesData()
        {
            var purchasesData = db.ALIS
                .Where(a => a.URUN.IsActive == true)
                .GroupBy(a => new { a.Urun, a.URUN.UrunAd })
                .Select(g => new { ProductName = g.Key.UrunAd, TotalQuantity = g.Sum(a => a.Adet) })
                .ToList();

            return Json(purchasesData, JsonRequestBehavior.AllowGet);
        }

    }
}