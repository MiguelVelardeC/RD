using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Artexacta.App.WebServices.alianza
{
    /// <summary>
    /// Summary description for EstadoAsegurado
    /// </summary>
    [Serializable]
    public class EstadoAsegurado
    {
        public int nBranch { get; set; }
        public int nProduct { get; set; }
        public int nPolicy { get; set; }
        public int nCertif { get; set; }
        public DateTime dEffectdate { get; set; }
        public DateTime dExpirdat { get; set; }
        public string sClient { get; set; }
        public string sCliename { get; set; }
        public decimal nRates { get; set; }
        public string sStatus { get; set; }
        public bool Valid { get; set; }

        public EstadoAsegurado()
        {
            nBranch = -1;
            nProduct = -1;
            nPolicy = -1;
            nCertif = -1;
            dEffectdate = DateTime.MinValue;
            dExpirdat = DateTime.MinValue;
            sClient = "";
            sCliename = "";
            nRates = Convert.ToDecimal(-1.0);
            sStatus = "";
            Valid = false;
        }

        public void validate()
        {
            if (nBranch > 0 && nProduct > 0 && nPolicy > 0 && nCertif >= 0 &&
                dEffectdate > DateTime.MinValue &&
                dExpirdat > DateTime.MinValue &&
                !string.IsNullOrWhiteSpace(sClient) &&
                !string.IsNullOrWhiteSpace(sCliename) &&
                !string.IsNullOrWhiteSpace(sStatus))
                Valid = true;
            else
                Valid = false;
        }

        public bool Activo
        {
            get
            {
                if (sStatus.StartsWith("Valida"))
                    return true;

                return false;
            }
        }

        public string GetCsv()
        {
            StringBuilder result = new StringBuilder();
            result.Append(nBranch.ToString() + "|" + nProduct.ToString() + "|" + nPolicy.ToString() + "|" + nCertif.ToString() + "|");
            result.Append(dEffectdate.Year.ToString() + "-" + dEffectdate.Month.ToString() + "-" + dEffectdate.Day.ToString() + "|");
            result.Append(dExpirdat.Year.ToString() + "-" + dExpirdat.Month.ToString() + "-" + dExpirdat.Day.ToString() + "|");
            result.Append(sClient + "|" + sCliename + "|" + nRates.ToString(CultureInfo.InvariantCulture) + "|");
            result.Append(sStatus.ToString() + "|" + Valid.ToString());

            return result.ToString();
        }
    }
}