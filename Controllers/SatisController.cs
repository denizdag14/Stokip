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
            return View(satisList);
        }
    }
}