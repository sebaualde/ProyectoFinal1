using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
namespace Persistencia
{
    public class Conexion
    {
        private static string _cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexionSqlServer"].ToString();
                
        public static string CadenaConexion
        {
            get
            {
                return _cadenaConexion;
            }
        }
    }
}
