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
        public ActionResult UploadFileMasiveUpEmployees(HttpPostedFileBase fileUpload, string typeFile)
        {
            Boolean flag        = false;
            Boolean flagLog     = false;
            String messageError = "none";
            string pathUploadFile    = "";
            string nameBinderLogs    = "LogFilesUpload";
            string pathLogUploadFile = Server.MapPath("~/Content/" + nameBinderLogs);
            string nameLogUploadFile = "";
            string nameFileLogUploadFile = "";
            int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
            try {
                if (typeFile == "CARGA") {
                    pathUploadFile    = "FilesUps";
                    nameLogUploadFile = "LOG_FILE_UP_";
                    Session["nameFileUp" + keyUser.ToString()] = fileUpload.FileName;
                } else if (typeFile == "BAJA") {
                    pathUploadFile    = "FilesDownS";
                    nameLogUploadFile = "LOG_FILE_DOWN_";
                    Session["nameFileDown" + keyUser.ToString()] = fileUpload.FileName;
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
                flag         = false;
                flagLog      = true;
            }
            return Json(new { Bandera = flag, Log = flagLog, NombreLog = nameFileLogUploadFile, FolderLog = nameBinderLogs, MensajeError = messageError, ArchivoCarga = fileUpload.FileName, Llave = keyUser }, JsonRequestBehavior.AllowGet) ;
        }

        public class ErrorDataLoadBean {
            public int iFilaError  { get; set; }
            public string sEmpresa { get; set; }
            public string sErrores { get; set; }
        }

        public class CorrectDataInsertBean
        {
            public int iFilaInsert { get; set; }
            public string sEmpresa { get; set; }
            public string sNombre { get; set; }
        }

        [HttpPost]
        public JsonResult InsertDataFileMasiveUps(string nameFile, int keyFile)
        {
            Boolean flag   = false;
            Boolean flagVE = false;
            Boolean flagSpreadSheet = false;
            Boolean flagCodeCorrect = false;
            Boolean flagSearch   = false;
            Boolean flagInsert   = false;
            String  messageError = "none";
            String showMessageErVal = "";
            String  nameFileSession = Session["nameFileUp" + keyFile.ToString()].ToString();
            String  pathUploadFile  = "FilesUps";
            String  pathLogsErVFile = "Logs_Validations";
            String nameFileLogMessage = "LOG_" + Path.GetFileNameWithoutExtension(nameFile) + DateTime.Now.ToString("dd-MM-yyy") + ".txt";
            String  pathCompleteSearch   = Server.MapPath("~/Content/" + pathUploadFile + "/");
            StringBuilder validationErMe = new StringBuilder("Errores de validacion: ");
            int cantReg = 0;
            int rowReco = 0;
            int rowActu = 1;
            List<ErrorDataLoadBean> errorDataLoadBeans         = new List<ErrorDataLoadBean>();
            List<CorrectDataInsertBean> correctDataInsertBeans = new List<CorrectDataInsertBean>();
            EmpleadosBean empleadosBean = new EmpleadosBean();
            EmpleadosDao empleadosDao   = new EmpleadosDao();
            ImssBean imssBean = new ImssBean();
            ImssDao  imssDao  = new ImssDao();
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
                    int[] listNationalityEmp = new int[] {484};
                    int[] listGenereEmployee = new int[] {55, 56};
                    int[] listStateCEmployee = new int[] {50, 51, 52, 53, 54};
                    int[] listTitleEmployee  = new int[] {57, 58, 59, 60, 61, 62, 63, 64, 180};
                    int[] listStatesAviables = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };
                    int[] listLevelOfStudies = new int[] {181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199 };
                    int[] listLevelSocioecon = new int[] {69, 70, 71};
                    int[] listTypePeriod     = new int[] {0, 2, 3};
                    int[] listTypeEmployee   = new int[] {75, 76, 77, 155, 156};
                    int[] listLevelEmployee  = new int[] {72, 73, 74};
                    int[] listDayType        = new int[] {80, 81, 82, 83};
                    int[] listContractType   = new int[] {89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99};
                    int[] listContractType2  = new int[] {140, 141, 142, 150, 283};
                    int[] listPaymentType    = new int[] {218, 219, 220, 221};
                    int[] listOptionsBanks   = new int[] {0, 2,6,9,12,14,19,21,30,32,36,37,42,44,58,59,60,62,72,102,103,106,108,110,112,113,116,124,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,143,145,166,168,600,601,602,605,606,607,608,610,614,615,616,617,618,619,620,621,622,623,626,627,628,629,630,631,632,633,634,636,637,638,640,642,646,647,648,649,651, 652,653,655,656,659,670,901,902,999};
                    using (var stream = System.IO.File.Open(pathCompleteSearch + nameFile, FileMode.Open, FileAccess.Read)) {
                        using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                            var result      = reader.AsDataSet(); 
                            DataTable table = result.Tables[0];
                            DataRow   row   = table.Rows[0];
                            // Comprobamos que la hoja tenga el nombre correcto
                            if (table.TableName == "IPSNet Cargas") {
                                flagSpreadSheet = true;
                            }
                            // Comprobamos que el codigo sea correcto
                            if (row[1].ToString() == "DATA" && row[2].ToString() == "MassiveLoads") {
                                flagCodeCorrect = true;
                            }
                            cantReg = Convert.ToInt32(row[0].ToString());
                            if (flagSpreadSheet && flagCodeCorrect) {
                                string Server = "201.149.34.185,15002";
                                string Db = "IPSNet_original";
                                string User = "IPSNet";
                                string Pass = "IPSNet2";
                                string conexionDB = "Data Source=" + Server + ";Initial Catalog=" + Db + ";User ID=" + User + ";Password=" + Pass + ";Integrated Security=False;MultipleActiveResultSets=True";
                                using (SqlConnection con = new SqlConnection(conexionDB)) {
                                    foreach (DataRow dr in table.Rows) {
                                        if (dr[1].ToString().Trim() != "DATA") {
                                            rowReco += 1;
                                            if ((cantReg + 1) == rowReco) {
                                                break;
                                            }
                                            rowActu += 1;
                                            // *  Validaciones TEmpleado * \\
                                            // Validamos la fecha en longitud
                                            if (dr[7].ToString().Length < 10) {
                                                validationErMe.Append("[*] La fecha " + dr[7].ToString() + " contiene una longitud menor a 10 caracteres. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor del titulo sea valido
                                            if (!listTitleEmployee.Any(x => x == Convert.ToInt32(dr[9]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[9].ToString() + " en la columna titulo no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor del genero sea valido
                                            if (!listGenereEmployee.Any(x => x == Convert.ToInt32(dr[10]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[10].ToString() + " en la columna genero no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor de la nacionalidad sea valido
                                            if (!listNationalityEmp.Any(x => x == Convert.ToInt32(dr[11]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[11].ToString() + " en la columna nacionalidad no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor del estado civil sea valido
                                            if (!listStateCEmployee.Any(x => x == Convert.ToInt32(dr[12]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[12].ToString() + " en la columna estado civil no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos la longitud del codigo postal
                                            if (dr[13].ToString().Length < 5 || dr[13].ToString().Length > 5) {
                                                validationErMe.Append("[*] La longitud del codigo postal " + dr[13].ToString() + " no puede ser mayor ni menor a 5 caracteres. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor del estado sea valido 
                                            if (!listStatesAviables.Any(x => x == Convert.ToInt32(dr[14]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[14].ToString() + " en la columna estado id no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor de ciudad no venga vacío
                                            if (dr[15].ToString() == "" && dr[15].ToString().Length == 0) {
                                                validationErMe.Append("[*] El valor de ciudad no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor de colonia no venga vacío
                                            if (dr[16].ToString() == "" && dr[16].ToString().Length == 0) {
                                                validationErMe.Append("[*] El valor de colonia no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el valor de calle no venga vacío
                                            if (dr[17].ToString() == "" && dr[17].ToString().Length == 0) {
                                                validationErMe.Append("[*] El valor de calle no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            }
                                            // Validamos que la longitud de telefono fijo sea igual a 10 caracteres
                                            if (dr[19].ToString() != "") {
                                                if (dr[19].ToString().Length > 10 || dr[19].ToString().Length < 10) {
                                                    validationErMe.Append("[*] La longitud del telefono fijo " + dr[19].ToString() + " no puede ser mayor ni menor a 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos que el valor de telfono movil no venga vacío
                                            if (dr[20].ToString() == "" && dr[20].ToString().Length == 0) {
                                                validationErMe.Append("[*] El valor de telfono movil no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            } else {
                                                if (dr[20].ToString().Length > 10 || dr[20].ToString().Length < 10) {
                                                    validationErMe.Append("[*] El valor de telefono movil " + dr[20].ToString() + " no puede ser mayor ni menor a 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos que el correo electronico no venga vacío
                                            if (dr[21].ToString() == "" && dr[21].ToString().Length == 0) {
                                                validationErMe.Append("[*] El valor de correo electronico no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            }
                                            // Validamos que la fecha de matrimonio venga correcta si existe
                                            if (dr[22].ToString() != "") {
                                                if (dr[22].ToString().Length < 10 || dr[22].ToString().Length > 10) {
                                                    validationErMe.Append("[*] La fecha de matrimonio " + dr[22].ToString() + " debe de contener una longitud de 10 caracteres");
                                                    flagVE = true;
                                                }
                                            }
                                            // * Validaciones TEmpleado_imss * \\
                                            // Validamos que exista una fecha efectiva
                                            if (dr[24].ToString() == "" && dr[24].ToString().Length == 0) {
                                                validationErMe.Append("[*] El valor de fecha efectiva imss no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            } else {
                                                if (dr[24].ToString().Length > 10 || dr[24].ToString().Length < 10) {
                                                    validationErMe.Append("[*] La fecha efectiva imss " + dr[24].ToString() + " debe de contener una longitud de 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos la longitud del registro imss
                                            if (dr[25].ToString().Length != 11) {
                                                validationErMe.Append("[*] El valor de Registro imss " + dr[25].ToString() + " debe de contener una longitud de 11 caracteres. ");
                                                flagVE = true;
                                            }
                                            // Validamos la longitud del rfc que sea menor a 14 caracteres
                                            if (dr[26].ToString().Length > 13) {
                                                validationErMe.Append("[*] El rfc " + dr[26].ToString() + " debe de contener una longitud menor a 14 caracteres. ");
                                                flagVE = true;
                                            }
                                            // Validamos la longitud del curp que sea menor a 19 caracteres
                                            if (dr[27].ToString().Length > 18 || dr[27].ToString().Length < 18) {
                                                validationErMe.Append("[*] La curp " + dr[27].ToString() + " debe de contener una longitud de 18 caracteres. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el nivel de estudios sea un nivel valido
                                            if (!listLevelOfStudies.Any(x => x == Convert.ToInt32(dr[28]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[28].ToString() + " en la columna nivel estudios no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos que el nivel socioeconomico sea un nivel valido
                                            if (!listLevelSocioecon.Any(x => x == Convert.ToInt32(dr[29]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[29].ToString() + " en la columna nivel socioeconomico no es valido. ");
                                                flagVE = true;
                                            }
                                            // *  Validaciones TEmpleado_nomina * \\
                                            // Validamos la fecha efectiva de la nomina
                                            if (dr[30].ToString() == "" || dr[30].ToString().Length == 0) {
                                                validationErMe.Append("[*] La fecha efectiva nomina no puede ir vacío, tiene que contener un valor. ");
                                                flagVE = true;
                                            } else { 
                                                if (dr[30].ToString().Length > 10 || dr[30].ToString().Length < 10) {
                                                    validationErMe.Append("[*] La fecha efectiva nomina " + dr[30].ToString() + " debe de contener una longitud de 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos el tipo de periodo sea valido
                                            if (!listTypePeriod.Any(x => x == Convert.ToInt32(dr[32]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[32].ToString() + " en la columna tipo periodo no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos el tipo de empleado sea valido
                                            if (!listTypeEmployee.Any(x => x == Convert.ToInt32(dr[33]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[33].ToString() + " en la columna tipo empleado no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos el nivel de empleado sea valido
                                            if (!listLevelEmployee.Any(x => x == Convert.ToInt32(dr[34]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[34].ToString() + " en la columna nivel empleado no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos el tipo de jornada sea valido
                                            if (!listDayType.Any(x => x == Convert.ToInt32(dr[35]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[35].ToString() + " en la columna tipo jornada no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos el tipo de contato sea valido
                                            if (!listContractType.Any(x => x == Convert.ToInt32(dr[36]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[36].ToString() + " en la columna tipo contrato no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos el tipo de contratacion sea valido
                                            if (!listContractType2.Any(x => x == Convert.ToInt32(dr[37]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[37].ToString() + " en la columna tipo contratacion no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos que la fecha de ingreso
                                            if (dr[38].ToString() == "" || dr[38].ToString().Length == 0) {
                                                validationErMe.Append("[*] La fecha de ingreso no puede ir vacío, debe contener un valor. ");
                                                flagVE = true;
                                            } else {
                                                if (dr[38].ToString().Length > 10 || dr[38].ToString().Length < 10) {
                                                    validationErMe.Append("[*] La fecha de ingreso " + dr[38].ToString() + " debe contener 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos la fecha de antiguedad
                                            if (dr[39].ToString() == "" || dr[39].ToString().Length == 0) {
                                                validationErMe.Append("[*]  La fecha de antiguedad no puede ir vacío, debe contener un valor. ");
                                                flagVE = true; 
                                            } else {
                                                if (dr[39].ToString().Length > 10 || dr[39].ToString().Length < 10) {
                                                    validationErMe.Append("[*] La fecha de antiguedad " + dr[39].ToString() + " debe contener 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos la fecha de vencimiento de contrato
                                            if (dr[40].ToString() != "" || dr[40].ToString().Length > 0) {
                                                if (dr[40].ToString().Length != 10) {
                                                    validationErMe.Append("[*] La fecha de contrato " + dr[40].ToString() + " debe contener 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Validamos los tipos de pago
                                            if (!listPaymentType.Any(x => x == Convert.ToInt32(dr[41]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[41].ToString() + " en la columna tipo de pago no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos el banco seleccionado
                                            if (!listOptionsBanks.Any(x => x == Convert.ToInt32(dr[42]))) {
                                                validationErMe.Append("[*] El valor ingresado " + dr[42].ToString() + " en la columna banco no es valido. ");
                                                flagVE = true;
                                            }
                                            // Validamos la longitud de la cuenta dependiendo el tipo de pago
                                            if (Convert.ToInt32(dr[41].ToString()) == 221) {
                                                if (dr[43].ToString().Length != 18) {
                                                    validationErMe.Append("[*] La cuenta " + dr[43].ToString() + " debe contener 18 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            if (Convert.ToInt32(dr[41].ToString()) == 219) {
                                                if (dr[43].ToString().Length != 10) {
                                                    validationErMe.Append("[*] La cuenta " + dr[43].ToString() + " debe contener 10 caracteres. ");
                                                    flagVE = true;
                                                }
                                            }
                                            // Si hay errores integramos los datos a la lista
                                            if (flagVE) {
                                                errorDataLoadBeans.Add(new ErrorDataLoadBean { 
                                                    iFilaError = rowActu, sEmpresa = dr[3].ToString(), sErrores = validationErMe.ToString()
                                                });
                                                validationErMe.Clear();
                                                validationErMe.Append("Errores de validacion: ");
                                                continue;
                                            }
                                            // Variables, TEmpleado
                                            int empresa    = Convert.ToInt32(dr[3].ToString());
                                            string nombre  = dr[4].ToString().ToUpper();
                                            string paterno = dr[5].ToString().ToUpper();
                                            string materno = dr[6].ToString().ToUpper();
                                            string fechaNa = Convert.ToDateTime(dr[7].ToString()).ToString("dd/MM/yyyy");
                                            string lugarNa = dr[8].ToString().ToUpper();
                                            int titulo_id  = Convert.ToInt32(dr[9].ToString());
                                            int genero_id  = Convert.ToInt32(dr[10].ToString());
                                            int nacion_id  = Convert.ToInt32(dr[11].ToString());
                                            int estado_id  = Convert.ToInt32(dr[12].ToString());
                                            string codigop = dr[13].ToString();
                                            int estadod_id = Convert.ToInt32(dr[14].ToString());
                                            string ciudad  = dr[15].ToString().ToUpper();
                                            string colonia = dr[16].ToString().ToUpper();
                                            string calle   = dr[17].ToString().ToUpper();
                                            string numeroc = dr[18].ToString();
                                            string telefof = dr[19].ToString();
                                            string telefom = dr[20].ToString();
                                            string correoe = dr[21].ToString();
                                            string fechama = Convert.ToDateTime(dr[22].ToString()).ToString("dd/MM/yyyy");
                                            string tiposan = dr[23].ToString();
                                            int usuario_id = Convert.ToInt32(Session["iIdUsuario"].ToString());
                                            // Variables, TEmpleado_imss
                                            string fechaei = Convert.ToDateTime(dr[24].ToString()).ToString("dd/MM/yyyy");
                                            string regimss = dr[25].ToString();
                                            string rfcempl = dr[26].ToString();
                                            string curpemp = dr[27].ToString();
                                            int nivelestud = Convert.ToInt32(dr[28].ToString());
                                            int nivelsocio = Convert.ToInt32(dr[29].ToString());
                                            // Variables, TEmpleado_nomina
                                            string fechaen = Convert.ToDateTime(dr[30].ToString()).ToString("dd/MM/yyyy");
                                            double salamen = Convert.ToDouble(dr[31].ToString());
                                            int tipoperiod = Convert.ToInt32(dr[32].ToString());
                                            int tipoemplea = Convert.ToInt32(dr[33].ToString());
                                            int nivelemple = Convert.ToInt32(dr[34].ToString());
                                            int tipojornad = Convert.ToInt32(dr[35].ToString());
                                            int tipocontra = Convert.ToInt32(dr[36].ToString());
                                            int tcontratac = Convert.ToInt32(dr[37].ToString());
                                            string feching = dr[38].ToString();
                                            string fechant = dr[39].ToString();
                                            string fechvco = dr[40].ToString();
                                            int tipopagoem = Convert.ToInt32(dr[41].ToString());
                                            int bancopagoe = Convert.ToInt32(dr[42].ToString());
                                            // Insertamos el registro en TEmpleado
                                            //empleadosBean = empleadosDao.sp_Empleados_Insert_Empleado(nombre, paterno, materno, genero_id, estado_id, fechaNa, lugarNa, titulo_id, nacion_id.ToString(), estadod_id, codigop, ciudad, colonia, calle, numeroc, telefof, telefom, correoe, usuario_id, empresa, tiposan, fechama);
                                            //// Insertamos el registro en TEmpleado_imss
                                            //imssBean = imssDao.sp_Imss_Insert_Imss(fechaei, regimss, rfcempl, curpemp, nivelestud, nivelsocio, keyFile, nombre, paterno, materno, fechaNa, empresa, 0);
                                            // Validamos que los registros se hayan hecho correctamente
                                            //if (empleadosBean.sMensaje == "success" && imssBean.sMensaje == "success") {
                                            //    correctDataInsertBeans.Add(new CorrectDataInsertBean { iFilaInsert = rowActu, sEmpresa = dr[3].ToString(), sNombre = nombre + " " + paterno + " " + materno });
                                            //    flagInsert = true;
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                    }
                    showMessageErVal = validationErMe.ToString();
                    if (!Directory.Exists(pathCompleteSearch + pathLogsErVFile)) {
                        Directory.CreateDirectory(pathCompleteSearch + pathLogsErVFile);
                    }
                    using (StreamWriter fileLog = new StreamWriter(pathCompleteSearch + pathLogsErVFile + @"\\" + nameFileLogMessage, false, Encoding.UTF8)) {
                        if (errorDataLoadBeans.Count > 0) {
                            fileLog.Write("* -- Errores de validaciones -- *\n");
                            foreach (ErrorDataLoadBean data in errorDataLoadBeans) {
                                fileLog.Write("[*] Fila = " + data.iFilaError.ToString() + ", [*] Empresa = " + data.sEmpresa + ",  [*] Mensaje = " + data.sErrores + "\n");
                            }
                        }
                        fileLog.Write("\n");
                        if (correctDataInsertBeans.Count > 0) {
                            fileLog.Write("* -- Datos insertados -- *\n");
                            foreach (CorrectDataInsertBean data in correctDataInsertBeans) {
                                fileLog.Write("[*] Fila = " + data.iFilaInsert.ToString() + ", [*] Empresa = " + data.sEmpresa + ", [*] Nombre = " + data.sNombre + "\n");
                            }
                        }
                        fileLog.Close();
                    }
                }
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
                flag         = false;
            }
            return Json(new { Bandera = flag, BanderaInsercion = flagInsert, BanderaBusqueda = flagSearch, BanderaH = flagSpreadSheet, BanderaC = flagCodeCorrect, BanderaVE = flagVE, MensajeError = messageError, Sesion = nameFileSession, Errores = showMessageErVal, FolderLog = pathLogsErVFile, ArchivoLog = nameFileLogMessage });
        }

    }
}