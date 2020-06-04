using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class ControlPayrollController : Controller
    {
        // GET: ControlPayroll
        public ActionResult Home()
        {
            if (Session["iIdUsuario"] == null)
            {
                return Redirect("../Home/Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult LoadMenu()
        {
            List<PermisosBean> permBean;
            MenuDao menDao = new MenuDao();
            int usuSesion = int.Parse(Session["iIdUsuario"].ToString());
            permBean = menDao.sp_Menu_Retrieve_Permisos_Usuario_Menu(usuSesion);
            string ad = "Sin entrar", nom = "Sin entrar", inc = "Sin entrar", kio = "Sin entrar", rh = "Sin entrar";
            foreach (var item in permBean)
            {
                if (item.sPerfil == "Administrador")
                {
                    ad = "Entro";
                    Session["Administrador"] = 1;
                }
                else { Session["Administrador"] = 0; }
                if (item.sPerfil == "Nomina")
                {
                    nom = "Entro";
                    Session["Nomina"] = 1;
                }
                else { Session["Nomina"] = 0; }
                if (item.sPerfil == "Incidencias")
                {
                    inc = "Entro";
                    Session["Incidencias"] = 1;
                }
                else { Session["Incidencias"] = 0; }
                if (item.sPerfil == "Kiosko")
                {
                    kio = "Entro";
                    Session["Kiosko"] = 1;
                }
                else { Session["Kiosko"] = 0; }
                if (item.sPerfil == "RH")
                {
                    rh = "Entro";
                    Session["RH"] = 1;
                }
                else { Session["RH"] = 0; }
            }
            var data = new { admin = ad, nomina = nom, incidencia = inc, kiosko = kio, rh };
            return Json(data);
        }

        [HttpPost]
        public JsonResult MenuInit()
        {
            List<MainMenuBean> MmenuBean;
            MainMenuDao MmenuDao = new MainMenuDao();
            int Sesion_IdUser = int.Parse(Session["iIdUsuario"].ToString());
            MmenuBean = MmenuDao.sp_Retrieve_Menu_Paths(Sesion_IdUser);
            string nav = "<nav class='nav nav-pills flex-column flex-sm-row sticky-top' >";
            string tabnav = "<div class='tab-content'>";
            string subnavs = "<ul class='nav nav-tabs flex-column flex-sm-row ' id='menuTab' role='tablist'>";

            int i = 1;
            foreach (var item in MmenuBean)
            {
                if (item.iParent == 0)
                {
                    nav += "<a class='flex-sm-fill text-sm-center btn btn-menu font-navs rounded-0' data-toggle='pill' onclick='change()' href='#pills-" + item.sNombre + "'> " + item.sNombre + " </a>";

                    List<MainMenuBean> submenus;
                    submenus = MmenuDao.Bring_Main_Menus(int.Parse(Session["Profile"].ToString()), item.iIdItem);
                    var ul = "<ul class='nav nav-tabs flex-column flex-sm-row ' id='menuTab' role='tablist'>";
                    if (i > 0)
                    {
                        subnavs += ul;
                    }
                    foreach (var subitem in submenus)
                    {
                        if (subitem.iParent == item.iIdItem)
                        {
                            subnavs += "<li class='nav-item'><a class='nav-link font-navs " + subitem.sNombre + "' onclick='seeview(" + '"' + subitem.sUrl + '"' + ")' href='#' ><span class='" + subitem.sIcono + "' style='color:#020F59;'></span> " + subitem.sNombre + " </a></li>";
                        }
                    }
                    subnavs += "</ul>";
                    i++;
                    tabnav += "<div class='tab-pane fade' id='pills-" + item.sNombre + "' role='tabpanel' aria-labelledby='" + item.sNombre + "-tab'>" + subnavs + "</div>";
                    subnavs = "";
                }
            }
            nav += "</nav>";
            tabnav += "</div>";
            return Json(nav += tabnav);
        }
    }
}