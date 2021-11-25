using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO.Utilidades
{
    public  static class ConnString
    {
        private static string _connString = "Server=localhost;Port=5432;User Id=postgres;Password=adm#56@dba;Database=airlinkexpress";

        public static string ConnStringa
        {                
            get
            {
                return _connString;
            }
        }
    }
}
