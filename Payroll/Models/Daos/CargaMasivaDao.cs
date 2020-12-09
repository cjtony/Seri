using ExcelDataReader;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Payroll.Models.Daos
{
    public class CargaMasivaDao : Conexion
    {
        public DataTable ValidaArchivo(string fileName, string fileType)
        {
            List<object> list = new List<object>();

            DataSet dataset = null;
            int typeoffile = 0;

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
            //using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            //{
            //    using (var reader = ExcelReaderFactory.CreateReader(stream))
            //    {
            //        var contador = reader.ResultsCount;
            //        var i = 1;
            //        while (reader.Read() /*&& i <= reader.RowCount*/ )
            //        {
            //            dataset = reader.AsDataSet(new ExcelDataSetConfiguration()

            //            {
            //                FilterSheet = (tableReader, sheetIndex) => true,
            //                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
            //                {
            //                    UseHeaderRow = true
            //                }
            //            });
            //            i++;
            //        }
            //    }
            //}
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    dataset = reader.AsDataSet(new ExcelDataSetConfiguration()

                    {
                        FilterSheet = (tableReader, sheetIndex) => true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                }
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
        public int Valida_Existe_Carga_Masiva(int Empresa_id, int Periodo, int Renglon, string Tabla, string Referencia)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_Existe_Carga_Masiva", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlRenglon", Renglon));
            cmd.Parameters.Add(new SqlParameter("@ctrlTabla", Tabla));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
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
        public List<string> InsertaCargaMasivaIncidencias(DataRow rows, int IsCargaMasiva, int Periodo)
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
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", rows["Referencia"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAplicacion", dia + "/" + mes + "/" + año));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", rows["Aplica_En_Finiquito"].ToString()));

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
        public List<string> InsertaCargaMasivaAusentismo(DataRow rows, int Periodo, int IsCargaMasiva)
        {
            string dia = DateTime.Today.ToString("dd");
            string mes = DateTime.Today.ToString("MM");
            string año = DateTime.Today.ToString("yyyy");

            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Insert_Ausentismo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            //cmd.Parameters.Add(new SqlParameter("@ctrlTipo_Ausentismo_id", rows["Tipo_Ausentismo"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows["Empresa_id"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows["Empleado_id"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlRecupera_Ausentismo", rows["Importe"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Ausentismo", rows["Plazos"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlDias_Ausentismo", rows["Leyenda"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlCertificado_imss", rows["Descripcion"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlComentarios_imss", dia + "/" + mes + "/" + año));
            //cmd.Parameters.Add(new SqlParameter("@ctrlCausa_FaltaInjustificada", rows["Periodo"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", rows["Periodo"].ToString()));
            var sbs = rows[2].ToString().Substring(0, 2);
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo_Ausentismo_id", rows[2].ToString().Substring(0, 2).Trim()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows[0].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows[1].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlRecupera_Ausentismo", 3));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Ausentismo", rows[3].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Ausentismo", rows[4].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCertificado_imss", rows[6].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlComentarios_imss", rows[7].ToString()));
            if (rows[2].ToString().Substring(0, 2).Trim() == "9")
            {
                cmd.Parameters.Add(new SqlParameter("@ctrlCausa_FaltaInjustificada", "FALTA INJUSTIFICADA " + dia + "/" + mes + "/" + año));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@ctrlCausa_FaltaInjustificada", ""));
            }

            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", rows[8].ToString()));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            data.Close(); this.conexion.Close(); this.Conectar().Close();
            return list;
        }
        public List<string> InsertaCargaMasivaCreditos(DataRow rows, int Periodo, int IsCargaMasiva)
        {
            string dia = DateTime.Today.ToString("dd");
            string mes = DateTime.Today.ToString("MM");
            string año = DateTime.Today.ToString("yyyy");

            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TCreditos_Insert_Credito", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows[1].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows[0].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoDescuento", rows[2].ToString().Substring(0, 2).Trim()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescuento", rows[4].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlNoCredito", rows[5].ToString().Substring(1, rows[5].ToString().Length - 1)));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAprovacionCredito", rows[6].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescontar", rows[3].ToString().Substring(0, 1)));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaBajaCredito", rows[7].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaReinicioCredito", ""));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaFiniquito", rows[8].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", rows[9].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            data.Close(); this.conexion.Close(); this.Conectar().Close();
            return list;
        }
        public List<string> InsertaCargaMasivaPensionesAlimenticias(DataRow rows, int Periodo, int IsCargaMasiva)
        {
            string dia = DateTime.Today.ToString("dd");
            string mes = DateTime.Today.ToString("MM");
            string año = DateTime.Today.ToString("yyyy");

            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPensiones_Alimenticias_Insert_Pensiones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows[0].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows[1].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCuota_fija", rows[2].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlPorcentaje", rows[3].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEn", rows[4].ToString().Substring(0, 1)));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescontar_en_Finiquito", ""));
            cmd.Parameters.Add(new SqlParameter("@ctrlNo_Oficio", rows[5].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Oficio", rows[6].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlBeneficiaria", rows[7].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlBanco", rows[8].ToString().Substring(0, 2).Trim()));
            cmd.Parameters.Add(new SqlParameter("@ctrlSucursal", rows[9].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlTarjeta_Vales", rows[11].ToString()));
            if (rows[10].ToString().Length == 0 || rows[10] == null)
            { cmd.Parameters.Add(new SqlParameter("@ctrlCuenta_Cheques", rows[10].ToString())); }
            else { cmd.Parameters.Add(new SqlParameter("@ctrlCuenta_Cheques", rows[10].ToString().Substring(1, rows[10].ToString().Length - 1))); }
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", rows[13].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", rows[12].ToString()));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            data.Close(); this.conexion.Close(); this.Conectar().Close();
            return list;
        }

    }
}