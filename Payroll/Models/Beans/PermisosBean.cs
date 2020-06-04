namespace Payroll.Models.Beans
{
    public class PermisosBean
    {
        public int iIdPermiso { get; set; }
        public int iIdPerfil { get; set; }
        public int iIdUsuario { get; set; }
        public string sPerfil { get; set; }
        public string sUsuario { get; set; }
        public int iValid { get; set; }
        public string sMensaje { get; set; }
    }
}