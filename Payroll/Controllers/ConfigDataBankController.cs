﻿using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class ConfigDataBankController : Controller
    {
        // GET: ConfigDataBank
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult LoadTypeDispersion()
        {
            Boolean flag = false;
            String  messageError = "none";
            List<CatalogoGeneralBean> lTypeDispersionBean = new List<CatalogoGeneralBean>();
            LoadDataTableDaoD         lTypeDispersionDaoD = new LoadDataTableDaoD();
            try {
                lTypeDispersionBean = lTypeDispersionDaoD.sp_TiposDispersion_Retrieve_TiposDispersion();
                if (lTypeDispersionBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, DatosDispersion = lTypeDispersionBean });
        }

        [HttpPost]
        public JsonResult LoadDataTableBanks()
        {
            Boolean flag = false;
            String messageError = "none";
            List<LoadDataTableBean> lDataBankBean = new List<LoadDataTableBean>();
            LoadDataTableDaoD lDataBankDaoD = new LoadDataTableDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                lDataBankBean = lDataBankDaoD.sp_Carga_Bancos_Empresa(keyBusiness);
                flag = (lDataBankBean.Count > 0) ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, DatosBancos = lDataBankBean });
        }

        [HttpPost]
        public JsonResult UpdateConfigBank(int keyBank, string numClientBank, string numBillBank, string numSquareBank, string numClabeBank, int interfaceGen)
        {
            Boolean flag = false;
            String messageError = "none";
            LoadDataTableBean dataBankBean = new LoadDataTableBean();
            LoadDataTableDaoD dataBankDaoD = new LoadDataTableDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                dataBankBean = dataBankDaoD.sp_Actualiza_Banco_Empresa(keyBusiness, keyBank, numClientBank, numBillBank, numSquareBank, numClabeBank, interfaceGen);
                flag = (dataBankBean.sMensaje == "update") ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }
    }
}