using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Payroll.Models.Beans;
using Payroll.Models.Daos;
using Payroll.Models.Utilerias;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using ExcelDataReader;
using System.Text.RegularExpressions;

namespace Payroll.Controllers
{
    public class MassiveUpsAndDownsController : Controller
    {

        // GET: MasiveLoads
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFileMasiveUpSalary(HttpPostedFileBase fileUpload, string typeFile)
        {
            Boolean flag = false;
            Boolean flagLog = false;
            String messageError = "none";
            string pathUploadFile = "";
            string nameBinderLogs = "LogFilesUploadSalary";
            string pathLogUploadFile = Server.MapPath("~/Content/" + nameBinderLogs);
            string nameLogUploadFile = "";
            string nameFileLogUploadFile = "";
            int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
            try {
                if (typeFile == "SALARIO") {
                    pathUploadFile = "FilesUpsSalary";
                    nameLogUploadFile = "LOG_FILE_UP_Salary";
                    Session["nameFileSalary" + keyUser.ToString()] = fileUpload.FileName;
                } else {
                    flag = false;
                }
                string pathComplete = Server.MapPath("~/Content/" + pathUploadFile + "/");
                if (!Directory.Exists(pathComplete)) {
                    Directory.CreateDirectory(pathComplete);
                }
                if (System.IO.File.Exists(pathComplete + @"\\" + fileUpload.FileName)) {
                    System.IO.File.Delete(pathComplete + @"\\" + fileUpload.FileName);
                }
                fileUpload.SaveAs(pathComplete + Path.GetFileName(fileUpload.FileName));
                flag = true;
            } catch (Exception exc) {
                if (!Directory.Exists(pathLogUploadFile)) {
                    Directory.CreateDirectory(pathLogUploadFile);
                }
                nameFileLogUploadFile = nameLogUploadFile + Path.GetFileNameWithoutExtension(fileUpload.FileName).ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                using (StreamWriter fileLog = new StreamWriter(pathLogUploadFile + @"\\" + nameFileLogUploadFile, false, Encoding.UTF8)) {
                    fileLog.Write("* -- FECHA: " + DateTime.Now.ToString() + " -- *\n");
                    fileLog.Write("* -- ARCHIVO: " + fileUpload.FileName + " -- *\n");
                    fileLog.Write("* -- DETALLE DEL ERROR -- */n");
                    fileLog.Write(exc.Message.ToString());
                    fileLog.Close();
                }
                flag = false;
                flagLog = true;
            }
            return Json(new { Bandera = flag, Log = flagLog, NombreLog = nameFileLogUploadFile, FolderLog = nameBinderLogs, MensajeError = messageError, ArchivoCarga = fileUpload.FileName, Llave = keyUser }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFileMasiveUpEmployees(HttpPostedFileBase fileUpload, string typeFile)
        {
            Boolean flag = false;
            Boolean flagLog = false;
            String messageError = "none";
            string pathUploadFile = "";
            string nameBinderLogs = "LogFilesUpload";
            string pathLogUploadFile = Server.MapPath("~/Content/" + nameBinderLogs);
            string nameLogUploadFile = "";
            string nameFileLogUploadFile = "";
            int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
            try
            {
                if (typeFile == "CARGA")
                {
                    pathUploadFile = "FilesUps";
                    nameLogUploadFile = "LOG_FILE_UP_";
                    Session["nameFileUp" + keyUser.ToString()] = fileUpload.FileName;
                }
                else if (typeFile == "BAJA")
                {
                    pathUploadFile = "FilesDowns";
                    nameLogUploadFile = "LOG_FILE_DOWN_";
                    Session["nameFileDown" + keyUser.ToString()] = fileUpload.FileName;
                }
                else
                {
                    flag = false;
                }
                string pathComplete = Server.MapPath("~/Content/" + pathUploadFile + "/");
                if (!Directory.Exists(pathComplete))
                {
                    Directory.CreateDirectory(pathComplete);
                }
                if (System.IO.File.Exists(pathComplete + @"\\" + fileUpload.FileName))
                {
                    System.IO.File.Delete(pathComplete + @"\\" + fileUpload.FileName);
                }
                fileUpload.SaveAs(pathComplete + Path.GetFileName(fileUpload.FileName));
                flag = true;
            }
            catch (Exception exc)
            {
                if (!Directory.Exists(pathLogUploadFile))
                {
                    Directory.CreateDirectory(pathLogUploadFile);
                }
                nameFileLogUploadFile = nameLogUploadFile + Path.GetFileNameWithoutExtension(fileUpload.FileName).ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                using (StreamWriter fileLog = new StreamWriter(pathLogUploadFile + @"\\" + nameFileLogUploadFile, false, Encoding.UTF8))
                {
                    fileLog.Write("* -- FECHA: " + DateTime.Now.ToString() + " -- *\n");
                    fileLog.Write("* -- ARCHIVO: " + fileUpload.FileName + " -- *\n");
                    fileLog.Write("* -- DETALLE DEL ERROR -- */n");
                    fileLog.Write(exc.Message.ToString());
                    fileLog.Close();
                }
                flag = false;
                flagLog = true;
            }
            return Json(new { Bandera = flag, Log = flagLog, NombreLog = nameFileLogUploadFile, FolderLog = nameBinderLogs, MensajeError = messageError, ArchivoCarga = fileUpload.FileName, Llave = keyUser }, JsonRequestBehavior.AllowGet);
        }

        public class ErrorDataLoadBean
        {
            public int iFilaError { get; set; }
            public string sEmpresa { get; set; }
            public string sErrores { get; set; }
        }

        public class CorrectDataInsertBean
        {
            public int iFilaInsert { get; set; }
            public string sEmpresa { get; set; }
            public string sNombre { get; set; }
            public string sNomina { get; set; }
        }

        public class ExceptionsBean
        {
            public string sTipo { get; set; }
            public string sMensaje { get; set; }
            public int iFilaExc { get; set; }
            public string sExcepcion { get; set; }
        }

        [HttpPost]
        public JsonResult InsertDataFileMasiveUpsSalary(string nameFile, int keyFile) 
        {
            Boolean flag = false;
            Boolean flagVE = false;
            Boolean flagSpreadSheet = false;
            Boolean flagCodeCorrect = false;
            Boolean flagSearch = false;
            Boolean flagInsert = false;
            Boolean flagSqlExc = false;
            String messageError = "none";
            String showMessageErVal = "";
            String nameFileSessionSalary = Session["nameFileSalary" + keyFile.ToString()].ToString();
            String pathUploadFile = "FilesUpsSalary";
            String pathLogsErVFile = "Logs_Validations";
            String nameFileLogMessage = "LOG_" + Path.GetFileNameWithoutExtension(nameFile) + DateTime.Now.ToString("dd-MM-yyy") + ".txt";
            String pathCompleteSearch = Server.MapPath("~/Content/" + pathUploadFile + "/");
            StringBuilder validationErMe = new StringBuilder("Errores de validacion: ");
            StringBuilder errMessage = new StringBuilder();
            int cantReg = 0;
            int rowReco = 0;
            int rowActu = 1;
            int rowInsert = 0;
            int rowErrVal = 0;
            int rowErrorInsert = 0;
            List<CatalogoGeneralBean> listTypeMovements = new List<CatalogoGeneralBean>();
            ListasAltasBajasMasivasDao listUpsAndDowns = new ListasAltasBajasMasivasDao();
            List<ErrorDataLoadBean> errorDataLoadBeans = new List<ErrorDataLoadBean>();
            List<ErroresLayoutBean> ValidacionesLayouts = new List<ErroresLayoutBean>();
            List<ExceptionsBean> exceptionsBeans = new List<ExceptionsBean>();

            StringBuilder msjValidacionInsert = new StringBuilder();
            try  {
                if (nameFileSessionSalary.Replace(" ","") == nameFile.Replace(" ", "")) {
                    flag = true;
                }
                if (flag) {
                    if (System.IO.File.Exists(pathCompleteSearch + nameFile)) {
                        flagSearch = true;
                    }
                }
                if (flagSearch) {
                    listTypeMovements = listUpsAndDowns.UpsAndDownsCatalogs(41, "Movimientos", 1);
                    using (var stream = System.IO.File.Open(pathCompleteSearch + nameFile, FileMode.Open, FileAccess.Read)) {
                        using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                            var result = reader.AsDataSet();
                            DataTable table = result.Tables[0];
                            DataRow row = table.Rows[0];
                            if (table.TableName == "IPSNet Salario") {
                                flagSpreadSheet = true;
                            }
                            if (row[1].ToString() == "DATA" && row[2].ToString() == "MassiveSalary") {
                                flagCodeCorrect = true;
                            }
                            cantReg = Convert.ToInt32(row[0].ToString());
                            if (flagSpreadSheet && flagCodeCorrect) {
                                foreach (DataRow dr in table.Rows) {
                                    if (dr[1].ToString().Trim() != "DATA") {
                                        rowReco += 1;
                                        if ((cantReg + 1) == rowReco) {
                                            break;
                                        }
                                        flagVE = false;
                                        rowActu += 1;
                                        /* VALIDACIONES */
                                        // EMPRESAS
                                        if (dr[3].ToString().Trim() == "" && Convert.ToInt32(dr[3].ToString().Trim()) == 0) {
                                            validationErMe.Append("[*] El campo EMPRESA " + dr[3].ToString().Trim() + " debe de contener un valor numerico y valido. ");
                                            flagVE = true;
                                        }
                                        // NUMERO NOMINA
                                        if (dr[4].ToString().Trim() == "" && Convert.ToInt32(dr[4].ToString().Trim()) == 0) {
                                            validationErMe.Append("[*] El campo NOMINA " + dr[4].ToString().Trim() + " debe de contener un valor numerico y valido. ");
                                            flagVE = true;
                                        }
                                        string tipoMov = dr[5].ToString().Trim();
                                        // TIPOS MOVIMIENTOS
                                        if (!listTypeMovements.Any(x => x.iId.ToString() == tipoMov)) {
                                            validationErMe.Append("[*] El valor ingresado " + dr[5].ToString() + " en la columna TIPO DE MOVIMIENTO no es valido. ");
                                            flagVE = true;
                                        }
                                        // SUELDO
                                        if (dr[6].ToString().Trim() == "") {
                                            validationErMe.Append("[*] El valor ingresado " + dr[6].ToString() + " en la columna NUEVO SALDO no es valido. ");
                                            flagVE = true;
                                        }
                                        // FECHA MOVIMIENTO
                                        if (dr[7].ToString().Trim() == "" && Convert.ToDateTime(dr[7].ToString().Trim()).ToString("dd/MM/yyyy").Length == 10) {
                                            validationErMe.Append("[*] El valor ingresado " + dr[7].ToString() + " en la columna FECHA MOVIMIENTO no es valido. ");
                                            flagVE = true;
                                        }

                                        // Si hay errores integramos los datos a la lista
                                        if (flagVE) {
                                            errorDataLoadBeans.Add(new ErrorDataLoadBean {
                                                iFilaError = rowActu,
                                                sEmpresa = dr[3].ToString().Trim(),
                                                sErrores = validationErMe.ToString().Trim()
                                            });
                                            validationErMe.Clear();
                                            validationErMe.Append("Errores de validacion: ");
                                            rowErrVal += 1;
                                            continue;
                                        }
                                        LoadTypePeriodPayrollBean periodBean = new LoadTypePeriodPayrollBean();
                                        LoadTypePeriodPayrollDaoD periodDaoD = new LoadTypePeriodPayrollDaoD();
                                        int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                                        periodBean = periodDaoD.sp_Load_Info_Periodo_Empr(keyBusiness,
                                                        Convert.ToInt32(DateTime.Now.Year.ToString().Trim())); 
                                        flag = (periodBean.sMensaje == "success") ? true : false;
                                        // VALORES EN VARIABLES
                                        int empresa = Convert.ToInt32(dr[3].ToString().Trim());
                                        int nomina = Convert.ToInt32(dr[4].ToString().Trim());
                                        int movimiento = Convert.ToInt32(dr[5].ToString().Trim());
                                        string sueldo =  dr[6].ToString().Trim().Replace(",","");
                                        string fechaMovimiento = Convert.ToDateTime(dr[7].ToString().Trim()).ToString("dd/MM/yyyy");
                                        int usuarioId = Convert.ToInt32(Session["iIdUsuario"].ToString().Trim());
                                        LayoutsDao layouts = new LayoutsDao();
                                        LayoutSalarioMasivoBean layoutSalario = new LayoutSalarioMasivoBean();
                                        layoutSalario = layouts.sp_Actualiza_Salario_Carga_Masiva(empresa, nomina, movimiento, sueldo, fechaMovimiento, usuarioId, periodBean.iTipoPeriodo, periodBean.iPeriodo, periodBean.iAnio);
                                        if (layoutSalario.sMensaje == "SUCCESS") {
                                            flagInsert = true;
                                            rowInsert += 1;
                                            string mensajeInAC = "";
                                            if (layoutSalario.iBanderaFecha == 1) {
                                                mensajeInAC = "Actualizado sobre la fecha efectiva indicada";
                                            } else {
                                                mensajeInAC = "Inserción nueva sobre la fecha efectiva indicada";
                                            }
                                            ValidacionesLayouts.Add(new ErroresLayoutBean { sNomina = nomina.ToString(), sEmpresa = empresa.ToString(), iFila = rowActu, sErroresInsercion = "none", sErroresValidacion = "none", sTipoInsercion = mensajeInAC , sMensaje = "SUCCESS"});
                                        } else {
                                            if (layoutSalario.iBandera1 == 0) {
                                                msjValidacionInsert.Append(" [*] No se encontro información coincidente con la nomina : " + nomina.ToString() + " de la empresa : " + empresa.ToString() + ".");
                                            }
                                            if (layoutSalario.iBandera2 == 0) {
                                                msjValidacionInsert.Append(" [*] Ocurrio un problema al insertar los datos de Nomina.");
                                            }
                                            if (layoutSalario.iBandera3 == 0) {
                                                msjValidacionInsert.Append(" [*] Ocurrio un problema al insertar los datos de Movimiento.");
                                            }
                                            ValidacionesLayouts.Add(new ErroresLayoutBean { sNomina = nomina.ToString(), sEmpresa = empresa.ToString(), iFila = rowActu, sErroresInsercion = msjValidacionInsert.ToString(), sErroresValidacion = layoutSalario.sMensaje, sMensaje = "ERROR" });
                                            rowErrorInsert += 1;
                                        }
                                        msjValidacionInsert.Clear();
                                    }
                                }
                            }
                        }
                    }
                    if (!Directory.Exists(pathCompleteSearch + pathLogsErVFile)) {
                        Directory.CreateDirectory(pathCompleteSearch + pathLogsErVFile);
                    }
                }
            } catch (SqlException sqlExc) {
                errMessage.Clear();
                for (int i = 0; i < sqlExc.Errors.Count; i++) {
                    errMessage.Append("Index #" + i + "\n" + "Mensaje: " + sqlExc.Errors[i].Message + "\n"
                        + "Numero de linea: " + sqlExc.Errors[i].LineNumber + "\n" + "Origen: " + sqlExc.Errors[i].Source + "\n" + "Procedimiento: " + sqlExc.Errors[i].Procedure + "\n");
                }
                exceptionsBeans.Add(new ExceptionsBean { sTipo = sqlExc.GetType().ToString(), sMensaje = errMessage.ToString(), iFilaExc = rowActu, sExcepcion = "SQL" });
                flagSqlExc = true;
            }  catch (Exception exc) {
                messageError = exc.Message.ToString();
                exceptionsBeans.Add(new ExceptionsBean { sTipo = exc.GetType().ToString(), sMensaje = messageError, iFilaExc = rowActu, sExcepcion = "General" });
                flag = false;
            }
            using (StreamWriter fileLog = new StreamWriter(pathCompleteSearch + pathLogsErVFile + @"\\" + nameFileLogMessage, false, Encoding.UTF8)) {
                if (ValidacionesLayouts.Count > 0) {
                    fileLog.Write("* -- Datos a insertar -- *\n");
                    foreach (ErroresLayoutBean data in ValidacionesLayouts) {
                        if (data.sMensaje == "SUCCESS") {
                            fileLog.Write("[*] Fila = " + data.iFila.ToString() + ", [*] Empresa = " + data.sEmpresa + ", [*] Nomina = " + data.sNomina + ", [*] MensajeError = " + data.sErroresValidacion + ", ValidacionInsercion = " + data.sTipoInsercion + "\n");
                        }
                    }
                    fileLog.Write("\n");
                }
                if (errorDataLoadBeans.Count > 0) {
                    fileLog.Write("* -- Errores de validaciones -- *\n");
                    foreach (ErroresLayoutBean data in ValidacionesLayouts) {
                        if (data.sMensaje == "ERROR") {
                            fileLog.Write("[*] Fila = " + data.iFila.ToString() + ", [*] Empresa = " + data.sEmpresa + ", [*] Nomina = " + data.sNomina + ", [*] MensajeError = " + data.sErroresValidacion + "\n");
                            rowErrVal += 1;
                        }
                    }
                    foreach (ErrorDataLoadBean data in errorDataLoadBeans) {
                        fileLog.Write("[*] Fila = " + data.iFilaError.ToString() + ", [*] Empresa = " + data.sEmpresa + ",  [*] Mensaje = " + data.sErrores + "\n");
                    }
                    fileLog.Write("\n");
                }
                if (exceptionsBeans.Count > 0) {
                    fileLog.Write("* -- Excepciones -- *");
                    foreach (ExceptionsBean data in exceptionsBeans) {
                        fileLog.Write("[*] Fila = " + data.iFilaExc.ToString() + ", [*] Excepcion = " + data.sExcepcion + ", [*] Tipo = " + data.sTipo + ", [*] Mensaje = " + data.sMensaje + "\n");
                    }
                    fileLog.Write("\n");
                }
                fileLog.Close();
            }
            Session.Remove(nameFileSessionSalary);
            return Json(new { Bandera = flag, BanderaInsercion = flagInsert, BanderaBusqueda = flagSearch, BanderaH = flagSpreadSheet, BanderaC = flagCodeCorrect, BanderaVE = flagVE, MensajeError = messageError, Sesion = nameFileSessionSalary, Errores = showMessageErVal, FolderLog = pathLogsErVFile, ArchivoLog = nameFileLogMessage, FilasOk = rowInsert, FilasEr = rowErrVal, FilasIn = cantReg });
        }

        [HttpPost]
        public JsonResult InsertDataFileMasiveUps(string nameFile, int keyFile)
        {
            Boolean flag = false;
            Boolean flagVE = false;
            Boolean flagSpreadSheet = false;
            Boolean flagCodeCorrect = false;
            Boolean flagSearch = false;
            Boolean flagInsert = false;
            Boolean flagSqlExc = false;
            String messageError = "none";
            String showMessageErVal = "";
            String nameFileSession = Session["nameFileUp" + keyFile.ToString()].ToString();
            String pathUploadFile = "FilesUps";
            String pathLogsErVFile = "Logs_Validations";
            String nameFileLogMessage = "LOG_" + Path.GetFileNameWithoutExtension(nameFile) + DateTime.Now.ToString("dd-MM-yyy") + ".txt";
            String pathCompleteSearch = Server.MapPath("~/Content/" + pathUploadFile + "/");
            StringBuilder validationErMe = new StringBuilder("Errores de validacion: ");
            StringBuilder errMessage = new StringBuilder();
            int cantReg = 0;
            int rowReco = 0;
            int rowActu = 1;
            int rowInsert = 0;
            int rowErrVal = 0;
            List<ErrorDataLoadBean> errorDataLoadBeans = new List<ErrorDataLoadBean>();
            List<CorrectDataInsertBean> correctDataInsertBeans = new List<CorrectDataInsertBean>();
            List<ExceptionsBean> exceptionsBeans = new List<ExceptionsBean>();
            EmpleadosBean empleadosBean = new EmpleadosBean();
            EmpleadosBean validaEmpleado = new EmpleadosBean();
            EmpleadosDao empleadosDao = new EmpleadosDao();
            ImssBean imssBean = new ImssBean();
            ImssDao imssDao = new ImssDao();
            DatosNominaBean datosNominaBean = new DatosNominaBean();
            DatosNominaDao datosNominaDao = new DatosNominaDao();
            InfoPositionInsert infoPositionInsert = new InfoPositionInsert();
            DatosPosicionesBean addPosicionBean = new DatosPosicionesBean();
            DatosPosicionesDao datoPosicionDao = new DatosPosicionesDao();
            List<CatalogoGeneralBean> listNationalities = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listGenereEmploye = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listStateCEmploye = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTittleEmploye = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listStatesAvailab = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listLevelOStudies = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listLevelSociEcon = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTypePeriodsAv = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTypeEmployees = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listLevelEmployee = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listDayTpEmployee = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTContrac1Empl = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTContrac2Empl = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTypePaymentEm = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listBankAvailable = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTypeSalary = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listTypePolitics = new List<CatalogoGeneralBean>();
            listTypePolitics.Add(new CatalogoGeneralBean { iId = 1 });
            listTypePolitics.Add(new CatalogoGeneralBean { iId = 2 });
            ListasAltasBajasMasivasDao listUpsAndDowns = new ListasAltasBajasMasivasDao();

            try {
                if (nameFileSession == nameFile) {
                    flag = true;
                }
                if (flag) {
                    if (System.IO.File.Exists(pathCompleteSearch + nameFile)) {
                        flagSearch = true;
                    }
                }
                if (flagSearch) {
                    // Listas de datos aceptables en la carga masiva
                    listNationalities = listUpsAndDowns.UpsAndDownsCatalogs(1, "Nacionalidades", 0);
                    listGenereEmploye = listUpsAndDowns.UpsAndDownsCatalogs(7, "Genero", 1);
                    listStateCEmploye = listUpsAndDowns.UpsAndDownsCatalogs(6, "EstadoC", 1);
                    listTittleEmploye = listUpsAndDowns.UpsAndDownsCatalogs(8, "Titulo", 1);
                    listStatesAvailab = listUpsAndDowns.UpsAndDownsCatalogs(1, "Estados", 1);
                    listLevelOStudies = listUpsAndDowns.UpsAndDownsCatalogs(20, "NivelE", 1);
                    listLevelSociEcon = listUpsAndDowns.UpsAndDownsCatalogs(10, "NivelS", 1);
                    listTypePeriodsAv = listUpsAndDowns.UpsAndDownsCatalogs(2, "TipoPe", 1);
                    listTypeEmployees = listUpsAndDowns.UpsAndDownsCatalogs(12, "TipoEm", 1);
                    listLevelEmployee = listUpsAndDowns.UpsAndDownsCatalogs(11, "NivelEm", 1);
                    listDayTpEmployee = listUpsAndDowns.UpsAndDownsCatalogs(13, "TipoJo", 1);
                    listTContrac1Empl = listUpsAndDowns.UpsAndDownsCatalogs(14, "TipoContrato", 1);
                    listTContrac2Empl = listUpsAndDowns.UpsAndDownsCatalogs(19, "TipoContratacion", 1);
                    listTypePaymentEm = listUpsAndDowns.UpsAndDownsCatalogs(22, "TipoPago", 1);
                    listBankAvailable = listUpsAndDowns.UpsAndDownsCatalogs(0, "Bancos", 0);
                    listTypeSalary = listUpsAndDowns.UpsAndDownsCatalogs(37, "TipoSalario", 1);
                    using (var stream = System.IO.File.Open(pathCompleteSearch + nameFile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            DataTable table = result.Tables[0];
                            DataRow row = table.Rows[0];
                            // Comprobamos que la hoja tenga el nombre correcto
                            if (table.TableName == "IPSNet Cargas")
                            {
                                flagSpreadSheet = true;
                            }
                            // Comprobamos que el codigo sea correcto
                            if (row[1].ToString() == "DATA" && row[2].ToString() == "MassiveLoads")
                            {
                                flagCodeCorrect = true;
                            }
                            cantReg = Convert.ToInt32(row[0].ToString());
                            if (flagSpreadSheet && flagCodeCorrect)
                            {
                                foreach (DataRow dr in table.Rows)
                                {
                                    if (dr[1].ToString().Trim() != "DATA")
                                    {
                                        rowReco += 1;
                                        if ((cantReg + 1) == rowReco)
                                        {
                                            break;
                                        }
                                        flagVE = false;
                                        rowActu += 1;
                                        // *  Validaciones TEmpleado * \\
                                        // Validamos la fecha en longitud
                                        if (dr[7].ToString().Trim().Length < 10)
                                        {
                                            validationErMe.Append("[*] La fecha " + dr[7].ToString().Trim() + " contiene una longitud menor a 10 caracteres. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor del titulo sea valido
                                        if (!listTittleEmploye.Any(x => x.iId == Convert.ToInt32(dr[9].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[9].ToString() + " en la columna titulo no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor del genero sea valido
                                        if (!listGenereEmploye.Any(x => x.iId == Convert.ToInt32(dr[10].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[10].ToString() + " en la columna genero no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor de la nacionalidad sea valido
                                        if (!listNationalities.Any(x => x.iId == Convert.ToInt32(dr[11].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[11].ToString() + " en la columna nacionalidad no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor del estado civil sea valido
                                        if (!listStateCEmploye.Any(x => x.iId == Convert.ToInt32(dr[12].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[12].ToString() + " en la columna estado civil no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos la longitud del codigo postal
                                        if (dr[13].ToString().Trim().Length < 5 || dr[13].ToString().Trim().Length > 5)
                                        {
                                            validationErMe.Append("[*] La longitud del codigo postal " + dr[13].ToString() + " no puede ser mayor ni menor a 5 caracteres. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor del estado sea valido 
                                        if (!listStatesAvailab.Any(x => x.iId == Convert.ToInt32(dr[14].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[14].ToString() + " en la columna estado id no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor de ciudad no venga vacío
                                        if (dr[15].ToString().Trim() == "" && dr[15].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] El valor de ciudad no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor de colonia no venga vacío
                                        if (dr[16].ToString().Trim() == "" && dr[16].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] El valor de colonia no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el valor de calle no venga vacío
                                        if (dr[17].ToString().Trim() == "" && dr[17].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] El valor de calle no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        // Validamos que la longitud de telefono fijo sea igual a 10 caracteres
                                        if (dr[19].ToString() != "")
                                        {
                                            if (dr[19].ToString().Trim().Length > 10 || dr[19].ToString().Trim().Length < 10)
                                            {
                                                validationErMe.Append("[*] La longitud del telefono fijo " + dr[19].ToString() + " no puede ser mayor ni menor a 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos que el valor de telfono movil no venga vacío
                                        if (dr[20].ToString().Trim() == "" && dr[20].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] El valor de telfono movil no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            if (dr[20].ToString().Trim().Length > 10 || dr[20].ToString().Trim().Length < 10)
                                            {
                                                validationErMe.Append("[*] El valor de telefono movil " + dr[20].ToString() + " no puede ser mayor ni menor a 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos que el correo electronico no venga vacío
                                        if (dr[21].ToString().Trim() == "" && dr[21].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] El valor de correo electronico no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        // Validamos que la fecha de matrimonio venga correcta si existe
                                        if (dr[22].ToString().Trim() != "")
                                        {
                                            if (dr[22].ToString().Trim().Length < 10 || dr[22].ToString().Trim().Length > 10)
                                            {
                                                validationErMe.Append("[*] La fecha de matrimonio " + dr[22].ToString() + " debe de contener una longitud de 10 caracteres");
                                                flagVE = true;
                                            }
                                        }
                                        // * Validaciones TEmpleado_imss * \\
                                        // Validamos que exista una fecha efectiva
                                        if (dr[24].ToString().Trim() == "" && dr[24].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] El valor de fecha efectiva imss no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            if (dr[24].ToString().Trim().Length > 10 || dr[24].ToString().Trim().Length < 10)
                                            {
                                                validationErMe.Append("[*] La fecha efectiva imss " + dr[24].ToString() + " debe de contener una longitud de 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos la longitud del registro imss
                                        if (dr[25].ToString().Trim().Length != 11)
                                        {
                                            validationErMe.Append("[*] El valor de Registro imss " + dr[25].ToString() + " debe de contener una longitud de 11 caracteres. ");
                                            flagVE = true;
                                        }
                                        // Validamos la longitud del rfc que sea menor a 14 caracteres
                                        if (dr[26].ToString().Trim().Length > 13)
                                        {
                                            validationErMe.Append("[*] El rfc " + dr[26].ToString() + " debe de contener una longitud menor a 14 caracteres. ");
                                            flagVE = true;
                                        }
                                        // Validamos la longitud del curp que sea menor a 19 caracteres
                                        if (dr[27].ToString().Trim().Length > 18 || dr[27].ToString().Trim().Length < 18)
                                        {
                                            validationErMe.Append("[*] La curp " + dr[27].ToString() + " debe de contener una longitud de 18 caracteres. ");
                                            flagVE = true;
                                        }
                                        // Validar que la curp sea valida
                                        // Definimos la expresion regular
                                        Regex regex = new Regex(@"[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$");
                                        if (dr[27].ToString().Trim().Length == 18)
                                        {
                                            if (!regex.IsMatch(dr[27].ToString().Trim()))
                                            {
                                                validationErMe.Append("[*] El formato de la curp " + dr[27].ToString() + " no es valido. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos que el nivel de estudios sea un nivel valido
                                        if (!listLevelOStudies.Any(x => x.iId == Convert.ToInt32(dr[28].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[28].ToString() + " en la columna nivel estudios no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el nivel socioeconomico sea un nivel valido
                                        if (!listLevelSociEcon.Any(x => x.iId == Convert.ToInt32(dr[29].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[29].ToString() + " en la columna nivel socioeconomico no es valido. ");
                                            flagVE = true;
                                        }
                                        // *  Validaciones TEmpleado_nomina * \\
                                        // Validamos la fecha efectiva de la nomina
                                        if (dr[30].ToString().Trim() == "" || dr[30].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] La fecha efectiva nomina no puede ir vacío, tiene que contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            if (dr[30].ToString().Trim().Length > 10 || dr[30].ToString().Trim().Length < 10)
                                            {
                                                validationErMe.Append("[*] La fecha efectiva nomina " + dr[30].ToString() + " debe de contener una longitud de 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos el tipo de periodo sea valido
                                        if (!listTypePeriodsAv.Any(x => x.iId == Convert.ToInt32(dr[32].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[32].ToString() + " en la columna tipo periodo no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos el tipo de empleado sea valido
                                        if (!listTypeEmployees.Any(x => x.iId == Convert.ToInt32(dr[33].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[33].ToString() + " en la columna tipo empleado no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos el nivel de empleado sea valido
                                        if (!listLevelEmployee.Any(x => x.iId == Convert.ToInt32(dr[34].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[34].ToString() + " en la columna nivel empleado no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos el tipo de jornada sea valido
                                        if (!listDayTpEmployee.Any(x => x.iId == Convert.ToInt32(dr[35].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[35].ToString() + " en la columna tipo jornada no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos el tipo de contato sea valido
                                        if (!listTContrac1Empl.Any(x => x.iId == Convert.ToInt32(dr[36].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[36].ToString() + " en la columna tipo contrato no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos el tipo de contratacion sea valido
                                        if (!listTContrac2Empl.Any(x => x.iId == Convert.ToInt32(dr[37].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[37].ToString() + " en la columna tipo contratacion no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que la fecha de ingreso
                                        if (dr[38].ToString().Trim() == "" || dr[38].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*] La fecha de ingreso no puede ir vacío, debe contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            if (dr[38].ToString().Trim().Length > 10 || dr[38].ToString().Trim().Length < 10)
                                            {
                                                validationErMe.Append("[*] La fecha de ingreso " + dr[38].ToString() + " debe contener 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos la fecha de antiguedad
                                        if (dr[39].ToString().Trim() == "" || dr[39].ToString().Trim().Length == 0)
                                        {
                                            validationErMe.Append("[*]  La fecha de antiguedad no puede ir vacío, debe contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            if (dr[39].ToString().Trim().Length > 10 || dr[39].ToString().Trim().Length < 10)
                                            {
                                                validationErMe.Append("[*] La fecha de antiguedad " + dr[39].ToString() + " debe contener 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos la fecha de vencimiento de contrato
                                        if (dr[40].ToString().Trim() != "" || dr[40].ToString().Trim().Length > 0)
                                        {
                                            if (dr[40].ToString().Length != 10)
                                            {
                                                validationErMe.Append("[*] La fecha de contrato " + dr[40].ToString() + " debe contener 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos los tipos de pago
                                        if (!listTypePaymentEm.Any(x => x.iId == Convert.ToInt32(dr[41].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[41].ToString() + " en la columna tipo de pago no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos el banco seleccionado
                                        if (!listBankAvailable.Any(x => x.iId == Convert.ToInt32(dr[42].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[42].ToString() + " en la columna banco no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos la longitud de la cuenta dependiendo el tipo de pago
                                        if (Convert.ToInt32(dr[41].ToString().Trim()) == 221)
                                        {
                                            if (dr[43].ToString().Length != 18)
                                            {
                                                validationErMe.Append("[*] La cuenta " + dr[43].ToString() + " debe contener 18 caracteres ya que se indico una cuenta de cheques clabe. ");
                                                flagVE = true;
                                            }
                                        }
                                        if (Convert.ToInt32(dr[41].ToString().Trim()) == 219)
                                        {
                                            if (dr[43].ToString().Length != 10)
                                            {
                                                validationErMe.Append("[*] La cuenta " + dr[43].ToString() + " debe contener 10 caracteres ya que se indico una cuenta de cheques. ");
                                                flagVE = true;
                                            }
                                        }
                                        Boolean flagContinue = true;
                                        // Validamos que la posicion no venga vacia
                                        if (dr[44].ToString().Trim().Length == 0 || dr[44].ToString().Trim() == "")
                                        {
                                            validationErMe.Append("[*] El valor de posicion id no puede ir vacío, debe contener un valor");
                                            flagVE = true;
                                            flagContinue = false;
                                        }
                                        // Definimos el valor de la empresa
                                        int empresa = Convert.ToInt32(dr[3].ToString().Trim());
                                        int posicionid = 0;
                                        if (flagContinue)
                                        {
                                            int posicionse = Convert.ToInt32(dr[44].ToString().Trim());
                                            // Validamos que la posicion exista y este disponible en la base de datos
                                            infoPositionInsert = datosNominaDao.sp_Valida_Posicion_Carga_Masiva(empresa, Convert.ToInt32(dr[44].ToString().Trim()));
                                            if (infoPositionInsert.iPosicion != 0 && infoPositionInsert.sMensaje == "SUCCESS")
                                            {
                                                posicionid = infoPositionInsert.iPosicion;
                                            }
                                            else
                                            {
                                                validationErMe.Append("[*] El codigo de posicion " + dr[44].ToString() + " ingresado no existe o no esta disponible. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos que el tipo de salario exista
                                        if (!listTypeSalary.Any(x => x.iId == Convert.ToInt32(dr[45].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[45].ToString() + " en la columna tipo salario no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que la politica indicada exista
                                        if (!listTypePolitics.Any(x => x.iId == Convert.ToInt32(dr[46].ToString().Trim())))
                                        {
                                            validationErMe.Append("[*] El valor ingresado " + dr[46].ToString() + " en la columna politica no es valido. ");
                                            flagVE = true;
                                        }
                                        // Validamos que la diferencia pactada no venga vacia
                                        if (dr[47].ToString().Trim() != "")
                                        {
                                            double df = 0;
                                            bool convertDiferencia = double.TryParse(dr[47].ToString().Trim(), out df);
                                            if (!convertDiferencia)
                                            {
                                                validationErMe.Append("[*] El valor ingresado " + dr[47].ToString() + " en la columna diferencia p debe de ser un valor decimal o entero. ");
                                                flagVE = true;
                                            }
                                        }
                                        else
                                        {
                                            validationErMe.Append("[*] El valor de diferencia p no puede ir vacío,  si no conoce el dato puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el transporte no venga vacio
                                        if (dr[48].ToString().Trim() != "")
                                        {
                                            double tr = 0;
                                            bool convertTransporte = double.TryParse(dr[48].ToString().Trim(), out tr);
                                            if (!convertTransporte)
                                            {
                                                validationErMe.Append("[*] El valor ingresado " + dr[48].ToString() + " en la columna transporte debe de ser un valor decimal o entero.");
                                                flagVE = true;
                                            }
                                        }
                                        else
                                        {
                                            validationErMe.Append("[*] El valor de transporte no puede ir vacío, si no conoce el dato puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el retroactivo no venga vacio 
                                        if (dr[49].ToString().Trim() != "")
                                        {
                                            Boolean flRet = false; ;
                                            if (dr[49].ToString().Trim() == "1" || dr[49].ToString().Trim() == "0")
                                            {
                                                flRet = true;
                                            }
                                            if (flRet == false)
                                            {
                                                validationErMe.Append("[*] El valor ingresado " + dr[49].ToString() + " en la columna retroactivo debe de ser un valor 1 o 0");
                                                flagVE = true;
                                            }
                                        }
                                        else
                                        {
                                            validationErMe.Append("[*] El valor de retroactivo no puede ir vacío, si no conoce el dato puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el numero de nomina no venga vacio
                                        if (dr[50].ToString().Trim() != "") {
                                            int payroll = 0;
                                            bool convertirNNomina = int.TryParse(dr[50].ToString().Trim(), out payroll);
                                            if (!convertirNNomina) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[50].ToString().Trim() 
                                                    + " debe de ser un valor entero . ");
                                                flagVE = true;
                                            } else {
                                                empleadosBean = empleadosDao.sp_Valida_Existencia_Numero_Nomina(
                                                    Convert.ToInt32(dr[50].ToString().Trim()), Convert.ToInt32(dr[3].ToString().Trim()));
                                                string msj = "";
                                                if (empleadosBean.sMensaje == "EXISTS") {
                                                    msj = "EXISTENTE";
                                                } else if (empleadosBean.sMensaje == "NOTDATA" || empleadosBean.sMensaje == "ERROR") {
                                                    msj = "ERROR";
                                                }
                                                if (empleadosBean.sMensaje != "NOTEXISTS") {
                                                    validationErMe.Append("[*] El numero de nomina " + dr[50].ToString().Trim() + " ya se encuentra ocupado, compruebe el siguiente mensaje = " + msj + " . ");
                                                    flagVE = true;
                                                }
                                            }
                                        } else {
                                            validationErMe.Append("[*] El valor de nomina no puede ir vacío, si no quiere indicar un número de nomina puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el clasif no venga vacio
                                        if (dr[51].ToString().Trim() != "") {
                                            int clasif = 0;
                                            bool convertirClasif = int.TryParse(dr[51].ToString().Trim(), out clasif);
                                            if (!convertirClasif) {
                                                validationErMe.Append("[*] El valor de CLASIF ingresado " + dr[51].ToString().Trim() + " debe de ser un valor entero. ");
                                                flagVE = true;
                                            }
                                        } else {
                                            validationErMe.Append("[*] El valor de CLASIF no puede ir vacío, si no conoce el dato puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que las prestaciones no venga vacio
                                        if (dr[52].ToString().Trim() != "") {
                                            int prestaciones = 0;
                                            bool convertirPrestaciones = int.TryParse(dr[52].ToString().Trim(), out prestaciones);
                                            if (!convertirPrestaciones)
                                            {
                                                validationErMe.Append("[*] El valor de Prestaciones ingresado " + dr[52].ToString().Trim() + " debe de ser un valor entero. ");
                                                flagVE = true;
                                            }
                                        } else {
                                            validationErMe.Append("[*] El valor de Prestaciones no puede ir vacío, si no conoce el dato puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que complemento especial no venga vacio
                                        if (dr[53].ToString().Trim() != "") {
                                            double complemento = 0;
                                            bool convertirComplemento = double.TryParse(dr[53].ToString().Trim(), out complemento);
                                            if (!convertirComplemento) {
                                                validationErMe.Append("[*] El valor de Complemento especial ingresado " + dr[53].ToString().Trim() + " debe de ser un valor entero o decimal. ");
                                                flagVE = true;
                                            }
                                        } else {
                                            validationErMe.Append("[*] El valor de Complemento especial no puede ir vacío, si no conoce el dato puede poner 0. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el empleado no exista
                                        validaEmpleado = empleadosDao.sp_Empleados_Validate_DatosImss(empresa, dr[27].ToString().Trim(), dr[26].ToString().Trim(), 0);
                                        if (validaEmpleado.sMensaje != "continue")
                                        {
                                            validationErMe.Append("[*] Los datos curp " + dr[27].ToString() + " o rfc " + dr[26].ToString() + " ya se encuentran registrados. ");
                                            flagVE = true;
                                        }
                                        // Si hay errores integramos los datos a la lista
                                        if (flagVE)
                                        {
                                            errorDataLoadBeans.Add(new ErrorDataLoadBean
                                            {
                                                iFilaError = rowActu,
                                                sEmpresa = dr[3].ToString().Trim(),
                                                sErrores = validationErMe.ToString().Trim()
                                            });
                                            validationErMe.Clear();
                                            validationErMe.Append("Errores de validacion: ");
                                            rowErrVal += 1;
                                            continue;
                                        }
                                        int filtroClasif = 0;
                                        if (dr[51].ToString() == "0") {
                                            filtroClasif = 368;
                                        } else if (dr[51].ToString() == "11") {
                                            filtroClasif = 369;
                                        } else if (dr[51].ToString() == "22") {
                                            filtroClasif = 370;
                                        } else if (dr[51].ToString() == "44") {
                                            filtroClasif = 371;
                                        } else if (dr[51].ToString() == "55") {
                                            filtroClasif = 372;
                                        } else {
                                            filtroClasif = 368;
                                        }
                                        int prestacionesSend = Convert.ToInt32(dr[52].ToString());
                                        double complementoSend = Convert.ToDouble(dr[53].ToString());
                                        // Variables, TEmpleado
                                        //int empresa = Convert.ToInt32(dr[3].ToString());
                                        int numeroNomina = Convert.ToInt32(dr[50].ToString().Trim());
                                        string nombre    = dr[4].ToString().ToUpper().Trim();
                                        string[] fechaNA = dr[7].ToString().Trim().Split('-');
                                        string paterno = dr[5].ToString().ToUpper().Trim();
                                        string materno = dr[6].ToString().ToUpper().Trim();
                                        string fechaNa = fechaNA[0] + "/" + fechaNA[1] + "/" + fechaNA[2];
                                        string lugarNa = dr[8].ToString().ToUpper().Trim();
                                        int titulo_id  = Convert.ToInt32(dr[9].ToString().Trim());
                                        int genero_id  = Convert.ToInt32(dr[10].ToString().Trim());
                                        int nacion_id  = Convert.ToInt32(dr[11].ToString().Trim());
                                        int estado_id  = Convert.ToInt32(dr[12].ToString().Trim());
                                        string codigop = dr[13].ToString().Trim();
                                        int estadod_id = Convert.ToInt32(dr[14].ToString().Trim());
                                        string ciudad  = dr[15].ToString().ToUpper().Trim();
                                        string colonia = dr[16].ToString().ToUpper().Trim();
                                        string calle   = dr[17].ToString().ToUpper().Trim();
                                        string numeroc = dr[18].ToString().Trim();
                                        string telefof = dr[19].ToString().Trim();
                                        string telefom = dr[20].ToString().Trim();
                                        string correoe = dr[21].ToString().Trim();
                                        string fechama = "";
                                        if (dr[22].ToString().Trim() != "") { 
                                            string[] fechaMA = dr[22].ToString().Trim().Split('-');
                                            fechama = fechaMA[0] + "/" + fechaMA[1] + "/" + fechaMA[2];
                                        }
                                        string tiposan = dr[23].ToString().Trim();
                                        int usuario_id = Convert.ToInt32(Session["iIdUsuario"].ToString().Trim());
                                        // Variables, TEmpleado_imss
                                        string fechaei = Convert.ToDateTime(dr[24].ToString()).ToString("dd/MM/yyyy").Trim();
                                        string regimss = dr[25].ToString().Trim();
                                        string rfcempl = dr[26].ToString().Trim();
                                        string curpemp = dr[27].ToString().Trim();
                                        int nivelestud = Convert.ToInt32(dr[28].ToString().Trim());
                                        int nivelsocio = Convert.ToInt32(dr[29].ToString().Trim());
                                        // Variables, TEmpleado_nomina
                                        string[] fechaEA = dr[30].ToString().Trim().Split('-');
                                        string fechaen = fechaEA[0] + "/" + fechaEA[1] + "/" + fechaEA[2];
                                        double salamen = Convert.ToDouble(dr[31].ToString().Trim());
                                        int tipoperiod = Convert.ToInt32(dr[32].ToString().Trim());
                                        int tipoemplea = Convert.ToInt32(dr[33].ToString().Trim());
                                        int nivelemple = Convert.ToInt32(dr[34].ToString().Trim());
                                        int tipojornad = Convert.ToInt32(dr[35].ToString().Trim());
                                        int tipocontra = Convert.ToInt32(dr[36].ToString().Trim());
                                        int tcontratac = Convert.ToInt32(dr[37].ToString().Trim());
                                        string feching = dr[38].ToString().Trim().Replace("-","/");
                                        string fechant = dr[39].ToString().Trim();
                                        string fechvco = dr[40].ToString().Trim();
                                        int tipopagoem = Convert.ToInt32(dr[41].ToString().Trim());
                                        int bancopagoe = Convert.ToInt32(dr[42].ToString().Trim());
                                        int tiposalario = Convert.ToInt32(dr[45].ToString().Trim());
                                        int politica = Convert.ToInt32(dr[46].ToString().Trim());
                                        double diferencia = Convert.ToDouble(dr[47].ToString().Trim());
                                        double transporte = Convert.ToDouble(dr[48].ToString().Trim());
                                        int retroactivo = Convert.ToInt32(dr[49].ToString().Trim());
                                        string cuentau = dr[43].ToString().Trim();
                                        double complementoEspecial = complementoSend;
                                        //int posicionid = Convert.ToInt32(dr[44].ToString());
                                        //Insertamos el registro en TEmpleado
                                        int nominaF = 0;
                                        if (numeroNomina != 0) {
                                            nominaF = numeroNomina;
                                        }
                                        empleadosBean = empleadosDao.sp_Empleados_Insert_Empleado(nombre, paterno, materno, genero_id, estado_id, fechaNa, lugarNa, titulo_id, nacion_id.ToString(), estadod_id, codigop, ciudad, colonia, calle, numeroc, telefof, telefom, correoe, usuario_id, empresa, tiposan, fechama, numeroNomina);
                                        // Insertamos el registro en TEmpleado_imss
                                        imssBean = imssDao.sp_Imss_Insert_Imss(fechaei, regimss, rfcempl, curpemp, nivelestud, nivelsocio, keyFile, nombre, paterno, materno, fechaNa, empresa, nominaF);
                                        // Insertamos el registro en TEmpleado_nomina
                                        datosNominaBean = datosNominaDao.sp_DatosNomina_Insert_DatoNomina(fechaen, salamen, tipoemplea, nivelemple, tipojornad, tipocontra, feching, fechant, fechvco, usuario_id, nombre, paterno, materno, fechaNa, empresa, tipoperiod, tcontratac, tipopagoem, bancopagoe, cuentau, posicionid, 0, tiposalario, politica, diferencia, transporte, retroactivo, 0, 0, 0, 0.00, filtroClasif, prestacionesSend, complementoEspecial);
                                        // Insertamos el registro en TPosiciones_asig
                                        addPosicionBean = datoPosicionDao.sp_PosicionesAsig_Insert_PosicionesAsig(posicionid, fechaen, fechaen, nombre, paterno, materno, fechaNa, usuario_id, empresa);
                                        // Validamos que los registros se hayan hecho correctamente
                                        if (empleadosBean.sMensaje == "success" && imssBean.sMensaje == "success" && datosNominaBean.sMensaje == "success" && addPosicionBean.sMensaje == "success")
                                        {
                                            correctDataInsertBeans.Add(new CorrectDataInsertBean { iFilaInsert = rowActu, sEmpresa = dr[3].ToString(), sNombre = nombre + " " + paterno + " " + materno, sNomina = numeroNomina.ToString() });
                                            flagInsert = true;
                                            rowInsert += 1;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    showMessageErVal = validationErMe.ToString();
                    if (!Directory.Exists(pathCompleteSearch + pathLogsErVFile))
                    {
                        Directory.CreateDirectory(pathCompleteSearch + pathLogsErVFile);
                    }
                }
            } catch (SqlException sqlExc) {
                errMessage.Clear();
                for (int i = 0; i < sqlExc.Errors.Count; i++) {
                    errMessage.Append("Index #" + i + "\n" + "Mensaje: " + sqlExc.Errors[i].Message + "\n"
                        + "Numero de linea: " + sqlExc.Errors[i].LineNumber + "\n" + "Origen: " + sqlExc.Errors[i].Source + "\n" + "Procedimiento: " + sqlExc.Errors[i].Procedure + "\n");
                }
                exceptionsBeans.Add(new ExceptionsBean { sTipo = sqlExc.GetType().ToString(), sMensaje = errMessage.ToString(), iFilaExc = rowActu, sExcepcion = "SQL" });
                flagSqlExc = true;
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
                exceptionsBeans.Add(new ExceptionsBean { sTipo = exc.GetType().ToString(), sMensaje = messageError, iFilaExc = rowActu, sExcepcion = "General" });
                flag = false;
            }
            using (StreamWriter fileLog = new StreamWriter(pathCompleteSearch + pathLogsErVFile + @"\\" + nameFileLogMessage, false, Encoding.UTF8)) {
                if (correctDataInsertBeans.Count > 0) {
                    fileLog.Write("* -- Datos insertados -- *\n");
                    foreach (CorrectDataInsertBean data in correctDataInsertBeans) {
                        fileLog.Write("[*] Fila = " + data.iFilaInsert.ToString() + ", [*] Empresa = " + data.sEmpresa + ", [*] Nombre = " + data.sNombre + ", [*] Nomina = " + data.sNomina + "\n");
                    }
                    fileLog.Write("\n");
                }
                if (errorDataLoadBeans.Count > 0)
                {
                    fileLog.Write("* -- Errores de validaciones -- *\n");
                    foreach (ErrorDataLoadBean data in errorDataLoadBeans)
                    {
                        fileLog.Write("[*] Fila = " + data.iFilaError.ToString() + ", [*] Empresa = " + data.sEmpresa + ",  [*] Mensaje = " + data.sErrores + "\n");
                    }
                    fileLog.Write("\n");
                }
                if (exceptionsBeans.Count > 0)
                {
                    fileLog.Write("* -- Excepciones -- *");
                    foreach (ExceptionsBean data in exceptionsBeans)
                    {
                        fileLog.Write("[*] Fila = " + data.iFilaExc.ToString() + ", [*] Excepcion = " + data.sExcepcion + ", [*] Tipo = " + data.sTipo + ", [*] Mensaje = " + data.sMensaje + "\n");
                    }
                    fileLog.Write("\n");
                }
                fileLog.Close();
            }
            Session.Remove(nameFileSession);
            return Json(new { Bandera = flag, BanderaInsercion = flagInsert, BanderaBusqueda = flagSearch, BanderaH = flagSpreadSheet, BanderaC = flagCodeCorrect, BanderaVE = flagVE, MensajeError = messageError, Sesion = nameFileSession, Errores = showMessageErVal, FolderLog = pathLogsErVFile, ArchivoLog = nameFileLogMessage, FilasOk = rowInsert, FilasEr = rowErrVal, FilasIn = cantReg });
        }


        [HttpPost]
        public JsonResult ReadDataFileMasiveDowns(string nameFile, int keyFile)
        {
            Boolean flag = false;
            Boolean flagVE = false;
            Boolean flagSpreadSheet = false;
            Boolean flagCodeCorrect = false;
            Boolean flagSearch = false;
            Boolean flagInsert = false;
            Boolean flagSqlExc = false;
            String messageError = "none";
            String showMessageErVal = "";
            String nameFileSession = Session["nameFileDown" + keyFile.ToString()].ToString();
            String pathUploadFile = "FilesDowns";
            String pathLogsErVFile = "Logs_Validations";
            String nameFileLogMessage = "LOG_" + Path.GetFileNameWithoutExtension(nameFile) + DateTime.Now.ToString("dd-MM-yyy") + ".txt";
            String pathCompleteSearch = Server.MapPath("~/Content/" + pathUploadFile + "/");
            StringBuilder validationErMe = new StringBuilder("Errores de validacion: ");
            StringBuilder errMessage = new StringBuilder();
            int cantReg = 0;
            int rowReco = 0;
            int rowActu = 1;
            int rowInsert = 0;
            int rowErrVal = 0;
            string dateStartPayment = "";
            string dateEndPayment = "";
            int yearAct = Convert.ToInt32(DateTime.Now.Year);
            List<ErrorDataLoadBean> errorDataLoadBeans = new List<ErrorDataLoadBean>();
            List<CorrectDataInsertBean> correctDataInsertBeans = new List<CorrectDataInsertBean>();
            List<ExceptionsBean> exceptionsBeans = new List<ExceptionsBean>();
            List<CatalogoGeneralBean> listTypesDownsEmployee = new List<CatalogoGeneralBean>();
            List<CatalogoGeneralBean> listMotiveDownEmployee = new List<CatalogoGeneralBean>();
            ListasAltasBajasMasivasDao listUpsAndDowns = new ListasAltasBajasMasivasDao();
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            BajasEmpleadosDaoD downEmployeeDaoD = new BajasEmpleadosDaoD();
            PeriodoActualBean periodActBean = new PeriodoActualBean();
            int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
            try
            {
                if (nameFileSession == nameFile)
                {
                    flag = true;
                }
                if (flag)
                {
                    if (System.IO.File.Exists(pathCompleteSearch + nameFile))
                    {
                        flagSearch = true;
                    }
                }
                if (flagSearch)
                {
                    listTypesDownsEmployee = listUpsAndDowns.UpsAndDownsCatalogs(12, "TipoBaja", 1);
                    listMotiveDownEmployee = listUpsAndDowns.UpsAndDownsCatalogs(29, "MotivoBaja", 1);
                    using (var stream = System.IO.File.Open(pathCompleteSearch + nameFile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            DataTable table = result.Tables[0];
                            DataRow row = table.Rows[0];
                            // Comprobamos que la hoja tenga el nombre correcto
                            if (table.TableName == "IPSNet Bajas")
                            {
                                flagSpreadSheet = true;
                            }
                            // Comprobamos que el codigo sea correcto
                            if (row[1].ToString() == "DATA" && row[2].ToString() == "MassiveDowns")
                            {
                                flagCodeCorrect = true;
                            }
                            cantReg = Convert.ToInt32(row[0].ToString().Trim());
                            if (flagSpreadSheet && flagCodeCorrect)
                            {
                                foreach (DataRow dr in table.Rows)
                                {
                                    if (dr[1].ToString().Trim() != "DATA")
                                    {
                                        rowReco += 1;
                                        if ((cantReg + 1) == rowReco)
                                        {
                                            break;
                                        }
                                        rowActu += 1;
                                        // Validamos que la empresa no venga vacio
                                        if (dr[3].ToString().Trim().Length == 0 || dr[3].ToString().Trim() == "")
                                        {
                                            validationErMe.Append("[*] El campo Empresa no puede ir vacío, debe contener un valor. ");
                                            flagVE = true;
                                        }
                                        // Validamos que la nomina no venga vacio
                                        if (dr[4].ToString().Trim().Length == 0 || dr[4].ToString().Trim() == "")
                                        {
                                            validationErMe.Append("[*] El campo Nomina no puede ir vacío, debe contener un valor. ");
                                            flagVE = true;
                                        }
                                        // Validamos que el tipo de baja no venga vacio y que sea valido
                                        if (dr[5].ToString().Trim().Length == 0 || dr[5].ToString().Trim() == "")
                                        {
                                            validationErMe.Append("[*] El campo Tipo de baja no puede ir vacío, debe contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            // Validamos que el tipo de baja sea valido
                                            if (!listTypesDownsEmployee.Any(x => x.iId == Convert.ToInt32(dr[5].ToString().Trim())))
                                            {
                                                validationErMe.Append("[*] El valor ingresado " + dr[5].ToString() + " en la columna tipo de baja no es valido. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos que el Motivo de baja no venga vacio y que sea valido
                                        if (dr[6].ToString().Trim().Length == 0 || dr[6].ToString().Trim() == "")
                                        {
                                            validationErMe.Append("[*] El campo Motivo de baja no puede ir vacío, debe contener un valor");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            // Validamos que el motivo de baja sea valido
                                            if (!listMotiveDownEmployee.Any(x => x.iId == Convert.ToInt32(dr[6].ToString().Trim())))
                                            {
                                                validationErMe.Append("[*] El valor ingresado " + dr[6].ToString() + " en la columna motivo de baja no es valido. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Validamos que la Fecha de baja no venga vacio y que sea valida
                                        if (dr[7].ToString().Trim().Length == 0 || dr[6].ToString().Trim() == "")
                                        {
                                            validationErMe.Append("[*] El campo Fecha de baja no puede ir vacío, debe contener un valor. ");
                                            flagVE = true;
                                        }
                                        else
                                        {
                                            if (dr[7].ToString().Length != 10)
                                            {
                                                validationErMe.Append("[*] La fecha de baja " + dr[7].ToString() + " debe contener 10 caracteres. ");
                                                flagVE = true;
                                            }
                                        }
                                        // Si hay errores integramos los datos a la lista
                                        if (flagVE)
                                        {
                                            errorDataLoadBeans.Add(new ErrorDataLoadBean
                                            {
                                                iFilaError = rowActu,
                                                sEmpresa = dr[3].ToString() + ". Nomina = " + dr[4].ToString().Trim(),
                                                sErrores = validationErMe.ToString()
                                            });
                                            validationErMe.Clear();
                                            validationErMe.Append("Errores de validacion: ");
                                            rowErrVal += 1;
                                            continue;
                                        }
                                        // Guardamos los valores en variables
                                        int empresa = Convert.ToInt32(dr[3].ToString().Trim());
                                        int empleado = Convert.ToInt32(dr[4].ToString().Trim());
                                        int tipobaja = Convert.ToInt32(dr[5].ToString().Trim());
                                        int motivobj = Convert.ToInt32(dr[6].ToString().Trim());
                                        string fechabaja = Convert.ToDateTime(dr[7].ToString().Trim()).ToString("dd/MM/yyyy");
                                        // Insertamos el registro en finiquitos sin calculo
                                        int keyPeriodAct = 0;
                                        periodActBean = downEmployeeDaoD.sp_Load_Info_Periodo_Empr(empresa, yearAct);
                                        if (periodActBean.sMensaje == "success")
                                        {
                                            keyPeriodAct = periodActBean.iPeriodo;
                                            yearAct = periodActBean.iAnio;
                                            dateStartPayment = periodActBean.sFecha_Inicio;
                                            dateEndPayment = periodActBean.sFecha_Final;
                                        }
                                        downEmployeeBean = downEmployeeDaoD.sp_Crea_Baja_Sin_Baja_Calculos(empresa, empleado, fechabaja, tipobaja, motivobj, yearAct, keyPeriodAct, keyUser);
                                        if (downEmployeeBean.sMensaje == "SUCCESS")
                                        {
                                            downEmployeeBean = downEmployeeDaoD.sp_BajaEmpleado_Update_EmpleadoNomina(empleado, empresa, tipobaja, fechabaja);
                                            if (downEmployeeBean.sMensaje == "SUCCESSUPD")
                                            {
                                                correctDataInsertBeans.Add(new CorrectDataInsertBean { iFilaInsert = rowActu, sEmpresa = dr[3].ToString().Trim(), sNombre = empleado.ToString().Trim() });
                                                flagInsert = true;
                                                rowInsert += 1;
                                            }
                                            else
                                            {
                                                errorDataLoadBeans.Add(new ErrorDataLoadBean
                                                {
                                                    iFilaError = rowActu,
                                                    sEmpresa = dr[3].ToString().Trim() + ". Nomina = " + dr[4].ToString().Trim(),
                                                    sErrores = "Error al actualizar el tipo de empleado. "
                                                });
                                            }
                                        }
                                        else
                                        {
                                            errorDataLoadBeans.Add(new ErrorDataLoadBean
                                            {
                                                iFilaError = rowActu,
                                                sEmpresa = dr[3].ToString().Trim() + ". Nomina = " + dr[4].ToString().Trim(),
                                                sErrores = "Error al insertar la baja sin calculos. "
                                            });
                                        }
                                    }
                                }

                            }
                        }
                    }
                    showMessageErVal = validationErMe.ToString();
                    if (!Directory.Exists(pathCompleteSearch + pathLogsErVFile))
                    {
                        Directory.CreateDirectory(pathCompleteSearch + pathLogsErVFile);
                    }
                }
            }
            catch (SqlException sqlExc)
            {
                errMessage.Clear();
                for (int i = 0; i < sqlExc.Errors.Count; i++)
                {
                    errMessage.Append("Index #" + i + "\n" + "Mensaje: " + sqlExc.Errors[i].Message + "\n"
                        + "Numero de linea: " + sqlExc.Errors[i].LineNumber + "\n" + "Origen: " + sqlExc.Errors[i].Source + "\n" + "Procedimiento: " + sqlExc.Errors[i].Procedure + "\n");
                }
                exceptionsBeans.Add(new ExceptionsBean { sTipo = sqlExc.GetType().ToString(), sMensaje = errMessage.ToString(), iFilaExc = rowActu, sExcepcion = "SQL" });
                flagSqlExc = true;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
                exceptionsBeans.Add(new ExceptionsBean { sTipo = exc.GetType().ToString(), sMensaje = messageError, iFilaExc = rowActu, sExcepcion = "General" });
                flag = false;
            }
            using (StreamWriter fileLog = new StreamWriter(pathCompleteSearch + pathLogsErVFile + @"\\" + nameFileLogMessage, false, Encoding.UTF8))
            {
                if (correctDataInsertBeans.Count > 0)
                {
                    fileLog.Write("* -- Bajas correctas -- *\n");
                    foreach (CorrectDataInsertBean data in correctDataInsertBeans)
                    {
                        fileLog.Write("[*] Fila = " + data.iFilaInsert.ToString() + ", [*] Empresa = " + data.sEmpresa + ", [*] Nomina = " + data.sNombre + "\n");
                    }
                    fileLog.Write("\n");
                }
                if (errorDataLoadBeans.Count > 0)
                {
                    fileLog.Write("* -- Errores de validaciones -- *\n");
                    foreach (ErrorDataLoadBean data in errorDataLoadBeans)
                    {
                        fileLog.Write("[*] Fila = " + data.iFilaError.ToString() + ", [*] Empresa = " + data.sEmpresa + ",  [*] Mensaje = " + data.sErrores + "\n");
                    }
                    fileLog.Write("\n");
                }
                if (exceptionsBeans.Count > 0)
                {
                    fileLog.Write("* -- Excepciones -- *");
                    foreach (ExceptionsBean data in exceptionsBeans)
                    {
                        fileLog.Write("[*] Fila = " + data.iFilaExc.ToString() + ", [*] Excepcion = " + data.sExcepcion + ", [*] Tipo = " + data.sTipo + ", [*] Mensaje = " + data.sMensaje + "\n");
                    }
                    fileLog.Write("\n");
                }
                fileLog.Close();
            }
            Session.Remove(nameFileSession);
            return Json(new { Bandera = flag, BanderaInsercion = flagInsert, BanderaBusqueda = flagSearch, BanderaH = flagSpreadSheet, BanderaC = flagCodeCorrect, BanderaVE = flagVE, MensajeError = messageError, Sesion = nameFileSession, Errores = showMessageErVal, FolderLog = pathLogsErVFile, ArchivoLog = nameFileLogMessage, FilasOk = rowInsert, FilasEr = rowErrVal, FilasIn = cantReg });
        }

    }
}