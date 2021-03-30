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

        [HttpPost]
        public JsonResult ValidateEmployeeReg(string fieldCurp, string fieldRfc)
        {
            Boolean flag         = false;
            String  messageError = "none";
            EmpleadosBean employeeBean = new EmpleadosBean();
            EmpleadosDao  employeeDaoD = new EmpleadosDao();
            try {
                int keyemp   = int.Parse(Session["IdEmpresa"].ToString());
                int keyUser  = 0;
                employeeBean = employeeDaoD.sp_Empleados_Validate_DatosImss(keyemp, fieldCurp.Trim(), fieldRfc.Trim(), keyUser);
                if (employeeBean.sMensaje != "continue") {
                    messageError = employeeBean.sMensaje;
                }
                if (employeeBean.sMensaje == "continue") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag          = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
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
            string[] rValidation = new string[2];
            int consecutive = 1;
            PuestosDao puestos = new PuestosDao();
            try {
                int keyemp  = int.Parse(Session["IdEmpresa"].ToString());
                rValidation = puestos.sp_Valida_Empresa_Codigo_Puesto(keyemp);
                consecutive = puestos.sp_Obtiene_Consecutivo_Codigo_Puesto(keyemp);
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
                string codePuesto = regcodpuesto;
                regcodpuesto = codeTypeJob + ceros + consecutiveCNew.ToString();
                if (rValidation[1] == "none") {
                    if (Convert.ToBoolean(rValidation[0]) == true) {
                        if (consecutive == Convert.ToInt32(codePuesto.Trim())) {
                            regcodpuesto = codePuesto.Trim();
                        } else {
                            regcodpuesto = consecutive.ToString();
                        }
                    }
                }
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
            PuestosBean puestos = new PuestosBean();
            PuestosDao puestosDao = new PuestosDao();
            puestos = puestosDao.sp_Puestos_Retrieve_Puesto(puesid);
            DatosPosicionesDao savePosicionDao = new DatosPosicionesDao();
            int usuario = Convert.ToInt32(Session["iIdUsuario"].ToString());
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            addPosicionBean = savePosicionDao.sp_Posiciones_Insert_Posicion(codposic, depaid, puesid, regpatcla, localityr, emprepreg, reportempr, usuario, keyemp);
            var data = new { result = addPosicionBean.sMensaje, Puesto = puestos.sNombrePuesto };
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
                addEmpleadoBean = empleadoDao.sp_Empleados_Insert_Empleado(name, apepat, apemat, sex, estciv, convertFNaci, lnaci, title, nacion, state, codpost, city, colony, street, numberst, telfij, telmov, email, usuario, empresa, tipsan, convertFMatr, 0);
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
        public JsonResult DataNomina(string fecefecnom, double salmen, int tipemp, int nivemp, int tipjor, int tipcon, string fecing, string fecant, string vencon, string empleado, string apepat, string apemat, string fechanaci, int tipper, int tipcontra, int tippag, int banuse, string cunuse, int position, int clvemp, int tiposueldo, int politica, double diferencia, double transporte, int retroactivo, Boolean flagSal, string motMoviSal, string fechMoviSal, double salmenact, int categoria, int pagopor, int fondo, double ultSdi, int clasif)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosNominaBean addDatoNomina = new DatosNominaBean();
            DatosNominaDao datoNominaDao  = new DatosNominaDao();
            LoadTypePeriodPayrollBean periodBean  = new LoadTypePeriodPayrollBean();
            LoadTypePeriodPayrollDaoD periodDaoD  = new LoadTypePeriodPayrollDaoD();
            DatosMovimientosBean datosMovimientos = new DatosMovimientosBean();
            DatosPosicionesDao datoPosicionDao    = new DatosPosicionesDao();
            string convertFEffdt = "";
            double diferenciaE = (diferencia < 1) ? 0.00 : diferencia;
            double transporteE = (transporte < 1) ? 0.00 : transporte;
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
                if (flagSal) {
                    periodBean = periodDaoD.sp_Load_Info_Periodo_Empr(keyemp, Convert.ToInt32(DateTime.Now.Year.ToString()));
                    datosMovimientos = datoPosicionDao.sp_Save_Data_History_Movements_Employee(clvemp, keyemp, "SUELDO", motMoviSal, salmen.ToString(), salmenact.ToString(), fechMoviSal, usuario, periodBean.iTipoPeriodo, periodBean.iPeriodo, periodBean.iAnio);
                }
                addDatoNomina = datoNominaDao.sp_DatosNomina_Insert_DatoNomina(convertFEffdt, salmen, tipemp, nivemp, tipjor, tipcon, convertFIngrs, convertFAcnti, convertFVenco, usuario, empleado, apepat, apemat, convertFNaciE, keyemp, tipper, tipcontra, tippag, banuse, cunuse, position, clvemp, tiposueldo, politica, diferenciaE, transporteE, retroactivo, categoria, pagopor, fondo, ultSdi, clasif);
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
                int keyemp      = int.Parse(Session["IdEmpresa"].ToString());
                int usuario     = Convert.ToInt32(Session["iIdUsuario"].ToString());
                addPosicionBean = datoPosicionDao.sp_PosicionesAsig_Insert_PosicionesAsig(clvstr, convertFEffdt, fechinipos, empleado, apepat, apemat, convertFNaciE, usuario, keyemp);
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            var data = new { result = addPosicionBean.sMensaje };
            return Json(data);
        }

        // Guarda los datos de la estructura al editar el empleado
        [HttpPost]
        public JsonResult DataEstructuraEdit(int clvstr,int clvact, string fechefectpos, string fechinipos, int clvemp, int clvnom, string fechmovi, string motmovi)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            string  convertFEffdt = Convert.ToDateTime(fechefectpos).ToString("dd/MM/yyyy");
            string  convertFIniP  = Convert.ToDateTime(fechinipos).ToString("dd/MM/yyyy");
            DatosPosicionesBean addPosicionBean   = new DatosPosicionesBean();
            DatosPosicionesDao datoPosicionDao    = new DatosPosicionesDao();
            LoadTypePeriodPayrollBean periodBean  = new LoadTypePeriodPayrollBean();
            LoadTypePeriodPayrollDaoD periodDaoD  = new LoadTypePeriodPayrollDaoD();
            DatosMovimientosBean datosMovimientos = new DatosMovimientosBean();
            try {
                int keyemp      = int.Parse(Session["IdEmpresa"].ToString());
                int usuario     = Convert.ToInt32(Session["iIdUsuario"].ToString());
                periodBean = periodDaoD.sp_Load_Info_Periodo_Empr(keyemp, Convert.ToInt32(DateTime.Now.Year.ToString()));
                addPosicionBean = datoPosicionDao.sp_PosicionesAsig_Insert_PosicionesAsigEdit(clvstr, convertFEffdt, convertFIniP, clvemp, clvnom, usuario, keyemp);
                datosMovimientos = datoPosicionDao.sp_Save_Data_History_Movements_Employee(clvemp, keyemp, "POSICION", motmovi, clvstr.ToString(), clvact.ToString(), fechmovi, usuario, periodBean.iTipoPeriodo, periodBean.iPeriodo, periodBean.iAnio);
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

        // Guarda los cambios aplicados al ultimo sdi
        [HttpPost]
        public JsonResult SaveUltSdi(int clvNom, double ultSdi, int keyEmployee)
        {
            Boolean flag = false;
            String  messageError = "none";
            DatosNominaBean datosNominaBean = new DatosNominaBean();
            DatosNominaDao datosNominaDao   = new DatosNominaDao();
            try {
                int keyBusiness = Convert.ToInt32(Session["IdEmpresa"].ToString());
                datosNominaBean = datosNominaDao.sp_Actualiza_Ult_Sdi(clvNom, ultSdi, keyBusiness, keyEmployee);
                if (datosNominaBean.sMensaje == "SUCCESS") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult SaveEditPosition(int newLocality, int position)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosPosicionesBean addPosicionBean = new DatosPosicionesBean();
            DatosPosicionesDao savePosicionDao  = new DatosPosicionesDao();
            try {
                int keyBusiness = Convert.ToInt32(Session["IdEmpresa"]);
                addPosicionBean = savePosicionDao.sp_Save_Edit_Position(position, newLocality, keyBusiness);
                if (addPosicionBean.sMensaje == "SUCCESS") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

    }

}