﻿using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class EditDataGeneralController : Controller
    {
        // GET: EditDataGeneral
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EditDepartament(string edidepart, string edidescdepart, string edinivestuc, string nivsuptxtedit, int ediedific, string edipiso, string ediubicac, int edicentrcost, int edireportaa, string edidgatxt, string edidirgentxt, string edidirejetxt, string edidiraretxt, int edidirgen, int edidireje, int edidirare, int clvdepart)
        {
            DepartamentosBean editDepartamentoBean = new DepartamentosBean();
            EditDepartamentosDao editDepartamentoDao = new EditDepartamentosDao();
            editDepartamentoBean = editDepartamentoDao.sp_Departamentos_Update_Departamento(edidepart, edidescdepart, edinivestuc, nivsuptxtedit, ediedific, edipiso, ediubicac, edicentrcost, edireportaa, edidgatxt, edidirgentxt, edidirejetxt, edidiraretxt, edidirgen, edidireje, edidirare, clvdepart);
            var data = new { result = editDepartamentoBean.sMensaje };
            return Json(data);
        }

        [HttpPost]
        public JsonResult EditPuesto(string edicodpuesto, string edipuesto, string edidescpuesto, int ediproffamily, int ediclasifpuesto, int edicolect, int edinivjerarpuesto, int ediperfmanager, int editabpuesto, int clvpuesto)
        {
            PuestosBean editPuestoBean = new PuestosBean();
            EditPuestosDao editPuestoDao = new EditPuestosDao();
            editPuestoBean = editPuestoDao.sp_Puestos_Update_Puesto(edicodpuesto, edipuesto, edidescpuesto, ediproffamily, ediclasifpuesto, edicolect, edinivjerarpuesto, ediperfmanager, editabpuesto, clvpuesto);
            var data = new { result = editPuestoBean.sMensaje };
            return Json(data);
        }

        // Edicion de los datos generales del empleados

        [HttpPost]
        public JsonResult EditDataGeneral(string name, string apepat, string apemat, int sex, int estciv, string fnaci, string lnaci, int title, int nacion, int state, string codpost, string city, string colony, string street, string numberst, string telfij, string telmov, string email, string tipsan, string fecmat, int clvemp)
        {
            Boolean flag         = false;
            String  messageError = "none";
            EmpleadosBean employeeBean      = new EmpleadosBean();
            EditEmpleadoDao editEmployeeDao = new EditEmpleadoDao();
            string convertFNaci = Convert.ToDateTime(fnaci).ToString("dd/MM/yyyy");
            string convertFMatr = "";
            if (fecmat != "") {
                convertFMatr = Convert.ToDateTime(fecmat).ToString("dd/MM/yyyy");
            }
            try {
                employeeBean = editEmployeeDao.sp_Empleados_Update_Empleado(name, apepat, apemat, sex, estciv, convertFNaci, lnaci, title, nacion, state, codpost, city, colony, street, numberst, telfij, telmov, email, convertFMatr, tipsan, clvemp);
                if (employeeBean.sMensaje != "success") {
                    messageError = employeeBean.sMensaje;
                }
                if (employeeBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        // Edicion de los datos del imss del empleado

        [HttpPost]
        public JsonResult EditDataImss(string regimss, string fecefe, string rfc, string curp, int nivest, int nivsoc, int clvimss, string fecefeact, int keyemployee)
        {
            Boolean flag         = false;
            String  messageError = "none";
            string test = "";
            ImssBean imssBean               = new ImssBean();
            EditEmpleadoDao editEmpleadoDao = new EditEmpleadoDao();
            string convertFEffdt            = "";
            if (fecefe != "") {
                convertFEffdt = Convert.ToDateTime(fecefe).ToString("dd/MM/yyyy");
            }
            string convertFEffdtAct = "";
            if (fecefeact != "") {
                convertFEffdtAct = Convert.ToDateTime(fecefeact).ToString("dd/MM/yyyy");
            }
            try {
                if (convertFEffdt != convertFEffdtAct) {
                    int usuario          = Convert.ToInt32(Session["iIdUsuario"].ToString());
                    int keyemp           = int.Parse(Session["IdEmpresa"].ToString());
                    ImssDao saveDataImss = new ImssDao();
                    imssBean             = saveDataImss.sp_Imss_Insert_Imss(convertFEffdt, regimss, rfc, curp, nivest, nivsoc, usuario, "none","none","none","none", keyemp, keyemployee);
                } else {
                    imssBean = editEmpleadoDao.sp_Imss_Update_DatoImss(regimss, convertFEffdt, rfc, curp, nivest, nivsoc, clvimss);
                }
                if (imssBean.sMensaje != "success") {
                    messageError = imssBean.sMensaje;
                } 
                if (imssBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Test = test });
        }

        // Edicion de los datos de la nomina del empleado

        [HttpPost]
        public JsonResult EditDataNomina(string fechefectact, string fecefecnom, double salmen, int tipper, int tipemp, int nivemp, int tipjor, int tipcon, int tipcontra, string fecing, string fecant, string vencon, int tippag, int banuse, string cunuse, int clvnom, int position)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosNominaBean nominaBean      = new DatosNominaBean();
            EditEmpleadoDao editEmpleadoDao = new EditEmpleadoDao();
            string convertFEffdtAct = "";
            if (fechefectact != "") {
                convertFEffdtAct = Convert.ToDateTime(fechefectact).ToString("dd/MM/yyyy");
            }
            string convertFEffdt = "";
            if (fecefecnom != "") {
                convertFEffdt = Convert.ToDateTime(fecefecnom).ToString("dd/MM/yyyy");
            }
            string convertFIngrs = "";
            if (fecing != "") {
                convertFIngrs = Convert.ToDateTime(fecing).ToString("dd/MM/yyyy");
            }
            string convertFAntiq = "";
            if (fecant != "") {
                convertFAntiq = Convert.ToDateTime(fecant).ToString("dd/MM/yyyy");
            }
            string convertFVencC = "";
            if (vencon != "") {
                convertFVencC = Convert.ToDateTime(vencon).ToString("dd/MM/yyyy");
            }
            try {
                nominaBean = editEmpleadoDao.sp_Nomina_Update_DatoNomina(convertFEffdt, salmen, tipper, tipemp, nivemp, tipjor, tipcon, tipcontra, convertFIngrs, convertFAntiq, convertFVencC, tippag, banuse, cunuse, clvnom, position);
                if (nominaBean.sMensaje != "success") {
                    messageError = nominaBean.sMensaje;
                }
                if (nominaBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Slario = salmen });
        }

        // Edicion de los datos de estructura del empleado

        [HttpPost]
        public JsonResult EditDataStructure(int numpla, int depaid, int puesid, string emprep, string report, string tippag, int banuse, string cunuse, int clvstr)
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            EditEmpleadoDao editEmpleadoDao = new EditEmpleadoDao();
            posicionBean = editEmpleadoDao.sp_Posiciones_Update_DatoPosicion(numpla, depaid, puesid, emprep, report, tippag, banuse, cunuse, clvstr);
            var data = new { result = posicionBean.sMensaje };
            return Json(data);
        }
        // Edita las regiones
        [HttpPost]
        public JsonResult EditRegionales(string descregionedit, string claregionedit, int clvregion)
        {
            RegionalesBean regionBean = new RegionalesBean();
            EditRegionalesDao editRegionalesDao = new EditRegionalesDao();
            regionBean = editRegionalesDao.sp_Regionales_Update_Regionales(descregionedit, claregionedit, clvregion);
            var data = new { result = regionBean.sMensaje };
            return Json(data);
        }

        // Edita las sucursales
        [HttpPost]
        public JsonResult EditSucursales(string descsucursaledit, string clasucursaledit, int clvsucursal)
        {
            SucursalesBean sucursalBean = new SucursalesBean();
            EditSucursalesDao editSucursalesDao = new EditSucursalesDao();
            sucursalBean = editSucursalesDao.sp_Sucursales_Update_Sucursales(descsucursaledit, clasucursaledit, clvsucursal);
            var data = new { result = sucursalBean.sMensaje };
            return Json(data);
        }
    }
}