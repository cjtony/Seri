using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class SaveDataGeneralController : Controller
    {
        // GET: SaveDataGeneral
        public ActionResult Index()
        {
            return View();
        }

        //Guarda los datos de puesto
        [HttpPost]
        public JsonResult SaveDataPuestos(int typeregpuesto, string regcodpuesto, string regpuesto, string regdescpuesto, int proffamily, int clasifpuesto, int regcolect, int nivjerarpuesto, int perfmanager, int tabpuesto)
        {
            Boolean flag         = false;
            String  messageError = "none";
            CodigoCatalogoBean codeCatBean = new CodigoCatalogoBean();
            CodigoCatalogosDao codeCatDaoD = new CodigoCatalogosDao();
            PuestosBean addPuestoBean      = new PuestosBean();
            SavePuestosDao savePuestoDao   = new SavePuestosDao();
            try {
                codeCatBean         = codeCatDaoD.sp_Dato_Codigo_Catalogo_Seleccionado(typeregpuesto);
                string codeTypeJob  = codeCatBean.sCodigo;
                int consecutiveCode = codeCatBean.iConsecutivo;
                int consecutiveCNew = consecutiveCode + 1;
                string ceros = "";
                if (consecutiveCNew.ToString().Length == 1) {
                    ceros = "0000";
                } else if (consecutiveCNew.ToString().Length == 2) {
                    ceros = "000";
                } else if (consecutiveCNew.ToString().Length == 3) {
                    ceros = "00";
                } else if (consecutiveCNew.ToString().Length == 4) {
                    ceros = "0";
                }
                regcodpuesto        = codeTypeJob + ceros + consecutiveCNew.ToString();
                int keyemp          = int.Parse(Session["IdEmpresa"].ToString());
                int usuario         = Convert.ToInt32(Session["iIdUsuario"].ToString());
                addPuestoBean       = savePuestoDao.sp_Puestos_Insert_Puestos(regcodpuesto, regpuesto, regdescpuesto, proffamily, clasifpuesto, regcolect, nivjerarpuesto, perfmanager, tabpuesto, usuario, keyemp, consecutiveCNew, typeregpuesto);
                if (addPuestoBean.sMensaje != "success") {
                    messageError = addPuestoBean.sMensaje;
                } 
                if (addPuestoBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Puesto = regcodpuesto });
        }

        //Guarda los datos del departamento
        [HttpPost]
        public JsonResult SaveDepartament(string regdepart, string descdepart, string nivestuc, string nivsuptxt, int edific, string piso, string ubicac, int centrcost, int reportaa, string dgatxt, string dirgentxt, string direjetxt, string diraretxt, int dirgen, int direje, int dirare)
        {
            DepartamentosBean addDepartamentoBean = new DepartamentosBean();
            SaveDepartamentosDao saveDepartamentoDao = new SaveDepartamentosDao();
            int usuario = Convert.ToInt32(Session["iIdUsuario"].ToString());
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            addDepartamentoBean = saveDepartamentoDao.sp_Departamentos_Insert_Departamento(keyemp, regdepart, descdepart, nivestuc, nivsuptxt, edific, piso, ubicac, centrcost, reportaa, dgatxt, dirgentxt, direjetxt, diraretxt, dirgen, direje, dirare, usuario);
            string result = "error";
            if (addDepartamentoBean.sMensaje == "success")
            {
                result = addDepartamentoBean.sMensaje;
            }
            var data = new { result = result };
            return Json(addDepartamentoBean);
        }

        [HttpPost]
        public JsonResult SavePositions(string codposic, int depaid, int puesid, int regpatcla, int localityr, int emprepreg, int reportempr)
        {
            DatosPosicionesBean addPosicionBean = new DatosPosicionesBean();
            DatosPosicionesDao savePosicionDao = new DatosPosicionesDao();
            int usuario = Convert.ToInt32(Session["iIdUsuario"].ToString());
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            addPosicionBean = savePosicionDao.sp_Posiciones_Insert_Posicion(codposic, depaid, puesid, regpatcla, localityr, emprepreg, reportempr, usuario, keyemp);
            var data = new { result = addPosicionBean.sMensaje };
            return Json(data);

        }

        //Guarda los datos generales del empleado
        [HttpPost]
        public JsonResult DataGeneral(string name, string apepat, string apemat, int sex, int estciv, string fnaci, string lnaci, int title, string nacion, int state, string codpost, string city, string colony, string street, string numberst, string telfij, string telmov, string email, string tipsan, string fecmat)
        {
            Boolean flag         = false;
            String  messageError = "none";
            EmpleadosBean addEmpleadoBean = new EmpleadosBean();
            EmpleadosDao empleadoDao      = new EmpleadosDao();
            string convertFNaci = "";
            if (fnaci != "") {
                convertFNaci = Convert.ToDateTime(fnaci).ToString("dd/MM/yyyy");
            }
            string convertFMatr = "";
            if (fecmat != "") {
                convertFMatr = Convert.ToDateTime(fecmat).ToString("dd/MM/yyyy");
            }
            try {
                int usuario     = Convert.ToInt32(Session["iIdUsuario"].ToString());
                int empresa     = int.Parse(Session["IdEmpresa"].ToString());
                addEmpleadoBean = empleadoDao.sp_Empleados_Insert_Empleado(name, apepat, apemat, sex, estciv, convertFNaci, lnaci, title, nacion, state, codpost, city, colony, street, numberst, telfij, telmov, email, usuario, empresa, tipsan, convertFMatr);
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            var data = new { result = addEmpleadoBean.sMensaje };
            return Json(addEmpleadoBean);
        }

        //Guarda los datos del imss del empleado
        [HttpPost]
        public JsonResult DataImss(string fecefe, string regimss, string rfc, string curp, int nivest, int nivsoc, string empleado, string apepat, string apemat, string fechanaci)
        {
            Boolean flag         = false;
            String  messageError = "none";
            ImssBean addImssBean = new ImssBean();
            ImssDao imssDao      = new ImssDao();
            string convertFEffdt = "";
            if (fecefe != "") {
                convertFEffdt = Convert.ToDateTime(fecefe).ToString("dd/MM/yyyy");
            }
            string convertFNaciE = "";
            if (fechanaci != "") {
                convertFNaciE = Convert.ToDateTime(fechanaci).ToString("dd/MM/yyyy");
            }
            try {
                int usuario = Convert.ToInt32(Session["iIdUsuario"].ToString());
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                addImssBean = imssDao.sp_Imss_Insert_Imss(convertFEffdt, regimss, rfc, curp, nivest, nivsoc, usuario, empleado, apepat, apemat, convertFNaciE, keyemp, 0);
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            var data = new { result = addImssBean.sMensaje };
            return Json(data);
        }

        //Guarda los datos de la nomina del empleado
        [HttpPost]
        public JsonResult DataNomina(string fecefecnom, double salmen, int tipemp, int nivemp, int tipjor, int tipcon, string fecing, string fecant, string vencon, string empleado, string apepat, string apemat, string fechanaci, int tipper, int tipcontra, int tippag, int banuse, string cunuse, int position, int clvemp)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosNominaBean addDatoNomina = new DatosNominaBean();
            DatosNominaDao datoNominaDao  = new DatosNominaDao();
            string convertFEffdt = "";
            if (fecefecnom != "") {
                convertFEffdt = Convert.ToDateTime(fecefecnom).ToString("dd/MM/yyyy");
            }
            string convertFIngrs = "";
            if (fecing != "") {
                convertFIngrs = Convert.ToDateTime(fecing).ToString("dd/MM/yyyy");
            }
            string convertFAcnti = "";
            if (fecant != "") {
                convertFAcnti = Convert.ToDateTime(fecant).ToString("dd/MM/yyyy");

            }
            string convertFVenco = "";
            if (vencon != "") {
                convertFVenco = Convert.ToDateTime(vencon).ToString("dd/MM/yyyy");
            }
            string convertFNaciE = "";
            if (fechanaci != "") {
                convertFNaciE = Convert.ToDateTime(fechanaci).ToString("dd/MM/yyyy");
            }
            try {
                int keyemp    = int.Parse(Session["IdEmpresa"].ToString());
                int usuario   = Convert.ToInt32(Session["iIdUsuario"].ToString());
                addDatoNomina = datoNominaDao.sp_DatosNomina_Insert_DatoNomina(convertFEffdt, salmen, tipemp, nivemp, tipjor, tipcon, convertFIngrs, convertFAcnti, convertFVenco, usuario, empleado, apepat, apemat, convertFNaciE, keyemp, tipper, tipcontra, tippag, banuse, cunuse, position, clvemp);
                if (addDatoNomina.sMensaje != "success") {
                    messageError = addDatoNomina.sMensaje;
                }
                if (addDatoNomina.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        //Guarda los datos de estructura del empleado
        [HttpPost]
        public JsonResult DataEstructura(int clvstr, string fechefectpos, string fechinipos, string empleado, string apepat, string apemat, string fechanaci)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosPosicionesBean addPosicionBean = new DatosPosicionesBean();
            DatosPosicionesDao datoPosicionDao  = new DatosPosicionesDao();
            string convertFEffdt = "";
            if (fechefectpos != "") {
                convertFEffdt = Convert.ToDateTime(fechefectpos).ToString("dd/MM/yyyy");
            }
            string convertFNaciE = "";
            if (fechanaci != "") {
                convertFNaciE = Convert.ToDateTime(fechanaci).ToString("dd/MM/yyyy");
            }
            string convertFIniP = "";
            if (fechinipos != "") {
                convertFIniP = Convert.ToDateTime(fechinipos).ToString("dd/MM/yyyy");
            }
            try {
                int usuario     = Convert.ToInt32(Session["iIdUsuario"].ToString());
                addPosicionBean = datoPosicionDao.sp_PosicionesAsig_Insert_PosicionesAsig(clvstr, convertFEffdt, fechinipos, empleado, apepat, apemat, convertFNaciE, usuario);
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            var data = new { result = addPosicionBean.sMensaje };
            return Json(data);
        }

        // Guarda los datos de la estructura al editar el empleado
        [HttpPost]
        public JsonResult DataEstructuraEdit(int clvstr, string fechefectpos, string fechinipos, int clvemp, int clvnom)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            string  convertFEffdt = Convert.ToDateTime(fechefectpos).ToString("dd/MM/yyyy");
            string  convertFIniP  = Convert.ToDateTime(fechinipos).ToString("dd/MM/yyyy");
            DatosPosicionesBean addPosicionBean = new DatosPosicionesBean();
            DatosPosicionesDao datoPosicionDao  = new DatosPosicionesDao();
            try {
                int usuario     = Convert.ToInt32(Session["iIdUsuario"].ToString());
                addPosicionBean = datoPosicionDao.sp_PosicionesAsig_Insert_PosicionesAsigEdit(clvstr, convertFEffdt, convertFIniP, clvemp, clvnom, usuario);
                if (addPosicionBean.sMensaje != "success") {
                    messageError = addPosicionBean.sMensaje;
                }
                if (addPosicionBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = true, MensajeError = messageError });
        }

        //Guarda los datos de las regionales
        [HttpPost]
        public JsonResult SaveRegionales(string descregion, string claregion)
        {
            RegionalesBean addRegionBean = new RegionalesBean();
            RegionesDao regionDao = new RegionesDao();
            int usuario = Convert.ToInt32(Session["iIdUsuario"].ToString());
            // Reemplazar por la session de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            addRegionBean = regionDao.sp_Regionales_Insert_Regionales(descregion, claregion, usuario, keyemp);
            var data = new { result = addRegionBean.sMensaje };
            return Json(data);
        }

        //Guarda los datos de las sucursales
        [HttpPost]
        public JsonResult SaveSucursales(string descsucursal, string clasucursal)
        {
            SucursalesBean addSucursalBean = new SucursalesBean();
            SucursalesDao sucursalDao = new SucursalesDao();
            int usuario = Convert.ToInt32(Session["iIdUsuario"].ToString());
            addSucursalBean = sucursalDao.sp_Sucursales_Insert_Sucursales(descsucursal, clasucursal, usuario);
            var data = new { result = addSucursalBean.sMensaje };
            return Json(data);
        }
    }
}