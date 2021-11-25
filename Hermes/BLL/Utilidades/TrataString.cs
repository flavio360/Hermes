using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.BLL.Utilidades
{
    public  class TrataString
    {



        public static string RemoveUltimoCaractereString(string stringtratar)
        {
            stringtratar = stringtratar.Remove(stringtratar.Length - 1);

            return stringtratar;
        }
    }
}
