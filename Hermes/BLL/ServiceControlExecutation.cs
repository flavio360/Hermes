using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.BLL
{
    public class ServiceControlExecutation
    {
        public bool ValidadtionHourExec(string hourExe)        
        {
            bool exec = false;

            string[] subs = hourExe.Split('|');

            for (int i = 0; i < subs.Length; i++)
            {
                var h = subs[i];
                
                if (DateTime.Now.AddHours(-3).ToString("HH").TrimStart(new Char[] { '0' }) == h)
                {
                    exec = true;

                    if (exec)
                    {
                        break;
                    }
                }
            }

            return exec;
        }
    }
}
