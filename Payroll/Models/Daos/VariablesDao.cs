using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Data;
using System.Data.SqlClient;
namespace Payroll.Models.Daos
{
    public class VariablesDao { }

    public class NumeroNominaDao : Conexion
    {
        public NumeroNominaBean sp_Consulta_NumeroNomina_Empresa(int keyemp)
        {
            NumeroNominaBean numeroNominaBean = new NumeroNominaBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Consulta_NumeroNomina_Empresa", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    numeroNominaBean.iNominaSiguiente = Convert.ToInt32(data["NominaSiguiente"].ToString());
                    numeroNominaBean.iNominaTope = Convert.ToInt32(data["NominaTope"].ToString());
                    numeroNominaBean.sMensaje = "success";
                }
                else
                {
                    numeroNominaBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return numeroNominaBean;
        }
    }

    public class NumeroPosicionDao : Conexion
    {
        public NumeroPosicionBean sp_Consulta_NumeroPosicion()
        {
            NumeroPosicionBean numeroPosicionBean = new NumeroPosicionBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Consulta_NumeroPosicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    numeroPosicionBean.iPosicionUltima = Convert.ToInt32(data["PosicionUltima"].ToString());
                    numeroPosicionBean.iPosicionSiguiente = Convert.ToInt32(data["PosicionSiguiente"].ToString());
                    numeroPosicionBean.sMensaje = "success";
                }
                else
                {
                    numeroPosicionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return numeroPosicionBean;
        }
    }

}