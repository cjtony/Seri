using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Payroll.Models.Daos
{
    public class MainMenuDao : Conexion
    {
        public List<MainMenuBean> sp_Retrieve_Menu_Paths(int Sesion_IdUser)
        {
            List<MainMenuBean> permBean = new List<MainMenuBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_Profile_User", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@IdUser", Sesion_IdUser));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                int PerfilUser = 0;
                if (data.HasRows)
                {
                    while (data.Read())
                    {

                        PerfilUser = int.Parse(data["Perfil"].ToString());

                    }
                }

                permBean = bringMenus(PerfilUser);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                this.conexion.Close();
                this.Conectar().Close();
            }

            return permBean;

        }

        public List<MainMenuBean> bringMenus(int Profile_User)
        {
            List<MainMenuBean> MmenuBean = new List<MainMenuBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_Menu_Paths", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@IdPerfil", Profile_User));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    MainMenuBean listMmenuBean = new MainMenuBean
                    {
                        iIdItem = int.Parse(data["IdItem"].ToString()),
                        sNombre = data["Nombre"].ToString(),
                        sIcono = data["Icono"].ToString(),
                        sUrl = data["Url"].ToString(),
                        iParent = int.Parse(data["Parent"].ToString())
                    };
                    MmenuBean.Add(listMmenuBean);
                }
            }
            data.Close();
            this.conexion.Close();this.Conectar().Close();

            return MmenuBean;
        }

        public List<MainMenuBean> Bring_Main_Menus(int Profile_User, int Id_Item)
        {
            List<MainMenuBean> list = new List<MainMenuBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_Main_Menus", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@IdPerfil", Profile_User));
            cmd.Parameters.Add(new SqlParameter("@IdItem", Id_Item));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    MainMenuBean listMmenuBean = new MainMenuBean
                    {
                        iIdItem = int.Parse(data["IdItem"].ToString()),
                        sNombre = data["Nombre"].ToString(),
                        sIcono = data["Icono"].ToString(),
                        sUrl = data["Url"].ToString(),
                        iParent = int.Parse(data["Parent"].ToString())
                    };
                    list.Add(listMmenuBean);
                }
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }




    }
}