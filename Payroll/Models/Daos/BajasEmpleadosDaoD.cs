using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;

namespace Payroll.Models.Daos
{
    public class BajasEmpleadosDaoD : Conexion
    {

        public int DiasRestantes() {
            int days = 0;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("fn_Dias_restantes", this.conexion) { CommandType = CommandType.Text };
                cmd.Parameters.Add(new SqlParameter("@fecha_inicial", "01/01/2019"));
                cmd.Parameters.Add(new SqlParameter("@fecha_final", "02/01/2020"));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    days = Convert.ToInt32(data[0]);
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            }
            return days;
        }

    }
}