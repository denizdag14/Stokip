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
            return View();
        }

        public JsonResult GetSalesData()
        {
            var salesData = db.SATIS
                .Where(s => s.URUN.IsActive == true)
                .GroupBy(s => s.Urun)
                .Select(g => new { ProductName = g.Key, TotalQuantity = g.Sum(s => s.Adet) })
                .ToList();

            return Json(salesData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchasesData()
        {
            var purchasesData = db.ALIS
                 .Where(a => a.URUN.IsActive == true)
                .GroupBy(a => a.Urun)
                .Select(g => new { ProductName = g.Key, TotalQuantity = g.Sum(a => a.Adet) })
                .ToList();

            return Json(purchasesData, JsonRequestBehavior.AllowGet);
        }

    }
}