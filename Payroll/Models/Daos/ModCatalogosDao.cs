using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class ModCatalogosDao : Conexion
    {
        // FUNCIONES PARA LAS FECHAS - PERIODO
        public List<InicioFechasPeriodoBean> sp_Retrieve_CInicio_Fechas_Periodo()
        {
            List<InicioFechasPeriodoBean> listBean = new List<InicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_CInicio_Fechas_Periodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        InicioFechasPeriodoBean Bean = new InicioFechasPeriodoBean();
                        Bean.id = data["Id"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Anio = data["Anio"].ToString();
                        Bean.Tipo_Periodo_Id = data["Tipo_Periodo_Id"].ToString();
                        Bean.DescripcionTipoPeriodo = data["DescripcionTipoPeriodo"].ToString();
                        Bean.Periodo = data["Periodo"].ToString();
                        Bean.Fecha_Inicio = data["Fecha_Inicio"].ToString();
                        Bean.Fecha_Final = data["Fecha_Final"].ToString();
                        Bean.Fecha_Proceso = data["Fecha_Proceso"].ToString();
                        Bean.Fecha_Pago = data["Fecha_Pago"].ToString();
                        Bean.Dias_Efectivos = data["Dias_Efectivos"].ToString();

                        listBean.Add(Bean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        public List<InicioFechasPeriodoBean> sp_Retrieve_CInicio_Fechas_Periodo_Detalle(int Empresa_id)
        {
            List<InicioFechasPeriodoBean> listBean = new List<InicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_CInicio_Fechas_Periodo_Detalle", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        InicioFechasPeriodoBean Bean = new InicioFechasPeriodoBean();
                        Bean.id = data["Id"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Anio = data["Anio"].ToString();
                        Bean.Tipo_Periodo_Id = data["Tipo_Periodo_Id"].ToString();
                        Bean.DescripcionTipoPeriodo = data["DescripcionTipoPeriodo"].ToString();
                        Bean.Periodo = data["Periodo"].ToString();
                        Bean.Fecha_Inicio = data["Fecha_Inicio"].ToString();
                        Bean.Fecha_Final = data["Fecha_Final"].ToString();
                        Bean.Fecha_Proceso = data["Fecha_Proceso"].ToString();
                        Bean.Fecha_Pago = data["Fecha_Pago"].ToString();
                        Bean.Dias_Efectivos = data["Dias_Efectivos"].ToString();
                        Bean.Nomina_Cerrada = data["Nomina_Cerrada"].ToString();

                        listBean.Add(Bean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        public List<string> sp_CInicio_Fechas_Periodo_Insert_Fecha_Periodo(int Empresa_id, int inano, int inperiodo, string infinicio, string inffinal, string infproceso, string infpago, int indiaspago)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CInicio_Fechas_Periodo_Insert_Fecha_Periodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlAno", inano));
                cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", inperiodo));
                cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Inicio", infinicio));
                cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Final", inffinal));
                cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Proceso", infproceso));
                cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Pago", infpago));
                cmd.Parameters.Add(new SqlParameter("@ctrlDias_Pagados", indiaspago));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sRespuesta"].ToString());
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        public List<string> sp_CInicio_Fechas_Periodo_Delete_Fecha_Periodo(int Empresa_id, int Id)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CInicio_Fechas_Periodo_Delete_Fecha_Periodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlId", Id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sRespuesta"].ToString());
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        // FUNCIONES PARA LAS POLITICAS DE VACACIONES
        public List<TabPoliticasVacacionesBean> sp_Retrieve_CPoliticasVacaciones()
        {
            List<TabPoliticasVacacionesBean> listBean = new List<TabPoliticasVacacionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_CPoliticasVacaciones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TabPoliticasVacacionesBean Bean = new TabPoliticasVacacionesBean();

                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Effdt = data["Effdt"].ToString();
                        Bean.Anos = data["Anos"].ToString();
                        Bean.Dias = data["Dias"].ToString();
                        Bean.Prima_Vacacional_Porcen = data["Prima_Vacacional_Porcen"].ToString();
                        Bean.Dias_Aguinaldo = data["Dias_Aguinaldo"].ToString();

                        listBean.Add(Bean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        // FUNCIONES PARA LAS POLITICAS DE VACACIONES FUTURAS
        public List<TabPoliticasVacacionesBean> sp_Retrieve_CPoliticasVacaciones_Futuras()
        {
            List<TabPoliticasVacacionesBean> listBean = new List<TabPoliticasVacacionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_CPoliticasVacaciones_Futuras", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TabPoliticasVacacionesBean Bean = new TabPoliticasVacacionesBean();

                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Effdt = data["Effdt"].ToString();

                        listBean.Add(Bean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        public List<TabPoliticasVacacionesBean> sp_Retrieve_CPoliticasVacaciones_Detalle(int Empresa_id)
        {
            List<TabPoliticasVacacionesBean> listBean = new List<TabPoliticasVacacionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_CPoliticasVacacione_Detalle", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TabPoliticasVacacionesBean Bean = new TabPoliticasVacacionesBean();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.Effdt = data["Effdt"].ToString();
                        Bean.Anos = data["Anos"].ToString();
                        Bean.Dias = data["Dias"].ToString();
                        Bean.Prima_Vacacional_Porcen = data["Prima_Vacacional_Porcen"].ToString();
                        Bean.Dias_Aguinaldo = data["Dias_Aguinaldo"].ToString();

                        listBean.Add(Bean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        public List<TabPoliticasVacacionesBean> sp_Retrieve_CPoliticasVacaciones_Futuras_Detalle(int Empresa_id, string Effdt)
        {
            List<TabPoliticasVacacionesBean> listBean = new List<TabPoliticasVacacionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_CPoliticasVacaciones_Futuras_Detalle", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlEffdt", Effdt));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TabPoliticasVacacionesBean Bean = new TabPoliticasVacacionesBean();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.Effdt = data["Effdt"].ToString();
                        Bean.Anos = data["Anos"].ToString();
                        Bean.Dias = data["Dias"].ToString();
                        Bean.Prima_Vacacional_Porcen = data["Prima_Vacacional_Porcen"].ToString();
                        Bean.Dias_Aguinaldo = data["Dias_Aguinaldo"].ToString();

                        listBean.Add(Bean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
        public List<string> sp_CPoliticasVacaciones_Insert_Effdt_Futura(int Empresa_id, string Effdt)
        {
            List<string> list = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CPoliticasVacaciones_Insert_Effdt_Futura", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlEffdt", Effdt));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        list.Add(data["iFlag"].ToString());
                        list.Add(data["sRespuesta"].ToString());
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<string> sp_CPoliticasVacaciones_Insert_Politica(string inEmpresa_id, string inEffdt, string inano, string indias, string inprimav, string indiasa)
        {
            List<string> list = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CPoliticasVacaciones_Insert_Politica", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", inEmpresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlEffdt", inEffdt));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", inano));
                cmd.Parameters.Add(new SqlParameter("@ctrlDias", indias));
                cmd.Parameters.Add(new SqlParameter("@ctrlPrimav", inprimav));
                cmd.Parameters.Add(new SqlParameter("@ctrlDiasa", indiasa));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        list.Add(data["iFlag"].ToString());
                        list.Add(data["sRespuesta"].ToString());
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<string> sp_CPoliticasVacaciones_Delete_Politica(int Empresa_id, string Effdt, int Anio)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CPoliticasVacaciones_Delete_Politica", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlEffdt", Effdt));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", Anio));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sRespuesta"].ToString());
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ModCatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listBean;
        }
    }
}