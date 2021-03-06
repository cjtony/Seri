﻿using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Xml;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices.ComTypes;

namespace Payroll.Models.Daos
{
    public class ListTablesDao { }


    public class ListEmpleadosDao : Conexion
    {

        public List<EmpleadosBean> sp_Empleados_Retrieve_Search_Empleados(int keyemp, string wordsearch, string filtered)
        {
            List<EmpleadosBean> listEmpleadosBean = new List<EmpleadosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Retrieve_Search_Empleados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlWordSearch", wordsearch));
                cmd.Parameters.Add(new SqlParameter("@ctrlFiltered", filtered));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosBean empleadoBean = new EmpleadosBean();
                        empleadoBean.iIdEmpleado = Convert.ToInt32(data["IdEmpleado"].ToString());
                        empleadoBean.sNombreEmpleado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data["Nombre_Empleado"].ToString() + " " + data["Apellido_Paterno_Empleado"].ToString() + " " + data["Apellido_Materno_Empleado"].ToString());
                        empleadoBean.iNumeroNomina = Convert.ToInt32(data["IdEmpleado"].ToString());
                        listEmpleadosBean.Add(empleadoBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ListTablesdao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listEmpleadosBean;
        }
        public List<EmpleadosBean> sp_Empleados_Retrieve_Empleados(int keyemp)
        {
            List<EmpleadosBean> listEmpleadosBean = new List<EmpleadosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Retrieve_Empleados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosBean empleadoBean = new EmpleadosBean();
                        empleadoBean.iIdEmpleado = Convert.ToInt32(data["IdEmpleado"].ToString());
                        empleadoBean.sNombreEmpleado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data["NombreEmpleado"].ToString() + " " + data["ApellidoPaternoEmpleado"].ToString() + " " + data["ApellidoMaternoEmpleado"].ToString());
                        empleadoBean.iNumeroNomina = Convert.ToInt32(data["NumeroNomina"].ToString());
                        listEmpleadosBean.Add(empleadoBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ListTablesdao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listEmpleadosBean;
        }
        public EmpleadosBean sp_Empleados_Retrieve_Empleado(int keyemploye, int keybusiness)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Retrieve_Empleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyemploye));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keybusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    empleadoBean.iIdEmpleado = Convert.ToInt32(data["IdEmpleado"].ToString());
                    empleadoBean.sNombreEmpleado = (String.IsNullOrEmpty(data["Nombre_Empleado"].ToString())) ? "" : data["Nombre_Empleado"].ToString();
                    empleadoBean.sApellidoPaterno = (String.IsNullOrEmpty(data["Apellido_Paterno_Empleado"].ToString())) ? "" : data["Apellido_Paterno_Empleado"].ToString();
                    empleadoBean.sApellidoMaterno = (String.IsNullOrEmpty(data["Apellido_Materno_Empleado"].ToString())) ? "" : data["Apellido_Materno_Empleado"].ToString();
                    empleadoBean.sFechaNacimiento = (String.IsNullOrEmpty(data["Fecha_Nacimiento_Empleado"].ToString())) ? "" : Convert.ToDateTime(data["Fecha_Nacimiento_Empleado"]).ToString("yyyy-MM-dd");
                    empleadoBean.sLugarNacimiento = (String.IsNullOrEmpty(data["Lugar_Nacimiento_Empleado"].ToString())) ? "" : data["Lugar_Nacimiento_Empleado"].ToString();
                    if (data["Cg_Titulo_id"].ToString().Length != 0)
                    {
                        empleadoBean.iTitulo_id = Convert.ToInt32(data["Cg_Titulo_id"].ToString());
                    }
                    else
                    {
                        empleadoBean.iTitulo_id = 0;
                    }
                    if (data["Cg_Genero_Empleado_id"].ToString().Length != 0)
                    {
                        empleadoBean.iGeneroEmpleado = Convert.ToInt32(data["Cg_Genero_Empleado_id"].ToString());
                    }
                    else
                    {
                        empleadoBean.iGeneroEmpleado = 0;
                    }
                    if (data["Nacionalidad_id"].ToString().Length != 0)
                    {
                        empleadoBean.iNacionalidad = Convert.ToInt32(data["Nacionalidad_id"].ToString());
                    }
                    else
                    {
                        empleadoBean.iNacionalidad = 0;
                    }
                    if (data["Cg_EstadoCivil_Empleado_id"].ToString().Length != 0)
                    {
                        empleadoBean.iEstadoCivil = Convert.ToInt32(data["Cg_EstadoCivil_Empleado_id"].ToString());
                    }
                    else
                    {
                        empleadoBean.iEstadoCivil = 0;
                    }
                    empleadoBean.sCodigoPostal = (String.IsNullOrEmpty(data["Codigo_Postal"].ToString())) ? "" : data["Codigo_Postal"].ToString();
                    if (data["Cg_Estado_id"].ToString().Length != 0)
                    {
                        empleadoBean.iEstado_id = Convert.ToInt32(data["Cg_Estado_id"].ToString());
                    }
                    else
                    {
                        empleadoBean.iEstado_id = 0;
                    }
                    empleadoBean.sCiudad = (String.IsNullOrEmpty(data["Ciudad"].ToString())) ? "" : data["Ciudad"].ToString();
                    empleadoBean.sColonia = (String.IsNullOrEmpty(data["Colonia"].ToString())) ? "" : data["Colonia"].ToString();
                    empleadoBean.sCalle = (String.IsNullOrEmpty(data["Calle"].ToString())) ? "" : data["Calle"].ToString();
                    empleadoBean.sNumeroCalle = (String.IsNullOrEmpty(data["Numero_Calle"].ToString())) ? "S/N" : data["Numero_Calle"].ToString();
                    empleadoBean.sTelefonoFijo = (String.IsNullOrEmpty(data["Telefono_Fijo"].ToString())) ? "" : data["Telefono_Fijo"].ToString();
                    empleadoBean.sTelefonoMovil = (String.IsNullOrEmpty(data["Telefono_Movil"].ToString())) ? "" : data["Telefono_Movil"].ToString();
                    empleadoBean.sCorreoElectronico = (String.IsNullOrEmpty(data["Correo_Electronico"].ToString())) ? "" : data["Correo_Electronico"].ToString();
                    empleadoBean.sFechaMatrimonio = (String.IsNullOrEmpty(data["Fecha_Matrimonio"].ToString())) ? "" : Convert.ToDateTime(data["Fecha_Matrimonio"]).ToString("yyyy-MM-dd");
                    empleadoBean.sTipoSangre = (String.IsNullOrEmpty(data["Tipo_Sangre"].ToString())) ? "" : data["Tipo_Sangre"].ToString();
                    empleadoBean.sMensaje = "success";
                }
                else
                {
                    empleadoBean.sMensaje = "ERRDB";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                empleadoBean.sMensaje = exc.Message.ToString();
                string origenerror = "ListTablesdao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return empleadoBean;
        }
        public ImssBean sp_Imss_Retrieve_ImssEmpleado(int keyemploye, int keyemp)
        {
            ImssBean imssBean = new ImssBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Imss_Retrieve_ImssEmpleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyemploye));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    imssBean.iIdImss = Convert.ToInt32(data["IdImss"].ToString());
                    imssBean.iEmpleado_id = Convert.ToInt32(data["Empleado_id"].ToString());
                    imssBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                    // (String.IsNullOrEmpty(data["Effdt"].ToString())) ? "" : data["Effdt"].ToString();
                    imssBean.sFechaEfectiva = Convert.ToDateTime(data["Effdt"].ToString()).ToString("yyyy-MM-dd");
                    imssBean.sRegistroImss = (String.IsNullOrEmpty(data["RegistroImss"].ToString())) ? "" : data["RegistroImss"].ToString();
                    imssBean.sRfc = (String.IsNullOrEmpty(data["RFC"].ToString())) ? "" : data["RFC"].ToString();
                    imssBean.sCurp = (String.IsNullOrEmpty(data["CURP"].ToString())) ? "" : data["CURP"].ToString();
                    if (data["Cg_NivelEstudio_id"].ToString().Length != 0) {
                        imssBean.iNivelEstudio_id = Convert.ToInt32(data["Cg_NivelEstudio_id"].ToString());
                    } else {
                        imssBean.iNivelEstudio_id = 0;
                    }
                    if (data["Cg_NivelSocioeconomico_id"].ToString().Length != 0) {
                        imssBean.iNivelSocioeconomico_id = Convert.ToInt32(data["Cg_NivelSocioeconomico_id"].ToString());
                    } else {
                        imssBean.iNivelSocioeconomico_id = 0;
                    }
                    imssBean.sMensaje = "success";
                }
                else {
                    imssBean.sMensaje = "ERRDB";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            } catch (Exception exc) {
                imssBean.sMensaje = exc.Message.ToString();
                string origenerror = "ListTablesdao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return imssBean;
        }
        public DatosNominaBean sp_Nominas_Retrieve_NominaEmpleado(int keyemploye, int keyemp)
        {
            DatosNominaBean nominaBean = new DatosNominaBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Nominas_Retrieve_NominaEmpleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyemploye));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    nominaBean.iIdNomina = Convert.ToInt32(data["IdNomina"].ToString());
                    nominaBean.iEmpleado_id = Convert.ToInt32(data["Empleado_id"].ToString());
                    nominaBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                    nominaBean.sFechaEfectiva = Convert.ToDateTime(data["Effdt"].ToString()).ToString("yyyy-MM-dd");
                    nominaBean.dSalarioMensual = Convert.ToDouble(data["SalarioMensual"].ToString());
                    if (data["Cg_TipoEmpleado_id"].ToString().Length != 0)
                    {
                        nominaBean.iTipoEmpleado_id = Convert.ToInt32(data["Cg_TipoEmpleado_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iTipoEmpleado_id = 0;
                    }
                    if (data["Cg_NivelEmpleado_id"].ToString().Length != 0)
                    {
                        nominaBean.iNivelEmpleado_id = Convert.ToInt32(data["Cg_NivelEmpleado_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iNivelEmpleado_id = 0;
                    }
                    if (data["Cg_TipoJornada_id"].ToString().Length != 0)
                    {
                        nominaBean.iTipoJornada_id = Convert.ToInt32(data["Cg_TipoJornada_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iTipoJornada_id = 0;
                    }
                    if (data["Cg_TipoContrato_id"].ToString().Length != 0)
                    {
                        nominaBean.iTipoContrato_id = Convert.ToInt32(data["Cg_TipoContrato_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iTipoContrato_id = 0;
                    }
                    if (data["Cg_TipoContratacion_id"].ToString().Length != 0)
                    {
                        nominaBean.iTipoContratacion_id = Convert.ToInt32(data["Cg_TipoContratacion_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iTipoContratacion_id = 0;
                    }
                    if (data["Cg_MotivoIncremento_id"].ToString().Length != 0)
                    {
                        nominaBean.iMotivoIncremento_id = Convert.ToInt32(data["Cg_MotivoIncremento_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iMotivoIncremento_id = 0;
                    }
                    nominaBean.sFechaIngreso = Convert.ToDateTime(data["Effdt"].ToString()).ToString("yyyy-MM-dd");
                    nominaBean.sFechaAntiguedad = Convert.ToDateTime(data["Effdt"].ToString()).ToString("yyyy-MM-dd");
                    nominaBean.sVencimientoContrato = Convert.ToDateTime(data["Effdt"].ToString()).ToString("yyyy-MM-dd");
                    if (data["Posicion_id"].ToString().Length != 0)
                    {
                        nominaBean.iPosicion_id = Convert.ToInt32(data["Posicion_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iPosicion_id = 0;
                    }
                    if (data["Cg_tipoPago_id"].ToString().Length != 0)
                    {
                        nominaBean.iTipoPago_id = Convert.ToInt32(data["Cg_tipoPago_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iTipoPago_id = 0;
                    }
                    if (data["Banco_id"].ToString().Length != 0)
                    {
                        nominaBean.iBanco_id = Convert.ToInt32(data["Banco_id"].ToString());
                    }
                    else
                    {
                        nominaBean.iBanco_id = 0;
                    }
                    nominaBean.sCuentaCheques = (String.IsNullOrEmpty(data["Cta_Cheques"].ToString())) ? "" : data["Cta_Cheques"].ToString();
                    nominaBean.iUsuarioAlta_id = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                    nominaBean.sFechaAlta = data["Fecha_Alta"].ToString();
                    nominaBean.sMensaje = "success";
                }
                else
                {
                    nominaBean.sMensaje = "ERRDB";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                nominaBean.sMensaje = exc.Message.ToString();
                string origenerror = "ListTablesdao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return nominaBean;
        }
        public DatosPosicionesBean sp_Posiciones_Retrieve_PosicionEmpleado(int keyemploye, int keyemp)
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Retrieve_PosicionEmpleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyemploye));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    //posicionBean.iIdPosicionAsig = Convert.ToInt32(data["IdPosicion_Asig"].ToString());
                    posicionBean.iIdPosicion = Convert.ToInt32(data["IdPosicion"].ToString());
                    posicionBean.sPosicionCodigo = data["PosCode1"].ToString();
                    posicionBean.sFechaEffectiva = Convert.ToDateTime(data["Effdt"].ToString()).ToString("yyyy-MM-dd");
                    //posicionBean.sFechaInicio = Convert.ToDateTime(data["Fecha_Inicio_Asign"].ToString()).ToString("yyyy-MM-dd");
                    posicionBean.iPuesto_id = Convert.ToInt32(data["Puesto_id"].ToString());
                    posicionBean.sNombrePuesto = data["NombrePuesto"].ToString();
                    posicionBean.sPuestoCodigo = data["PuestoCodigo"].ToString();
                    posicionBean.iDepartamento_id = Convert.ToInt32(data["Departamento_id"].ToString());
                    posicionBean.sDeptoCodigo = data["Depto_Codigo"].ToString();
                    posicionBean.sNombreDepartamento = data["DescripcionDepartamento"].ToString();
                    posicionBean.iIdLocalidad = Convert.ToInt32(data["Localidad_id"].ToString());
                    posicionBean.sLocalidad = data["Descripcion"].ToString();
                    posicionBean.iIdReportaAPosicion = Convert.ToInt32(data["Reporta_A_Posicion_id"].ToString());
                    //posicionBean.sCodRepPosicion = data["PosCode2"].ToString();
                    posicionBean.iIdRegistroPat = Convert.ToInt32(data["RegPat_id"].ToString());
                    posicionBean.sRegistroPat = data["Afiliacion_IMSS"].ToString();
                    posicionBean.iIdReportaAEmpresa = Convert.ToInt32(data["Reporta_A_Empresa"].ToString());
                    posicionBean.sNombreEmrpesaRepo = data["NombreEmpresa"].ToString();
                    posicionBean.sMensaje = "success";
                }
                else
                {
                    posicionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "ListTablesdao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return posicionBean;
        }
        public List<EmpleadosEmpresaBean> sp_EmpleadosDEmpresa_Retrieve_EmpleadosDEmpresa(int CtrliIdEmpresa,int CtrliTipoPeriodo,int CtrliPeriodo)
        {
            List<EmpleadosEmpresaBean> list = new List<EmpleadosEmpresaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EmpleadosDEmpresa_Retrieve_EmpleadosDEmpresa", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoPeriodo", CtrliTipoPeriodo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosEmpresaBean ls = new EmpleadosEmpresaBean();

                        ls.iIdEmpleado = int.Parse(data["IdEmpleado"].ToString());
                        ls.sNombreCompleto = data["Nombre_Empleado"].ToString();
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

        public List<EmisorReceptorBean> sp_EmisorReceptor_Retrieve_EmisorReceptor(int CrtliIdEmpresa, int CrtliIdEmpleado)
        {
            List<EmisorReceptorBean> list = new List<EmisorReceptorBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EmisorReceptor_Retrieve_EmisorReceptor", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpresa", CrtliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpleado", CrtliIdEmpleado));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmisorReceptorBean ls = new EmisorReceptorBean();
                        ls.sNombreEmpresa = data["RazonSocial"].ToString();
                        ls.sCalle = data["Calle"].ToString();
                        //ls.sColonia = data["Colonia"].ToString();
                        ls.sCiudad = data["Ciudad"].ToString();
                        ls.sRFC = data["RFC"].ToString();
                        ls.sAfiliacionIMSS = data["Afiliacion_IMSS"].ToString();
                        ls.sNombreComp = data["NombreComp"].ToString();
                        ls.sRFCEmpleado = data["RFCEmpleado"].ToString();
                        ls.iIdEmpleado = int.Parse(data["IdEmpleado"].ToString());
                        ls.sDescripcionDepartamento = data["DescripcionDepartamento"].ToString();
                        ls.sNombrePuesto = data["NombrePuesto"].ToString();
                        ls.sFechaIngreso = data["FechaIngreso"].ToString();
                        ls.sTipoContrato = data["TipoContrato"].ToString();
                        ls.sCentroCosto = data["CentroCosto"].ToString();
                        ls.dSalarioMensual = decimal.Parse(data["SalarioMensual"].ToString());
                        ls.sRegistroImss = data["RegistroImss"].ToString();
                        ls.sCURP = data["CURP"].ToString();
                        ls.sDescripcion = data["Descripcion"].ToString();
                        ls.sCtaCheques = data["Cta_Cheques"].ToString();
                        ls.iRegimenFiscal = int.Parse(data["Regimen_Fiscal_id"].ToString());
                        ls.iIdNomina = int.Parse(data["IdNomina"].ToString());

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

        public List<CInicioFechasPeriodoBean> sp_DatosPerido_Retrieve_DatosPerido(int CtrliIdPeriodo)
        {
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DatosPerido_Retrieve_DatosPerido", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdPeriodo", CtrliIdPeriodo));
                //cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                //cmd.Parameters.Add(new SqlParameter("@CtrliTipoPereriodo", CtrliTipoPereriodo));
                //cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean LP = new CInicioFechasPeriodoBean();
                        {
                            LP.iId = int.Parse(data["Id"].ToString());
                            LP.sNominaCerrada = data["Nomina_Cerrada"].ToString();
                            LP.sFechaInicio = data["Fecha_Inicio"].ToString();
                            LP.sFechaFinal = data["Fecha_Final"].ToString();
                            LP.sFechaProceso = data["Fecha_Proceso"].ToString();
                            LP.sFechaPago = data["Fecha_Pago"].ToString();
                            LP.iDiasEfectivos = int.Parse(data["Dias_Efectivos"].ToString());
                            LP.iPeriodo = int.Parse(data["Periodo"].ToString());
                        };

                        list.Add(LP);
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

        public List<XMLBean> sp_FileCer_Retrieve_CCertificados(string CtrlsRFC)
        {
            List<XMLBean> list = new List<XMLBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_FileCer_Retrieve_CCertificados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrlsRFC", CtrlsRFC));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        XMLBean Lxml = new XMLBean();
                        {
                            Lxml.sfilecer = data["file_cer"].ToString();
                            Lxml.sfilekey = data["file_key"].ToString();
                            Lxml.stransitorio = data["transitorio"].ToString();

                        };

                        list.Add(Lxml);
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

        public List<XMLBean> sp_ObtenFolioCCertificados_RetrieveUpdate_Ccertificados(string rfc)
        {
            List<XMLBean> list = new List<XMLBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ObtenFolioCCertificados_RetrieveUpdate_Ccertificados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@rfc", rfc));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        XMLBean Lxml = new XMLBean();
                        {
                            Lxml.ifolio = int.Parse(data["regresa"].ToString());

                        };

                        list.Add(Lxml);
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

        public List<ReciboNominaBean> sp_SaldosTotales_Retrieve_TPlantillasCalculos(int CtrlIdEmpresa, int CtrlIdEmpleado, int CtrlPeriodo)
        {
            List<ReciboNominaBean> list = new List<ReciboNominaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_SaldosTotales_Retrieve_TPlantillasCalculos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrlIdEmpresa", CtrlIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrlIdEmpleado", CtrlIdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@CtrlPeriodo", CtrlPeriodo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ReciboNominaBean ls = new ReciboNominaBean();
                        {
                            ls.iIdTipoPeriodo = int.Parse(data["Tipo_Periodo_id"].ToString());
                            ls.iIdCalculoshd = int.Parse(data["Calculos_Hd_id"].ToString());
                            ls.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            ls.dSaldo = decimal.Parse(data["Saldo"].ToString());
                            ls.sEspejo = data["es_espejo"].ToString();
                        }

                        list.Add(ls);
                    }

                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public HttpResponse response { get; }
        public List<EmisorReceptorBean> GXMLNOM(int IdEmpresa, string sNombreComple, string path, int Periodo, int anios, int Tipodeperido)
        {
            int IdCalcHD,iperiodo;
            List<string> NomArchXML = new List<string>();
            string[] Nombre = sNombreComple.Split(' ');
            string Idempleado = Nombre[0].ToString();
            int NumEmpleado = Convert.ToInt32(Idempleado.ToString());
            int id = int.Parse(Idempleado);
            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();
            ListDatEmisor = sp_EmisorReceptor_Retrieve_EmisorReceptor(IdEmpresa, id);
            List<ReciboNominaBean> LisTRecibo = new List<ReciboNominaBean>();
            FuncionesNomina Dao = new FuncionesNomina();

            string Prefijo = "cfdi";
            string Prefijo2 = "nomina12";
            string parametro1 = "http://www.w3.org/2001/XMLSchema-instance";
            string EspacioDeNombreNomina = "http://www.sat.gob.mx/nomina12";
            string parametro3 = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd";
            string EspacioDeNombre = "http://www.sat.gob.mx/cfd/3";
            string s_certificadoKey = ""; string s_certificadoCer = ""; string ArchivoXmlFile; string NomArch; string s_transitorio = "";

            int nRfcEmisor = 0;
            int nImporte = 1;
            int nCurpReceptor = 2;
            int nRfcReceptor = 3;
            int nFechaInicialPago = 4;
            int nFechaFinalPago = 5;
            int nFechaPago = 6;

            string Emisor;
            string EmisorRFC;
            string ReceptorCurp;
            string ReceptorRFC;
            string FileCadenaXslt;
            string sUsoCFDI;

            string sFechaInicialPago;
            string sFechaFinalPago;
            string sFechaPago;
            string sDiasEfectivos;
            string anoarchivo;

            if (ListDatEmisor.Count > 0)
            {
                Emisor = ListDatEmisor[0].sNombreEmpresa;
                EmisorRFC = ListDatEmisor[0].sRFC;
                ReceptorCurp = ListDatEmisor[0].sCURP;
                ReceptorRFC = ListDatEmisor[0].sRFCEmpleado;
                NomArch = "F";
                ArchivoXmlFile = path + NomArch;
                FileCadenaXslt = path + "cadenaoriginal_3_3.xslt";
                sUsoCFDI = "P01";
                var culture = CultureInfo.CreateSpecificCulture("es-MX");
                var styles = DateTimeStyles.None;
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = dt1;
                DateTime dt3 = dt1;
                string folio = "";
                List<CInicioFechasPeriodoBean> LFechaPerido = new List<CInicioFechasPeriodoBean>();
                LFechaPerido = sp_DatosPerido_Retrieve_DatosPerido(Periodo);
                iperiodo = LFechaPerido[0].iPeriodo;
                if (LFechaPerido[0].sMensaje == null)
                {

                    bool fechaValida = DateTime.TryParse(LFechaPerido[0].sFechaInicio, culture, styles, out dt1);
                    fechaValida = DateTime.TryParse(LFechaPerido[0].sFechaFinal, culture, styles, out dt2);
                    fechaValida = DateTime.TryParse(LFechaPerido[0].sFechaPago, culture, styles, out dt3);

                    sFechaInicialPago = String.Format("{0:yyyy-MM-dd}", dt1);
                    sFechaFinalPago = String.Format("{0:yyyy-MM-dd}", dt2);
                    sFechaPago = String.Format("{0:yyyy-MM-dd}", dt3);
                    anoarchivo = String.Format("{0:yyyy}", dt2);

                    List<ReciboNominaBean> ListTotales = new List<ReciboNominaBean>();
                    ListTotales = sp_SaldosTotales_Retrieve_TPlantillasCalculos(IdEmpresa, NumEmpleado, LFechaPerido[0].iPeriodo);
                    LisTRecibo = Dao.sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(IdEmpresa, id, LFechaPerido[0].iPeriodo);
                    IdCalcHD = LisTRecibo[0].iIdCalculoshd;
                    //Partidas
                    string tipoNom = " ";
                    string TotalPercepciones = " ";
                    string totalDeduciones = " ";
                    string totalRecibo = " ";
                    string SueldoDiario = " ";
                    string SuedoAgravado = " ";
                    if (ListTotales.Count > 0)
                    {
                        for (int i = 0; i < ListTotales.Count; i++)
                        {
                            if (ListTotales[i].sEspejo == "False") { tipoNom = "0"; }
                            if (ListTotales[i].sEspejo == "True") { tipoNom = "1"; }
                            if (ListTotales[i].iIdRenglon == 990) { TotalPercepciones = string.Format("{0:N2}", ListTotales[i].dSaldo); }
                            if (ListTotales[i].iIdRenglon == 1990) { totalDeduciones = string.Format("{0:N2}", ListTotales[i].dSaldo); }
                            if (ListTotales[i].iIdRenglon == 9999) { totalRecibo = string.Format("{0:N2}", ListTotales[i].dSaldo); }
                            if (ListTotales[i].iIdRenglon == 9992) { SueldoDiario = string.Format("{0:N2}", ListTotales[i].dSaldo); }
                            if (ListTotales[i].iIdRenglon == 9993) { SuedoAgravado = string.Format("{0:N2}", ListTotales[i].dSaldo); }
                        }
                        TotalPercepciones = TotalPercepciones.Replace(",", "");
                        totalDeduciones = totalDeduciones.Replace(",", "");
                        totalRecibo = totalRecibo.Replace(",", "");
                    }

                    //Antiguedad 
                    string sAntiguedad = "";
                    List<XMLBean> LisCer = new List<XMLBean>();
                    LisCer = sp_FileCer_Retrieve_CCertificados(EmisorRFC);

                    s_certificadoKey = path + LisCer[0].sfilekey;
                    s_certificadoCer = path + LisCer[0].sfilecer;
                    s_transitorio = LisCer[0].stransitorio;


                    System.Security.Cryptography.X509Certificates.X509Certificate CerSAT;
                    CerSAT = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(s_certificadoCer);
                    byte[] bcert = CerSAT.GetSerialNumber();
                    string CerNo = LibreriasFacturas.StrReverse((string)Encoding.UTF8.GetString(bcert));
                    byte[] CERT_SIS = CerSAT.GetRawCertData();

                    List<XMLBean> LFolio = new List<XMLBean>();
                    LFolio = sp_ObtenFolioCCertificados_RetrieveUpdate_Ccertificados(EmisorRFC);

                    if (LFolio != null) folio = LFolio[0].ifolio.ToString();
                    else ListDatEmisor[0].sMensaje = "Erro en Genera el folio Contacte a sistemas";
                    string sNombre = Nombre[1] + " " + Nombre[2] + " " + Nombre[3];
                    string sRegistroPatronal = ListDatEmisor[0].sAfiliacionIMSS;
                    string sNumSeguridadSocial = ListDatEmisor[0].sRegistroImss;
                    fechaValida = DateTime.TryParse(ListDatEmisor[0].sFechaIngreso, culture, styles, out dt3);
                    string sFechaInicioRelLaboral = String.Format("{0:yyyy-MM-dd}", dt3); ;
                    string ticontrato = "0" + ListDatEmisor[0].sTipoContrato;
                    string[] contrato = ticontrato.Split(' ');
                    string scontrato = contrato[0].ToString();
                    string sTipoContrato = scontrato;
                    string sNumEmpleado = Convert.ToString(ListDatEmisor[0].iIdEmpleado);
                    string sDepartamento = ListDatEmisor[0].sDescripcionDepartamento;
                    string sPuesto = ListDatEmisor[0].sNombrePuesto;
                    string sBanco = ListDatEmisor[0].sDescripcion;
                    string sCuentaBancaria = ListDatEmisor[0].sCtaCheques;
                    string sSalarioDiarioIntegrado = SueldoDiario;
                    string sNombreEmisor = ListDatEmisor[0].sNombreEmpresa;
                    string sRegimenFiscal = Convert.ToString(ListDatEmisor[0].iRegimenFiscal);

                    //Antiguedad

                    DateTime f1 = DateTime.Parse(sFechaInicioRelLaboral);
                    DateTime f2 = DateTime.Parse(sFechaFinalPago);
                    TimeSpan diferencia = f2.Subtract(f1);
                    sAntiguedad = "P" + ((int)(diferencia.Days / 7)).ToString() + "W";
                    StreamWriter writer;
                    XmlTextWriter xmlWriter;


                    // Nombre del archivo XML
                    int NoidCalhd = ListTotales[0].iIdCalculoshd;
                    NomArch = NomArch + NoidCalhd + "_CFDI_E16_F" + anoarchivo;
                    if (LFechaPerido[0].iPeriodo > 9)
                    {
                        NomArch = NomArch + ListTotales[0].iIdTipoPeriodo + "0" + LFechaPerido[0].iPeriodo + tipoNom + "_N" + ListDatEmisor[0].iIdNomina;
                    }
                    if (LFechaPerido[0].iPeriodo < 10)
                    {
                        NomArch = NomArch + ListTotales[0].iIdTipoPeriodo + "00" + LFechaPerido[0].iPeriodo + tipoNom + "_N" + ListDatEmisor[0].iIdNomina;
                    }

                    ArchivoXmlFile = ArchivoXmlFile + NomArch;
                    NomArchXML.Add(ArchivoXmlFile);

                    //Crear archivo XML
                    writer = File.CreateText(ArchivoXmlFile);
                    writer.Close();

                    //Preparar archivo
                    xmlWriter = new XmlTextWriter(ArchivoXmlFile, System.Text.Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteStartDocument();

                    //Insertar elementos
                    xmlWriter.WriteStartElement(Prefijo, "Comprobante", EspacioDeNombre);
                    xmlWriter.WriteAttributeString("xmlns", "xsi", null, parametro1);
                    xmlWriter.WriteAttributeString("xmlns", Prefijo2, null, EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("xsi", "schemaLocation", null, parametro3);
                    xmlWriter.WriteAttributeString("xmlns", Prefijo, null, EspacioDeNombre);
                    xmlWriter.WriteAttributeString("Version", "3.3");
                    xmlWriter.WriteAttributeString("Serie", "NOM");
                    xmlWriter.WriteAttributeString("Folio", folio);
                    string FechaEmision = DateTime.Now.ToString("s");
                    xmlWriter.WriteAttributeString("Fecha", FechaEmision);
                    xmlWriter.WriteAttributeString("Sello", "");
                    xmlWriter.WriteAttributeString("FormaPago", "99");
                    xmlWriter.WriteAttributeString("NoCertificado", CerNo);
                    string sCertificado = Convert.ToBase64String(CERT_SIS);
                    xmlWriter.WriteAttributeString("Certificado", sCertificado);
                    xmlWriter.WriteAttributeString("SubTotal", TotalPercepciones.ToString());
                    xmlWriter.WriteAttributeString("Descuento", totalDeduciones.ToString());
                    xmlWriter.WriteAttributeString("Moneda", "MXN");
                    xmlWriter.WriteAttributeString("Total", totalRecibo.ToString());
                    xmlWriter.WriteAttributeString("TipoDeComprobante", "N");
                    xmlWriter.WriteAttributeString("MetodoPago", "PUE");
                    xmlWriter.WriteAttributeString("LugarExpedicion", "04600");

                    xmlWriter.WriteStartElement(Prefijo, "Emisor", EspacioDeNombre);
                    xmlWriter.WriteAttributeString("Rfc", EmisorRFC);
                    xmlWriter.WriteAttributeString("Nombre", sNombreEmisor);
                    xmlWriter.WriteAttributeString("RegimenFiscal", sRegimenFiscal);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(Prefijo, "Receptor", EspacioDeNombre);
                    xmlWriter.WriteAttributeString("Rfc", ReceptorRFC);
                    xmlWriter.WriteAttributeString("Nombre", sNombre);
                    xmlWriter.WriteAttributeString("UsoCFDI", sUsoCFDI);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(Prefijo, "Conceptos", EspacioDeNombre);
                    xmlWriter.WriteStartElement(Prefijo, "Concepto", EspacioDeNombre);
                    xmlWriter.WriteAttributeString("ClaveProdServ", "84111505");
                    xmlWriter.WriteAttributeString("Cantidad", "1");
                    xmlWriter.WriteAttributeString("ClaveUnidad", "ACT");
                    xmlWriter.WriteAttributeString("Descripcion", "Pago de nómina");
                    xmlWriter.WriteAttributeString("Descuento", totalDeduciones.ToString());
                    xmlWriter.WriteAttributeString("ValorUnitario", TotalPercepciones.ToString());
                    xmlWriter.WriteAttributeString("Importe", TotalPercepciones.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(Prefijo, "Complemento", EspacioDeNombre);
                    xmlWriter.WriteStartElement(Prefijo2, "Nomina", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("Version", "1.2");
                    xmlWriter.WriteAttributeString("TipoNomina", "O");
                    xmlWriter.WriteAttributeString("FechaPago", sFechaPago);
                    xmlWriter.WriteAttributeString("FechaInicialPago", sFechaInicialPago);
                    xmlWriter.WriteAttributeString("FechaFinalPago", sFechaFinalPago);
                    // dias Efectivos 
                    decimal iTdias = LFechaPerido[0].iDiasEfectivos;
                    int TDias = 0;
                    string Dias = LisTRecibo[0].sNombre_Renglon;
                    sDiasEfectivos = Convert.ToString(iTdias);
                          
                      
                    if (Dias.Length > 7)
                    {
                        if (LisTRecibo[0].iIdRenglon == 0)
                        {
                            string[] dias = Dias.Split(':');
                            Dias = dias[1].ToString();
                            Dias = Dias.Replace("}", "");
                        }
                        else {
                            Dias = "0";
                        }
                        decimal DiasNo = Convert.ToDecimal(Dias);
                        iTdias = iTdias - DiasNo;
                        TDias = Convert.ToInt16(iTdias);
                        sDiasEfectivos = Convert.ToString(TDias);

                    }
                    xmlWriter.WriteAttributeString("NumDiasPagados", sDiasEfectivos.ToString());
                    xmlWriter.WriteAttributeString("TotalPercepciones", TotalPercepciones.ToString());
                    xmlWriter.WriteAttributeString("TotalDeducciones", totalDeduciones.ToString());
                    xmlWriter.WriteAttributeString("TotalOtrosPagos", "0.00");
                    xmlWriter.WriteAttributeString("xmlns", Prefijo2, null, EspacioDeNombreNomina);

                    xmlWriter.WriteStartElement(Prefijo2, "Emisor", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("RfcPatronOrigen", EmisorRFC);
                    xmlWriter.WriteAttributeString("RegistroPatronal", sRegistroPatronal);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(Prefijo2, "Receptor", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("Curp", ReceptorCurp);
                    xmlWriter.WriteAttributeString("NumSeguridadSocial", sNumSeguridadSocial);
                    xmlWriter.WriteAttributeString("FechaInicioRelLaboral", sFechaInicioRelLaboral);
                    xmlWriter.WriteAttributeString("Antigüedad", sAntiguedad);
                    xmlWriter.WriteAttributeString("TipoContrato", sTipoContrato);
                    xmlWriter.WriteAttributeString("Sindicalizado", "No");
                    xmlWriter.WriteAttributeString("TipoJornada", "06");
                    xmlWriter.WriteAttributeString("TipoRegimen", "02");
                    xmlWriter.WriteAttributeString("NumEmpleado", sNumEmpleado);
                    xmlWriter.WriteAttributeString("Departamento", sDepartamento);
                    xmlWriter.WriteAttributeString("Puesto", sPuesto);
                    xmlWriter.WriteAttributeString("RiesgoPuesto", "1");
                    xmlWriter.WriteAttributeString("PeriodicidadPago", "04");
                    if (sCuentaBancaria.Length >= 7 && sCuentaBancaria.Length < 18)
                    {
                        if (sBanco.Length > 0)
                        {
                            xmlWriter.WriteAttributeString("Banco", sBanco);
                            xmlWriter.WriteAttributeString("CuentaBancaria", sCuentaBancaria);
                        }
                        else {
                            xmlWriter.WriteAttributeString("Banco", "0");
                            xmlWriter.WriteAttributeString("CuentaBancaria", sCuentaBancaria);

                        }

                    }
                    else
                    if ((sCuentaBancaria.Length == 18))
                    {

                        xmlWriter.WriteAttributeString("CuentaBancaria", sCuentaBancaria);
                    }
                    xmlWriter.WriteAttributeString("SalarioBaseCotApor", SuedoAgravado);
                    xmlWriter.WriteAttributeString("SalarioDiarioIntegrado", SueldoDiario);
                    xmlWriter.WriteAttributeString("ClaveEntFed", "DIF");
                    xmlWriter.WriteEndElement();

                    // Percepciones
                    xmlWriter.WriteStartElement(Prefijo2, "Percepciones", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("TotalExento", "0.00");
                    xmlWriter.WriteAttributeString("TotalGravado", TotalPercepciones.ToString());
                    xmlWriter.WriteAttributeString("TotalSueldos", TotalPercepciones.ToString());
                    decimal Isr = 0;

                    if (LisTRecibo.Count > 0)
                    {
                        for (int i = 0; i < LisTRecibo.Count; i++)
                        {
                            if (LisTRecibo[i].iElementoNomina == 39)
                            {
                                string lengRenglon = "";
                                string ImporGra = string.Format("{0:N2}", LisTRecibo[i].dSaldo);
                                ImporGra = ImporGra.Replace(",", "");
                                string IdRenglon = Convert.ToString(LisTRecibo[i].iIdRenglon);
                                string concepto = LisTRecibo[i].sNombre_Renglon;
                                if (IdRenglon == "1")
                                {
                                    concepto = "Sueldo {" + sDiasEfectivos + " Dias}";
                                    lengRenglon = "001";
                                }
                                lengRenglon = "010";
                                int idReglontama = IdRenglon.Length;
                                if (idReglontama == 1) { IdRenglon = "00" + IdRenglon; };
                                if (idReglontama == 2) { IdRenglon = "0" + IdRenglon; };



                                xmlWriter.WriteStartElement(Prefijo2, "Percepcion", EspacioDeNombreNomina);
                                xmlWriter.WriteAttributeString("ImporteExento", "0.00");
                                xmlWriter.WriteAttributeString("TipoPercepcion", lengRenglon);
                                xmlWriter.WriteAttributeString("Clave", IdRenglon);
                                xmlWriter.WriteAttributeString("Concepto", concepto.ToString());
                                xmlWriter.WriteAttributeString("ImporteGravado", ImporGra.ToString());
                                xmlWriter.WriteEndElement();

                            }
                            if (LisTRecibo[i].iElementoNomina == 40)
                            {
                                string IdRenglon = Convert.ToString(LisTRecibo[i].iIdRenglon);
                                if (IdRenglon == "1001") { Isr = LisTRecibo[i].dSaldo; }
                            }
                        }

                    }
                    xmlWriter.WriteEndElement();

                    decimal Deduciones = Convert.ToDecimal(totalDeduciones.ToString());
                    string deduciones = string.Format("{0:N2}", Deduciones - Isr);
                    string isr = string.Format("{0:N2}", Isr);
                    isr = isr.Replace(",", "");
                    // Deducciones
                    xmlWriter.WriteStartElement(Prefijo2, "Deducciones", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("TotalImpuestosRetenidos", isr);
                    xmlWriter.WriteAttributeString("TotalOtrasDeducciones", deduciones);
                    if (LisTRecibo.Count > 0)
                    {
                        for (int i = 0; i < LisTRecibo.Count; i++)
                        {
                            if (LisTRecibo[i].iElementoNomina == 40)
                            {
                                string lengRenglon = "";
                                string ImporGra = string.Format("{0:N2}", LisTRecibo[i].dSaldo);
                                ImporGra = ImporGra.Replace(",", "");
                                string IdRenglon = Convert.ToString(LisTRecibo[i].iIdRenglon);
                                string concepto = LisTRecibo[i].sNombre_Renglon;


                                lengRenglon = "010";
                                int idReglontama = IdRenglon.Length;
                                if (idReglontama == 1) { IdRenglon = "00" + IdRenglon; };
                                if (idReglontama == 2) { IdRenglon = "0" + IdRenglon; };
                                if (idReglontama == 3) { lengRenglon = "100"; };
                                if (IdRenglon == "1001") { lengRenglon = "002"; }

                                xmlWriter.WriteStartElement(Prefijo2, "Deduccion", EspacioDeNombreNomina);
                                xmlWriter.WriteAttributeString("Importe", ImporGra.ToString());
                                xmlWriter.WriteAttributeString("TipoDeduccion", lengRenglon);
                                xmlWriter.WriteAttributeString("Clave", IdRenglon);
                                xmlWriter.WriteAttributeString("Concepto", concepto.ToString());
                                xmlWriter.WriteEndElement();

                            }

                        }

                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(Prefijo2, "OtrosPagos", EspacioDeNombreNomina);
                    xmlWriter.WriteStartElement(Prefijo2, "OtroPago", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("TipoOtroPago", "002");
                    xmlWriter.WriteAttributeString("Clave", "198");
                    xmlWriter.WriteAttributeString("Concepto", "Subsidio al Empleado");
                    xmlWriter.WriteAttributeString("Importe", string.Format("{0:0.00}", 0));
                    xmlWriter.WriteStartElement(Prefijo2, "SubsidioAlEmpleo", EspacioDeNombreNomina);
                    xmlWriter.WriteAttributeString("SubsidioCausado", string.Format("{0:0.00}", 0));
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();


                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    //Cerrar
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    Dao.sp_Tsellos_InsertUPdate_TSellosSat(0, IdCalcHD, IdEmpresa, NumEmpleado, anios, Tipodeperido, iperiodo, "Nomina", " ", " ", " ", " ", " ", " ");
                    //FileCadenaXslt = path + "cadenaoriginal_3_3.xslt";
                    string Cadena = LibreriasFacturas.GetCadenaOriginal(ArchivoXmlFile, FileCadenaXslt, path);
                    string selloDigitalOriginal = LibreriasFacturas.ObtenerSelloDigital(Cadena, s_certificadoKey, s_transitorio);
                    LibreriasFacturas.AplicarSelloDigital(selloDigitalOriginal, ArchivoXmlFile);
                    // Quitar el BOM
                    string line;
                    StreamReader sr = new StreamReader(ArchivoXmlFile);
                    StreamWriter sw = new StreamWriter(path + NomArch + ".xml", false, new UTF8Encoding(false));

                    //Read the first line of text
                    line = sr.ReadLine();

                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //write the line
                        //line = line.Replace("xmlns_nomina", "xmlns:nomina");
                        line = line.Replace("utf-8", "UTF-8");
                        sw.WriteLine(line);                //Read the next line
                        line = sr.ReadLine();
                    }

                    //close the file
                    sr.Close();
                    sw.Close();
                    File.Delete(ArchivoXmlFile); //Borra archivo temporal
                    // 1 
                    //DirectoryInfo dir = new DirectoryInfo(@"C:\reportes\");
                    // 2 
                    string nombreArchivoZip = "ZipXML.zip";
                    FileStream stream = new FileStream(path + nombreArchivoZip, FileMode.OpenOrCreate);
                    // 3 
                    ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
                    // 4 
                    System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(path);
                    FileInfo[] sourceFiles = directorio.GetFiles("*" + NomArch + ".xml");
                    foreach (FileInfo sourceFile in sourceFiles)
                    {
                        // 5 
                        Stream sourceStream = sourceFile.OpenRead();
                        // 6 
                        ZipArchiveEntry entry = archive.CreateEntry(sourceFile.Name);
                        // 7 
                        Stream zipStream = entry.Open();
                        // 8 
                        sourceStream.CopyTo(zipStream);
                        // 9 
                        zipStream.Close();
                        sourceStream.Close();
                    }

                    // 10 
                    archive.Dispose();
                    stream.Close();
                    // descargar zip             


                }


            }

            return ListDatEmisor;
        }

        public List<string> Archivo()
        {

            List<string> zips = new List<string>();

            return zips;
        }

    }


}