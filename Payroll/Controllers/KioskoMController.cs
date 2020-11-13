using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class KioskoMController : Controller
    {
        // GET: KioscoM
        public PartialViewResult ConsultaRecibo()
        {
            return PartialView();
        }

        public PartialViewResult AutorizacionVacaciones()
        {
            return PartialView();
        }

    }
}