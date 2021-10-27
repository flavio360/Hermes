using Hermes.ADO;
using System;
using System.Collections.Generic;
using System.IO;


namespace Hermes.APP
{
    public class RecordLog
    {
        //public static void RecordLogSended(TrackingDTO tracking, ResponseDTO response)
        public static void RecordLogSended(List<Tracking> tracking)
        {
            try
            {
                string pathFile = @"C:\LogHermes\" + DateTime.Now.ToString("yyyyMMdd");
                System.IO.Directory.CreateDirectory(pathFile);
                pathFile = pathFile + @"\Recorded_objects_" + DateTime.Now.ToString("yyyyMMddT_hhmm") + ".txt";
                File.Create(pathFile).Close();

                StreamWriter writer = new StreamWriter(pathFile);
                writer.Write("HERMES - Trackings Log Sended, Airlink X Interlog/n");
                writer.WriteLine("Started process at " + DateTime.Now.ToString());               

                foreach (var item in tracking)
                {
                    writer.WriteLine("Codigo:..." + item.Codigo+"...........Status Code:..."+item.Code + "...........Data do registro:..."+item.Created);
                }

                writer.WriteLine("Process completed at " + DateTime.Now.ToString());
                writer.Close();
            }
            catch (Exception ex)
            {
                RecordLogSended(ex.ToString());
            }
        
        }

        public static void RecordLogSended(string msg)
        {
            try
            {
                string pathFile = @"C:\LogHermes\" + DateTime.Now.ToString("yyyyMMdd");
                System.IO.Directory.CreateDirectory(pathFile);
                pathFile = pathFile + @"\ERROR_in_Recorded_objects_" + DateTime.Now.ToString("yyyyMMddT_hhmm") + ".txt";
                File.Create(pathFile).Close();

                StreamWriter writer = new StreamWriter(pathFile);

                writer.Write("HERMES - Trackings Log ERROR/n");
                writer.WriteLine("Started process at " + DateTime.Now.ToString());
                writer.WriteLine("/nERROR caused by "+ msg);
                writer.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
