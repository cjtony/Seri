using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class MenuDao : Conexion
    {
        public List<PermisosBean> sp_Menu_Retrieve_Permisos_Usuario_Menu(int usuSesion)
        {
            List<PermisosBean> permBean = new List<PermisosBean>();
            List<string> permisos = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Menu_Retrieve_Permisos_Usuario_Menu", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlsIdUsuario", usuSesion));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        PermisosBean listPermBean = new PermisosBean();
                        listPermBean.sPerfil = data["Perfil"].ToString();
                        listPermBean.sUsuario = data["Usuario"].ToString();
                        permBean.Add(listPermBean);
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                this.conexion.Close(); this.Conectar().Close();
            }

            return permBean;
        }
    }
}