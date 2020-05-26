using Payroll.Models.Utilerias;
using Payroll.Models.Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Payroll.Models.Daos
{
    public class ModCatalogosDao : Conexion
    {
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
    }
}