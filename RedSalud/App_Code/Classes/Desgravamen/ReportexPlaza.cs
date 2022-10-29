using EvoPdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for CitaMedica
    /// </summary>
    public class ReportexPlaza
    {
        public string Estudio { get; set; }

        public decimal precio { get; set; }

        //Cochabamba
        public int CBB { get; set; }
        //La Paz
        public int LPZ { get; set; }
        //Oruro
        public int ORU { get; set; }
        //Potosí
        public int PTS { get; set; }
        //Sucre
        public int SCR { get; set; }
        //Santa Cruz
        public int STC { get; set; }
        //Trinidad
        public int TRI { get; set; }
        //Tarija
        public int TRJ { get; set; }

        //El Alto (La Paz) **
        public int ALT { get; set; }
        //Cobija**
        public int COB { get; set; }
        //Montero **
        public int MON { get; set; }

        public int totalcant
        {
            get { return CBB+ LPZ+ ORU+ PTS+ SCR+ STC + TRI + TRJ + ALT + COB + MON; }
        }

        public decimal subCBB
        {
            get { return CBB * precio; }
        }
        public decimal subLPZ
        {
            get { return LPZ * precio; }
        }
        public decimal subORU
        {
            get { return ORU * precio; }
        }
        public decimal subPTS
        {
            get { return PTS * precio; }
        }
        public decimal subSCR
        {
            get { return SCR * precio; }
        }
        public decimal subSTC
        {
            get { return STC * precio; }
        }
        public decimal subTRI
        {
            get { return TRI * precio; }
        }
        public decimal subTRJ
        {
            get { return TRJ * precio; }
        }
        public decimal subALT
        {
            get { return ALT * precio; }
        }

        public decimal subCOB
        {
            get { return COB * precio; }
        }

        public decimal subMON
        {
            get { return MON * precio; }
        }

        public decimal subtotalcant
        {
            get { return totalcant * precio; }
        }
    }
}