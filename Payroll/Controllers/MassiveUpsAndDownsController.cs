using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

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
            try {
                int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
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
            return Json(new { Bandera = flag, Log = flagLog, NombreLog = nameFileLogUploadFile, FolderLog = nameBinderLogs, MensajeError = messageError }, JsonRequestBehavior.AllowGet) ;
        }

    }
}