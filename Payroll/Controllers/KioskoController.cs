using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class KioskoController : Controller
    {
        // GET: Kiosko
        public PartialViewResult SolicitudVacaciones()
        {
            return PartialView();
        }
        public PartialViewResult Autorizadores()
        {
            return PartialView();
        }
    }
}