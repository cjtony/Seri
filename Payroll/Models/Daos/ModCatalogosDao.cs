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
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.Tipo_Periodo_Id = data["Tipo_Periodo_Id"].ToString();
                        Bean.DescripcionTipoPeriodo = data["DescripcionTipoPeriodo"].ToString();
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
        public List<string> sp_CInicio_Fechas_Periodo_Insert_Fecha_Periodo(int Empresa_id, int inano, int inperiodo, string infinicio, string inffinal, string infproceso, string infpago, int indiaspago, int intipoperiodoid)
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
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo_id", intipoperiodoid));
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
        public List<TabPoliticasVacacionesBean> sp_CPoliticasVacaciones_Retrieve_Politica(int Empresa_id, string Effdt, string Anio)
        {
            List<TabPoliticasVacacionesBean> listBean = new List<TabPoliticasVacacionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CPoliticasVacaciones_Retrieve_Politica", this.conexion)
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
                        TabPoliticasVacacionesBean Bean = new TabPoliticasVacacionesBean();

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
        public List<EmpleadosxEmpresaBean> sp_CEmpresas_Retrieve_NoEmpleados()
        {
            List<EmpleadosxEmpresaBean> listBean = new List<EmpleadosxEmpresaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_NoEmpleados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosxEmpresaBean list = new EmpleadosxEmpresaBean();
                        list.Empresa_id = data["IdEmpresa"].ToString();
                        list.NombreEmpresa = data["NombreEmpresa"].ToString();
                        list.No = data["No"].ToString();
                        listBean.Add(list);
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
        public List<string> sp_TPuestos_Retrieve_Puestos_Empresa(int Empresa_id)
        {
            List<string> list = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TPuestos_Retrieve_Puestos_Empresa", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                SqlDataReader data = cmd.ExecuteReader();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        list.Add(data["IdPuesto"].ToString());
                        list.Add(data["Empresa_id"].ToString());
                        list.Add(data["PuestoCodigo"].ToString());
                        list.Add(data["NombrePuesto"].ToString());
                        list.Add(data["DescripcionPuesto"].ToString());
                        list.Add(data["NombreProfesion"].ToString());
                        list.Add(data["ClasificacionPuesto"].ToString());
                        list.Add(data["Colectivo"].ToString());
                        list.Add(data["NivelJerarquico"].ToString());
                        list.Add(data["PerformanceManager"].ToString());
                        list.Add(data["Tabulador"].ToString());
                        list.Add(data["Fecha_Alta"].ToString());
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
        public List<List<string>> sp_TPuestos_Retrieve_Empresas()
        {
            List<List<string>> lista = new List<List<string>>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TPuestos_Retrieve_Empresas", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        List<string> list = new List<string>();
                        list.Add(data["Empresa_id"].ToString());
                        list.Add(data["NombreEmpresa"].ToString());
                        list.Add(data["NumeroPuestos"].ToString());
                        lista.Add(list);
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
            return lista;
        }
        public List<DataPuestosBean> sp_Tpuestos_Search_Puesto(int Empresa_id, string Search)
        {
            
            List<DataPuestosBean> list = new List<DataPuestosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Tpuestos_Search_Puesto", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlSearch", Search));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    DataPuestosBean listEmpleados = new DataPuestosBean();
                    if (int.Parse(data["iFlag"].ToString()) == 0)
                    {
                        listEmpleados.iFlag = int.Parse(data["iFlag"].ToString());
                        listEmpleados.idPuesto = data["IdPuesto"].ToString();
                        listEmpleados.NombrePuesto = data["NombrePuesto"].ToString();
                        listEmpleados.DescripcionPuesto = data["DescripcionPuesto"].ToString();
                        listEmpleados.PuestoCodigo = data["PuestoCodigo"].ToString();
                        listEmpleados.fecha_alta = data["Fecha_Alta"].ToString();
                    }
                    else
                    {
                        listEmpleados.iFlag = int.Parse(data["iFlag"].ToString());
                        listEmpleados.NombrePuesto = data["title"].ToString();
                        listEmpleados.DescripcionPuesto = data["resume"].ToString();
                    }
                    list.Add(listEmpleados);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<DataPuestosBean> sp_TPuestos_Retrieve_Puesto(int Empresa_id, string Puesto_id)
        {

            List<DataPuestosBean> list = new List<DataPuestosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPuestos_Retrieve_Puesto", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPuesto_id", Puesto_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    DataPuestosBean listEmpleados = new DataPuestosBean();
                    listEmpleados.idPuesto = data["IdPuesto"].ToString();
                    listEmpleados.NombrePuesto = data["NombrePuesto"].ToString();
                    listEmpleados.DescripcionPuesto = data["DescripcionPuesto"].ToString();
                    listEmpleados.PuestoCodigo = data["PuestoCodigo"].ToString();
                    listEmpleados.fecha_alta = data["Fecha_Alta"].ToString();
                    listEmpleados.Empresa_id = data["Empresa_id"].ToString();
                    listEmpleados.NombreProfesion = data["NombreProfesion"].ToString();
                    listEmpleados.ClasificacionPuesto = data["ClasificacionPuesto"].ToString();
                    listEmpleados.Colectivo = data["Colectivo"].ToString();
                    listEmpleados.NivelJerarquico = data["NivelJerarquico"].ToString();
                    listEmpleados.PerformanceManager = data["PerformanceManager"].ToString();
                    listEmpleados.Tabulador = data["Tabulador"].ToString();

                    list.Add(listEmpleados);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<DataPuestosBean> sp_TPuestos_Retrieve_AllPuestos()
        {

            List<DataPuestosBean> list = new List<DataPuestosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPuestos_Retrieve_AllPuestos", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    DataPuestosBean listEmpleados = new DataPuestosBean();
                    listEmpleados.idPuesto = data["IdPuesto"].ToString();
                    listEmpleados.NombrePuesto = data["NombrePuesto"].ToString();
                    listEmpleados.DescripcionPuesto = data["DescripcionPuesto"].ToString();
                    listEmpleados.PuestoCodigo = data["PuestoCodigo"].ToString();
                    listEmpleados.fecha_alta = data["Fecha_Alta"].ToString();
                    listEmpleados.Empresa_id = data["Empresa_id"].ToString();
                    listEmpleados.NombreProfesion = data["NombreProfesion"].ToString();
                    listEmpleados.ClasificacionPuesto = data["ClasificacionPuesto"].ToString();
                    listEmpleados.Colectivo = data["Colectivo"].ToString();
                    listEmpleados.NivelJerarquico = data["NivelJerarquico"].ToString();
                    listEmpleados.PerformanceManager = data["PerformanceManager"].ToString();
                    listEmpleados.Tabulador = data["Tabulador"].ToString();

                    list.Add(listEmpleados);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<List<string>> sp_CGruposEmpresas_Retrieve_Grupos()
        {
            List<List<string>> lista = new List<List<string>> ();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CGruposEmpresas_Retrieve_Grupos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlGrupo_id", Grupo_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        List<string> list = new List<string>();
                        list.Add(data["IdGrupoEmpresa"].ToString());
                        list.Add(data["NombreGrupo"].ToString());
                        list.Add(data["EstadoGrupo"].ToString());
                        lista.Add(list);
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
            return lista;
        }
        public List<CRenglonesBean> sp_CRenglones_Retrieve_CRenglones(int IdEmpresa, int ctrliElemntoNOm)
        {
            List<CRenglonesBean> list = new List<CRenglonesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CRenglones_Retrieve_CRenglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@ctrliElemntoNOm", ctrliElemntoNOm));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CRenglonesBean ls = new CRenglonesBean();
                        {
                            ls.iIdEmpresa = int.Parse(data["Empresa_id"].ToString());
                            ls.sIdEmpresa = data["NombreEmpresa"].ToString();
                            ls.sNombreRenglon = data["NombreRenglon"].ToString();
                            ls.sIdElementoNomina = data["Valor"].ToString();
                            ls.sIdSeccionReporte = data["Cg_Seccion_en_Reporte_id"].ToString();
                            ls.sIdAcumulado = data["acumulado"].ToString();
                            ls.sCancelado = data["Cancelado"].ToString();
                            ls.sTipodeRenglon = data["Tipo_renglon"].ToString();
                            ls.sEspejo = data["rng_espejo"].ToString();
                            ls.slistCalculos = data["listacalculo"].ToString();
                            ls.sCuentaCont = data["Cuenta_Contable"].ToString();
                            ls.sDespCuCont = data["Descripcion_Cuenta_Contable"].ToString();
                            ls.sCargAbCuenta = data["cargo_abono_cuenta"].ToString();
                            ls.sIdSat = data["sat"].ToString();
                            ls.sMensaje = "success";
                        };


                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<List<string>> sp_CGruposEmpresas_Retrieve_EmpresasGrupo(int Grupo_id)
        {
            List<List<string>> lista = new List<List<string>> ();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CGruposEmpresas_Retrieve_EmpresasGrupo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlGrupo_id", Grupo_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        List<string> list = new List<string>();
                        list.Add(data["IdEmpresa"].ToString());
                        list.Add(data["NombreEmpresa"].ToString());
                        lista.Add(list);
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
            return lista;
        }
        public List<string> sp_CPoliticasVacaciones_Update_Politica(int Empresa_id, string Effdt, int Anio, int Dias, int Diasa, int Prima, int Anion)
        {
            List<string> lista = new List<string> ();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CPoliticasVacaciones_Update_Politica", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlEffdt", Effdt));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", Anio));
                cmd.Parameters.Add(new SqlParameter("@ctrlDias", Dias));
                cmd.Parameters.Add(new SqlParameter("@ctrlDiasa", Diasa));
                cmd.Parameters.Add(new SqlParameter("@ctrlPrima", Prima));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnion", Anion));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        lista.Add(data["iFlag"].ToString());
                        lista.Add(data["sMensaje"].ToString());
                        
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
            return lista;
        }
        public List<InicioFechasPeriodoBean> sp_CInicio_Fechas_Periodo_Retrieve_Periodo(int Empresa_id, int Id)
        {
            List<InicioFechasPeriodoBean> listBean = new List<InicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CInicio_Fechas_Periodo_Retrieve_Periodo", this.conexion)
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
                        InicioFechasPeriodoBean Bean = new InicioFechasPeriodoBean();
                        Bean.id = data["Id"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.Anio = data["Anio"].ToString();
                        Bean.Tipo_Periodo_Id = data["Tipo_Periodo_Id"].ToString();
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
        public List<string> sp_CInicio_Fechas_Periodo_Update_Periodo(int Empresa_id,int Id, int inano, int inperiodo, string infinicio, string inffinal, string infproceso, string infpago, int indiaspago)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CInicio_Fechas_Periodo_Update_Periodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlId", Id));
                cmd.Parameters.Add(new SqlParameter("@ctrlAno", inano));
                cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", inperiodo));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaInicio", infinicio));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaFinal", inffinal));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaProceso", infproceso));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaPago", infpago));
                cmd.Parameters.Add(new SqlParameter("@ctrlDias", indiaspago));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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

        public List<TabBancosEmpresas> sp_BancosEmpresas_Retrieve_Bancos(int Empresa_id)
        {
            List<TabBancosEmpresas> listBean = new List<TabBancosEmpresas>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_BancosEmpresas_Retrieve_Bancos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TabBancosEmpresas Bean = new TabBancosEmpresas();
                        Bean.idBanco_Emp = data["idBanco_Emp"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.Banco_id = data["Banco_id"].ToString();
                        Bean.Descripcion = data["Descripcion"].ToString();
                        Bean.Num_cliente = data["Num_cliente"].ToString();
                        Bean.Plaza = data["Plaza"].ToString();
                        Bean.Num_Cta_Empresa = data["Num_Cta_Empresa"].ToString();
                        Bean.Clabe = data["Clabe"].ToString();
                        Bean.tipo_banco_id = data["tipo_banco_id"].ToString();
                        Bean.tipo_banco = data["tipo_banco"].ToString();
                        Bean.Cancelado = data["Cancelado"].ToString();
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
        public List<string> sp_BancosEmpresas_Insert_Banco(int Empresa_id, int Banco_id, int TipoBanco, int Cliente, int Plaza, string CuentaEmp, string Clabe)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_BancosEmpresas_Insert_Banco", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlBanco_id", Banco_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoBanco_id", TipoBanco));
                cmd.Parameters.Add(new SqlParameter("@ctrlCliente", Cliente));
                cmd.Parameters.Add(new SqlParameter("@ctrlPlaza", Plaza));
                cmd.Parameters.Add(new SqlParameter("@ctrlCuentaEmp", CuentaEmp));
                cmd.Parameters.Add(new SqlParameter("@ctrlClabe", Clabe));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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
        public List<string> sp_BancosEmpresas_updatebanco_Banco(int Banco_id, int TipoBanco, int Id, int Cliente, int Plaza, string CuentaEmp, string Clabe)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_BancosEmpresas_updatebanco_Banco", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlBanco_id", Banco_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoBanco_id", TipoBanco));
                cmd.Parameters.Add(new SqlParameter("@ctrlId", Id));
                cmd.Parameters.Add(new SqlParameter("@ctrlCliente", Cliente));
                cmd.Parameters.Add(new SqlParameter("@ctrlPlaza", Plaza));
                cmd.Parameters.Add(new SqlParameter("@ctrlCuentaEmp", CuentaEmp));
                cmd.Parameters.Add(new SqlParameter("@ctrlClabe", Clabe));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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
        public List<string> sp_BancosEmpresas_updatestatus_Banco(int key, int Id)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_BancosEmpresas_updatestatus_Banco", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlKey", key));
                cmd.Parameters.Add(new SqlParameter("@ctrlId", Id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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
        public List<DataUsersBean> sp_CUsuarios_Retrieve_Users()
        {
            List<DataUsersBean> listBean = new List<DataUsersBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CUsuarios_Retrieve_Users", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlKey", key));
                //cmd.Parameters.Add(new SqlParameter("@ctrlId", Id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DataUsersBean list = new DataUsersBean(); 
                        list.IdUsuario = data["IdUsuario"].ToString();
                        list.Usuario = data["Usuario"].ToString();
                        list.Perfil_id = data["Perfil_id"].ToString();
                        list.Ps = data["Password"].ToString();
                        list.Cancelado = data["Cancelado"].ToString();
                        list.Alta_por = data["Alta_por"].ToString();
                        list.Fecha_Alta = data["FechaAlta"].ToString();
                        listBean.Add(list);
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
        public List<DataProfilesBean> sp_CPerfiles_Retrieve_Perfiles()
        {
            List<DataProfilesBean> listBean = new List<DataProfilesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CPerfiles_Retrieve_Perfiles", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlKey", key));
                //cmd.Parameters.Add(new SqlParameter("@ctrlId", Id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DataProfilesBean list = new DataProfilesBean();
                        list.IdPerfil = data["IdPerfil"].ToString();
                        list.Perfil = data["Perfil"].ToString();
                        list.Cancelado = data["Cancelado"].ToString();
                        list.Alta_por = data["Usuario"].ToString();
                        list.Fecha_Alta = data["FechaAlta"].ToString();
                        listBean.Add(list);
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
        // FUNCION PARA RETORNAR EL DETALLE DE LOS CENTROS DE COSTO
        public List<DataCentrosCosto> sp_TCentrosCostos_Retrieve_CentrosCostoxEmpresa( int Empresa_id)
        {
            List<DataCentrosCosto> listBean = new List<DataCentrosCosto>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TCentrosCostos_Retrieve_CentrosCostoxEmpresa", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DataCentrosCosto Bean = new DataCentrosCosto();

                        Bean.IdCentroCosto = data["IdCentroCosto"].ToString();
                        Bean.Empresa_id = data["Empresa_id"].ToString();
                        Bean.NombreEmpresa = data["NombreEmpresa"].ToString();
                        Bean.CentroCosto = data["CentroCosto"].ToString();
                        Bean.Descripcion = data["Descripcion"].ToString();
                        Bean.Estado = data["Estado"].ToString();
                        Bean.Fecha_Alta = data["Fecha_Alta"].ToString();

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
        // FUNCION PARA TRAER DE Cgeneral LOS TIPOS DE DESCUENTO
        public List<TipoDescuentoBean> sp_TipoDescuento_Retrieve_TipoDescuentos()
        {
            List<TipoDescuentoBean> Bean = new List<TipoDescuentoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TipoDescuento_Retrieve_TipoDescuentos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlCatalogoId", catalogid));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TipoDescuentoBean list = new TipoDescuentoBean();
                        list.Id = data["id"].ToString();
                        list.Nombre = data["Valor"].ToString();
                        list.Descripcion = data["Descripcion"].ToString();
                        Bean.Add(list);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "CatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return Bean;
        }
        public List<string> sp_Catalogos_Insert_Centro_Costo(int Empresa_id, string Nombre, string Descripcion, int Usuario_id)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Catalogos_Insert_Centro_Costo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlNombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario_id", Usuario_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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
        public List<string> sp_CRegionales_Insert_Regional(int Empresa_id, string ClaveRegion, string Descripcion, int Usuario_id)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CRegionales_Insert_Regional", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@ctrlClaveRegion", ClaveRegion));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario_id", Usuario_id));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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
        public List<string> sp_TRegistroPatronal_update_Status(int Empresa_id, int RegPat_id, int Status)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TRegistroPatronal_update_Status", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlRegPat_id", RegPat_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlStatus", Status));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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
        public List<string> sp_TRegistroPatronal_insert_RegistroPatronal(int Empresa_id, string Afiliacion_IMSS, string NombreAfiliacion, string RiesgoTrabajo, int Clase)
        {
            List<string> listBean = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TRegistroPatronal_insert_RegistroPatronal", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@ctrlAfiliacion", Afiliacion_IMSS));
                cmd.Parameters.Add(new SqlParameter("@ctrlNombreAfiliacion", NombreAfiliacion));
                cmd.Parameters.Add(new SqlParameter("@ctrlRiesgoTrabajo", RiesgoTrabajo));
                cmd.Parameters.Add(new SqlParameter("@ctrlClase", Clase));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listBean.Add(data["iFlag"].ToString());
                        listBean.Add(data["sMensaje"].ToString());
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

        /// List Tipo de renglon Consulta
        public List<TipoRenglonBean> sp_TipoRenglon_Retrieve_TipoRenlgon()
        {
            List<TipoRenglonBean> list = new List<TipoRenglonBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TipoRenglon_Retrieve_TipoRenlgon", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TipoRenglonBean ls = new TipoRenglonBean();
                        ls.iIdRenglon = int.Parse(data["Id"].ToString());
                        ls.sTipoRenglon = data["Descripcion"].ToString();
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }


        /// List Elemento Nomina
        public List<ElementoNominaBean> sp_CElemntoNomina_Retrieve_Cgeneral()
        {
            List<ElementoNominaBean> list = new List<ElementoNominaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CElemntoNomina_Retrieve_Cgeneral", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ElementoNominaBean ls = new ElementoNominaBean();
                        ls.iIdValor = int.Parse(data["IdValor"].ToString());
                        ls.sValor = data["Valor"].ToString();
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }


        /// List Calculo
        public List<ListaCalculoBean> sp_ListCalculo_Retrieve_ClistaCalculo()
        {
            List<ListaCalculoBean> list = new List<ListaCalculoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ListCalculo_Retrieve_ClistaCalculo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ListaCalculoBean ls = new ListaCalculoBean();
                        ls.iIdCalculo = int.Parse(data["Id"].ToString());
                        ls.sNombreCalculo = data["Descripcion"].ToString();
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        //Acumulado Renglones
        public List<CRenglonesBean> sp_Acumulados_Retrieve_CRenglones(int CtrliIdEmpresa, int CtrliIdElemto)
        {
            List<CRenglonesBean> list = new List<CRenglonesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Acumulados_Retrieve_CRenglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdElemto", CtrliIdElemto));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CRenglonesBean ls = new CRenglonesBean();
                        {
                            ls.iIdRenglon = int.Parse(data["IdRenglon"].ToString());
                            ls.sNombreRenglon = data["Nombre_Renglon"].ToString();
                            ls.sMensaje = "success";
                        };


                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        // Lista sat
        public List<ListSatBean> sp_ListSat_Retrieve_CSatRenglones()
        {
            List<ListSatBean> list = new List<ListSatBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ListSat_Retrieve_CSatRenglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ListSatBean ls = new ListSatBean();
                        ls.idSat = int.Parse(data["IdSat"].ToString());
                        ls.sSat = data["Descripcion"].ToString();
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        // Lista Reporte
        public List<SeccionReporte>sp_SeccionReporte_Retrieve_Cgeneral()
        {
            List<SeccionReporte> list = new List<SeccionReporte>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_SeccionReporte_Retrieve_Cgeneral", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        SeccionReporte ls = new SeccionReporte();
                        ls.iIdValor = int.Parse(data["IdValor"].ToString());
                        ls.SNombreReporte = data["Valor"].ToString();
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public CRenglonesBean ps_Renglon_Insert_CRenglones(int CtrliIdEmpresa,int CtrliIdRenglon,
            string CtrlsNomRenglon, int CtrliElemtoNom,int CtrliIdReporte,int CtrliAcumulado,
            int CtrliCancelado,int CtrliTipoRenglon,int CtrliEspejo,
            int CtrliLisCalculo, string CtrlsCuntCont, string CtrlsDesCuen,
            string CtrlsCarAbo,int CtrliIdSAT,int CtrliPenAlim)
        {


            CRenglonesBean bean = new CRenglonesBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("ps_Renglon_Insert_CRenglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdRenglon", CtrliIdRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrlsNomRenglon", CtrlsNomRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrliElemtoNom", CtrliElemtoNom));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdReporte", CtrliIdReporte));
                cmd.Parameters.Add(new SqlParameter("@CtrliAcumulado", CtrliAcumulado));
                cmd.Parameters.Add(new SqlParameter("@CtrliCancelado", CtrliCancelado));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoRenglon", CtrliTipoRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrliEspejo", CtrliEspejo));
                cmd.Parameters.Add(new SqlParameter("@CtrliLisCalculo", CtrliLisCalculo));
                cmd.Parameters.Add(new SqlParameter("@CtrlsCuntCont", CtrlsCuntCont));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDesCuen", CtrlsDesCuen));
                cmd.Parameters.Add(new SqlParameter("@CtrlsCarAbo", CtrlsCarAbo));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdSAT", CtrliIdSAT));
                cmd.Parameters.Add(new SqlParameter("@CtrliPenAlim", CtrliPenAlim));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public CRenglonesBean ps_Renglon_Update_CRenglones(int CtrliIdEmpresa, int CtrliIdRenglon,
         string CtrlsNomRenglon, int CtrliIdReporte, int CtrliAcumulado,
         int CtrliCancelado, int CtrliEspejo,
         string CtrlsCuntCont, string CtrlsDesCuen,
         string CtrlsCarAbo, int CtrliIdSAT, int CtrliPenAlim)
        {


            CRenglonesBean bean = new CRenglonesBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("ps_Renglon_Update_CRenglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdRenglon", CtrliIdRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrlsNomRenglon", CtrlsNomRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdReporte", CtrliIdReporte));
                cmd.Parameters.Add(new SqlParameter("@CtrliAcumulado", CtrliAcumulado));
                cmd.Parameters.Add(new SqlParameter("@CtrliCancelado", CtrliCancelado));
                cmd.Parameters.Add(new SqlParameter("@CtrliEspejo", CtrliEspejo));
                cmd.Parameters.Add(new SqlParameter("@CtrlsCuntCont", CtrlsCuntCont));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDesCuen", CtrlsDesCuen));
                cmd.Parameters.Add(new SqlParameter("@CtrlsCarAbo", CtrlsCarAbo));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdSAT", CtrliIdSAT));
                cmd.Parameters.Add(new SqlParameter("@CtrliPenAlim", CtrliPenAlim));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

    }
}