using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class CatalogosDao { }
    public class CatalogoGeneralDao : Conexion
    {
        public List<CatalogoGeneralBean> sp_CatalogoGeneral_Consulta_CatalogoGeneral(int state, string type, int keycat, int keycam)
        {
            List<CatalogoGeneralBean> listCatGenBean = new List<CatalogoGeneralBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CatalogoGeneral_Consulta_CatalogoGeneral", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlCancelado", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlId", keycat));
                cmd.Parameters.Add(new SqlParameter("@ctrlCampoCatalogoId", keycam));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CatalogoGeneralBean catGenBean = new CatalogoGeneralBean();
                        catGenBean.iId = Convert.ToInt32(data["id"].ToString());
                        catGenBean.iCampoCatalogoId = Convert.ToInt32(data["Campos_Catalogo_Id"].ToString());
                        catGenBean.sValor = data["Valor"].ToString().ToUpper();
                        catGenBean.iIdValor = Convert.ToInt32(data["IdValor"].ToString());
                        if (keycat != 0)
                        {
                            catGenBean.sDescripcion = (String.IsNullOrEmpty(data["Descripcion"].ToString())) ? "Sin resultado" : data["Descripcion"].ToString();
                            catGenBean.iCancelado = Convert.ToInt32(data["Cancelado"].ToString());
                            catGenBean.iIdUsuAlta = Convert.ToInt32(data["UsuAlta_id"].ToString());
                            catGenBean.sFechaAlta = data["FechaAlta"].ToString();
                        }
                        listCatGenBean.Add(catGenBean);
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
            return listCatGenBean;
        }
    }
    public class InfDomicilioDao : Conexion
    {

        public List<InfDomicilioBean> sp_CatalogoGeneral_Retrieve_CatalogoGeneral(int catalogid)
        {
            List<InfDomicilioBean> listInfDomBean = new List<InfDomicilioBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CatalogoGeneral_Retrieve_CatalogoGeneral", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlCatalogoId", catalogid));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        InfDomicilioBean infDomBean = new InfDomicilioBean();
                        infDomBean.iIdValor = Convert.ToInt32(data["IdValor"].ToString());
                        infDomBean.sValor = data["Valor"].ToString();
                        infDomBean.iId = Convert.ToInt32(data["id"].ToString());
                        listInfDomBean.Add(infDomBean);
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
            return listInfDomBean;
        }

        public List<InfDomicilioBean> sp_Domicilio_Retrieve_Domicilio(int codepost, int state)
        {
            List<InfDomicilioBean> listInfDomBean = new List<InfDomicilioBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Domicilio_Retrieve_Domicilio", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPostal", codepost));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEstado", state));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        InfDomicilioBean infDomBean = new InfDomicilioBean();
                        infDomBean.sCiudad = data["Ciudad"].ToString();
                        infDomBean.iIdColonia = Convert.ToInt32(data["IdColonia"].ToString());
                        infDomBean.sColonia = data["Colonia"].ToString();
                        infDomBean.iIdMunicipio = Convert.ToInt32(data["IdMunicipio"].ToString());
                        listInfDomBean.Add(infDomBean);
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
            return listInfDomBean;
        }
        public List<InfoDireccionByCPBean> sp_Domicilio_Retrieve_Domicilio2(int codepost)
        {
            List<InfoDireccionByCPBean> listInfDomBean = new List<InfoDireccionByCPBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Domicilio_Retrieve_Domicilio2", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlCodigoPostal", codepost));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        InfoDireccionByCPBean infDomBean = new InfoDireccionByCPBean();
                        if (data["Ciudad"].ToString() == "" || data["Ciudad"].ToString().Length == 0 || data["Ciudad"] == null)
                        { infDomBean.sCiudad = "Sin Ciudad"; }
                        else { infDomBean.sCiudad = data["Ciudad"].ToString(); }
                        infDomBean.iIdColonia = int.Parse(data["IdColonia"].ToString());
                        infDomBean.sColonia = data["Colonia"].ToString();
                        infDomBean.iIdMunicipio = int.Parse(data["IdMunicipio"].ToString());
                        infDomBean.sMunicipio = data["Municipio"].ToString();
                        infDomBean.sEstado = data["Estado"].ToString();
                        infDomBean.iIdEstado = int.Parse(data["IdEstado"].ToString());
                        listInfDomBean.Add(infDomBean);
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
            return listInfDomBean;
        }

    }
    public class NivelEstudiosDao : Conexion
    {
        public List<NivelEstudiosBean> sp_NivelEstudios_Retrieve_NivelEstudios(int state, string type, int keynivel)
        {
            List<NivelEstudiosBean> listNivEstBean = new List<NivelEstudiosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NivelEstudios_Retrieve_NivelEstudios", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoNivelEstudio", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdNivelEstudio", keynivel));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NivelEstudiosBean nivEstBean = new NivelEstudiosBean();
                        nivEstBean.iIdNivelEstudio = Convert.ToInt32(data["IdNivelEstudio"].ToString());
                        nivEstBean.sNombreNivelEstudio = data["NombreNivelEstudio"].ToString();
                        nivEstBean.iEstadoNivelEstudio = Convert.ToInt32(data["EstadoNivelEstudio"].ToString());
                        nivEstBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        nivEstBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listNivEstBean.Add(nivEstBean);
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
            return listNivEstBean;
        }
    }
    public class TabuladoresDao : Conexion
    {
        public List<TabuladoresBean> sp_Tabuladores_Retrieve_Tabuladores(int state, string type, int keytab)
        {
            List<TabuladoresBean> listTabBean = new List<TabuladoresBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Tabuladores_Retrieve_Tabuladores", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoTabulador", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdTabulador", keytab));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TabuladoresBean tabBean = new TabuladoresBean();
                        tabBean.iIdTabulador = Convert.ToInt32(data["IdTabulador"].ToString());
                        tabBean.sTabulador = data["Tabulador"].ToString();
                        tabBean.iEstadoTabulador = Convert.ToInt32(data["EstadoTabulador"].ToString());
                        tabBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        tabBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listTabBean.Add(tabBean);
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
            return listTabBean;
        }
    }
    public class BancosDao : Conexion
    {
        public List<BancosBean> sp_Bancos_Retrieve_Bancos(int keyban)
        {
            List<BancosBean> listBanBean = new List<BancosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Bancos_Retrieve_Bancos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdBanco", keyban));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        BancosBean banBean = new BancosBean();
                        banBean.iIdBanco = Convert.ToInt32(data["IdBanco"].ToString());
                        banBean.sNombreBanco = data["Descripcion"].ToString();
                        listBanBean.Add(banBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return listBanBean;
        }
    }
    public class PuestosDao : Conexion
    {
        public List<PuestosBean> sp_Puestos_Retrieve_Search_Puestos(string wordsearch, int keyemp)
        {
            List<PuestosBean> listPuestoBean = new List<PuestosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Puestos_Retrieve_Search_Puestos", this.conexion)
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
                        PuestosBean puestoBean = new PuestosBean();
                        puestoBean.iIdPuesto = Convert.ToInt32(data["IdPuesto"].ToString());
                        puestoBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                        puestoBean.sCodigoPuesto = data["PuestoCodigo"].ToString();
                        puestoBean.sNombrePuesto = data["NombrePuesto"].ToString();
                        puestoBean.sDescripcionPuesto = data["DescripcionPuesto"].ToString();
                        puestoBean.iIdProfesionFamilia = Convert.ToInt32(data["ProfesionFamilia_id"].ToString());
                        puestoBean.iIdClasificacionPuesto = Convert.ToInt32(data["Cg_ClasificacionPuesto_id"].ToString());
                        puestoBean.iIdColectivo = Convert.ToInt32(data["Cg_Colectivo_id"].ToString());
                        puestoBean.iIdNivelJerarquico = Convert.ToInt32(data["Cg_NivelJerarquico_id"].ToString());
                        puestoBean.iIdPerfomanceManager = Convert.ToInt32(data["Cg_PerfomanceManager_id"].ToString());
                        puestoBean.iIdTabulador = Convert.ToInt32(data["Cg_Tabulador_id"].ToString());
                        puestoBean.iEstadoPuesto = Convert.ToInt32(data["EstadoPuesto"].ToString());
                        puestoBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        puestoBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listPuestoBean.Add(puestoBean);
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
            return listPuestoBean;
        }

        public PuestosBean sp_Puestos_Retrieve_Puesto(int clvpuesto)
        {
            PuestosBean puestoBean = new PuestosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Puestos_Retrieve_Puesto", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdPuesto", clvpuesto));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    puestoBean.iIdPuesto = Convert.ToInt32(data["IdPuesto"].ToString());
                    puestoBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                    puestoBean.sCodigoPuesto = data["PuestoCodigo"].ToString();
                    puestoBean.sNombrePuesto = data["NombrePuesto"].ToString();
                    puestoBean.sDescripcionPuesto = data["DescripcionPuesto"].ToString();
                    puestoBean.iIdProfesionFamilia = Convert.ToInt32(data["ProfesionFamilia_id"].ToString());
                    puestoBean.iIdClasificacionPuesto = Convert.ToInt32(data["Cg_ClasificacionPuesto_id"].ToString());
                    puestoBean.iIdColectivo = Convert.ToInt32(data["Cg_Colectivo_id"].ToString());
                    puestoBean.iIdNivelJerarquico = Convert.ToInt32(data["Cg_NivelJerarquico_id"].ToString());
                    puestoBean.iIdPerfomanceManager = Convert.ToInt32(data["Cg_PerfomanceManager_id"].ToString());
                    puestoBean.iIdTabulador = Convert.ToInt32(data["Cg_Tabulador_id"].ToString());
                    puestoBean.iEstadoPuesto = Convert.ToInt32(data["EstadoPuesto"].ToString());
                    puestoBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                    puestoBean.sFechaAlta = data["Fecha_Alta"].ToString();
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
            return puestoBean;
        }
        public List<PuestosBean> sp_Puestos_Retrieve_Puestos(int state, string type, int keyjob, int keyemp)
        {
            List<PuestosBean> listPuestoBean = new List<PuestosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Puestos_Retrieve_Puestos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoPuesto", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdPuesto", keyjob));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        PuestosBean puestoBean = new PuestosBean();
                        puestoBean.iIdPuesto = Convert.ToInt32(data["IdPuesto"].ToString());
                        puestoBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                        puestoBean.sCodigoPuesto = data["PuestoCodigo"].ToString();
                        puestoBean.sNombrePuesto = data["NombrePuesto"].ToString();
                        puestoBean.sDescripcionPuesto = data["DescripcionPuesto"].ToString();
                        puestoBean.iIdProfesionFamilia = Convert.ToInt32(data["ProfesionFamilia_id"].ToString());
                        puestoBean.iIdClasificacionPuesto = Convert.ToInt32(data["Cg_ClasificacionPuesto_id"].ToString());
                        puestoBean.iIdColectivo = Convert.ToInt32(data["Cg_Colectivo_id"].ToString());
                        puestoBean.iIdNivelJerarquico = Convert.ToInt32(data["Cg_NivelJerarquico_id"].ToString());
                        puestoBean.iIdPerfomanceManager = Convert.ToInt32(data["Cg_PerfomanceManager_id"].ToString());
                        puestoBean.iIdTabulador = Convert.ToInt32(data["Cg_Tabulador_id"].ToString());
                        puestoBean.iEstadoPuesto = Convert.ToInt32(data["EstadoPuesto"].ToString());
                        puestoBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        puestoBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listPuestoBean.Add(puestoBean);
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
            return listPuestoBean;
        }
    }
    public class ProfesionesFamiliaDao : Conexion
    {
        public List<ProfesionesFamiliaBean> sp_ProfesionesFamilia_Retrieve_ProfesionesFamilia(int state, string type, int keyprof)
        {
            List<ProfesionesFamiliaBean> listProfFamilyBean = new List<ProfesionesFamiliaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ProfesionesFamilia_Retrieve_ProfesionesFamilia", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoProfesion", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdProfesion", keyprof));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ProfesionesFamiliaBean profFamilyBean = new ProfesionesFamiliaBean();
                        profFamilyBean.iIdProfesionFamilia = Convert.ToInt32(data["IdProfesionFamilia"].ToString());
                        profFamilyBean.sNombreProfesion = data["NombreProfesion"].ToString();
                        profFamilyBean.iEstadoProfesion = Convert.ToInt32(data["EstadoProfesion"].ToString());
                        profFamilyBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        profFamilyBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listProfFamilyBean.Add(profFamilyBean);
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
            return listProfFamilyBean;
        }
    }
    public class EtiquetasContablesDao : Conexion
    {
        public List<EtiquetasContablesBean> sp_EtiquetasContables_Retrieve_EtiquetasContables(int state, string type, int keytag)
        {
            List<EtiquetasContablesBean> listTagContBean = new List<EtiquetasContablesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EtiquetasContables_Retrieve_EtiquetasContables", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoEtiqueta", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEtiquetaContable", keytag));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EtiquetasContablesBean tagContBean = new EtiquetasContablesBean();
                        tagContBean.iIdEtiquetaContable = Convert.ToInt32(data["IdEtiquetaContable"].ToString());
                        tagContBean.sNombreEtiquetaContable = data["NombreEtiquetaContable"].ToString();
                        tagContBean.iEstadoEtiquetaContable = Convert.ToInt32(data["EstadoEtiquetaContable"].ToString());
                        tagContBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        tagContBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listTagContBean.Add(tagContBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return listTagContBean;
        }
    }
    public class NivelJerarDao : Conexion
    {
        public List<NivelJerarBean> sp_NivelJerarquico_Retrieve_NivelJerarquico(int state, string type, int keyniv)
        {
            List<NivelJerarBean> listNivJerarBean = new List<NivelJerarBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NivelJerarquico_Retrieve_NivelJerarquico", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoNivel", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdNivelJerarquico", keyniv));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NivelJerarBean nivJerarBean = new NivelJerarBean();
                        nivJerarBean.iIdNivelJerarquico = Convert.ToInt32(data["IdNivelJerarquico"].ToString());
                        nivJerarBean.sNombreNivelJerarquico = data["NombreNivelJerarquico"].ToString();
                        nivJerarBean.iEstadoNivelJerarquico = Convert.ToInt32(data["EstadoNivelJerarquico"].ToString());
                        nivJerarBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        nivJerarBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listNivJerarBean.Add(nivJerarBean);
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
            return listNivJerarBean;
        }
    }
    public class PerfomanceManagerDao : Conexion
    {
        public List<PerfomanceManagerBean> sp_PerfomanceManager_Retrieve_PerfomanceManager(int state, string type, int keyper)
        {
            List<PerfomanceManagerBean> listPerfManBean = new List<PerfomanceManagerBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PerfomanceManager_Retrieve_PerfomanceManager", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoPerfomance", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdPerfomance", keyper));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        PerfomanceManagerBean perfManBean = new PerfomanceManagerBean();
                        perfManBean.iIdPerfomanceManager = Convert.ToInt32(data["IdPerfomanceManager"].ToString());
                        perfManBean.sPerfomanceManager = data["PerfomanceManager"].ToString();
                        perfManBean.iEstadoPerfomance = Convert.ToInt32(data["EstadoPerfomance"].ToString());
                        perfManBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        perfManBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listPerfManBean.Add(perfManBean);
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
            return listPerfManBean;
        }
    }
    public class EmpresasDao : Conexion
    {
        public List<EmpresasBean> sp_Empresas_Retrieve_Empresas(int state, string type, int keyemp)
        {
            List<EmpresasBean> listEmpresasBean = new List<EmpresasBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empresas_RetrieveFiltro_Empresas", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoEmpresa", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpresasBean empresaBean = new EmpresasBean();
                        empresaBean.iIdEmpresa = Convert.ToInt32(data["IdEmpresa"].ToString());
                        empresaBean.sNombreEmpresa = data["NombreEmpresa"].ToString();
                        listEmpresasBean.Add(empresaBean);
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
            return listEmpresasBean;
        }
    }

    public class CodigoCatalogosDao : Conexion {
        
        public List<CodigoCatalogoBean> sp_Datos_Codigo_Catalogo(string typeJob, int keyEmpr)
        {
            List<CodigoCatalogoBean> listCodeCatBean = new List<CodigoCatalogoBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Codigo_Catalogo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Catalogo", typeJob));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyEmpr));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        CodigoCatalogoBean codeCatBean = new CodigoCatalogoBean();
                        codeCatBean.iId          = Convert.ToInt32(data["id"].ToString());
                        codeCatBean.sCatalogo    = data["Catalogo"].ToString();
                        codeCatBean.sCodigo      = data["Codigo"].ToString();
                        codeCatBean.sDescripcion = data["Descripcion"].ToString();
                        codeCatBean.sCancelado   = data["Cancelado"].ToString();
                        codeCatBean.iConsecutivo = (data["Consecutivo"].ToString().Length > 0) ? Convert.ToInt32(data["Consecutivo"].ToString()) : 0;
                        listCodeCatBean.Add(codeCatBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listCodeCatBean;
        }

        public CodigoCatalogoBean sp_Dato_Codigo_Catalogo_Seleccionado(int keytypejob)
        {
            CodigoCatalogoBean codeCatBean = new CodigoCatalogoBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Dato_Codigo_Catalogo_Seleccionado", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdRegistro", keytypejob));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    codeCatBean.iId          = Convert.ToInt32(data["id"].ToString());
                    codeCatBean.sCatalogo    = data["Catalogo"].ToString();
                    codeCatBean.sCodigo      = data["Codigo"].ToString();
                    codeCatBean.sDescripcion = data["Descripcion"].ToString();
                    codeCatBean.sCancelado   = data["Cancelado"].ToString();
                    codeCatBean.iConsecutivo = (data["Consecutivo"].ToString().Length > 0) ? Convert.ToInt32(data["Consecutivo"].ToString()) : 0;
                } else {
                    codeCatBean.sMensaje = "NODATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                codeCatBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return codeCatBean;
        }

    }

    public class CentrosCostosDao : Conexion
    {
        public List<CentrosCostosBean> sp_CentrosCostos_Retrieve_Search_CentrosCostos(string wordsearch, int keyemp)
        {
            List<CentrosCostosBean> listCentrosCostos = new List<CentrosCostosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CentrosCostos_Retrieve_Search_CentrosCostos", this.conexion)
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
                        CentrosCostosBean centroCostoBean = new CentrosCostosBean();
                        centroCostoBean.iIdCentroCosto = Convert.ToInt32(data["IdCentroCosto"].ToString());
                        centroCostoBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                        centroCostoBean.sCentroCosto = data["CentroCosto"].ToString();
                        centroCostoBean.sDescripcionCentroCosto = data["DescripcionCentroCosto"].ToString();
                        centroCostoBean.iEstadoCentroCosto = Convert.ToInt32(data["EstadoCentroCosto"].ToString());
                        centroCostoBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        centroCostoBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listCentrosCostos.Add(centroCostoBean);
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
            return listCentrosCostos;
        }

        public List<CentrosCostosBean> sp_CentrosCostos_Retrieve_CentrosCostos(int state, string type, int keycos, int keyemp)
        {
            List<CentrosCostosBean> listCentrosCostosBean = new List<CentrosCostosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CentrosCostos_Retrieve_CentrosCostos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoCentro", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdCentroCosto", keycos));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CentrosCostosBean centroCostoBean = new CentrosCostosBean();
                        centroCostoBean.iIdCentroCosto = Convert.ToInt32(data["IdCentroCosto"].ToString());
                        centroCostoBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                        centroCostoBean.sCentroCosto = data["CentroCosto"].ToString();
                        centroCostoBean.sDescripcionCentroCosto = data["DescripcionCentroCosto"].ToString();
                        centroCostoBean.iEstadoCentroCosto = Convert.ToInt32(data["EstadoCentroCosto"].ToString());
                        centroCostoBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        centroCostoBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listCentrosCostosBean.Add(centroCostoBean);
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
            return listCentrosCostosBean;
        }
    
        public CentrosCostosBean sp_Data_Centro_Costo (int keyemp, int keycentrcost)
        {
            CentrosCostosBean centrCostBean = new CentrosCostosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Data_Centro_Costo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdCentroCosto", keycentrcost));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    centrCostBean.iIdCentroCosto = Convert.ToInt32(data["IdCentroCosto"].ToString());
                    centrCostBean.iIdEmpresa     = Convert.ToInt32(data["Empresa_id"].ToString());
                    centrCostBean.sCentroCosto   = data["CentroCosto"].ToString();
                    centrCostBean.sDescripcionCentroCosto = data["DescripcionCentroCosto"].ToString();
                    centrCostBean.iEstadoCentroCosto      =  Convert.ToInt32(data["EstadoCentroCosto"].ToString());
                    centrCostBean.sMensaje                = "success";
                } else {
                    centrCostBean.sMensaje = "NODATALOAD";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                centrCostBean.sMensaje = exc.Message.ToString();
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return centrCostBean;
        }
    
        public CentrosCostosBean sp_Update_Centro_Costo(int keycentrcost, string ncentrocosto, string dcentrocosto, int keyemp)
        {
            CentrosCostosBean centrCostBean = new CentrosCostosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Update_Centro_Costo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdCentroCosto", keycentrcost));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyemp));
                cmd.Parameters.Add(new SqlParameter("@CentroCosto", ncentrocosto));
                cmd.Parameters.Add(new SqlParameter("@DescripcionCC", dcentrocosto));
                if (cmd.ExecuteNonQuery() > 0) {
                    centrCostBean.sMensaje = "success";
                } else {
                    centrCostBean.sMensaje = "NOTUPDATE";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                centrCostBean.sMensaje = exc.Message.ToString();
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return centrCostBean;
        }
        
        public CentrosCostosBean sp_Insert_Centro_Costo(int keyEmpr, string ncentrcost, string dcentrcost, int keyUser)
        {
            CentrosCostosBean centrCostBean = new CentrosCostosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Insert_Centro_Costo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyEmpr));
                cmd.Parameters.Add(new SqlParameter("@CentroCosto", ncentrcost));
                cmd.Parameters.Add(new SqlParameter("@DescripcionCC", dcentrcost));
                cmd.Parameters.Add(new SqlParameter("@UsuarioA", keyUser));
                if (cmd.ExecuteNonQuery() > 0) {
                    centrCostBean.sMensaje = "success";
                } else {
                    centrCostBean.sMensaje = "ERRINSERT";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                centrCostBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return centrCostBean;
        }

    }
    public class EdificiosDao : Conexion
    {
        public List<EdificiosBean> sp_Edificios_Retrieve_Search_Edificios(string wordsearch)
        {
            List<EdificiosBean> listEdificiosBean = new List<EdificiosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Edificios_Retrieve_Search_Edificios", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlWordSearch", wordsearch));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EdificiosBean edificioBean = new EdificiosBean();
                        edificioBean.iIdEdificio = Convert.ToInt32(data["IdEdificio"].ToString());
                        edificioBean.sNombreEdificio = data["NombreEdificio"].ToString();
                        edificioBean.sCodigoPostal = data["CodigoPostal"].ToString();
                        edificioBean.sCalle = data["Calle"].ToString();
                        edificioBean.sDelegacion = data["Delegacion"].ToString();
                        edificioBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        edificioBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listEdificiosBean.Add(edificioBean);
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
            return listEdificiosBean;
        }

        public List<EdificiosBean> sp_Edificios_Retrieve_Edificios(string type, int keyedi)
        {
            List<EdificiosBean> listEdificiosBean = new List<EdificiosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Edificios_Retrieve_Edificios", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEdificio", keyedi));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EdificiosBean edificioBean = new EdificiosBean();
                        edificioBean.iIdEdificio = Convert.ToInt32(data["IdEdificio"].ToString());
                        edificioBean.sNombreEdificio = data["NombreEdificio"].ToString();
                        edificioBean.sCodigoPostal = data["CodigoPostal"].ToString();
                        edificioBean.sCalle = data["Calle"].ToString();
                        edificioBean.sDelegacion = data["Delegacion"].ToString();
                        edificioBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        edificioBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listEdificiosBean.Add(edificioBean);
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
            return listEdificiosBean;
        }
    }
    public class NivelEstructuraDao : Conexion
    {
        public List<NivelEstructuraBean> sp_NivelEstructura_Retrieve_NivelEstructura(int state, string type, int keyniv, int keyemp)
        {
            List<NivelEstructuraBean> listNivelEstructuraBean = new List<NivelEstructuraBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NivelEstructura_Retrieve_NivelEstructura", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstadoNivel", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdNivel", keyniv));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaId", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NivelEstructuraBean nivelEstructuraBean = new NivelEstructuraBean();
                        nivelEstructuraBean.iIdNivelEstructura = Convert.ToInt32(data["IdNivelEstructura"].ToString());
                        nivelEstructuraBean.iEmpresaId = Convert.ToInt32(data["Empresa_id"].ToString());
                        nivelEstructuraBean.sNivelEstructura = data["NivelEstructura"].ToString();
                        nivelEstructuraBean.sDescripcion = data["Descripcion"].ToString();
                        nivelEstructuraBean.iEstadoNivelEstructura = Convert.ToInt32(data["EstadoNivelEstructura"].ToString());
                        nivelEstructuraBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        nivelEstructuraBean.SFechaAlta = data["Fecha_Alta"].ToString();
                        listNivelEstructuraBean.Add(nivelEstructuraBean);
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
            return listNivelEstructuraBean;
        }
    }
    public class DepartamentosDao : Conexion
    {
        public DepartamentosBean sp_Departamentos_Retrieve_Departamento(int clvdep)
        {
            DepartamentosBean departamentosBean = new DepartamentosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Departamentos_Retrieve_Departamento", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdDepartamento", clvdep));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    departamentosBean.iIdDepartamento = Convert.ToInt32(data["IdDepartamento"].ToString());
                    departamentosBean.iEmpresaId = Convert.ToInt32(data["Empresa_id"].ToString());
                    departamentosBean.sDeptoCodigo = data["Depto_Codigo"].ToString();
                    departamentosBean.sDescripcionDepartamento = data["DescripcionDepartamento"].ToString();
                    departamentosBean.sNivelEstructura = (String.IsNullOrEmpty(data["NivelEstructura"].ToString())) ? "" : data["NivelEstructura"].ToString();
                    departamentosBean.sNivelSuperior = (String.IsNullOrEmpty(data["NivelSuperior"].ToString())) ? "" : data["NivelSuperior"].ToString();
                    if (data["Edificio_id"].ToString().Length != 0)
                    {
                        departamentosBean.iEdificioId = Convert.ToInt32(data["Edificio_id"].ToString());
                    }
                    else { departamentosBean.iEdificioId = 0; }
                    departamentosBean.sEdificioN = data["NombreEdificio"].ToString();
                    departamentosBean.sPiso = (String.IsNullOrEmpty(data["Piso"].ToString())) ? "" : data["Piso"].ToString();
                    departamentosBean.sUbicacion = (String.IsNullOrEmpty(data["Ubicacion"].ToString())) ? "" : data["Ubicacion"].ToString();
                    if (data["CentroCosto_id"].ToString().Length != 0)
                    {
                        departamentosBean.iCentroCostoId = Convert.ToInt32(data["CentroCosto_id"].ToString());
                    }
                    else { departamentosBean.iCentroCostoId = 0; }
                    departamentosBean.sCentroCosto = data["CentroCosto"].ToString();
                    if (data["EmpresaReporta_id"].ToString().Length != 0)
                    {
                        departamentosBean.iEmpresaReportaId = Convert.ToInt32(data["EmpresaReporta_id"].ToString());
                    }
                    else { departamentosBean.iEmpresaReportaId = 0; }
                    departamentosBean.sDGA = (String.IsNullOrEmpty(data["DGA"].ToString())) ? "" : data["DGA"].ToString();
                    departamentosBean.sDirecGen = (String.IsNullOrEmpty(data["Direccion_General"].ToString())) ? "" : data["Direccion_General"].ToString();
                    departamentosBean.sDirecEje = (String.IsNullOrEmpty(data["Direccion_Ejecutiva"].ToString())) ? "" : data["Direccion_Ejecutiva"].ToString();
                    departamentosBean.sDirecAre = (String.IsNullOrEmpty(data["Direccion_Areas"].ToString())) ? "" : data["Direccion_Areas"].ToString();
                    departamentosBean.iEmpreDirGen = Convert.ToInt32(data["Empr_Dir_General_id"].ToString());
                    departamentosBean.iEmpreDirEje = Convert.ToInt32(data["Empr_Dir_Ejecutiva_id"].ToString());
                    departamentosBean.iEmpreDirAre = Convert.ToInt32(data["Empr_Dir_Area_id"].ToString());
                    departamentosBean.sCancelado = data["Cancelado"].ToString();
                    departamentosBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                    departamentosBean.sFechaAlta = data["Fecha_Alta"].ToString();
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
            return departamentosBean;
        }
        public List<DepartamentosBean> sp_Departamentos_Retrieve_Search_Departamentos(string wordsearch, int keyemp, string type)
        {
            List<DepartamentosBean> listDepartamentoBean = new List<DepartamentosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Departamentos_Retrieve_Search_Departamentos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlWordSearch", wordsearch));
                cmd.Parameters.Add(new SqlParameter("@ctrlEmpresaId", keyemp));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipo", type));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DepartamentosBean departamentosBean = new DepartamentosBean();
                        departamentosBean.iIdDepartamento = Convert.ToInt32(data["IdDepartamento"].ToString());
                        departamentosBean.iEmpresaId = Convert.ToInt32(data["Empresa_id"].ToString());
                        departamentosBean.sDeptoCodigo = data["Depto_Codigo"].ToString();
                        departamentosBean.sDescripcionDepartamento = data["DescripcionDepartamento"].ToString();
                        departamentosBean.sNivelEstructura = (String.IsNullOrEmpty(data["NivelEstructura"].ToString())) ? "" : data["NivelEstructura"].ToString();
                        departamentosBean.sNivelSuperior = (String.IsNullOrEmpty(data["NivelSuperior"].ToString())) ? "" : data["NivelSuperior"].ToString();
                        if (data["Edificio_id"].ToString().Length != 0)
                        {
                            departamentosBean.iEdificioId = Convert.ToInt32(data["Edificio_id"].ToString());
                        }
                        else { departamentosBean.iEdificioId = 0; }
                        departamentosBean.sPiso = (String.IsNullOrEmpty(data["Piso"].ToString())) ? "" : data["Piso"].ToString();
                        departamentosBean.sUbicacion = (String.IsNullOrEmpty(data["Ubicacion"].ToString())) ? "" : data["Ubicacion"].ToString();
                        if (data["CentroCosto_id"].ToString().Length != 0)
                        {
                            departamentosBean.iCentroCostoId = Convert.ToInt32(data["CentroCosto_id"].ToString());
                        }
                        else { departamentosBean.iCentroCostoId = 0; }
                        if (data["EmpresaReporta_id"].ToString().Length != 0)
                        {
                            departamentosBean.iEmpresaReportaId = Convert.ToInt32(data["EmpresaReporta_id"].ToString());
                        }
                        else { departamentosBean.iEmpresaReportaId = 0; }
                        departamentosBean.sDGA = (String.IsNullOrEmpty(data["DGA"].ToString())) ? "" : data["DGA"].ToString();
                        departamentosBean.sDirecGen = (String.IsNullOrEmpty(data["Direccion_General"].ToString())) ? "" : data["Direccion_General"].ToString();
                        departamentosBean.sDirecEje = (String.IsNullOrEmpty(data["Direccion_Ejecutiva"].ToString())) ? "" : data["Direccion_Ejecutiva"].ToString();
                        departamentosBean.sDirecAre = (String.IsNullOrEmpty(data["Direccion_Areas"].ToString())) ? "" : data["Direccion_Areas"].ToString();
                        departamentosBean.iEmpreDirGen = Convert.ToInt32(data["Empr_Dir_General_id"].ToString());
                        departamentosBean.iEmpreDirEje = Convert.ToInt32(data["Empr_Dir_Ejecutiva_id"].ToString());
                        departamentosBean.iEmpreDirAre = Convert.ToInt32(data["Empr_Dir_Area_id"].ToString());
                        departamentosBean.sCancelado = data["Cancelado"].ToString();
                        departamentosBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        departamentosBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listDepartamentoBean.Add(departamentosBean);
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
            return listDepartamentoBean;
        }
        public List<DepartamentosBean> sp_Departamentos_Retrieve_Departamentos(int state, string type, int keydep, int keyemp)
        {
            List<DepartamentosBean> listDepartamentosBean = new List<DepartamentosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Departamentos_Retrieve_Departamentos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlEstado", state));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoFiltro", type));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdDepartamento", keydep));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DepartamentosBean departamentosBean = new DepartamentosBean();
                        departamentosBean.iIdDepartamento = Convert.ToInt32(data["IdDepartamento"].ToString());
                        departamentosBean.iEmpresaId = Convert.ToInt32(data["Empresa_id"].ToString());
                        departamentosBean.sDeptoCodigo = data["Depto_Codigo"].ToString();
                        departamentosBean.sDescripcionDepartamento = data["DescripcionDepartamento"].ToString();
                        departamentosBean.sNivelEstructura = (String.IsNullOrEmpty(data["NivelEstructura"].ToString())) ? "" : data["NivelEstructura"].ToString();
                        departamentosBean.sNivelSuperior = (String.IsNullOrEmpty(data["NivelSuperior"].ToString())) ? "" : data["NivelSuperior"].ToString();
                        if (data["Edificio_id"].ToString().Length != 0)
                        {
                            departamentosBean.iEdificioId = Convert.ToInt32(data["Edificio_id"].ToString());
                        }
                        else { departamentosBean.iEdificioId = 0; }
                        departamentosBean.sPiso = (String.IsNullOrEmpty(data["Piso"].ToString())) ? "" : data["Piso"].ToString();
                        departamentosBean.sUbicacion = (String.IsNullOrEmpty(data["Ubicacion"].ToString())) ? "" : data["Ubicacion"].ToString();
                        if (data["CentroCosto_id"].ToString().Length != 0)
                        {
                            departamentosBean.iCentroCostoId = Convert.ToInt32(data["CentroCosto_id"].ToString());
                        }
                        else { departamentosBean.iCentroCostoId = 0; }
                        if (data["EmpresaReporta_id"].ToString().Length != 0)
                        {
                            departamentosBean.iEmpresaReportaId = Convert.ToInt32(data["EmpresaReporta_id"].ToString());
                        }
                        else { departamentosBean.iEmpresaReportaId = 0; }
                        departamentosBean.sDGA = (String.IsNullOrEmpty(data["DGA"].ToString())) ? "" : data["DGA"].ToString();
                        departamentosBean.sDirecGen = (String.IsNullOrEmpty(data["Direccion_General"].ToString())) ? "" : data["Direccion_General"].ToString();
                        departamentosBean.sDirecEje = (String.IsNullOrEmpty(data["Direccion_Ejecutiva"].ToString())) ? "" : data["Direccion_Ejecutiva"].ToString();
                        departamentosBean.sDirecAre = (String.IsNullOrEmpty(data["Direccion_Areas"].ToString())) ? "" : data["Direccion_Areas"].ToString();
                        departamentosBean.iEmpreDirGen = Convert.ToInt32(data["Empr_Dir_General_id"].ToString());
                        departamentosBean.iEmpreDirEje = Convert.ToInt32(data["Empr_Dir_Ejecutiva_id"].ToString());
                        departamentosBean.iEmpreDirAre = Convert.ToInt32(data["Empr_Dir_Area_id"].ToString());
                        departamentosBean.sCancelado = data["Cancelado"].ToString();
                        departamentosBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_id"].ToString());
                        departamentosBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listDepartamentosBean.Add(departamentosBean);
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
            return listDepartamentosBean;
        }
    }
    public class TipoPeriodosDao : Conexion
    {
        public List<TipoPeriodosBean> sp_TipoPeriodos_Retrieve_TipoPeriodos()
        {
            List<TipoPeriodosBean> listTipoPeriodo = new List<TipoPeriodosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TipoPeriodos_Retrieve_TipoPeriodos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TipoPeriodosBean tipoPeriodo = new TipoPeriodosBean();
                        tipoPeriodo.iId = Convert.ToInt32(data["id"].ToString());
                        tipoPeriodo.sValor = data["Valor"].ToString().ToUpper();
                        listTipoPeriodo.Add(tipoPeriodo);
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
            return listTipoPeriodo;
        }
    }
    public class LocalidadesDao : Conexion
    {

        public List<LocalidadesBean2> sp_Localidades_Retrieve_Search_Localidades(string wordsearch, int keyemp)
        {
            List<LocalidadesBean2> listLocalidadesBean = new List<LocalidadesBean2>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Localidades_Retrieve_Search_Localidades", this.conexion)
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
                        LocalidadesBean2 localidadBean = new LocalidadesBean2();
                        localidadBean.iIdLocalidad = Convert.ToInt32(data["IdLocalidad"].ToString());
                        localidadBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                        localidadBean.iCodigoLocalidad = Convert.ToInt32(data["Codigo_Localidad"].ToString());
                        localidadBean.sDescripcion = data["Descripcion"].ToString();
                        localidadBean.sRegistroPatronal = data["Afiliacion_IMSS"].ToString();
                        //localidadBean.dTazIva = Convert.ToDouble(data["TasaIva"].ToString());
                        if (data["RegistroPatronal_id"].ToString().Length != 0)
                        {
                            localidadBean.iRegistroPatronal_id = Convert.ToInt32(data["RegistroPatronal_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iRegistroPatronal_id = 0;
                        }
                        if (data["Regional_id"].ToString().Length != 0)
                        {
                            localidadBean.iRegional_id = Convert.ToInt32(data["Regional_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iRegional_id = 0;
                        }
                        if (data["ZonaEconomica_id"].ToString().Length != 0)
                        {
                            localidadBean.iZonaEconomica_id = Convert.ToInt32(data["ZonaEconomica_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iZonaEconomica_id = 0;
                        }
                        if (data["Sucursal_id"].ToString().Length != 0)
                        {
                            localidadBean.iSucursal_id = Convert.ToInt32(data["Sucursal_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iSucursal_id = 0;
                        }
                        if (data["Cg_Estado_id"].ToString().Length != 0)
                        {
                            localidadBean.iEstado_id = Convert.ToInt32(data["Cg_Estado_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iEstado_id = 0;
                        }
                        listLocalidadesBean.Add(localidadBean);
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
            return listLocalidadesBean;
        }

        public List<LocalidadesBean2> sp_TLocalicades_Retrieve_Localidades(int keyemp)
        {
            List<LocalidadesBean2> listLocalidadesBean = new List<LocalidadesBean2>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TLocalicades_Retrieve_Localidades", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        LocalidadesBean2 localidadBean = new LocalidadesBean2();
                        localidadBean.iIdLocalidad = Convert.ToInt32(data["IdLocalidad"].ToString());
                        localidadBean.iIdEmpresa = Convert.ToInt32(data["Empresa_id"].ToString());
                        localidadBean.iCodigoLocalidad = Convert.ToInt32(data["Codigo_Localidad"].ToString());
                        localidadBean.sDescripcion = data["Descripcion"].ToString();
                        localidadBean.dTazIva = Convert.ToDouble(data["TasaIva"].ToString());
                        if (data["RegistroPatronal_id"].ToString().Length != 0)
                        {
                            localidadBean.iRegistroPatronal_id = Convert.ToInt32(data["RegistroPatronal_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iRegistroPatronal_id = 0;
                        }
                        if (data["Regional_id"].ToString().Length != 0)
                        {
                            localidadBean.iRegional_id = Convert.ToInt32(data["Regional_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iRegional_id = 0;
                        }
                        if (data["ZonaEconomica_id"].ToString().Length != 0)
                        {
                            localidadBean.iZonaEconomica_id = Convert.ToInt32(data["ZonaEconomica_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iZonaEconomica_id = 0;
                        }
                        if (data["Sucursal_id"].ToString().Length != 0)
                        {
                            localidadBean.iSucursal_id = Convert.ToInt32(data["Sucursal_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iSucursal_id = 0;
                        }
                        if (data["Cg_Estado_id"].ToString().Length != 0)
                        {
                            localidadBean.iEstado_id = Convert.ToInt32(data["Cg_Estado_id"].ToString());
                        }
                        else
                        {
                            localidadBean.iEstado_id = 0;
                        }
                        listLocalidadesBean.Add(localidadBean);
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
            return listLocalidadesBean;
        }

        public LocalidadesBean sp_Dato_Localidad_Seleccionada(int keyLocality, int keyEmp)
        {
            LocalidadesBean localityBean = new LocalidadesBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Dato_Localidad_Seleccionada", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdLocalidad", keyLocality));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyEmp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    localityBean.IdLocalidad         = Convert.ToInt32(data["IdLocalidad"].ToString());
                    localityBean.Empresa_id          = Convert.ToInt32(data["Empresa_id"].ToString());
                    localityBean.Codigo_Localidad    = Convert.ToInt32(data["Codigo_Localidad"].ToString());
                    localityBean.Descripcion         = data["Descripcion"].ToString();
                    localityBean.TasaIva             = data["TazaIva"].ToString();
                    localityBean.RegistroPatronal_id = Convert.ToInt32(data["RegistroPatronal_id"].ToString());
                    localityBean.ZonaEconomica_id    = Convert.ToInt32(data["ZonaEconomica_id"].ToString());
                    localityBean.Regional_id         = Convert.ToInt32(data["Regional_id"].ToString());
                    localityBean.ClaveRegional       = data["Clave_Regional"].ToString();
                    localityBean.DescripcionRegional = data["Descripcion_Regional"].ToString();
                    localityBean.Sucursal_id         = Convert.ToInt32(data["Sucursal_id"].ToString());
                    localityBean.ClaveSucursal       = data["Clave_Sucursal"].ToString();
                    localityBean.DescripcionSucursal = data["Descripcion_Sucursal"].ToString();
                    localityBean.Estado_id           = Convert.ToInt32(data["Cg_Estado_id"].ToString());
                    localityBean.sMensaje            = "success";
                } else {
                    localityBean.sMensaje = "NODATALOAD";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                localityBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return localityBean;
        }

        public LocalidadesBean2 sp_Update_Localidad(int keylocality, string desclocality, string ivalocality, int regpatlocality, int idreglocality, int zonelocality, int idsuclocality, int estatelocality)
        {
            LocalidadesBean2 saveEditLocBean = new LocalidadesBean2();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Update_Localidad", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdLocalidad", keylocality));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", desclocality));
                cmd.Parameters.Add(new SqlParameter("@TasIva", ivalocality));
                cmd.Parameters.Add(new SqlParameter("@RegistroPatronalId", regpatlocality));
                cmd.Parameters.Add(new SqlParameter("@RegionalId", idreglocality));
                cmd.Parameters.Add(new SqlParameter("@ZonaEconomicaId", zonelocality));
                cmd.Parameters.Add(new SqlParameter("@SucursalId", idsuclocality));
                cmd.Parameters.Add(new SqlParameter("@EstadoId", estatelocality));
                if (cmd.ExecuteNonQuery() > 0) {
                    saveEditLocBean.sMensaje = "success";
                } else {
                    saveEditLocBean.sMensaje = "NOTUPDATE";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                saveEditLocBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return saveEditLocBean;
        }

        public LocalidadesBean2 sp_Insert_Localidad(string desclocality, string ivalocality, int regpatlocality, int idreglocality, int zonelocality, int idsuclocality, int estatelocality, int keyemp)
        {
            LocalidadesBean2 saveDataLocBean = new LocalidadesBean2();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Insert_Localidad", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Descripcion", desclocality));
                cmd.Parameters.Add(new SqlParameter("@TasIva", ivalocality));
                cmd.Parameters.Add(new SqlParameter("@RegistroPatronalId", regpatlocality));
                cmd.Parameters.Add(new SqlParameter("@RegionalId", idreglocality));
                cmd.Parameters.Add(new SqlParameter("@ZonaEconomicaId", zonelocality));
                cmd.Parameters.Add(new SqlParameter("@SucursalId", idsuclocality));
                cmd.Parameters.Add(new SqlParameter("@EstadoId", estatelocality));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (data["Respuesta"].ToString() == "INSERT") {
                        saveDataLocBean.sMensaje         = "success";
                        saveDataLocBean.iCodigoLocalidad = Convert.ToInt32(data["Codigo"].ToString());
                    } else {
                        saveDataLocBean.sMensaje = data["Respuesta"].ToString();
                    }
                } else {
                    saveDataLocBean.sMensaje = "ERRINSERT";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                saveDataLocBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return saveDataLocBean;
        }

    }

    public class ZonaEconomicaDao : Conexion
    {
        public List<ZonaEconomicaBean> sp_Datos_Zonas_Economicas()
        {
            List<ZonaEconomicaBean> zoneEcoBean = new List<ZonaEconomicaBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Zonas_Economicas", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        zoneEcoBean.Add(new ZonaEconomicaBean { 
                            iIdZonaEconomica = Convert.ToInt32(data["IdZonaEconomica"].ToString()),
                            sDescripcion     = data["Descripcion"].ToString()
                        });
                    }
                }
                cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return zoneEcoBean;
        }
    }

    public class RegistroPatronalDao : Conexion
    {
        public List<RegistroPatronalBean2> sp_Registro_Patronal_Retrieve_Registros_Patronales(int keyemp)
        {
            List<RegistroPatronalBean2> listRegPatronal = new List<RegistroPatronalBean2>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Registro_Patronal_Retrieve_Registros_Patronales", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliEmpresa_id", keyemp));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        RegistroPatronalBean2 regPatronal = new RegistroPatronalBean2();
                        regPatronal.iIdRegPat = Convert.ToInt32(data["IdRegPat"].ToString());
                        regPatronal.iEmpresaid = Convert.ToInt32(data["Empresa_id"].ToString());
                        regPatronal.sAfiliacionIMSS = data["Afiliacion_IMSS"].ToString();
                        regPatronal.sNombreAfiliacion = data["Nombre_Afiliacion"].ToString();
                        regPatronal.sRiesgoTrabajo = data["Riesgo_Trabajo"].ToString();
                        regPatronal.iClasesRegPat_id = Convert.ToInt32(data["ClasesRegPat_id"].ToString());
                        regPatronal.sCancelado = data["Cancelado"].ToString();
                        listRegPatronal.Add(regPatronal);
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
            return listRegPatronal;
        }
    }
    public class RegionesDao : Conexion
    {
        public List<RegionalesBean> sp_Regionales_Retrieve_Search_Regionales(string wordsearch, int keyemp)
        {
            List<RegionalesBean> listRegionalesBean = new List<RegionalesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Regionales_Retrieve_Search_Regionales", this.conexion)
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
                        RegionalesBean regionalBean = new RegionalesBean();
                        regionalBean.iIdRegional = Convert.ToInt32(data["IdRegional"].ToString());
                        regionalBean.iEmpresaId = Convert.ToInt32(data["Empresa_id"].ToString());
                        regionalBean.sDescripcionRegional = data["Descripcion_Regional"].ToString();
                        regionalBean.sClaveRegional = data["Clave_Regional"].ToString();
                        regionalBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_Id"].ToString());
                        regionalBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listRegionalesBean.Add(regionalBean);
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
            return listRegionalesBean;
        }

        public RegionalesBean sp_Regionales_Retrieve_Regional(int clvregion)
        {
            RegionalesBean regionalBean = new RegionalesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Regionales_Retrieve_Regional", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdRegional", clvregion));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    regionalBean.iIdRegional = Convert.ToInt32(data["IdRegional"].ToString());
                    regionalBean.iEmpresaId = Convert.ToInt32(data["Empresa_id"].ToString());
                    regionalBean.sDescripcionRegional = data["Descripcion_Regional"].ToString();
                    regionalBean.sClaveRegional = data["Clave_Regional"].ToString();
                    regionalBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_Id"].ToString());
                    regionalBean.sFechaAlta = data["Fecha_Alta"].ToString();
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
            return regionalBean;
        }
        public List<RegionalesBean> sp_Regionales_Retrieve_Regionales()
        {
            List<RegionalesBean> listRegionalesBean = new List<RegionalesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Regionales_Retrieve_Regionales", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        RegionalesBean regionalBean = new RegionalesBean();
                        regionalBean.iIdRegional = Convert.ToInt32(data["IdRegional"].ToString());
                        regionalBean.sDescripcionRegional = data["Descripcion_Regional"].ToString();
                        regionalBean.sClaveRegional = data["Clave_Regional"].ToString();
                        regionalBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_Id"].ToString());
                        regionalBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listRegionalesBean.Add(regionalBean);
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
            return listRegionalesBean;
        }

        public RegionalesBean sp_Regionales_Insert_Regionales(string descregion, string claregion, int usuario, int keyemp)
        {
            RegionalesBean regionBean = new RegionalesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Regionales_Insert_Regionales", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", descregion));
                cmd.Parameters.Add(new SqlParameter("@ctrlClaveRegion", claregion));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyemp));
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
                string origenerror = "CatalogosDao";
                string mensajeerror = exc.ToString();
                CapturaErroresBean capturaErrorBean = new CapturaErroresBean();
                CapturaErrores capturaErrorDao = new CapturaErrores();
                capturaErrorBean = capturaErrorDao.sp_Errores_Insert_Errores(origenerror, mensajeerror);
                Console.WriteLine(exc);
            }
            return regionBean;
        }
    }
    public class SucursalesDao : Conexion
    {

        public List<SucursalesBean> sp_Sucursales_Retrieve_Search_Sucursales(string wordsearch)
        {
            List<SucursalesBean> listSucursalesBean = new List<SucursalesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Sucursales_Retrieve_Search_Sucursales", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlWordSearch", wordsearch));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        SucursalesBean sucursalBean = new SucursalesBean();
                        sucursalBean.iIdSucursal = Convert.ToInt32(data["IdSucursal"].ToString());
                        sucursalBean.sDescripcionSucursal = data["Descripcion_Sucursal"].ToString();
                        sucursalBean.sClaveSucursal = data["Clave_Sucursal"].ToString();
                        sucursalBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_Id"].ToString());
                        sucursalBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listSucursalesBean.Add(sucursalBean);
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
            return listSucursalesBean;
        }

        public SucursalesBean sp_Sucursales_Retrieve_Sucursal(int clvsucursal)
        {
            SucursalesBean sucursalBean = new SucursalesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Sucursales_Retrieve_Sucursal", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdSucursal", clvsucursal));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    sucursalBean.iIdSucursal = Convert.ToInt32(data["IdSucursal"].ToString());
                    sucursalBean.sDescripcionSucursal = data["Descripcion_Sucursal"].ToString();
                    sucursalBean.sClaveSucursal = data["Clave_Sucursal"].ToString();
                    sucursalBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_Id"].ToString());
                    sucursalBean.sFechaAlta = data["Fecha_Alta"].ToString();
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
            return sucursalBean;
        }
        public List<SucursalesBean> sp_Sucursales_Retrieve_Sucursales()
        {
            List<SucursalesBean> listSucursalesBean = new List<SucursalesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Sucursales_Retrieve_Sucursales", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        SucursalesBean sucursalBean = new SucursalesBean();
                        sucursalBean.iIdSucursal = Convert.ToInt32(data["IdSucursal"].ToString());
                        sucursalBean.sDescripcionSucursal = data["Descripcion_Sucursal"].ToString();
                        sucursalBean.sClaveSucursal = data["Clave_Sucursal"].ToString();
                        sucursalBean.iUsuarioAltaId = Convert.ToInt32(data["Usuario_Alta_Id"].ToString());
                        sucursalBean.sFechaAlta = data["Fecha_Alta"].ToString();
                        listSucursalesBean.Add(sucursalBean);
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
            return listSucursalesBean;
        }

        public SucursalesBean sp_Sucursales_Insert_Sucursales(string descsucursal, string clasucursal, int usuario)
        {
            SucursalesBean sucursalBean = new SucursalesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Sucursales_Insert_Sucursales", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", descsucursal));
                cmd.Parameters.Add(new SqlParameter("@ctrlClaveSucursal", clasucursal));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuario", usuario));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (data["sRespuesta"].ToString() == "") {
                        sucursalBean.sMensaje = "success";
                    } else {
                        sucursalBean.sMensaje = data["sRespuesta"].ToString();
                    }
                } else {
                    sucursalBean.sMensaje = "error";
                }
                cmd.Dispose(); cmd.Parameters.Clear(); conexion.Close();
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
            return sucursalBean;
        }

    }
    public class NacionalidadesDao : Conexion
    {
        public List<NacionalidadesBean> sp_Nacionalidades_Retrieve_Nacionalidades()
        {
            List<NacionalidadesBean> listNacionBean = new List<NacionalidadesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Nacionalidades_Retrieve_Nacionalidades", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NacionalidadesBean nacionBean = new NacionalidadesBean();
                        nacionBean.iIdNacionalidad = Convert.ToInt32(data["IdNacionalidad"].ToString());
                        nacionBean.sDescripcion = data["Descripcion"].ToString();
                        listNacionBean.Add(nacionBean);
                    }
                }
                cmd.Dispose(); cmd.Parameters.Clear(); data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return listNacionBean;
        }
    }
}