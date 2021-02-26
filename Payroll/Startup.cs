using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using System.Diagnostics;
using Hangfire.Storage.Monitoring;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using Payroll.Models.Daos;
using System.Collections.Generic;
using Payroll.Controllers;

[assembly: OwinStartup(typeof(Payroll.Startup))]

namespace Payroll 
{
   
    public class Startup : Conexion
    {
        
        /// <summary>
        ///  configuracion del hangfire 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Data Source = GSERIPROD01; Initial Catalog=IPSNet_Copia; User ID= IPSNet;Password= IPSNet2;Integrated Security= False;MultipleActiveResultSets=true", new SqlServerStorageOptions

            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true

            });
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        public void ConsultaTpJobs(int op, int iIdjobs,int iIdTarea)
        {
            // fecha del procesos 
            string FechaProceso = Fecha();
            // realiza la tarea 
            BackgroundJob.Enqueue(() => Proceso(op, iIdjobs, iIdTarea, FechaProceso));
          
            // guarda en la BD el proceso 
            List<HangfireJobs> id = new List<HangfireJobs>();
            FuncionesNomina Dao = new FuncionesNomina();
            id = Dao.sp_IdJobsHangfireJobs_Retrieve_IdJobsHangfireJobs(FechaProceso);
            int Idjobs = Convert.ToInt32(id[0].iId.ToString());
            string StatusJobs = "En Cola";
            string Nombrejobs = "ConsultaTpJobs";
            string Parametros =  op +","+ iIdjobs+"," + iIdTarea+","+ FechaProceso;
            Dao.Sp_TPProcesosJobs_insert_TPProcesosJobs(Idjobs, StatusJobs, Nombrejobs, Parametros,0);
        }

        /// <summary>
        ///  Actualiza  TPCalculosHdLn
        /// </summary>
        /// <param name="iNominaCerrada"></param>
        /// <param name="idNum"></param>
        public void ActualizaCalculoshd(int iNominaCerrada, int idNum)
        {
            // fecha del procesos 
            string FechaProceso = Fecha();
            // realiza la tarea 
            var jobId = BackgroundJob.Schedule(() => Actu(iNominaCerrada, idNum, FechaProceso), TimeSpan.FromMinutes(2));
           // var jobId = BackgroundJob.Enqueue(() => Actu(iNominaCerrada, idNum, FechaProceso));
            // guarda en la BD el proceso 
            List<HangfireJobs> id = new List<HangfireJobs>();
            FuncionesNomina Dao = new FuncionesNomina();
            id = Dao.sp_IdJobsHangfireJobs_Retrieve_IdJobsHangfireJobs(FechaProceso);
            int Idjobs = Convert.ToInt16(id[0].iId.ToString());
            string StatusJobs = "En Cola";
            string Nombrejobs = "ActualizaCalculoshd";
            string Parametros = iNominaCerrada + ","+ idNum+"," + FechaProceso;
            Dao.Sp_TPProcesosJobs_insert_TPProcesosJobs(Idjobs, StatusJobs, Nombrejobs, Parametros,0);
        }

        ///Procoeso cunsulta BD en la tabla TPProcesosJobs
        public List<TPProcesos> Proceso(int op,int iIdjobs, int iIdTarea, string tiempo)
        {
            int op1=0, op2=0, op3=0;
            if (op == 1) {
                op1 = 1;
                op2 = 0;
                op3 = 0;
            }

            if (op == 2)
            {
                op1 = 0;
                op2 = 1;
                op3 = 0;
            }

            if (op == 3)
            {
                op1 = 0;
                op2 = 0;
                op3 = 1;
            }

            List<TPProcesos> LE = new List<TPProcesos>();
            FuncionesNomina Dao = new FuncionesNomina();
            LE =Dao.sp_TPProcesosJobs_Retrieve_TPProcesosJobs(op1, op2, op3, iIdjobs, iIdTarea);
            return LE;
           
        }
        // Proceso, Actualiza BD Tp CalculosHd Ln
        public void Actu(int iNominaCerrada,int idNum, string fecha )
        {
            Controllers.NominaController obj = new Controllers.NominaController();
            obj.UpdateCalculoshd(idNum, iNominaCerrada);        
        }

        // vuelve a realizar la tarea selecionada 
        public void Reprocesos(int IdJobs)
        {
            // fecha del procesos 
            string FechaProceso = Fecha();
            int op = 1;
            int iIdTarea = 0;
            List<TPProcesos> LProce = new List<TPProcesos>();
            LProce = Proceso(op, IdJobs, iIdTarea, FechaProceso);
            string Nombrejobs = LProce[0].sNombre;
            string Parametros = LProce[0].sParametros;
            string[] valores = Parametros.Split(',');        

            if (Nombrejobs == "ActualizaCalculoshd") {
                ActualizaCalculoshd(Convert.ToInt16(valores[0]),Convert.ToInt16(valores[1]));
            }

        }

        // proceso actualiza en BD TpProcesosJons cada 30 segundos 
        public void ActBDTbJobs() {
            RecurringJob.AddOrUpdate(() => ProcesosContinuos(), Cron.Minutely);
            var jobId = BackgroundJob.Enqueue(() => ProcesosContinuos());
        }

        public void ProcesosContinuos() 
       {
            FuncionesNomina Dao = new FuncionesNomina();
            Dao.sp_EstatusTpProcesosJobs_Update_EstatusTpProcesosJobs();
        }

        public string Fecha() 
        {
            // fecha del procesos 
            string tiempo = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
            DateTime fecha = new DateTime();
            fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            string day = fecha.Day.ToString();
            string month = fecha.Month.ToString();
            string year = fecha.Year.ToString();
            string hora = fecha.Hour.ToString();
            string minutos = fecha.Minute.ToString();
            string segundos = fecha.Second.ToString();
            string milsegundos = fecha.Millisecond.ToString();
            string fechajobs = year + "-" + month + "-" + day + " " + hora + ":" + minutos + ":" + segundos + ":" + milsegundos;
            return fechajobs;
        }

        public void  ProcesoNom( string NomProceso, int idDefinicionhd, int anio,int TipoPeriodo, int periodo, int idEmpresa,int iCalxEmple,string Path,string NameUser)
        {
            string FechaProceso = Fecha();

            if (NomProceso == "CNomina")
            {

                var jobId = BackgroundJob.Enqueue(() => Cnomia(anio, TipoPeriodo, periodo, idDefinicionhd, idEmpresa, iCalxEmple, FechaProceso, Path, NameUser));

                //Task checkJobState = Task.Factory.StartNew(() =>
                //{
                //    while (true)
                //    {
                //        var monitoringApi = JobStorage.Current.GetMonitoringApi();
                //        JobDetailsDto jobDetails = monitoringApi.JobDetails(jobId);
                //        string currentState = jobDetails.History[0].StateName;
                //        if (currentState != "Enqueued" && currentState != "Processing")
                //        {
                //            break;
                //        }
                //        TimeSpan.FromSeconds(30); // adjust to a coarse enough value for your scenario
                //    }
                //});            

                string sFolio = "";
                if (periodo > 9)
                {
                    if (TipoPeriodo > 0)
                    {
                        sFolio = anio.ToString() + (TipoPeriodo * 10) + periodo + "0";
                    }
                    if (TipoPeriodo < 1)
                    {
                        sFolio = anio.ToString() + "00" + periodo + "0";
                    }

                }
                if (periodo > 0 && periodo < 10)
                {
                    if (TipoPeriodo > 0)
                    {
                        sFolio = anio.ToString() + (TipoPeriodo * 10) + "0" + periodo + "0";
                    }
                    if (TipoPeriodo < 1)
                    {
                        sFolio = anio.ToString() + "00" + "0" + periodo + "0";

                    }
                }

       

                List<HangfireJobs> id = new List<HangfireJobs>();
                FuncionesNomina Dao = new FuncionesNomina();
                id = Dao.sp_CalculosHd_IDProcesJobs_Retrieve_TPlantillaCalculosHD(idDefinicionhd,int.Parse(sFolio));
                int Idjobs = Convert.ToInt32(jobId);
                string StatusJobs = "En Cola";
                string Nombrejobs = "CNomina1";
                string Parametros = anio + "," + TipoPeriodo + "," + periodo + "," + idDefinicionhd + "," + idEmpresa + "," + iCalxEmple + "," + FechaProceso;
                Dao.Sp_TPProcesosJobs_insert_TPProcesosJobs(Idjobs, StatusJobs, Nombrejobs, Parametros, id[0].iId);

            }

        }

        public void Cnomia(int anio, int TipoPeriodo, int Periodo, int IdDefinicion, int IdEmpresa, int LisEmpleado, string fecha, string Path,string NameUsuario) {
            if (Path == null) {

                NominaController contol = new NominaController();

                Path = contol.path();
            };
            //    string path2 = Path;
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = anio + "," + TipoPeriodo + "," + Periodo + "," + IdDefinicion + "," + IdEmpresa + "," + LisEmpleado+","+ NameUsuario;
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.FileName = Path;
            Process.Start(psi);

            //Path = Path.Replace("prueba.bat", "Prueba2.bat");
            //ProcessStartInfo psi2 = new ProcessStartInfo();
            //psi2.Arguments = anio + "," + TipoPeriodo + "," + Periodo + "," + IdDefinicion + "," + IdEmpresa + "," + LisEmpleado;
            //psi2.CreateNoWindow = true;
            //psi2.WindowStyle = ProcessWindowStyle.Hidden;
            //psi2.FileName = Path;
            //Process.Start(psi2);
            //Path = path2.Replace("prueba.bat", "Prueba3.bat");
            //ProcessStartInfo psi3 = new ProcessStartInfo();
            //psi3.Arguments = anio + "," + TipoPeriodo + "," + Periodo + "," + IdDefinicion + "," + IdEmpresa + "," + LisEmpleado;
            //psi3.CreateNoWindow = true;
            //psi3.WindowStyle = ProcessWindowStyle.Hidden;
            //psi3.FileName = Path;
            //Process.Start(psi3);

        }

    }
}
