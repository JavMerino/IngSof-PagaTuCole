using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class SqlConection
    {
        public static string CadenaConexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["PagaTuColeDB"].ConnectionString;
            }
        }
    }
}