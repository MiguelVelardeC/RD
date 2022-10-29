using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using log4net;
using System.Data;
using System.Configuration;

namespace Artexacta.App.Utilities.Chart
{
    /// <summary>
    /// Summary description for ChartUtilities
    /// </summary>
    public class ChartUtilities
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        //private static string[] ColorsHeatMap = {
        //                                            "FF3300", "FF3306", "E8330E", "DB3316",
        //                                            "CE331D", "C23325", "B4332D", "A73335",
        //                                            "9A333D", "BD3344", "81334C", "743353",
        //                                            "67335B", "5A3363", "4D336B", "403373",
        //                                            "33337A", "263382", "19338A", "0C3392",
        //                                            "003399"
        //                                        };
        private static string[] ColorsHeatMap = {
                                                    "FF0000", "FF2200", "FF4700", "FF6A00",
                                                    "FF8100", "FF9900", "FFB000", "FFC800",
                                                    "FFCC00", "FFCC00", "FFCC00", "F9C805",
                                                    "DCB71C", "BFA633", "A3954A", "868361",
                                                    "697278", "4C681F", "2F4FA6", "133EBD",
                                                    "0033CC"
                                                };

        public ChartUtilities()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Color numbers cna only be integers from 0 to 20 (inclusive). If the number given is greater than
        /// 20 it will return the 20th color... if it's less than 0 it will return the 0th color.
        /// </summary>
        /// <param name="colorNumber"></param>
        /// <returns>the color number given as parameter as a string. This represents a color in a heatmap
        /// from red to blue</returns>
        private static string GetColorFromIndex(int colorNumber)
        {
            int colorIndex = colorNumber;

            if (colorNumber < 0)
                colorIndex = 0;

            if (colorNumber > 20)
                colorIndex = 20;

            return ColorsHeatMap[colorIndex];
        }

        /// <summary>
        /// Given a maximum and minum values as decimals, the method will calculate the best approximation
        /// for the color in the heatmap for the given value. Also, the client should include information
        /// about the ordering of the variable. (higherIsBetter... )
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="higherIsBetter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetColor(decimal min, decimal max, bool higherIsBetter, decimal value)
        {
            decimal interval = max - min;

            if (Math.Abs(interval) == 0)
                return GetColorFromIndex(20);

            decimal index = (value - min) / interval * 20;

            return GetColorFromIndex(Convert.ToInt32(Math.Round(index)));
        }

        public static string FormatNumber(decimal number)
        {
            string result = "";
            decimal absolute = Math.Abs(number);

            if (absolute > 1000000)
                result = Math.Round(number / 1000000, 1).ToString("N1") + "M";
            else if (absolute > 1000)
                result = Math.Round(number / 1000, 1).ToString("N1") + "K";
            else
                result = number.ToString("N1");

            return result;
        }

        public static object SafeStringForXml(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "";

            return name.Replace('\'', ' ');
        }

        public static string SetVariableAppPrefix()
        {
            string pathFolder = "";
            //string pathFolder = HttpContext.Current.Request.ApplicationPath;
            //log.Debug("The folder for the application: " + pathFolder);

            //if (pathFolder.EndsWith("/"))
            //    return " var systemApplicationPath = '/';\n";

            //int index = pathFolder.LastIndexOf("/");
            //if (index < 0)
            //{
            //    log.Error("Could not evaluate the path for " + pathFolder);
            //    return "/";
            //}

            //return " var systemApplicationPath = '" + pathFolder.Substring(index) + "';\n";
            try
            {
                pathFolder = ConfigurationManager.AppSettings["KPTApplicationPath"].ToString();
            }
            catch (Exception q)
            {
                log.Error("Not configured properly, setting to /", q);
                pathFolder = "/";
            }
            return " var systemApplicationPath = '" + pathFolder + "';\n";
        }
    }
}