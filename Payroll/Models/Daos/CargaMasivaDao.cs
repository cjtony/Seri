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
            int typeoffile = 0;


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
            switch (fileType)
            {
                case "incidencias":
                    typeoffile = 0;
                    break;
                case "ausentismos":
                    typeoffile = 1;
                    break;
                case "creditos":
                    typeoffile = 2;
                    break;
                case "pensiones":
                    typeoffile = 3;
                    break;
            }
            return dataset.Tables[typeoffile];
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
        public int Valida_Empleado(string Empresa_id, string Empleado_id)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Empleado_Empresa", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
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
        public int Valida_Periodo(string Empresa_id, string Periodo, string Anio)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Periodo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlAnio", Anio));
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
        public int Valida_Renglon(string Empresa_id, string Renglon_id)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Renglon", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlRenglon_id", Renglon_id));
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
        public List<string> InsertaCargaMasivaIncidencias(DataRow rows)
        {
            string dia = DateTime.Today.ToString("dd");
            string mes = DateTime.Today.ToString("MM");
            string año = DateTime.Today.ToString("yyyy");

            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_incidencias_Insert_Incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows["Empresa_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows["Empleado_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoIncidencia", rows["Renglon_id"].ToString()));

            if (rows["Renglon_id"].ToString() == "71")
            { cmd.Parameters.Add(new SqlParameter("@ctrlCantidad", rows["Numero_dias"].ToString())); }
            else
            { cmd.Parameters.Add(new SqlParameter("@ctrlCantidad", rows["Importe"].ToString())); }

            cmd.Parameters.Add(new SqlParameter("@ctrlPlazos", rows["Plazos"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlLeyenda", rows["Leyenda"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", rows["Descripcion"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAplicacion", dia + "/" + mes + "/" + año));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", rows["Periodo"].ToString()));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["Descripcion"].ToString());
                }
            }
            data.Close();this.conexion.Close();this.Conectar().Close();
            return list;
        }
        public List<string> InsertaCargaMasivaAusentismo(DataRow rows)
        {
            string dia = DateTime.Today.ToString("dd");
            string mes = DateTime.Today.ToString("MM");
            string año = DateTime.Today.ToString("yyyy");

            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_incidencias_Insert_Incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo_Ausentismo_id", rows["Tipo_Ausentismo"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows["Empresa_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows["Empleado_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlRecupera_Ausentismo", rows["Importe"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Ausentismo", rows["Plazos"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Ausentismo", rows["Leyenda"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCertificado_imss", rows["Descripcion"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlComentarios_imss", dia + "/" + mes + "/" + año));
            cmd.Parameters.Add(new SqlParameter("@ctrlCausa_FaltaInjustificada", rows["Periodo"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", rows["Periodo"].ToString()));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["Descripcion"].ToString());
                }
            }
            data.Close(); this.conexion.Close(); this.Conectar().Close();
            return list;
        }
    }
}