using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class EditDataGeneralDao : Conexion { 
    
        public EmpleadosBean sp_Check_Available_Number_Payroll_Save(int business, int key, int newNumber, int keyImss, int keyNom)
        {
            EmpleadosBean empleado = new EmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Check_Available_Number_Payroll_Save", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", business));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", key));
                cmd.Parameters.Add(new SqlParameter("@IdImss", keyImss));
                cmd.Parameters.Add(new SqlParameter("@IdNomina", keyNom));
                cmd.Parameters.Add(new SqlParameter("@NuevaNomina", newNumber));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {

                } else {

                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                empleado.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return empleado;
        }

    }

    public class EditDepartamentosDao : Conexion
    {
        public DepartamentosBean sp_Departamentos_Update_Departamento(string edidepart, string edidescdepart, string edinivestuc, string nivsuptxtedit, int ediedific, string edipiso, string ediubicac, int edicentrcost, int edireportaa, string edidgatxt, string edidirgentxt, string edidirejetxt, string edidiraretxt, int edidirgen, int edidireje, int edidirare, int clvdepart)
        {
            DepartamentosBean departamentoBean = new DepartamentosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Departamentos_Update_Departamento", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlDepartamento", edidepart));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", edidescdepart));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelEstructura", edinivestuc));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelSuperior", nivsuptxtedit));
                cmd.Parameters.Add(new SqlParameter("@ctrlEdificio", ediedific));
                cmd.Parameters.Add(new SqlParameter("@ctrlPiso", edipiso));
                cmd.Parameters.Add(new SqlParameter("@ctrlUbicacion", ediubicac));
                cmd.Parameters.Add(new SqlParameter("@ctrlCentroCosto", edicentrcost));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaReporta", edireportaa));
                cmd.Parameters.Add(new SqlParameter("@ctrlDGA", edidgatxt));
                cmd.Parameters.Add(new SqlParameter("@ctrlDirecGen", edidirgentxt));
                cmd.Parameters.Add(new SqlParameter("@ctrlDirecEje", edidirejetxt));
                cmd.Parameters.Add(new SqlParameter("@ctrlDirecAre", edidiraretxt));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmprDGen", edidirgen));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmprDEje", edidireje));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmprDAre", edidirare));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdDepartamento", clvdepart));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    departamentoBean.sMensaje = "success";
                }
                else
                {
                    departamentoBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return departamentoBean;
        }
    }

    public class EditPuestosDao : Conexion
    {
        public PuestosBean sp_Puestos_Update_Puesto(string edicodpuesto, string edipuesto, string edidescpuesto, int ediproffamily, int ediclasifpuesto, int edicolect, int edinivjerarpuesto, int ediperfmanager, int editabpuesto, int clvpuesto)
        {
            PuestosBean puestoBean = new PuestosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Puestos_Update_Puesto", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPuesto", edicodpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlNombrePuesto", edipuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescriPuesto", edidescpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlProfesionFamId", ediproffamily));
                cmd.Parameters.Add(new SqlParameter("@ctrlClasificacioId", ediclasifpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlColectivoId", edicolect));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelJerarId", edinivjerarpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlPerfomanceId", ediperfmanager));
                cmd.Parameters.Add(new SqlParameter("@ctrlTabuladorId", editabpuesto));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdPuesto", clvpuesto));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    puestoBean.sMensaje = "success";
                }
                else
                {
                    puestoBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return puestoBean;
        }
    }

    public class EditEmpleadoDao : Conexion
    {
        public EmpleadosBean sp_Empleados_Update_Empleado(string name, string apepat, string apemat, int sex, int estciv, string fnaci, string lnaci, int title, int nacion, int state, string codpost, string city, string colony, string street, string numberst, string telfij, string telmov, string email, string fecmat, string tipsan, int clvemp)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empleados_Update_Empleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlNombre", name.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoPaterno", apepat.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlApellidoMaterno", apemat.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@ctrlSexo", sex));
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoCivil", estciv));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaNacimiento", fnaci));
                cmd.Parameters.Add(new SqlParameter("@ctrlLugarNacimiento", lnaci));
                cmd.Parameters.Add(new SqlParameter("@ctrlTitulo", title));
                cmd.Parameters.Add(new SqlParameter("@ctrlNacionalidad", nacion));
                cmd.Parameters.Add(new SqlParameter("@ctrlEstado", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPostal", codpost));
                cmd.Parameters.Add(new SqlParameter("@ctrlCiudad", city));
                cmd.Parameters.Add(new SqlParameter("@ctrlColonia", colony));
                cmd.Parameters.Add(new SqlParameter("@ctrlCalle", street));
                cmd.Parameters.Add(new SqlParameter("@ctrlNumeroCalle", numberst));
                cmd.Parameters.Add(new SqlParameter("@ctrlTelefonoFijo", telfij));
                cmd.Parameters.Add(new SqlParameter("@ctrlTelefonoMovil", telmov));
                cmd.Parameters.Add(new SqlParameter("@ctrlCorreoElectronico", email));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaMatrimonio", fecmat));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoSangre", tipsan));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", clvemp));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    empleadoBean.sMensaje = "success";
                }
                else
                {
                    empleadoBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                empleadoBean.sMensaje = exc.Message.ToString();
            }
            return empleadoBean;
        }

        public ImssBean sp_Imss_Update_DatoImss(string regimss, string fecefe, string rfc, string curp, int nivest, int nivsoc, int clvimss)
        {
            ImssBean imssBean = new ImssBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Imss_Update_DatoImss", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaEfectiva", fecefe));
                cmd.Parameters.Add(new SqlParameter("@ctrlRegistroImss", regimss));
                cmd.Parameters.Add(new SqlParameter("@ctrlRfc", rfc));
                cmd.Parameters.Add(new SqlParameter("@ctrlCurp", curp));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelEstudios", nivest));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelSocioeconomico", nivsoc));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdImss", clvimss));
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
                Console.WriteLine(exc);
            }
            return imssBean;
        }

        public DatosNominaBean sp_Nomina_Update_DatoNomina(string fecefecnom, double salmen, int tipper, int tipemp, int nivemp, int tipjor, int tipcon, int tipcontra, string fecing, string fecant, string vencon, int tippag, int banuse, string cunuse, int clvnom, int position, int tiposueldo, int politica, double diferencia, double transporte, int empresa, int retroactivo, int categoria, int pagopor, int fondo, int clasif)
        {
            DatosNominaBean nominaBean = new DatosNominaBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Nomina_Update_DatoNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaEfectiva",    fecefecnom));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo",      tipper));
                cmd.Parameters.Add(new SqlParameter("@ctrlSalarioMensual",   salmen));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoEmpleado",     tipemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlNivelEmpleado",    nivemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoJornada",      tipjor));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoContrato",     tipcon));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoContratacion", tipcontra));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaIngreso",     fecing));
                cmd.Parameters.Add(new SqlParameter("@ctrlFechaAntiguedad",  fecant));
                cmd.Parameters.Add(new SqlParameter("@ctrlVencimientoCont",  vencon));
                cmd.Parameters.Add(new SqlParameter("@ctrlPosicionId",       position));
                cmd.Parameters.Add(new SqlParameter("@ctrlPagoId",           tippag));
                cmd.Parameters.Add(new SqlParameter("@ctrlBancoId",          banuse));
                cmd.Parameters.Add(new SqlParameter("@ctrlCtaCheques",       cunuse));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdNomina",         clvnom));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa",        empresa));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoSueldo",       tiposueldo));
                cmd.Parameters.Add(new SqlParameter("@ctrlPolitica",         politica));
                cmd.Parameters.Add(new SqlParameter("@ctrlDiferencia",       diferencia));
                cmd.Parameters.Add(new SqlParameter("@ctrlTransporte",       transporte));
                cmd.Parameters.Add(new SqlParameter("@ctrlRetroactivo",      retroactivo));
                cmd.Parameters.Add(new SqlParameter("@ctrlCategoria",        categoria));
                cmd.Parameters.Add(new SqlParameter("@ctrlPagoPor",          pagopor));
                cmd.Parameters.Add(new SqlParameter("@ctrlFondo", fondo));
                cmd.Parameters.Add(new SqlParameter("@ctrlClasif", clasif));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    nominaBean.sMensaje = "success";
                }
                else
                {
                    nominaBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                nominaBean.sMensaje = exc.Message.ToString();
                Console.WriteLine(exc);
            }
            return nominaBean;
        }

        public DatosPosicionesBean sp_Posiciones_Update_DatoPosicion(int numpla, int depaid, int puesid, string emprep, string report, string tippag, int banuse, string cunuse, int clvstr)
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Posiciones_Update_DatoPosicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlPuestoId", puesid));
                cmd.Parameters.Add(new SqlParameter("@ctrlDepartamentoId", depaid));
                cmd.Parameters.Add(new SqlParameter("@ctrlPosicion", numpla));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaRe", emprep));
                cmd.Parameters.Add(new SqlParameter("@ctrlReportaA", report));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPago", tippag));
                cmd.Parameters.Add(new SqlParameter("@ctrlBanco_id", banuse));
                cmd.Parameters.Add(new SqlParameter("@ctrlCuenta", cunuse));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdPosicion", clvstr));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    posicionBean.sMensaje = "success";
                }
                else
                {
                    posicionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return posicionBean;
        }

    }

    public class EditRegionalesDao : Conexion
    {
        public RegionalesBean sp_Regionales_Update_Regionales(string descregionedit, string claregionedit, int clvregion)
        {
            RegionalesBean regionBean = new RegionalesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Regionales_Update_Regionales", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcionEd", descregionedit));
                cmd.Parameters.Add(new SqlParameter("@ctrlClaveRegionEd", claregionedit));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdRegionalEd", clvregion));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    regionBean.sMensaje = "success";
                }
                else
                {
                    regionBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "EditDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return regionBean;
        }
    }

    public class EditSucursalesDao : Conexion
    {
        public SucursalesBean sp_Sucursales_Update_Sucursales(string descsucursaledit, string clasucursaledit, int clvsucursal)
        {
            SucursalesBean sucuesalesBean = new SucursalesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Sucursales_Update_Sucursales", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcionEd", descsucursaledit));
                cmd.Parameters.Add(new SqlParameter("@ctrlClaveSucursalEd", clasucursaledit));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdSucursalEd", clvsucursal));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    sucuesalesBean.sMensaje = "success";
                }
                else
                {
                    sucuesalesBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
            }
            catch (Exception exc)
            {
                string origenerror = "EditDataGeneralDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return sucuesalesBean;
        }
    }



}