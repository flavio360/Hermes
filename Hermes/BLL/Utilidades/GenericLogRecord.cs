using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.BLL.Utilidades
{
    public class GenericLogRecord
    {
        
        public void RecordLog(string descriptionLog,string pathLog, string fileName)
        {
            try
            {
                if (!Directory.Exists(pathLog))
                {
                    Directory.CreateDirectory(pathLog);
                }

                pathLog = pathLog + fileName +  DateTime.Now.ToString("_yyyy-MM-dd") + ".txt";
                StreamWriter lObjEscreveTexto = new StreamWriter(pathLog, true);

                lObjEscreveTexto.WriteLine(string.Concat(descriptionLog));

                lObjEscreveTexto.Flush();
                lObjEscreveTexto.Close();
                lObjEscreveTexto.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
