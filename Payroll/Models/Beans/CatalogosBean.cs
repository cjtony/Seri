using System;

namespace Payroll.Models.Beans
{
    public class CatalogosBean
    {
    }
    public class CatalogoGeneralBean
    {
        public int iId { get; set; }
        public int iCampoCatalogoId { get; set; }
        public int iIdValor { get; set; }
        public string sValor { get; set; }
        public string sDescripcion { get; set; }
        public int iCancelado { get; set; }
        public int iIdUsuAlta { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class InfDomicilioBean
    {

        // Beans catalogo general estados \\
        public int iId { get; set; }
        public int iIdValor { get; set; }
        public string sValor { get; set; }
        public int iIdCodigoPostal { get; set; }
        public string sCiudad { get; set; }
        public int iIdColonia { get; set; }
        public string sColonia { get; set; }
        public int iIdMunicipio { get; set; }
        public string sMensaje { get; set; }
    }
    public class InfoDireccionByCPBean
    {
        public int iIdEstado { get; set; }
        public string sEstado { get; set; }
        public int iIdMunicipio { get; set; }
        public string sMunicipio { get; set; }
        public int iIdColonia { get; set; }
        public string sDelegacion { get; set; }
        public string sColonia { get; set; }
        public string sCiudad { get; set; }
    }
    public class NivelEstudiosBean
    {
        public int iIdNivelEstudio { get; set; }
        public string sNombreNivelEstudio { get; set; }
        public int iEstadoNivelEstudio { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class TabuladoresBean
    {
        public int iIdTabulador { get; set; }
        public string sTabulador { get; set; }
        public int iEstadoTabulador { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class BancosBean
    {
        public int iIdBanco { get; set; }
        public string sNombreBanco { get; set; }
        public int iClave { get; set; }
        public int iEstadoBanco { get; set; }
        public string sUsuarioRegistroBanco { get; set; }
        public string sFechaRegistroBanco { get; set; }
        public string sUsuarioModificaBanco { get; set; }
        public string sFechaModificaBanco { get; set; }
        public string sMensaje { get; set; }
    }
    public class PuestosBean
    {
        public int iIdPuesto { get; set; }
        public int iIdEmpresa { get; set; }
        public string sCodigoPuesto { get; set; }
        public string sNombrePuesto { get; set; }
        public string sDescripcionPuesto { get; set; }
        public int iIdProfesionFamilia { get; set; }
        public int iIdEtiquetaContable { get; set; }
        public string sOcupacionPuesto { get; set; }
        public int iIdClasificacionPuesto { get; set; }
        public int iIdColectivo { get; set; }
        public int iIdNivelJerarquico { get; set; }
        public int iIdPerfomanceManager { get; set; }
        public int iIdTabulador { get; set; }
        public int iEstadoPuesto { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class ProfesionesFamiliaBean
    {
        public int iIdProfesionFamilia { get; set; }
        public string sNombreProfesion { get; set; }
        public int iEstadoProfesion { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class EtiquetasContablesBean
    {
        public int iIdEtiquetaContable { get; set; }
        public string sNombreEtiquetaContable { get; set; }
        public int iEstadoEtiquetaContable { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class NivelJerarBean
    {
        public int iIdNivelJerarquico { get; set; }
        public string sNombreNivelJerarquico { get; set; }
        public int iEstadoNivelJerarquico { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class PerfomanceManagerBean
    {
        public int iIdPerfomanceManager { get; set; }
        public string sPerfomanceManager { get; set; }
        public int iEstadoPerfomance { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class EmpresasBean
    {
        public int iIdEmpresa { get; set; }
        public string sNombreEmpresa { get; set; }
        public string sRazonSocial { get; set; }
        public int iIdEstado { get; set; }
        public int iIdCiudad { get; set; }
        public string sCiudad { get; set; }
        public string sCalle { get; set; }
        public string sColonia { get; set; }
        public string sGiro { get; set; }
        public string fRfc { get; set; }
        public int iIdRegistroPatronal { get; set; }
        public string sRegistroPatronal { get; set; }
        public int iNominaAutomaticaUltimo { get; set; }
        public int iNominaAutomaticaInicial { get; set; }
        public int iNominaAutomaticaFinal { get; set; }
        public string sSueldoPeriodo { get; set; }
        public string sPagoSueldo { get; set; }
        public int iEstadoEmpresa { get; set; }
        public string sUsuarioRegistro { get; set; }
        public string sFechaRegistro { get; set; }
        public string sMensaje { get; set; }
    }
    public class CentrosCostosBean
    {
        public int iIdCentroCosto { get; set; }
        public int iIdEmpresa { get; set; }
        public string sCentroCosto { get; set; }
        public string sDescripcionCentroCosto { get; set; }
        public int iEstadoCentroCosto { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class EdificiosBean
    {
        public int iIdEdificio { get; set; }
        public string sNombreEdificio { get; set; }
        public string sCodigoPostal { get; set; }
        public string sCalle { get; set; }
        public string sDelegacion { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class NivelEstructuraBean
    {
        public int iIdNivelEstructura { get; set; }
        public int iEmpresaId { get; set; }
        public string sNivelEstructura { get; set; }
        public string sDescripcion { get; set; }
        public int iEstadoNivelEstructura { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string SFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class DepartamentosBean
    {
        public int iIdDepartamento { get; set; }
        public int iEmpresaId { get; set; }
        public string sDeptoCodigo { get; set; }
        public string sDescripcionDepartamento { get; set; }
        public string sNivelEstructura { get; set; }
        public string sNivelSuperior { get; set; }
        public int iEdificioId { get; set; }
        public string sEdificioN { get; set; }
        public string sPiso { get; set; }
        public string sUbicacion { get; set; }
        public int iCentroCostoId { get; set; }
        public string sCentroCosto { get; set; }
        public int iEmpresaReportaId { get; set; }
        public string sEmpresaReportaA { get; set; }
        public string sDGA { get; set; }
        public string sDirecGen { get; set; }
        public string sDirecEje { get; set; }
        public string sDirecAre { get; set; }
        public int iEmpreDirGen { get; set; }
        public int iEmpreDirEje { get; set; }
        public int iEmpreDirAre { get; set; }
        public string sCancelado { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class EmpleadosBean
    {
        public int iIdEmpleado { get; set; }
        public string sNombreEmpleado { get; set; }
        public string sApellidoPaterno { get; set; }
        public string sApellidoMaterno { get; set; }
        public string sFechaNacimiento { get; set; }
        public string sLugarNacimiento { get; set; }
        public int iTitulo_id { get; set; }
        public int iGeneroEmpleado { get; set; }
        public int iNacionalidad { get; set; }
        public int iEstadoCivil { get; set; }
        public string sCodigoPostal { get; set; }
        public int iEstado_id { get; set; }
        public string sCiudad { get; set; }
        public string sColonia { get; set; }
        public string sCalle { get; set; }
        public string sNumeroCalle { get; set; }
        public string sTelefonoFijo { get; set; }
        public string sTelefonoMovil { get; set; }
        public string sCorreoElectronico { get; set; }
        public string sFechaMatrimonio { get; set; }
        public string sTipoSangre { get; set; }
        public string sUsuarioRegistroEmpleado { get; set; }
        public string sFechaRegistroEmpleado { get; set; }
        public int iNumeroNomina { get; set; }
        public string sMensaje { get; set; }
    }
    public class ImssBean
    {
        public int iIdImss { get; set; }
        public int iEmpleado_id { get; set; }
        public int iEmpresa_id { get; set; }
        public string sFechaEfectiva { get; set; }
        public string sRegistroImss { get; set; }
        public string sRfc { get; set; }
        public string sCurp { get; set; }
        public int iNivelEstudio_id { get; set; }
        public int iNivelSocioeconomico_id { get; set; }
        public int iEstadoImss { get; set; }
        public string iUsuarioAlta_id { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class DatosNominaBean
    {
        public int iIdNomina { get; set; }
        public int iEmpleado_id { get; set; }
        public int iEmpresa_id { get; set; }
        public string sFechaEfectiva { get; set; }
        public int iTipoPeriodo { get; set; }
        public double dSalarioMensual { get; set; }
        public int iTipoEmpleado_id { get; set; }
        public int iNivelEmpleado_id { get; set; }
        public int iTipoJornada_id { get; set; }
        public int iTipoContrato_id { get; set; }
        public int iTipoContratacion_id { get; set; }
        public int iMotivoIncremento_id { get; set; }
        public string sFechaIngreso { get; set; }
        public string sFechaAntiguedad { get; set; }
        public string sVencimientoContrato { get; set; }
        public int iPosicion_id { get; set; }
        public int iTipoPago_id { get; set; }
        public int iBanco_id { get; set; }
        public string sCuentaCheques { get; set; }
        public int iUsuarioAlta_id { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class DatosPosicionesBean
    {
        public int iIdPosicionAsig { get; set; }
        public int iIdPosicion { get; set; }
        public int iEmpresa_id { get; set; }
        public string sPosicionCodigo { get; set; }
        public int iPuesto_id { get; set; }
        public string sPuestoCodigo { get; set; }
        public string sNombrePuesto { get; set; }
        public string sNombreE { get; set; }
        public string sPaternoE { get; set; }
        public string sMaternoE { get; set; }
        public string sFechaEffectiva { get; set; }
        public string sFechaInicio { get; set; }
        public int iDepartamento_id { get; set; }
        public string sNombreDepartamento { get; set; }
        public string sDeptoCodigo { get; set; }
        public int iPosicion { get; set; }
        public string sEmpresaReporta { get; set; }
        public int iIdReportaAPosicion { get; set; }
        public string sCodRepPosicion { get; set; }
        public int iIdReportaAEmpresa { get; set; }
        public string sNombreEmrpesaRepo { get; set; }
        public int iIdRegistroPat { get; set; }
        public string sRegistroPat { get; set; }
        public int iIdLocalidad { get; set; }
        public string sLocalidad { get; set; }
        public string sUsuarioRegistroPosicion { get; set; }
        public string sFechaRegistroPosicion { get; set; }
        public string sMensaje { get; set; }
    }
    public class TipoPeriodosBean
    {
        public int iId { get; set; }
        public string sValor { get; set; }
    }
    public class LocalidadesBean
    {
        public int IdLocalidad { get; set; }
        public int Empresa_id { get; set; }
        public int Codigo_Localidad { get; set; }
        public string Descripcion { get; set; }
        public string TasaIva { get; set; }
        public int RegistroPatronal_id { get; set; }
        public int Regional_id { get; set; }
        public string ClaveRegional { get; set; }
        public string DescripcionRegional { get; set; }
        public int ZonaEconomica_id { get; set; }
        public int Sucursal_id { get; set; }
        public string ClaveSucursal { get; set; }
        public string DescripcionSucursal { get; set; }
        public int Estado_id { get; set; }
        public string sMensaje { get; set; }
    }
    public class LocalidadesBean2
    {
        public int iIdLocalidad { get; set; }
        public int iIdEmpresa { get; set; }
        public int iCodigoLocalidad { get; set; }
        public string sDescripcion { get; set; }
        public double dTazIva { get; set; }
        public int iRegistroPatronal_id { get; set; }
        public string sRegistroPatronal { get; set; }
        public int iRegional_id { get; set; }
        public int iZonaEconomica_id { get; set; }
        public int iSucursal_id { get; set; }
        public int iEstado_id { get; set; }
        public string sMensaje { get; set; }
    }
    public class RegistroPatronalBean
    {
        public int IdRegPat { get; set; }
        public int Empresa_id { get; set; }
        public string Afiliacion_IMSS { get; set; }
        public string Nombre_Afiliacion { get; set; }
        public string Riesgo_Trabajo { get; set; }
        public int ClasesRegPat_id { get; set; }
        public string Cancelado { get; set; }
    }
    public class RegistroPatronalBean2
    {
        public int iIdRegPat { get; set; }
        public int iEmpresaid { get; set; }
        public string sAfiliacionIMSS { get; set; }
        public string sNombreAfiliacion { get; set; }
        public string sRiesgoTrabajo { get; set; }
        public int iClasesRegPat_id { get; set; }
        public string sCancelado { get; set; }
    }
    public class CClases_RegPatBean
    {
        public int IdClase { get; set; }
        public string Nombre_Clase { get; set; }
        public string Descripcion_Clase { get; set; }
    }
    public class RegimenFiscalBean
    {
        public int IdRegimenFiscal { get; set; }
        public string Descripcion { get; set; }
    }
    public class RegionalesBean
    {
        public int iIdRegional { get; set; }
        public int iEmpresaId { get; set; }
        public string sDescripcionRegional { get; set; }
        public string sClaveRegional { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class SucursalesBean
    {
        public int iIdSucursal { get; set; }
        public string sDescripcionSucursal { get; set; }
        public string sClaveSucursal { get; set; }
        public int iUsuarioAltaId { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }
    }
    public class ZonaEconomicaBean
    {
        public int iIdZonaEconomica { get; set; }
        public string sDescripcion { get; set; }

    }
    public class AusentismosBean
    {
        public int iIdTipoAusentismo { get; set; }
        public string sNombreAusentismo { get; set; }
        public string sDescripcionAusentismo { get; set; }

    }
    public class DescEmpleadoVacacionesBean
    {
        public int iFlag { get; set; }
        public int IdEmpleado { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Apellido_Materno_Empleado { get; set; }
        public string Apellido_Paterno_Empleado { get; set; }
        public string DescripcionDepartamento { get; set; }
        public string DescripcionPuesto { get; set; }
        public string FechaIngreso { get; set; }
        public string Empresa_id { get; set; }
        public int Id_Per_Vac { get; set; }
        public string Fecha_Aniversario { get; set; }
        public int Id_Per_Vac_Ln { get; set; }
        public int Anio { get; set; }
        public int DiasPrima { get; set; }
        public int DiasDisfrutados { get; set; }
        public int DiasRestantes { get; set; }


    }
    public class PVacacionesBean
    {
        public int iFlag { get; set; }
        public int IdEmpleado { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Apellido_Materno_Empleado { get; set; }
        public string Apellido_Paterno_Empleado { get; set; }
        public string DescripcionDepartamento { get; set; }
        public string DescripcionPuesto { get; set; }
        public string FechaIngreso { get; set; }
        public int Id_Per_Vac { get; set; }
        public string FechaAntiguedad { get; set; }
        public string aniversario_proximo { get; set; }
        public string aniversario_anterior { get; set; }
        public int Id_Per_Vac_Ln { get; set; }
        public string Periodo { get; set; }
        public int DiasPrima { get; set; }
        public int DiasDisfrutados { get; set; }
        public int DiasRestantes { get; set; }


    }
    public class PeriodosVacacionesBean
    {
        public int IdPer_vac_Dist { get; set; }
        public int Per_vac_Ln_id { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Fin { get; set; }
        public int Dias { get; set; }
        public string Agendadas { get; set; }
        public string Disfrutadas { get; set; }
        public string Cancelado { get; set; }

    }
    public class CreditosBean
    {
        public int IdCredito { get; set; }
        public int Empleado_id { get; set; }
        public int Empresa_id { get; set; }
        public string TipoDescuento { get; set; }
        public string SeguroVivienda { get; set; }
        public string Descuento { get; set; }
        public string NoCredito { get; set; }
        public string FechaAprovacionCredito { get; set; }
        public string Descontar { get; set; }
        public string FechaBaja { get; set; }
        public string FechaReinicio { get; set; }
        public string Finalizado { get; set; }
    }
    public class AusentismosEmpleadosBean
    {
        public int IdAusentismo { get; set; }
        public int Tipo_Ausentismo_id { get; set; }
        public string Nombre_Ausentismo { get; set; }
        public int Empleado_id { get; set; }
        public int Empresa_id { get; set; }
        public string RecuperaAusentismo { get; set; }
        public string Fecha_Ausentismo { get; set; }
        public int Dias_Ausentismo { get; set; }
        public string Certificado_imss { get; set; }
        public string Comentarios_imss { get; set; }
        public string Causa_FaltaInjustificada { get; set; }
    }
    public class PensionesAlimentariasBean
    {
        public int IdPension { get; set; }
        public int Empleado_id { get; set; }
        public int Empresa_id { get; set; }
        public string Cuota_Fija { get; set; }
        public int Porcentaje { get; set; }
        public string AplicaEn { get; set; }
        public string Descontar_en_Finiquito { get; set; }
        public string No_Oficio { get; set; }
        public string Fecha_Oficio { get; set; }
        public string Tipo_Calculo { get; set; }
        public string Aumentar_segun_salario_minimo_general { get; set; }
        public string Aumentar_segun_aumento_de_sueldo { get; set; }
        public string Beneficiaria { get; set; }
        public int Banco { get; set; }
        public string Sucursal { get; set; }
        public string Tarjeta_vales { get; set; }
        public string Cuenta_cheques { get; set; }
        public string Fecha_baja { get; set; }
    }
    public class CapturaErroresBean
    {
        public int iIdCapturaError { get; set; }
        public string sOrigenError { get; set; }
        public string sMensajeError { get; set; }
        public DateTime dFechaError { get; set; }
        public string sMensaje { get; set; }
    }
    public class NacionalidadesBean
    {
        public int iIdNacionalidad { get; set; }
        public string sDescripcion { get; set; }
        public string sMensaje { get; set; }
    }
    public class TipoEmpleadoBean
    {
        public int IdTipo_Empleado { get; set; }
        public string Descripcion { get; set; }
    }
    public class MotivoBajaBean
    {
        public int IdMotivo_Baja { get; set; }
        public string Descripcion { get; set; }
        public int TipoEmpleado_id { get; set; }
    }
    public class IncidenciasBean
    {
        public int IdTRegistro_Incidencia { get; set; }
        public int Renglon { get; set; }
        public int Cantidad { get; set; }
        public int Plazos { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        public string Fecha_Aplicacion { get; set; }
    }
    public class VW_TipoIncidenciaBean
    {
        public int Ren_incid_id { get; set; }
        public string Descripcion { get; set; }
    }
    public class IncidenciasProgramadasBean
    {
        public int Id { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Apellido_Paterno_Empleado { get; set; }
        public string Apellido_Materno_Empleado { get; set; }
        public int Empleado_id { get; set; }
        public string Nombre_Renglon { get; set; }
        public int Renglon_id { get; set; }
        public string Monto_aplicar { get; set; }
        public int Numero_dias { get; set; }
    }
    public class TabIncidenciasBean
    {   public int Incidencia_id { get; set; }
        public int IncidenciaP_id { get; set; }
        public string Nombre_Renglon { get; set; }
        public string VW_TipoIncidencia_id { get; set; }
        public int Cantidad { get; set; }
        public int Plazos { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Aplicacion { get; set; }
        public string NPeriodo { get; set; }
    }
    public class InicioFechasPeriodoBean
    {
        public string id { get; set; }
        public string Empresa_id { get; set; }
        public string NombreEmpresa { get; set; }
        public string Anio { get; set; }
        public string Tipo_Periodo_Id { get; set; }
        public string DescripcionTipoPeriodo { get; set; }
        public string Periodo { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Final { get; set; }
        public string Fecha_Proceso { get; set; }
        public string Fecha_Pago { get; set; }
        public string Dias_Efectivos { get; set; }
        public string Nomina_Cerrada { get; set; }
    }
    public class TabPoliticasVacacionesBean
    {
        public string Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string Empresa_id { get; set; }
        public string Effdt { get; set; }
        public string Anos { get; set; }
        public string Dias { get; set; }
        public string Prima_Vacacional_Porcen { get; set; }
        public string Dias_Aguinaldo { get; set; }
    }
    //public class CatalogoPuestosBean
    //{
    //    public int id { get; set; }
    //    public string Catalogo { get; set; }
    //    public string Descripcion { get; set; }
    //    public string Cancelado { get; set; }
    //    public string Codigo { get; set; }
    //}
    public class EmpleadosxEmpresaBean
    {
        public string Empresa_id { get; set; }
        public string NombreEmpresa { get; set; }
        public string No { get; set; }
    }
    public class DataPuestosBean
    {
        public int iFlag { get; set; }
        public string idPuesto { get; set; }
        public string PuestoCodigo { get; set; }
        public string NombrePuesto { get; set; }
        public string DescripcionPuesto { get; set; }
        public string Empresa_id { get; set; }
        public string NombreProfesion { get; set; }
        public string ClasificacionPuesto { get; set; }
        public string Colectivo { get; set; }
        public string NivelJerarquico { get; set; }
        public string PerformanceManager { get; set; }
        public string Tabulador { get; set; }
        public string fecha_alta { get; set; }
    }

    public class TipoRenglonBean { 
         
        public int iIdRenglon { get; set; }
        public String sTipoRenglon { get; set; }
        public string sMensaje { get; set; }


    }

    public class ListaCalculoBean
    {

        public int iIdCalculo { get; set; }
        public String sNombreCalculo { get; set; }
        public string sMensaje { get; set; }


    }

    public class TabBancosEmpresas
    {
        public string idBanco_Emp { get; set; }
        public string Empresa_id { get; set; }
        public string Banco_id { get; set; }
        public string Descripcion { get; set; }
        public string Num_cliente { get; set; }
        public string Plaza { get; set; }
        public string Num_Cta_Empresa { get; set; }
        public string Clabe { get; set; }
        public string tipo_banco_id { get; set; }
        public string tipo_banco { get; set; }
        public string Cancelado { get; set; }
    }
    public class DataUsersBean
    {
        public string IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Perfil_id { get; set; }
        public string Ps { get; set; }
        public string Alta_por { get; set; }
        public string Fecha_Alta { get; set; }
        public string Cancelado { get; set; }
    }
    public class DataProfilesBean
    {
        public string IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Cancelado { get; set; }
        public string Alta_por { get; set; }
        public string Fecha_Alta { get; set; }
    }
    public class SeccionReporte { 
        public int iIdValor { get; set; }
        public string SNombreReporte { get; set; }
        public string sMensaje { get; set; }
    }

}