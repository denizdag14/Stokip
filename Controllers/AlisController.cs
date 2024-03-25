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
            var alisList = db.ALIS.ToList();
            return View(alisList);
        }
    }
}