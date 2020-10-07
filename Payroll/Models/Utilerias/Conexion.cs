using System;
using System.Data.SqlClient;

namespace Payroll.Models.Utilerias
{
    public class Conexion
    {
        // BD Local 
        //  
        //static readonly string Server = "DESKTOP-PDPH161";
        //static readonly string Db = "IPSNet";
        //static readonly string User = "sa";
        //static readonly string Pass = "omejia";
        // Bd ip local
        //static readonly string Server = "192.168.51.9";
        //static readonly string Db = "IPSNet";
        //static readonly string User = "IPSNet";
        //static readonly string Pass = "IPSNet2";
        //
        //
        // BD Server produccion
        //
        static readonly string Server = "201.149.34.185,15002";
        static readonly string Db = "IPSNet";
        static readonly string User = "IPSNet";
        static readonly string Pass = "IPSNet2";
        protected SqlConnection conexion { get; set; }

        protected SqlConnection Conectar()
        {
            try
            {
                conexion = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + Db + ";User ID=" + User + ";Password=" + Pass + ";Integrated Security=False");
                //  conexion = new SqlConnection("Data Source = DESKTOP-CNPFA5C; Initial Catalog=IPSNet; Integrated Security = true");

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