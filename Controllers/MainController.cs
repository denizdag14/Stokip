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
            if (Session["userID"] == null)
            {
                return RedirectToAction("UserIndex", "User");
            }
            short userID = (short)Session["userID"];
            var satisList = db.SATIS.Where(u => u.SatisUser == userID).ToList();
            ViewBag.satisList = satisList;
            var alisList = db.ALIS.Where(u => u.AlisUser == userID).ToList();
            return View(alisList);
        }

        public JsonResult GetSalesData()
        {
            short userID = (short)Session["userID"];
            var salesData = db.SATIS
                .Where(u => u.SatisUser == userID)
                .GroupBy(a => new { a.Urun, a.URUN.UrunAd })
                .Select(g => new { ProductName = g.Key.UrunAd, TotalQuantity = g.Sum(a => a.Adet) })
                .ToList();

            return Json(salesData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchasesData()
        {
            short userID = (short)Session["userID"];
            var purchasesData = db.ALIS
                .Where(u => u.AlisUser == userID)
                .GroupBy(a => new { a.Urun, a.URUN.UrunAd })
                .Select(g => new { ProductName = g.Key.UrunAd, TotalQuantity = g.Sum(a => a.Adet) })
                .ToList();

            return Json(purchasesData, JsonRequestBehavior.AllowGet);
        }

    }
}