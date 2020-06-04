using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public PartialViewResult GenerarReporte()
        {
            return PartialView();
        }

    }
}