﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class RHController : Controller
    {
        public PartialViewResult Biometrico()
        {
            return PartialView();
        }

    }
}