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

        //  Tabla de HorarioHD
        public JsonResult RetrieveHorarios()
        {
            int EmpresaID = int.Parse(Session["IdEmpresa"].ToString());
            List<EmpreHorarioBean> LTbProc = new List<EmpreHorarioBean>();
            FuncionBiometricoDao dao = new FuncionBiometricoDao();
            LTbProc = dao.sp_HorarioEmpresa_Retrieve_CHorarioHD(EmpresaID);
            return Json(LTbProc);
        }

        //Update los horarios de la empresa
        [HttpPost]
        public JsonResult UpdateHorario(int HrId, int turno, string sDescripcion, string sHoraEntrada, string shoraSalida, string sHrEntradaPa, string sHrSalidaPa, int iTipoTurnocheck, int iTipoPausacheck, int iDiasDes, int iTipoTurno, int iTipoPausa)
        {
            EmpreHorarioBean bean = new EmpreHorarioBean();
            FuncionBiometricoDao dao = new FuncionBiometricoDao();
            int iIdusuario = int.Parse(Session["iIdUsuario"].ToString());
            bean = dao.sp_updateChora_Update_CHorarioHd(HrId, turno, sDescripcion, sHoraEntrada, shoraSalida, sHrEntradaPa, sHrSalidaPa, iTipoTurnocheck, iTipoPausacheck, iDiasDes, iTipoTurno, iTipoPausa,0, iIdusuario);
            return Json(bean);
        }

        //Eliminar Datos de tabla chorarios
        [HttpPost]
        public JsonResult DeletHorario(int HrId)
        {
            EmpreHorarioBean bean = new EmpreHorarioBean();
            FuncionBiometricoDao dao = new FuncionBiometricoDao();
            int iIdusuario = int.Parse(Session["iIdUsuario"].ToString()); 
            String text = "";
            bean = dao.sp_updateChora_Update_CHorarioHd(HrId,0, text, text, text, text, text, 0, 0, 0, 0, 0, 1, iIdusuario);
            return Json(bean);
        }

        //  Tabla de HorarioLn
        public JsonResult RetrieveHrSemanl()
        {
            int EmpresaID = int.Parse(Session["IdEmpresa"].ToString());
            List<EmprHrSemanalBea> LTbProc = new List<EmprHrSemanalBea>();
            FuncionBiometricoDao dao = new FuncionBiometricoDao();
            LTbProc = dao.sp_HrSemanal_Retrieve_ChorarioLn(EmpresaID);
            return Json(LTbProc);
        }
    }
}