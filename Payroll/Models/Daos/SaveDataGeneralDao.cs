using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class SaveDataGeneralDao
    {

    }
    public class SavePuestosDao : Conexion
    {
        public PuestosBean sp_Puestos_Insert_Puestos(string regcodpuesto, string regpuesto, string regdescpuesto, int proffamily, int clasifpuesto, int regcolect, int nivjerarpuesto, int perfmanager, int tabpuesto, int usuario, int keyemp)
        {
            PuestosBean addPuestoBean = new PuestosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Puestos_Insert_Puesto", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPuesto", regcodpuesto.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlNombrePuesto", regpuesto.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescriPuesto", regdescpuesto.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlProfesionFamId", proffamily));
                cmd.Parameters.Add(new SqlParameter("@ctrlClasificacioId", clasifpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlColectivoId", regcolect));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelJerarId", nivjerarpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlPerfomanceId", perfmanager));
                cmd.Parameters.Add(new SqlParameter("@ctrlTabuladorId", tabpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuarioId", usuario));
                SqlCommand validate = new SqlCommand("sp_Puestos_Validate_Puesto", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                validate.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                validate.Parameters.Add(new SqlParameter("@ctrlCodigoPuesto", regcodpuesto.ToUpper()));
                SqlDataReader data = validate.ExecuteReader();
                if (data.Read())
                {
                    if (data["sRespuesta"].ToString() == "insert")
                    {
                        data.Close();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            addPuestoBean.sMensaje = "success";
                        }
                        else
                        {
                            addPuestoBean.sMensaje = "error";
                        }
                    }
                    else
                    {
                        addPuestoBean.sMensaje = data["sRespuesta"].ToString();
                    }
                }
                else
                {
                    addPuestoBean.sMensaje = data["sRespuesta"].ToString();
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return addPuestoBean;
        }
    }
    public class SaveDepartamentosDao : Conexion
    {
        public DepartamentosBean sp_Departamentos_Insert_Departamento(int keyemp, string regdepart, string descdepart, string nivestuc, string nivsuptxt, int edific, string piso, string ubicac, int centrcost, int reportaa, string dgatxt, string dirgentxt, string direjetxt, string diraretxt, int dirgen, int direje, int dirare, int usuario)
        {
            DepartamentosBean addDepartamentoBean = new DepartamentosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Departamentos_Insert_Departamento", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaId", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlDepartamento", regdepart.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", descdepart.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelEstructura", nivestuc));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelSuperior", nivsuptxt.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlEdificio", edific));
                cmd.Parameters.Add(new SqlParameter("@ctrlPiso", piso.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlUbicacion", ubicac.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlCentroCosto", centrcost));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaReporta", reportaa));
                cmd.Parameters.Add(new SqlParameter("@ctrlDGA", dgatxt.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlDirecGen", dirgentxt.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlDirecEje", direjetxt.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlDirecAre", diraretxt.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmprDGen", dirgen));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmprDEje", direje));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmprDAre", dirare));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                SqlCommand validate = new SqlCommand("sp_Departamentos_Validate_Departamento", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                validate.Parameters.Add(new SqlParameter("@ctrlEmpresaId", keyemp));
                validate.Parameters.Add(new SqlParameter("@ctrlDepartamento", regdepart.ToUpper()));
                SqlDataReader data = validate.ExecuteReader();
                if (data.Read())
                {
                    if (data["sRespuesta"].ToString() == "insert")
                    {
                        data.Close();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            addDepartamentoBean.sMensaje = "success";
                        }
                        else
                        {
                            addDepartamentoBean.sMensaje = "error";
                        }
                    }
                    else
                    {
                        addDepartamentoBean.sMensaje = data["sRespuesta"].ToString();
                    }
                }
                else
                {
                    addDepartamentoBean.sMensaje = data["sRespuesta"].ToString();
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return addDepartamentoBean;
        }
    }
    public class EmpleadosDao : Conexion
    {
        public EmpleadosBean sp_Empleados_Insert_Empleado(string name, string apepat, string apemat, int sex, int estciv, string fnaci, string lnaci, int title, string nacion, int state, string codpost, string city, string colony, string street, string numberst, string telfij, string telmov, string email, int usuario, int keyemp, string tipsan, string fecmat)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Insert_Empleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlNombre", name.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoPaterno", apepat.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoMaterno", apemat.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlSexo", sex));
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoCivil", estciv));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaNacimiento", fnaci));
                cmd.Parameters.Add(new SqlParameter("@ctrlLugarNacimiento", lnaci.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlTitulo", title));
                cmd.Parameters.Add(new SqlParameter("@ctrlNacionalidad", nacion));
                cmd.Parameters.Add(new SqlParameter("@ctrlEstado", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPostal", codpost));
                cmd.Parameters.Add(new SqlParameter("@ctrlCiudad", city.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlColonia", colony.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlCalle", street.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlNumeroCalle", numberst));
                cmd.Parameters.Add(new SqlParameter("@ctrlTelefonoFijo", telfij));
                cmd.Parameters.Add(new SqlParameter("@ctrlTelefonoMovil", telmov));
                cmd.Parameters.Add(new SqlParameter("@ctrlCorreoElectronico", email));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoSangre", tipsan));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaMatrimonio", fecmat));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlCommand validate = new SqlCommand("sp_Empleados_Validate_Empleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                validate.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                validate.Parameters.Add(new SqlParameter("@ctrlNombre", name.ToUpper()));
                validate.Parameters.Add(new SqlParameter("@ctrlApellidoPaterno", apepat.ToUpper()));
                validate.Parameters.Add(new SqlParameter("@ctrlApellidoMaterno", apemat.ToUpper()));
                SqlDataReader data = validate.ExecuteReader();
                if (data.Read())
                {
                    if (data["sRespuesta"].ToString() == "insert")
                    {
                        data.Close();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            empleadoBean.sMensaje = "success";
                        }
                        else
                        {
                            empleadoBean.sMensaje = "error";
                        }
                    }
                    else
                    {
                        empleadoBean.sMensaje = data["sRespuesta"].ToString();
                    }
                }
                else
                {
                    empleadoBean.sMensaje = data["sRespuesta"].ToString();
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return empleadoBean;
        }
        public EmpleadosBean sp_Empleado_Update_PosicionNomina(int clvemp, int keyemp)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleado_Update_PosicionNomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", clvemp));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    if (data["sRespuesta"].ToString() == "success")
                    {
                        empleadoBean.sMensaje = "Actualizado";
                    }
                    else
                    {
                        empleadoBean.sMensaje = data["sRespuesta"].ToString();
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return empleadoBean;
        }
    }
    public class ImssDao : Conexion
    {
        public ImssBean sp_Imss_Insert_Imss(string fecefe, string regimss, string rfc, string curp, int nivest, int nivsoc, int usuario, string empleado, string apepat, string apemat, string fechanaci, int keyemp)
        {
            ImssBean imssBean = new ImssBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Imss_Insert_Imss", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaEfectiva", fecefe));
                cmd.Parameters.Add(new SqlParameter("@ctrlRegistroImss", regimss));
                cmd.Parameters.Add(new SqlParameter("@ctrlRfc", rfc));
                cmd.Parameters.Add(new SqlParameter("@ctrlCurp", curp));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelEstudios", nivest));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelSocioeconomico", nivsoc));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado", empleado.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoP", apepat.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoM", apemat.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaNaci", fechanaci));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa", keyemp));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    imssBean.sMensaje = "success";
                }
                else
                {
                    imssBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return imssBean;
        }
    }
    public class DatosNominaDao : Conexion
    {
        public DatosNominaBean sp_DatosNomina_Insert_DatoNomina(string fecefecnom, double salmen, int tipemp, int nivemp, int tipjor, int tipcon, string fecing, string fecant, string vencon, string estats, int usuario, string empleado, string apepat, string apemat, string fechanaci, int keyemp, int tipper, int tipcontra, int tippag, int banuse, string cunuse, int position, int clvemp)
        {
            DatosNominaBean datoNominaBean = new DatosNominaBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DatosNomina_Insert_DatoNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaEfectiva", fecefecnom));
                cmd.Parameters.Add(new SqlParameter("@ctrlSalarioMensual", salmen));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo", tipper));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoEmpleado", tipemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelEmpleado", nivemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoJornada", tipjor));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoContrato", tipcon));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoContratacion", tipcontra));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaIngreso", fecing));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaAntiguedad", fecant));
                cmd.Parameters.Add(new SqlParameter("@ctrlVencimientoCont", vencon));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado", empleado));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoP", apepat));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoM", apemat));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaNaci", fechanaci));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlPosicion", position));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPago", tippag));
                cmd.Parameters.Add(new SqlParameter("@ctrlBancoId", banuse));
                cmd.Parameters.Add(new SqlParameter("@ctrlCuentaCh", cunuse));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", clvemp));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    datoNominaBean.sMensaje = "success";
                }
                else
                {
                    datoNominaBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return datoNominaBean;
        }
    }
    public class DatosPosicionesDao : Conexion
    {
        public DatosPosicionesBean sp_Posiciones_Retrieve_Posicion(int clvposition)
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Retrieve_Posicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdPosicion", clvposition));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    posicionBean.sCodRepPosicion = data["CodRep"].ToString();
                    posicionBean.iIdPosicion = Convert.ToInt32(data["IdPosicion"].ToString());
                    posicionBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                    posicionBean.sPosicionCodigo = data["PosicionCodigo"].ToString();
                    posicionBean.iPuesto_id = Convert.ToInt32(data["Puesto_id"].ToString());
                    posicionBean.sNombrePuesto = data["NombrePuesto"].ToString();
                    posicionBean.iDepartamento_id = Convert.ToInt32(data["Departamento_id"].ToString());
                    posicionBean.sNombreDepartamento = data["Depto_Codigo"].ToString();
                    posicionBean.iIdReportaAPosicion = Convert.ToInt32(data["Reporta_A_Posicion_id"].ToString());
                    posicionBean.iIdReportaAEmpresa = Convert.ToInt32(data["Reporta_A_Empresa"].ToString());
                    posicionBean.iIdRegistroPat = Convert.ToInt32(data["IdRegPat"].ToString());
                    posicionBean.sRegistroPat = data["Afiliacion_IMSS"].ToString();
                    posicionBean.iIdLocalidad = Convert.ToInt32(data["IdLocalidad"].ToString());
                    posicionBean.sLocalidad = data["Descripcion"].ToString();

                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return posicionBean;
        }
        public List<DatosPosicionesBean> sp_Posiciones_Retrieve_Search_Disp_Posiciones(string wordsearch, int keyemp)
        {
            List<DatosPosicionesBean> listPosicionBean = new List<DatosPosicionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Retrieve_Search_Disp_Posiciones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlWordSearch", wordsearch));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DatosPosicionesBean posicionBean = new DatosPosicionesBean();
                        posicionBean.iIdPosicion = Convert.ToInt32(data["IdPosicion"].ToString());
                        posicionBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                        posicionBean.sPosicionCodigo = data["PosicionCodigo"].ToString();
                        posicionBean.iPuesto_id = Convert.ToInt32(data["Puesto_id"].ToString());
                        posicionBean.sNombrePuesto = data["NombrePuesto"].ToString();
                        posicionBean.iDepartamento_id = Convert.ToInt32(data["Departamento_id"].ToString());
                        posicionBean.sNombreDepartamento = data["Depto_Codigo"].ToString();
                        posicionBean.iIdReportaAPosicion = Convert.ToInt32(data["Reporta_A_Posicion_id"].ToString());
                        posicionBean.iIdReportaAEmpresa = Convert.ToInt32(data["Reporta_A_Empresa"].ToString());
                        posicionBean.iIdRegistroPat = Convert.ToInt32(data["IdRegPat"].ToString());
                        posicionBean.sRegistroPat = data["Afiliacion_IMSS"].ToString();
                        posicionBean.iIdLocalidad = Convert.ToInt32(data["IdLocalidad"].ToString());
                        posicionBean.sLocalidad = data["Descripcion"].ToString();
                        listPosicionBean.Add(posicionBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listPosicionBean;
        }
        public DatosPosicionesBean sp_Posicion_Consecutivo_Posicion(int keyemp)
        {
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posicion_Consecutivo_Posicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    datoPosicionBean.sMensaje = "success";
                    datoPosicionBean.sPosicionCodigo = data["SigCode"].ToString();
                }
                else
                {
                    datoPosicionBean.sMensaje = "error";
                    datoPosicionBean.sPosicionCodigo = "0";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return datoPosicionBean;
        }
        public List<DatosPosicionesBean> sp_Posiciones_Retrieve_Search_Posiciones(string wordsearch, int keyemp, string type)
        {
            List<DatosPosicionesBean> listPosicionBean = new List<DatosPosicionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Retrieve_Search_Posiciones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlWordSearch", wordsearch));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaId", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DatosPosicionesBean posicionBean = new DatosPosicionesBean();
                        posicionBean.iIdPosicion = Convert.ToInt32(data["IdPosicion"].ToString());
                        posicionBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                        posicionBean.sPosicionCodigo = data["PosicionCodigo"].ToString();
                        posicionBean.iPuesto_id = Convert.ToInt32(data["Puesto_id"].ToString());
                        posicionBean.sNombrePuesto = data["NombrePuesto"].ToString();
                        posicionBean.iDepartamento_id = Convert.ToInt32(data["Departamento_id"].ToString());
                        posicionBean.sNombreDepartamento = data["Depto_Codigo"].ToString();
                        posicionBean.iIdReportaAPosicion = Convert.ToInt32(data["Reporta_A_Posicion_id"].ToString());
                        posicionBean.iIdRegistroPat = Convert.ToInt32(data["IdRegPat"].ToString());
                        posicionBean.sRegistroPat = data["Afiliacion_IMSS"].ToString();
                        posicionBean.iIdLocalidad = Convert.ToInt32(data["IdLocalidad"].ToString());
                        posicionBean.sLocalidad = data["Descripcion"].ToString();
                        listPosicionBean.Add(posicionBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listPosicionBean;
        }
        public List<DatosPosicionesBean> sp_Posiciones_Retrieve_Posiciones(int keyemp, string typefil)
        {
            List<DatosPosicionesBean> listPosicionBean = new List<DatosPosicionesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Retrieve_Posiciones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaId", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", typefil));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DatosPosicionesBean posicionBean = new DatosPosicionesBean();
                        posicionBean.iIdPosicion = Convert.ToInt32(data["IdPosicion"].ToString());
                        posicionBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                        posicionBean.iPuesto_id = Convert.ToInt32(data["Puesto_id"].ToString());
                        posicionBean.sNombrePuesto = data["NombrePuesto"].ToString();
                        posicionBean.iDepartamento_id = Convert.ToInt32(data["Departamento_id"].ToString());
                        posicionBean.sNombreDepartamento = data["Depto_Codigo"].ToString();
                        posicionBean.iIdReportaAPosicion = Convert.ToInt32(data["Reporta_A_Posicion_id"].ToString());
                        posicionBean.iIdRegistroPat = Convert.ToInt32(data["IdRegPat"].ToString());
                        posicionBean.sRegistroPat = data["Afiliacion_IMSS"].ToString();
                        posicionBean.iIdLocalidad = Convert.ToInt32(data["IdLocalidad"].ToString());
                        posicionBean.sLocalidad = data["Descripcion"].ToString();
                        listPosicionBean.Add(posicionBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return listPosicionBean;
        }
        public DatosPosicionesBean sp_Posiciones_Insert_Posicion(string codposic, int depaid, int puesid, int regpatcla, int localityr, int emprepreg, int reportempr, int usuario, int keyemp)
        {
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Insert_Posicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlPosicionCod", codposic));
                cmd.Parameters.Add(new SqlParameter("@ctrlDepartamentoId", depaid));
                cmd.Parameters.Add(new SqlParameter("@ctrlPuestoId", puesid));
                cmd.Parameters.Add(new SqlParameter("@ctrlLocalidadId", localityr));
                cmd.Parameters.Add(new SqlParameter("@ctrlReportaAId", emprepreg));
                cmd.Parameters.Add(new SqlParameter("@ctrlReportaEmpr", reportempr));
                cmd.Parameters.Add(new SqlParameter("@ctrlRegistroPa", regpatcla));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    datoPosicionBean.sMensaje = "success";
                }
                else
                {
                    datoPosicionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return datoPosicionBean;
        }
        public DatosPosicionesBean sp_PosicionesAsig_Insert_PosicionesAsig(int clvstr, string fechefectpos, string fechinipos, string empleado, string apepat, string apemat, string fechanaci, int usuario)
        {
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PosicionesAsig_Insert_PosicionesAsig", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlPosicionId", clvstr));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaEffec", fechefectpos));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaInici", fechinipos));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado", empleado));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoP", apepat));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoM", apemat));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaNaci", fechanaci));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    datoPosicionBean.sMensaje = "success";
                }
                else
                {
                    datoPosicionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return datoPosicionBean;
        }
        public DatosPosicionesBean sp_PosicionesAsig_Insert_PosicionesAsigEdit(int clvstr, string fechefectpos, string fechinipos, int clvemp, int clvposasig, int clvnom, int usuario)
        {
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PosicionesAsig_Insert_PosicionesAsigEdit", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlPosicionId", clvstr));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaEffec", fechefectpos));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaInici", fechinipos));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", clvemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdNomina", clvnom));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    datoPosicionBean.sMensaje = "success";
                }
                else
                {
                    datoPosicionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "SaveDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return datoPosicionBean;
        }

    }

}