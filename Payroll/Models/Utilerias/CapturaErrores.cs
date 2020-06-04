using Payroll.Models.Beans;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Utilerias
{
    public class CapturaErrores : Conexion
    {

        public CapturaErroresBean sp_Errores_Insert_Errores(string origen, string mensaje)
        {
            CapturaErroresBean capturaError = new CapturaErroresBean();
            try
            {
                this.Conectar();
                SqlCommand error = new SqlCommand("sp_Errores_Insert_Errores", this.conexion) { CommandType = CommandType.StoredProcedure };
                string origenerror = origen;
                string mensajeerror = mensaje;
                error.Parameters.Add(new SqlParameter("@ctrlOrigen", origenerror));
                error.Parameters.Add(new SqlParameter("@ctrlMensaje", mensajeerror));
                if (error.ExecuteNonQuery() > 0)
                {
                    capturaError.sMensaje = "success";
                }
                else
                {
                    capturaError.sMensaje = "error";
                }
                error.Dispose(); error.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return capturaError;
        }

    }
}