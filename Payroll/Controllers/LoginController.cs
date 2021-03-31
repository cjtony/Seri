using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PasswordChange() {
            return View();
        }
        [HttpPost]
        public JsonResult LoginValidate(string username, string password)
        {
            UsuariosBean usuBean = new UsuariosBean();
            UsuariosDao usuDao = new UsuariosDao();
            usuBean = usuDao.sp_Login_Retrieve_Usuario_Inicia_Sesion(username, password);
            Session["iIdUsuario"] = usuBean.iIdUsuario;
            Session["sUsuario"] = usuBean.sUsuario;
            Session["Profile"] = usuBean.iPerfil;
            return Json(usuBean);
        }
        public ActionResult Logout()
        {
            Console.WriteLine("---------numero de variables----------");
            Console.WriteLine(Session.Count);
            Console.WriteLine("---------numero de variables----------");
            Session.Remove("iIdUsuario");
            Session.Remove("sUsuario");
            Session.Remove("Administrador");
            Session.Remove("Profile");
            Session.Remove("sEmpresa");
            Session.Remove("IdEmpresa");
            Session.Contents.RemoveAll();
            Session.Clear();
            Session.Abandon();
            return Redirect("../Home/Index");
        }

    }
}