using ExcelDataReader;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Payroll.Models.Daos
{
    public class CargaMasivaDao : Conexion
    {
        public DataTable ValidaArchivo(string fileName, string fileType)
        {
            List<object> list = new List<object>();

            DataSet dataset = null;

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var i = 1;
                    while (reader.Read() && i <= reader.RowCount)
                    {
                        dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        i++;
                    }
                }
            }

            return dataset.Tables[0];
        }

        public int ValidaEmpresa(string Empresa_id)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Empresa", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    value = int.Parse(data["Result"].ToString());
                }
            }

            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return value;
        }
    }
}