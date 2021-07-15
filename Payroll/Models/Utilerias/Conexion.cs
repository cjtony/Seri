using System;
using System.Data.SqlClient;

namespace Payroll.Models.Utilerias
{
    public class Conexion
    {
        static readonly string Server = "GSERIPROD01";
        static readonly string Db = "IPSNet";
        static readonly string User = "IPSNet";
        static readonly string Pass = "IPSNet2";
        protected SqlConnection conexion { get; set; }

        protected SqlConnection Conectar()
        {
            try
            {
                conexion = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + Db + ";User ID=" + User + ";Password=" + Pass + ";Integrated Security=False");
                conexion.Open();
                return conexion;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

    }
}