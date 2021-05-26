using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;

namespace Payroll.Models.Daos
{
    public class SaveDataGeneralDao
    {

    }
    public class SavePuestosDao : Conexion
    {
        public PuestosBean sp_Puestos_Insert_Puestos(string regcodpuesto, string regpuesto, string regdescpuesto, int proffamily, int clasifpuesto, int regcolect, int nivjerarpuesto, int perfmanager, int tabpuesto, int usuario, int keyemp, int consecutivenew, int typeregpuesto)
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
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPuesto", regcodpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlNombrePuesto", regpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescriPuesto", regdescpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlProfesionFamId", proffamily));
                cmd.Parameters.Add(new SqlParameter("@ctrlClasificacioId", clasifpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlColectivoId", regcolect));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelJerarId", nivjerarpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlPerfomanceId", perfmanager));
                cmd.Parameters.Add(new SqlParameter("@ctrlTabuladorId", tabpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuarioId", usuario));
                cmd.Parameters.Add(new SqlParameter("@Consecutivo", consecutivenew));
                cmd.Parameters.Add(new SqlParameter("@keyCodeCatalog", typeregpuesto));
                SqlCommand validate = new SqlCommand("sp_Puestos_Validate_Puesto", this.conexion) {

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
            } catch (Exception exc) {
                addPuestoBean.sMensaje = exc.Message.ToString();

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
        public EmpleadosBean sp_Valida_Existencia_Numero_Nomina(int keyEmployee, int keyBusiness)
        {
            EmpleadosBean empleados = new EmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Valida_Existencia_Numero_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Existe"].ToString() == "1") {
                        empleados.sMensaje = "EXISTS";
                    } else if (dataReader["Existe"].ToString() == "0") {
                        empleados.sMensaje = "NOTEXISTS";
                    } else {
                        empleados.sMensaje = "NOTDATA";
                    }
                } else {
                    empleados.sMensaje = "ERROR";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                empleados.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return empleados;
        }

        public EmpleadosBean sp_Empleados_Validate_DatosImss(int keyemp, string fieldCurp, string fieldRfc, int keyUser)
        {
            EmpleadosBean employeeBean = new EmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Validate_DatosImss", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyemp));
                cmd.Parameters.Add(new SqlParameter("@Curp", fieldCurp));
                cmd.Parameters.Add(new SqlParameter("@Rfc", fieldRfc));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", keyUser));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    employeeBean.sMensaje = (data["Respuesta"].ToString() == "notexists") ? "continue" : data["Respuesta"].ToString();
                } else {
                    employeeBean.sMensaje = "nodata";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                employeeBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return employeeBean;
        }

        public EmpleadosBean sp_Empleados_Insert_Empleado(string name, string apepat, string apemat, int sex, int estciv, string fnaci, string lnaci, int title, string nacion, int state, string codpost, string city, string colony, string street, string numberst, string telfij, string telmov, string email, int usuario, int keyemp, string tipsan, string fecmat, int keyEmployee)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Insert_Empleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyEmployee));
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

        public String ConvertDateText(string dateConvert)
        {
            String convertDate = "";
            try
            {
                string year = dateConvert.Substring(0, 4);
                string month = dateConvert.Substring(5, 2);
                string day = dateConvert.Substring(8, 2);
                string[] days = new string[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado" };
                string[] months = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                convertDate = day + " de " + months[Convert.ToInt32(month) - 1] + " del " + year;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            return convertDate;
        }
        public ImssBean sp_Imss_Insert_Imss(string fecefe, string regimss, string rfc, string curp, int nivest, int nivsoc, int usuario, string empleado, string apepat, string apemat, string fechanaci, int keyemp, int keyemployee)
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
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyemployee));
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

        public List<ImssBean> sp_Carga_Historial_Imss (int keyEmployee, int keyBusiness)
        {
            List<ImssBean> listImssBean = new List<ImssBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Carga_Historial_Imss", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        ImssBean imssBean = new ImssBean();
                        imssBean.iIdImss  = Convert.ToInt32(data["IdImss"]);
                        imssBean.sFechaEfectiva = Convert.ToDateTime(data["Effdt"]).ToString("yyyy-MM-dd");
                        imssBean.sRegistroImss  = data["RegistroImss"].ToString();
                        imssBean.sRfc           = data["RFC"].ToString();
                        imssBean.sCurp          = data["CURP"].ToString();
                        imssBean.sNivelEstudio  = data["NivelEstudio"].ToString();
                        imssBean.sNivelSocieconomico = data["NivelSocioEconomico"].ToString();
                        imssBean.sFechaAlta     = ConvertDateText(Convert.ToDateTime(data["Fecha_Alta"]).ToString("yyyy-MM-dd"));
                        listImssBean.Add(imssBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listImssBean;
        }

    }
    public class DatosNominaDao : Conexion
    { 

        public DatosNominaBean sp_Actualiza_Ult_Sdi(int clvNom, double ultSdi, int keyBusiness, int keyEmployee)
        {
            DatosNominaBean datosNominaBean = new DatosNominaBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Actualiza_Ult_Sdi", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdNomina", clvNom));
                cmd.Parameters.Add(new SqlParameter("@UltSdi",   ultSdi));
                cmd.Parameters.Add(new SqlParameter("@EmpresaId", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@EmpleadoId", keyEmployee));
                if (cmd.ExecuteNonQuery() > 0) {
                    datosNominaBean.sMensaje = "SUCCESS";
                } else {
                    datosNominaBean.sMensaje = "ERROR";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                datosNominaBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosNominaBean;
        }

        public InfoPositionInsert sp_Valida_Posicion_Carga_Masiva(int keyBusiness, int codePosition)
        {
            InfoPositionInsert infoPositionInsert = new InfoPositionInsert();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Valida_Posicion_Carga_Masiva", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigo", codePosition));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (data["Respuesta"].ToString() == "SUCCESS") {
                        infoPositionInsert.iPosicion = Convert.ToInt32(data["IdPosicion"].ToString());
                        infoPositionInsert.sMensaje  = "SUCCESS";
                    } else {
                        infoPositionInsert.iPosicion = 0;
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return infoPositionInsert;
        }

        public DatosNominaBean sp_DatosNomina_Insert_DatoNomina(string fecefecnom, double salmen, int tipemp, int nivemp, int tipjor, int tipcon, string fecing, string fecant, string vencon, int usuario, string empleado, string apepat, string apemat, string fechanaci, int keyemp, int tipper, int tipcontra, int tippag, int banuse, string cunuse, int position, int clvemp, int tiposueldo, int politica, double diferencia, double transporte, int retroactivo, int categoria, int pagopor, int fondo, double sdi, int clasif)
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
                cmd.Parameters.Add(new SqlParameter("@TipoSueldo", tiposueldo));
                cmd.Parameters.Add(new SqlParameter("@Politica", politica));
                cmd.Parameters.Add(new SqlParameter("@Diferencia", diferencia));
                cmd.Parameters.Add(new SqlParameter("@Transporte", transporte));
                cmd.Parameters.Add(new SqlParameter("@Retroactivo", retroactivo));
                cmd.Parameters.Add(new SqlParameter("@categoria", categoria));
                cmd.Parameters.Add(new SqlParameter("@pago", pagopor));
                cmd.Parameters.Add(new SqlParameter("@fondo", fondo));
                cmd.Parameters.Add(new SqlParameter("@sdi", sdi));
                cmd.Parameters.Add(new SqlParameter("@clasif", clasif));
                if (cmd.ExecuteNonQuery() > 0) {
                    datoNominaBean.sMensaje = "success";
                } else {
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

        public DatosPosicionesBean sp_Save_Edit_Position(int position, int newLocality, int newDepartament, int newPost, int keyBusiness)
        {
            DatosPosicionesBean posicionesBean = new DatosPosicionesBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Save_Edit_Position", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdPosicion", position));
                cmd.Parameters.Add(new SqlParameter("@IdLocalidad", newLocality));
                cmd.Parameters.Add(new SqlParameter("@IdDepartamento", newDepartament));
                cmd.Parameters.Add(new SqlParameter("@IdPuesto", newPost));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                if (cmd.ExecuteNonQuery() > 0) {
                    posicionesBean.sMensaje = "SUCCESS";
                } else {
                    posicionesBean.sMensaje = "ERROR";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                posicionesBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return posicionesBean;
        }

        public DatosMovimientosBean sp_Save_Data_History_Movements_Employee(int keyEmployee, int keyBusiness, string typeMov, string descMov, string newValue, string beforeValue, string dateMov, int keyUser, int keyPeriod, int period, int year)
        {
            DatosMovimientosBean datosMovimientos = new DatosMovimientosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Save_Data_History_Movements_Employee", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Empleado_id", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@TipoMovimiento", typeMov));
                cmd.Parameters.Add(new SqlParameter("@MotivoMovimiento", descMov));
                cmd.Parameters.Add(new SqlParameter("@ValorAnterior", beforeValue));
                cmd.Parameters.Add(new SqlParameter("@ValorNuevo", newValue));
                cmd.Parameters.Add(new SqlParameter("@FechaMovimiento", dateMov));
                cmd.Parameters.Add(new SqlParameter("@Usuario_id", keyUser));
                cmd.Parameters.Add(new SqlParameter("@Periodo_id", keyPeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                if (cmd.ExecuteNonQuery() > 0) {
                    datosMovimientos.sMensaje = "SUCCESS";
                } else {
                    datosMovimientos.sMensaje = "ERROR";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                datosMovimientos.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosMovimientos;
        }

        public String ConvertDateText(string dateConvert)
        {
            String convertDate = "";
            try
            {
                string year = dateConvert.Substring(0, 4);
                string month = dateConvert.Substring(5, 2);
                string day = dateConvert.Substring(8, 2);
                string[] days = new string[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado" };
                string[] months = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                convertDate = day + " de " + months[Convert.ToInt32(month) - 1] + " del " + year;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            return convertDate;
        }
        public List<DatosPosicionesBean> sp_Carga_Historial_Posiciones(int keyBusiness, int keyEmployee)
        {
            List<DatosPosicionesBean> posicionBean = new List<DatosPosicionesBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Carga_Historial_Posiciones", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosPosicionesBean posicion = new DatosPosicionesBean();
                        posicion.iIdPosicion         = Convert.ToInt32(data["Posicion_id"].ToString());
                        posicion.sPosicionCodigo     = data["PosicionCodigo"].ToString();
                        posicion.sNombreDepartamento = data["DescripcionDepartamento"].ToString();
                        posicion.sNombrePuesto       = data["NombrePuesto"].ToString();
                        posicion.sLocalidad          = data["Localidad"].ToString();
                        posicion.sRegistroPat        = data["Afiliacion_IMSS"].ToString();
                        posicion.sNombreE            = data["Nombre"].ToString();
                        posicion.iIdReportaAPosicion = Convert.ToInt32(data["Reporta_A_Posicion_Id"].ToString());
                        posicion.sFechaEffectiva     = Convert.ToDateTime(data["Effdt"]).ToString("yyyy-MM-dd");
                        posicion.sFechaInicio        = ConvertDateText(Convert.ToDateTime(data["Fecha_Alta"]).ToString("yyyy-MM-dd"));
                        posicionBean.Add(posicion);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return posicionBean;
        }

        public DatosPosicionesBean sp_Posiciones_Retrieve_Posicion(int clvposition)
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            Validaciones v = new Validaciones();
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
                    posicionBean.iIdPosicion     = Convert.ToInt32(v.ValidationsInts(data["IdPosicion"].ToString()));
                    posicionBean.iEmpresa_id     = Convert.ToInt32(v.ValidationsInts(data["Empresa_id"].ToString()));
                    posicionBean.sPosicionCodigo = data["PosicionCodigo"].ToString();
                    posicionBean.iPuesto_id      = Convert.ToInt32(v.ValidationsInts(data["Puesto_id"].ToString()));
                    posicionBean.sNombrePuesto   = data["NombrePuesto"].ToString();
                    posicionBean.iDepartamento_id    = Convert.ToInt32(v.ValidationsInts(data["Departamento_id"].ToString()));
                    posicionBean.sNombreDepartamento = data["DescripcionDepartamento"].ToString();
                    posicionBean.iIdReportaAPosicion = Convert.ToInt32(data["CodRep"].ToString());
                    posicionBean.iIdReportaAEmpresa  = Convert.ToInt32(v.ValidationsInts(data["Reporta_A_Empresa"].ToString()));
                    posicionBean.iIdRegistroPat = Convert.ToInt32(v.ValidationsInts(data["IdRegPat"].ToString()));
                    posicionBean.sRegistroPat   = data["Afiliacion_IMSS"].ToString();
                    posicionBean.iIdLocalidad   = Convert.ToInt32(v.ValidationsInts(data["IdLocalidad"].ToString()));
                    posicionBean.sLocalidad     = data["Descripcion"].ToString();

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

        public string ValidationsIns (string value)
        {
            string result = "";
            try {
                if (value.Trim() != "") {
                    result = value.Trim();
                } else {
                    result = "0";
                }
            } catch (Exception exc) {
                result = "0";
            }
            return result;
        }

        public List<DatosPosicionesBean> sp_Posiciones_Retrieve_Search_Disp_Posiciones(string wordsearch, int keyemp, string search)
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
                cmd.Parameters.Add(new SqlParameter("@ctrlSearch", search));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DatosPosicionesBean posicionBean = new DatosPosicionesBean();
                        posicionBean.iIdPosicion = Convert.ToInt32(ValidationsIns(data["IdPosicion"].ToString()));
                        posicionBean.iEmpresa_id = Convert.ToInt32(ValidationsIns(data["Empresa_id"].ToString()));
                        posicionBean.sPosicionCodigo = data["PosicionCodigo"].ToString();
                        posicionBean.iPuesto_id = Convert.ToInt32(ValidationsIns(data["Puesto_id"].ToString()));
                        posicionBean.sNombrePuesto = data["NombrePuesto"].ToString();
                        posicionBean.iDepartamento_id = Convert.ToInt32(ValidationsIns(data["Departamento_id"].ToString()));
                        posicionBean.sNombreDepartamento = data["Depto_Codigo"].ToString();
                        posicionBean.iIdReportaAPosicion = Convert.ToInt32(ValidationsIns(data["Reporta_A_Posicion_id"].ToString()));
                        posicionBean.iIdReportaAEmpresa = Convert.ToInt32(ValidationsIns(data["Reporta_A_Empresa"].ToString()));
                        posicionBean.iIdRegistroPat = Convert.ToInt32(ValidationsIns(data["IdRegPat"].ToString()));
                        posicionBean.sRegistroPat = data["Afiliacion_IMSS"].ToString();
                        posicionBean.iIdLocalidad = Convert.ToInt32(ValidationsIns(data["IdLocalidad"].ToString()));
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
        public List<DatosPosicionesBean> sp_Posiciones_Retrieve_Search_Posiciones(string wordsearch, int keyemp, string type, string filter)
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
                cmd.Parameters.Add(new SqlParameter("@ctrlFiltroBusqueda", filter));
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
                        if (data["TYPEDATA"].ToString() == "YESEMP") {
                            posicionBean.sNombreE = data["Nombre_Empleado"].ToString() + " " + data["Apellido_Paterno_Empleado"].ToString() + " " + data["Apellido_Materno_Empleado"].ToString();
                            posicionBean.sPaternoE = data["Apellido_Paterno_Empleado"].ToString();
                            posicionBean.sMaternoE = data["Apellido_Materno_Empleado"].ToString();
                        }
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
        public DatosPosicionesBean sp_PosicionesAsig_Insert_PosicionesAsig(int clvstr, string fechefectpos, string fechinipos, string empleado, string apepat, string apemat, string fechanaci, int usuario, int keyemp)
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
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyemp));
                if (cmd.ExecuteNonQuery() > 0) {
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
        public DatosPosicionesBean sp_PosicionesAsig_Insert_PosicionesAsigEdit(int clvstr, string fechefectpos, string fechinipos, int clvemp, int clvnom, int usuario, int keyemp)
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
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
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


    public class ListasAltasBajasMasivasDao : Conexion
    {

        public List<CatalogoGeneralBean> UpsAndDownsCatalogs(int keyField, string typeCat, int inCGen)
        {
            List<CatalogoGeneralBean> catalogoGenerals = new List<CatalogoGeneralBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Catalogos_Altas_Bajas_Masivas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdCampo", keyField));
                cmd.Parameters.Add(new SqlParameter("@TipoCam", typeCat));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while(data.Read()) {
                        CatalogoGeneralBean catalogo = new CatalogoGeneralBean();
                        if (typeCat == "Nacionalidades" && inCGen == 0) {
                            catalogo.iId          = Convert.ToInt32(data["IdNacionalidad"]);
                            catalogo.sDescripcion = data["Descripcion"].ToString();
                        }
                        if (typeCat == "Bancos" && inCGen == 0) {
                            catalogo.iId          = Convert.ToInt32(data["IdBanco"]);
                            catalogo.sDescripcion = data["Descripcion"].ToString();
                        }
                        if (inCGen == 1) {
                            if (typeCat == "TipoPe") {
                                catalogo.iId          = Convert.ToInt32(data["IdValor"]);
                                catalogo.sDescripcion = data["Valor"].ToString();
                            } else {
                                catalogo.iId          = Convert.ToInt32(data["id"]);
                                catalogo.sDescripcion = data["Valor"].ToString();
                            }
                        }
                        catalogoGenerals.Add(catalogo);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return catalogoGenerals;
        }

    }

}