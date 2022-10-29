using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Helper
{
    public class General
    {
        //private Seguridad.Seguridad db = new Seguridad.Seguridad();
        public General()
        {}
        static General()
        {

        }

        public static string FileNameFormat(string fileName)
        {
            DateTime now = DateTime.Now;
            if (fileName == null || fileName.Trim() == string.Empty)
            {
                fileName = "file.jpg";
            }
            string newFileName = string.Format("{0}{1}{2}{3}{4}{5}_{6}", now.Month, now.Day, now.Year, now.Hour, now.Minute, now.Second,
                fileName.Replace("#", "").Replace("&", "").Replace("<", "").Replace(">", "").Replace("%", "")
                .Replace("$", "").Replace("#", "").Replace("@", "").Replace("!", "").Replace("~", "").Replace(",", "")
                .Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("(", "").Replace(")", "")
                .Replace("/", "").Replace(":", "").Replace(" ", "")
                );
            return newFileName;
        }

        public static string FileExtension(string fileName)
        {
            var split = fileName.Split('.');
            return split[split.Length - 1]; 
        }
    }
}
