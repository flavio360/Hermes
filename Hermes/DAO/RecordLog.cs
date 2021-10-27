using Hermes.ADO;
using Hermes.DTO.API;
using System;
using System.Collections.Generic;
using System.IO;


namespace Hermes.APP
{
    public class RecordLog
    {
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
                ErrorLogRecording(ex.ToString());
            }
        
        }

        public static void ErrorLogRecording(string msg, string code=null, string pedido=null)
        {
            try
            {
                string pathFile = @"C:\LogHermes\" + DateTime.Now.ToString("yyyyMMdd");
                System.IO.Directory.CreateDirectory(pathFile);
                pathFile = pathFile + @"\ERROR_in_Recorded_objects_" + DateTime.Now.ToString("yyyyMMddT_hhmm") +"_Order_"+ pedido + ".txt";
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

        public static void LogPOST(OrderResponse order, string pedido = null)
        {
            try
            {
                
                string pathFile = @"C:\LogHermes\Service_Post_API\" + DateTime.Now.ToString("yyyyMMdd");
                System.IO.Directory.CreateDirectory(pathFile);
               
                pathFile = pathFile + @"\LOG_POST_" + DateTime.Now.ToString("yyyyMMddT_hh")+ ".txt";                

                if (!File.Exists(pathFile))
                {
                    StreamWriter st = File.AppendText(pathFile);

                    st.Write("####  HERMES - Post API Interlog #####"+ DateTime.Now.ToString("dd MMMM yyyy")+ "\n");
                    st.Close();
                }

                 StreamWriter s = File.AppendText(pathFile);

                 var situation = order.Status == "200" ? "Success | Delivery Code: " + order.Delivery : "Fail= " + order.Error.Message.ToString();

                 s.WriteLine("Status: " + situation+"| Order: " + pedido + "| Process executed at : " + DateTime.Now.ToString("yyyyMMddT_hhmm"));
                 s.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
