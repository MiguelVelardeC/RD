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
    /// 
    public class ReportexPlazaxProveedor
    {
        public string Estudio { get; set; }

        //Cochabamba
        public decimal precioCBB { get; set; }
        public int CBB { get; set; }

        //La Paz
        public decimal precioLPZ { get; set; }
        public int LPZ { get; set; }

        //Oruro
        public decimal precioORU { get; set; }
        public int ORU { get; set; }

        //Potosí
        public decimal precioPTS { get; set; }
        public int PTS { get; set; }

        //Sucre
        public decimal precioSCR { get; set; }
        public int SCR { get; set; }

        //Santa Cruz
        public decimal precioSTC { get; set; }
        public int STC { get; set; }

        //Trinidad
        public decimal precioTRI { get; set; }
        public int TRI { get; set; }

        //Tarija
        public decimal precioTRJ { get; set; }
        public int TRJ { get; set; }

        //El Alto (La Paz) **
        public decimal precioALT { get; set; }
        public int ALT { get; set; }

        //Cobija**
        public decimal precioCOB { get; set; }
        public int COB { get; set; }

        //Montero **
        public decimal precioMON { get; set; }
        public int MON { get; set; }

        public int totalcant
        {
            get { return CBB+ LPZ+ ORU+ PTS+ SCR+ STC + TRI + TRJ + ALT + COB + MON; }
        }

        public decimal subCBB
        {
            get { return CBB * precioCBB; }
        }
        public decimal subLPZ
        {
            get { return LPZ * precioLPZ; }
        }
        public decimal subORU
        {
            get { return ORU * precioORU; }
        }
        public decimal subPTS
        {
            get { return PTS * precioPTS; }
        }
        public decimal subSCR
        {
            get { return SCR * precioSCR; }
        }
        public decimal subSTC
        {
            get { return STC * precioSTC; }
        }
        public decimal subTRI
        {
            get { return TRI * precioTRI; }
        }
        public decimal subTRJ
        {
            get { return TRJ * precioTRJ; }
        }
        public decimal subALT
        {
            get { return ALT * precioALT; }
        }

        public decimal subCOB
        {
            get { return COB * precioCOB; }
        }

        public decimal subMON
        {
            get { return MON * precioMON; }
        }

        public decimal subtotalcant
        {
            get { return subCBB+ subLPZ + subORU + subPTS + subSCR + subSTC + subTRI + subTRJ + subALT + subCOB + subMON; }
        }
    }
}