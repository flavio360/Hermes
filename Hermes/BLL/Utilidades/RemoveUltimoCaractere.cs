using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO.Utilidades
{
    public static class RemoveUltimoCaractere
    {
        public static string RemoveUltimoCaractereString(string stringtratar) 
        { 
            
        stringtratar = stringtratar.Remove(stringtratar.Length - 1);

         return stringtratar;
        }
    }
}
