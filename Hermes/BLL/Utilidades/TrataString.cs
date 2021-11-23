using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.BLL.Utilidades
{
    public  class TrataString
    {
        public static bool VerificarHoraExecucao()
        {
            bool exec = false;

            //var t_exec = DateTime.Now.AddHours(-3).Hour.ToString();
            var t_exec = DateTime.Now.AddHours(-3).ToString("HH");

            if (t_exec == "06" || t_exec == "12" || t_exec == "18" || t_exec == "00" || t_exec == "08")
            {
                exec = true;
            }

            return exec;
        }
    }
}
