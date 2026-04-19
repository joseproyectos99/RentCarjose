using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 

namespace RentCar
{
    internal class Conexion
    {
        public static MySqlConnection obtenerConexion()
        {
            string cadenaConexion = "server=localhost;database=rentcar;uid=root;pwd=0725;";
            return new MySqlConnection(cadenaConexion);
        }
    }
}