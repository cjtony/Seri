using ExcelDataReader;
using Payroll.Models.Beans;
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
                case "créditos":
                    typeoffile = 2;
                    break;
                case "pensiones":
                    typeoffile = 3;
                    break;
                case "vacaciones":
                    typeoffile = 4;
                    break;
            }
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
        public DataTable ValidaArchivo(string fileName)
        {
            List<object> list = new List<object>();

            DataSet dataset = null;
            
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
        public int ValidaEmpresaTipoPeriodo(string Empresa_id, string tipo_periodo)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Empresa_TipoPeriodo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo_id", tipo_periodo));
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
        public List<string> Valida_Vacaciones(string Empresa_id, string Empleado_id, string Anio, string Dias)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Vacaciones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlAnio", Anio));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias", Dias));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["Result"].ToString());
                    list.Add(data["sMensaje"].ToString());
                }
            }

            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
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
        public int Valida_PeriodoExistente(string Empresa_id, string Anio, string tipo_perido, string Periodo)
        {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_ExistePeriodo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlAnio", Anio));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo", tipo_perido));
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
        public int Valida_FormatoFechas(string FechaIn) {
            int value = 0;
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_Fechas", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlFechain", FechaIn));
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
        public ReturnBean ValidaCertificado(string Certificado) {
            ReturnBean Bean = new ReturnBean();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Valida_CM_CertificadoIMSS", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlCertificado", Certificado));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    Bean.iFlag = int.Parse(data["iFlag"].ToString());
                    Bean.sRespuesta = data["sRespuesta"].ToString();
                }
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return Bean;
        }
        public ReturnBean Valida_Periodo_Especial_Existe(string Empresa_id, string Periodo_especial)
        {
            ReturnBean Bean = new ReturnBean();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("Valida_Periodo_Especial_Existe", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo_especial", Periodo_especial));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    Bean.iFlag = int.Parse(data["iFlag"].ToString());
                    Bean.sRespuesta = data["sRespuesta"].ToString();
                }
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return Bean;
        }
        public List<string> InsertaCargaMasivaIncidencias(DataRow rows, int IsCargaMasiva, int Periodo, string Referencia)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_incidencias_Insert_Incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows["Empresa_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows["Empleado_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoIncidencia", rows["Renglon_id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCantidad", rows["Importe"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias", rows["Numero_dias"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlPlazos", rows["Plazos"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlLeyenda", rows["Leyenda"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAplicacion", rows["Fecha_Aplicacion"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", rows["Aplica_En_Finiquito"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDiashrs", rows["Dias_hrs"].ToString()));

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
        public List<string> InsertaCargaMasivaAusentismo(DataRow rows, int Periodo, int IsCargaMasiva, string Referencia)
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
            var sbs = rows[2].ToString().Substring(0, 2);
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo_Ausentismo_id", rows[2].ToString().Substring(0, 2).Trim()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows[0].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows[1].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlRecupera_Ausentismo", 3));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Ausentismo", rows[3].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Ausentismo", rows[4].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCertificado_imss", rows[5].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlComentarios_imss", rows[6].ToString()));
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
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", rows[8].ToString()));
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
        public List<string> InsertaCargaMasivaCreditos(DataRow rows, int Periodo, int IsCargaMasiva, string Referencia)
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
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoDescuento", rows[2].ToString().Substring(0, 3).Trim()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescuento", rows[4].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlNoCredito", rows[5].ToString().Substring(1, rows[5].ToString().Length - 1)));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAprovacionCredito", rows[6].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescontar", rows[3].ToString().Substring(0, 3).Trim()));
            if (rows[7].ToString().Length == 0 || rows[7].ToString() == null)
            {  cmd.Parameters.Add(new SqlParameter("@ctrlFechaBajaCredito", "0"));  }
            else  {  cmd.Parameters.Add(new SqlParameter("@ctrlFechaBajaCredito", rows[7].ToString()));  }
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaReinicioCredito", ""));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaFiniquito", rows[8].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
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
        public List<string> InsertaCargaMasivaPensionesAlimenticias(DataRow rows, int Periodo, int IsCargaMasiva, string Referencia)
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
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
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
        public List<string> InsertaCargaMasivaVacaciones(DataRow rows, int Periodo, int IsCargaMasiva, string Referencia, int Usuario_id)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPeriodosVacaciones_insert_Vacaciones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", rows[1].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows[0].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlAnio", rows[2].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_inicio", rows[3].ToString()));
            if (rows[4].ToString() == "" || rows[4] == null || rows[4].ToString().Length == 0)
            {   cmd.Parameters.Add(new SqlParameter("@ctrlFecha_fin", "0"));    }
            else {  cmd.Parameters.Add(new SqlParameter("@ctrlFecha_fin", rows[4].ToString())); }
            cmd.Parameters.Add(new SqlParameter("@ctrlDias", rows[5].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", IsCargaMasiva));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlUsuario_id", Usuario_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sMensaje"].ToString());
                }
            }
            data.Close(); this.conexion.Close(); this.Conectar().Close();
            return list;
        }
        public List<string> InsertaCargaMasivaPeriodos(DataRow rows)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CInicio_Fechas_Periodo_Insert_Fecha_Periodo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", rows[0].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlAno", rows[1].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", rows[3].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Inicio", rows[4].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Final", rows[5].ToString())); 
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Proceso", rows[6].ToString())); 
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Pago", rows[7].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Pagados", rows[8].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo_id", rows[2].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ctrlEspecial", "0"));
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
        public List<string> sp_Cancela_CargaMasiva(string tabla, string referencia)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Cancela_CargaMasiva", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", referencia));
            cmd.Parameters.Add(new SqlParameter("@ctrlTabla", tabla));
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