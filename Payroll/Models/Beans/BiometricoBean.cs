﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;


namespace Payroll.Models.Beans
{
    public class BioBean
    {



    }

    public class EmpreHorarioBean
    {
        public int iIdHorario { get; set; }
        public int iTurno { get; set; }
        public string sHrEnt { get; set; }
        public string sHrSal { get; set; }
        public string sHrEntCom { get; set; }
        public string sHrSalCom { get; set; }
        public string sDescrip { get; set; }
        public int iUsuario { get; set; }
        public string sTipoTurno { get; set; }
        public int iTipoPausa { get; set; }
        public string sDiasDesc { get; set; }
        public int iCancelado { get; set; }

        public string sMensaje { get; set; }

    }

}