using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class PruebaEmpresaDao : Conexion
    {

        public List<PruebaEmpresaBean> sp_Retrieve_PruevaEmpresas()
        {
            List<PruebaEmpresaBean> list = new List<PruebaEmpresaBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_Empresas", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    PruebaEmpresaBean listEmpresas = new PruebaEmpresaBean
                    {
                        IdEmpresa = int.Parse(data["IdEmpresa"].ToString()),
                        NombreEmpresa = data["NombreEmpresa"].ToString()
                    };
                    list.Add(listEmpresas);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<PruebaEmpresaBean> sp_Retrieve_NombreEmpresas()
        {
            List<PruebaEmpresaBean> list = new List<PruebaEmpresaBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_Empresas", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    PruebaEmpresaBean listEmpresas = new PruebaEmpresaBean();
                    //
                    //--------------QUITAR ESTE FILTRO UNA VEZ QUE SE ARREGLEN LAS EMPRESAS Y LOS EMPLEADOS 
                    //if (int.Parse(data["IdEmpresa"].ToString()) > 3)
                    //{
                        listEmpresas.IdEmpresa = int.Parse(data["IdEmpresa"].ToString());
                        listEmpresas.RazonSocial = data["RazonSocial"].ToString();
                        listEmpresas.NombreEmpresa = data["NombreEmpresa"].ToString();
                        list.Add(listEmpresas);
                    //}
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }

        public int sp_Retrieve_ClaveEmpresa()
        {
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_ClaveEmpresa", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            int ClaveEmpresa = 0;
            if (data.HasRows)
            {
                while (data.Read())
                {
                    ClaveEmpresa = int.Parse(data["max"].ToString());
                }
            }
            data.Close();
            return ClaveEmpresa + 1;
        }
        public List<PruebaEmpresaBean> sp_Retrieve_NombreEmpresa(int IdEmpresa)
        {
            List<PruebaEmpresaBean> list = new List<PruebaEmpresaBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_Empresa", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    PruebaEmpresaBean listEmpresas = new PruebaEmpresaBean
                    {
                        IdEmpresa = int.Parse(data["IdEmpresa"].ToString()),
                        RazonSocial = data["RazonSocial"].ToString(),
                        NombreEmpresa = data["NombreEmpresa"].ToString()
                    };
                    list.Add(listEmpresas);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<string> sp_CEmpresas_Retrieve_EmpresaD(int IdEmpresa)
        {
            List<string> empresa = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_EmpresaD", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", IdEmpresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {

                    empresa.Add(data["IdEmpresa"].ToString());
                    empresa.Add(data["NombreEmpresa"].ToString());
                    empresa.Add(data["RazonSocial"].ToString());
                    empresa.Add(data["CodigoPostal"].ToString());
                    empresa.Add(data["Estado"].ToString());
                    empresa.Add(data["Ciudad"].ToString());
                    empresa.Add(data["Delegacion"].ToString());
                    empresa.Add(data["Colonia"].ToString());
                    empresa.Add(data["Calle"].ToString());
                    empresa.Add(data["Giro"].ToString());
                    empresa.Add(data["RFC"].ToString());
                    empresa.Add(data["FechaAlta"].ToString());
                    empresa.Add(data["Descripcion"].ToString());
                    empresa.Add(data["banco_interbancarios"].ToString());
                    empresa.Add(data["reg_imss_empresa"].ToString());

                }
            }
            else
            {
                empresa = null;
            }
            data.Close();

            return empresa;
        }
        public List<string> sp_CEmpresas_Retrieve_Empresa(int IdEmpresa)
        {
            List<string> empresa = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_Empresa", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {

                    empresa.Add(data["IdEmpresa"].ToString());
                    empresa.Add(data["NombreEmpresa"].ToString());
                    empresa.Add(data["RazonSocial"].ToString());
                    empresa.Add(data["CodigoPostal"].ToString());
                    empresa.Add(data["Estado_id"].ToString());
                    empresa.Add(data["Ciudad"].ToString());
                    empresa.Add(data["Delegacion"].ToString());
                    //empresa.Add(data["Colonia_id"].ToString());
                    empresa.Add(data["Calle"].ToString());
                    empresa.Add(data["Giro"].ToString());
                    empresa.Add(data["RFC"].ToString());
                    empresa.Add(data["FechaAlta"].ToString());
                    empresa.Add(data["Regimen_Fiscal_id"].ToString());
                    empresa.Add(data["banco_interbancarios"].ToString());
                    empresa.Add(data["reg_imss_empresa"].ToString());
                }
            }
            else
            {
                empresa = null;
            }
            data.Close();

            return empresa;
        }
        public List<string> sp_Insert_FirstStep_Empresas(string inNombre_empresa, string inNomCorto_empresa, string inRfc_empresa, string inGiro_empresa, int inRegimenFiscal_Empresa, int inCodigo_postal, int inEstado_empresa, int inMunicipio_empresa, string inCiudad_empresa, string inDelegacion, int inColonia_empresa, string inCalle_Empresa, string inAfiliacionesIMSS, string inNombre_Afiliacion, string inRiesgoTrabajo, int usuario_id, int inClase, string infinicio, string inffinal, string infpago, string infproceso, int indiaspagados, int intipoperiodo)
        {
            List<string> res = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CEmpresas_Insert_Empresas", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlsNombreEmpresa", inNomCorto_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrlsRazonSocial", inNombre_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrliCodigoPostal", inCodigo_postal));
            cmd.Parameters.Add(new SqlParameter("@ctrliEstadoId", inEstado_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrliCiudad", inCiudad_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrliDelegacion", inDelegacion));
            cmd.Parameters.Add(new SqlParameter("@ctrlsColonia", inColonia_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrlsCalle", inCalle_Empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrlsGiro", inGiro_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrliRegimenFiscal", inRegimenFiscal_Empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrlsRFC", inRfc_empresa));
            cmd.Parameters.Add(new SqlParameter("@ctrliUsuarioAltaId", usuario_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlsAfiliacionIMSS", inAfiliacionesIMSS));
            cmd.Parameters.Add(new SqlParameter("@ctrlsNombre_Afiliacion", inNombre_Afiliacion));
            cmd.Parameters.Add(new SqlParameter("@ctrliRiesgoTrabajo", inRiesgoTrabajo));
            cmd.Parameters.Add(new SqlParameter("@ctrliClaseRP", inClase));

            cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo_id", intipoperiodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Inicio", infinicio));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Final", inffinal));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Proceso", infpago));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Pago", infproceso));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Pagados", indiaspagados));

            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    res.Add(data["iFlag"].ToString());
                    res.Add(data["sRespuesta"].ToString());
                    res.Add(data["id"].ToString());
                    res.Add(data["RT"].ToString());
                }
            }
            else
            {
                res = null;
            }
            data.Close();
            return res;
        }
        public List<RegistroPatronalBean> sp_Registro_Patronal_Retrieve_Registros_Patronales(int id_empresa)
        {
            List<RegistroPatronalBean> lista = new List<RegistroPatronalBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Registro_Patronal_Retrieve_Registros_Patronales", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliEmpresa_id", id_empresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    RegistroPatronalBean list = new RegistroPatronalBean
                    {
                        IdRegPat = int.Parse(data["IdRegPat"].ToString()),
                        Afiliacion_IMSS = data["Afiliacion_IMSS"].ToString(),
                        Nombre_Afiliacion = data["Nombre_Afiliacion"].ToString(),
                        Empresa_id = int.Parse(data["Empresa_id"].ToString()),
                        Riesgo_Trabajo = data["Riesgo_Trabajo"].ToString(),
                        ClasesRegPat_id = int.Parse(data["ClasesRegPat_id"].ToString()),
                        Cancelado = data["Cancelado"].ToString()
                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<RegistroPatronalBean> sp_Registro_Patronal_Retrieve_Registro_Patronal(int Empresa_id, int IdRegPat)
        {
            List<RegistroPatronalBean> lista = new List<RegistroPatronalBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Registro_Patronal_Retrieve_Registro_Patronal", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlIdRegPat", IdRegPat));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    RegistroPatronalBean list = new RegistroPatronalBean
                    {
                        IdRegPat = int.Parse(data["IdRegPat"].ToString()),
                        Afiliacion_IMSS = data["Afiliacion_IMSS"].ToString(),
                        Nombre_Afiliacion = data["Nombre_Afiliacion"].ToString(),
                        Empresa_id = int.Parse(data["Empresa_id"].ToString()),
                        Riesgo_Trabajo = data["Riesgo_Trabajo"].ToString(),
                        ClasesRegPat_id = int.Parse(data["ClasesRegPat_id"].ToString()),
                        Cancelado = data["Cancelado"].ToString()
                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<LocalidadesBean> sp_TLocalicades_Retrieve_Localidades(int IdEmpresa)
        {
            List<LocalidadesBean> lista = new List<LocalidadesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TLocalicades_Retrieve_Localidades", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    LocalidadesBean list = new LocalidadesBean();
                    list.IdLocalidad = int.Parse(data["IdLocalidad"].ToString());
                    list.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    list.Codigo_Localidad = int.Parse(data["Codigo_Localidad"].ToString());
                    list.Descripcion = data["Descripcion"].ToString();
                    list.TasaIva = data["TazaIva"].ToString();

                    if (data["RegistroPatronal_id"].ToString().Length != 0) { list.RegistroPatronal_id = int.Parse(data["RegistroPatronal_id"].ToString()); }
                    else
                    {
                        list.RegistroPatronal_id = 0;
                    }

                    if (data["Regional_id"].ToString().Length != 0) { list.Regional_id = int.Parse(data["Regional_id"].ToString()); }
                    else
                    {
                        list.Regional_id = 0;
                    }

                    if (data["ZonaEconomica_id"].ToString().Length != 0) { list.ZonaEconomica_id = int.Parse(data["ZonaEconomica_id"].ToString()); }
                    else
                    {

                        list.ZonaEconomica_id = 0;
                    }

                    if (data["Sucursal_id"].ToString().Length != 0) { list.Sucursal_id = int.Parse(data["Sucursal_id"].ToString()); }
                    else
                    {
                        list.Sucursal_id = 0;
                    }

                    if (data["Cg_Estado_id"].ToString().Length != 0) { list.Estado_id = int.Parse(data["Cg_Estado_id"].ToString()); }
                    else
                    {

                        list.Estado_id = 0;
                    }

                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<CClases_RegPatBean> sp_CClases_RegPat()
        {
            List<CClases_RegPatBean> list = new List<CClases_RegPatBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CClases_RegPat_Retrieve_Clases_RegPat", this.conexion)
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
                    CClases_RegPatBean listEmpresas = new CClases_RegPatBean
                    {
                        IdClase = int.Parse(data["IdClase"].ToString()),
                        Nombre_Clase = data["Nombre_Clase"].ToString(),
                        Descripcion_Clase = data["Descripcion_Clase"].ToString()
                    };
                    list.Add(listEmpresas);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<string> sp_Registro_Patronal_Insert_Registros_Patronales(int Empresa_id, string Afiliacion_IMSS, string NombreAfiliacion, string Riesgo_Trabajo, int ClasesRegPat, int Status)
        {
            List<string> res = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Registro_Patronal_Insert_Registros_Patronales", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlsAfiliacion_IMSS", Afiliacion_IMSS));
            cmd.Parameters.Add(new SqlParameter("@ctrlsNombreAfiliacion", NombreAfiliacion));
            cmd.Parameters.Add(new SqlParameter("@ctrlsRiesgo_Trabajo", Riesgo_Trabajo));
            cmd.Parameters.Add(new SqlParameter("@ctrliClaseRegPat_id", ClasesRegPat));
            cmd.Parameters.Add(new SqlParameter("@ctrliStatus", Status));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    res.Add(data["iFlag"].ToString());
                    res.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                res = null;
            }
            data.Close();
            return res;
        }
        public List<RegimenFiscalBean> sp_CRegimen_Fiscal_Retrieve()
        {
            List<RegimenFiscalBean> list = new List<RegimenFiscalBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CRegimen_Fiscal_Retrieve_Regimenes_Fiscales", this.conexion)
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
                    RegimenFiscalBean ls = new RegimenFiscalBean
                    {
                        IdRegimenFiscal = int.Parse(data["IdRegimen_Fiscal"].ToString()),
                        Descripcion = data["Descripcion"].ToString()

                    };
                    list.Add(ls);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<RegionalesBean> sp_CRegionales_Retrieve_Regionales_xEmpresa(int id_empresa)
        {
            List<RegionalesBean> lista = new List<RegionalesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CRegionales_Retrieve_Regionales_xEmpresa", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliEmpresa_id", id_empresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    RegionalesBean list = new RegionalesBean
                    {
                        iIdRegional = int.Parse(data["IdRegional"].ToString()),
                        iEmpresaId = int.Parse(data["Empresa_id"].ToString()),
                        sClaveRegional = data["Clave_Regional"].ToString(),
                        sDescripcionRegional = data["Descripcion_Regional"].ToString(),
                        sFechaAlta = data["Fecha_Alta"].ToString()
                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<SucursalesBean> sp_CSucursales_Retrieve_Sucursales()
        {
            List<SucursalesBean> lista = new List<SucursalesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Sucursales_Retrieve_Sucursales", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    SucursalesBean list = new SucursalesBean
                    {
                        iIdSucursal = int.Parse(data["IdSucursal"].ToString()),
                        sDescripcionSucursal = data["Descripcion_Sucursal"].ToString(),
                        sClaveSucursal = data["Clave_Sucursal"].ToString()
                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<ZonaEconomicaBean> sp_CZonaEconomica_Retrieve_ZonaEconomica()
        {
            List<ZonaEconomicaBean> lista = new List<ZonaEconomicaBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CZona_Economica", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    ZonaEconomicaBean list = new ZonaEconomicaBean
                    {
                        iIdZonaEconomica = int.Parse(data["IdZonaEconomica"].ToString()),
                        sDescripcion = data["Descripcion"].ToString()
                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<string> sp_TLocalidades_Insert_Localidades(int Empresa_id, string Descripcion, string TasaIva, int RegistroPatronal_id, int Regional_id, int ZonaEconomica_id, int Sucursal_id, int Estado_id)
        {
            List<string> res = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TLocalidades_Insert_Localidades", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlsDescripcion", Descripcion));
            cmd.Parameters.Add(new SqlParameter("@ctrlsTasaIva", TasaIva));
            cmd.Parameters.Add(new SqlParameter("@ctrliRegistroPatronal_id", RegistroPatronal_id));
            cmd.Parameters.Add(new SqlParameter("@ctrliRegional_id", Regional_id));
            cmd.Parameters.Add(new SqlParameter("@ctrliZonaEconomica_id", ZonaEconomica_id));
            cmd.Parameters.Add(new SqlParameter("@ctrliSucursal_id", Sucursal_id));
            cmd.Parameters.Add(new SqlParameter("@ctrliCg_Estado_id", Estado_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    res.Add(data["iFlag"].ToString());
                    res.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                res = null;
            }
            data.Close();

            return res;
        }
        public List<AusentismosBean> sp_TiposAusentimo_Retrieve_TiposAusentismo()
        {
            List<AusentismosBean> lista = new List<AusentismosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CatalogoGeneral_Retrieve_CatalogoGeneral", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlCatalogoId", 18));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    AusentismosBean list = new AusentismosBean
                    {
                        iIdTipoAusentismo = int.Parse(data["idValor"].ToString()),
                        sNombreAusentismo = data["Valor"].ToString(),
                        sDescripcionAusentismo = data["Descripcion"].ToString()
                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<PVacacionesBean> sp_TperiodosVacaciones_Retrieve_PeriodosVacaciones(int IdEmpleado, int IdEmpresa)
        {
            List<PVacacionesBean> lista = new List<PVacacionesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPeriodos_Verify_AllPeriods", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpleado", IdEmpleado));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    PVacacionesBean list = new PVacacionesBean
                    {
                        Periodo = data["aniversario_anterior"].ToString().Substring(0, 10) + " a " + data["aniversario_proximo"].ToString().Substring(0, 10),
                        DiasPrima = int.Parse(data["DiasPrima"].ToString()),
                        DiasDisfrutados = int.Parse(data["DiasDisfrutados"].ToString()),
                        DiasRestantes = int.Parse(data["DiasRestantes"].ToString()),
                        Id_Per_Vac_Ln = int.Parse(data["IdPer_Vac_Ln"].ToString())

                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<PeriodosVacacionesBean> sp_Retrieve_TPeriodosVacacionesDist_Retrieve_VacacionesDist(int PerVacLn_id)
        {
            List<PeriodosVacacionesBean> lista = new List<PeriodosVacacionesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_TPeriodosVacacionesDist_Retrieve_VacacionesDist", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlPerVacLn_id", PerVacLn_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    PeriodosVacacionesBean list = new PeriodosVacacionesBean
                    {
                        IdPer_vac_Dist = int.Parse(data["IdPer_vac_Dist"].ToString()),
                        Per_vac_Ln_id = int.Parse(data["Per_vac_Ln_id"].ToString()),
                        Fecha_Inicio = data["Fecha_Inicio"].ToString(),
                        Fecha_Fin = data["Fecha_Fin"].ToString(),
                        Dias = int.Parse(data["Dias"].ToString()),
                        Agendadas = data["Agendadas"].ToString(),
                        Disfrutadas = data["Disfrutadas"].ToString(),
                        Cancelado = data["Cancelado"].ToString()

                    };
                    lista.Add(list);
                }
            }
            else
            {
                lista = null;
            }
            data.Close();

            return lista;
        }
        public List<string> sp_TPeriodosVacaciones_Dist_Set_PeriodoDisfrutado(int IdPer_vac_Dist)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPeriodosVacaciones_Dist_Set_PeriodoDisfrutado", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlIdPer_vac_Dist", IdPer_vac_Dist));

            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    string ls = data["sRespuesta"].ToString();
                    list.Add(ls);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }

        public List<string> sp_TPeriodosDist_Insert_Periodo(int PerVacLn_id, string FechaInicio, string FechaFin, int Dias)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPeriodosDist_Insert_Periodo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlPerVacLn_id", PerVacLn_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaInicio", FechaInicio));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaFin", FechaFin));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias", Dias));

            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    string ls = data["sRespuesta"].ToString();
                    list.Add(ls);
                }
            }
            else
            {
                list = null;
            }
            data.Close();

            return list;
        }
        public List<string> sp_CInicio_Fechas_Periodo_Verify_id(int Empresa_id)
        {
            List<string> periodo = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_CInicio_Fechas_Periodo_Verify_id", this.conexion)
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

                    periodo.Add(data["Id"].ToString());
                    periodo.Add(data["Empresa_id"].ToString());
                    periodo.Add(data["Anio"].ToString());
                    periodo.Add(data["Tipo_periodo_id"].ToString());
                    periodo.Add(data["Periodo"].ToString());
                    periodo.Add(data["Nomina_Cerrada"].ToString());
                    periodo.Add(data["Fecha_Inicio"].ToString());
                    periodo.Add(data["Fecha_Final"].ToString());
                    periodo.Add(data["Fecha_Proceso"].ToString());
                    periodo.Add(data["Fecha_Pago"].ToString());
                    periodo.Add(data["Dias_Efectivos"].ToString());

                }
            }
            else
            {
                periodo = null;
            }
            data.Close();

            return periodo;
        }
    }
}