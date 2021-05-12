using iTextSharp.text;
using iTextSharp.text.pdf;
using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Payroll.Controllers
{
    public class RHController : Controller
    {
        public PartialViewResult Biometrico()
        {
            return PartialView();
        }

        //Inserta los horarios de la empresa
        [HttpPost]
        public JsonResult InsertHrsEmpresa(int  IdEmpresa,int turno, string sDescripcion, string sHoraEntrada,string shoraSalida,string sHrEntradaPa, string sHrSalidaPa, int iTipoTurnocheck, int iTipoPausacheck ,int iDiasDes, int iCancelado,int iTipoTurno, int iTipoPausa)
        {
            EmpreHorarioBean bean = new EmpreHorarioBean();
            FuncionBiometricoDao dao = new FuncionBiometricoDao();
            int iIdusuario = int.Parse(Session["iIdUsuario"].ToString());
            bean = dao.sp_Insert_CHorarioHd(IdEmpresa, turno, sDescripcion, sHoraEntrada, shoraSalida, sHrEntradaPa, sHrSalidaPa, iTipoTurnocheck, iTipoPausacheck, iDiasDes, iCancelado, iIdusuario, iTipoTurno, iTipoPausa);
            return Json(bean);
        }


    }
}