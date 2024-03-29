﻿using Hermes.DTO.API;
using System;
using System.Collections.Generic;
using System.IO;


namespace Hermes.APP
{
    public class RecordLog
    {
        public static void RecordLogSended(List<LoadTracking> tracking)
        {
            try
            {
                foreach (var item in tracking)
                {
                    StreamWriter lObjEscreveTexto = new StreamWriter(@"C:\LogHermes\Service_Status_Exec\ERROR_in_Recorded_objects_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
                    var dataAtual = DateTime.Now.ToString("yyyyMMddTHH:mm");

                    lObjEscreveTexto.WriteLine(string.Concat("Codigo:..." + item.Codigo + "...........Status Code:..." + item.Code + "...........Data do registro:..." + item.Created, Environment.NewLine));

                    lObjEscreveTexto.Flush();
                    lObjEscreveTexto.Close();
                    lObjEscreveTexto.Dispose();
                }
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
                StreamWriter lObjEscreveTexto = new StreamWriter(@"C:\LogHermes\Service_Status_Exec\ERROR_in_Recorded_objects_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
                var dataAtual = DateTime.Now.ToString("yyyyMMdd T HH:mm");

                lObjEscreveTexto.WriteLine(string.Concat( "Erro na gravação do pedido "+ pedido, "Observação: " + msg, " | " + code, Environment.NewLine));

                lObjEscreveTexto.Flush();
                lObjEscreveTexto.Close();
                lObjEscreveTexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void LogPOST(OrderResponse order, string pedido = null, string statusCode=null )
        {
            try
            {
                string situation;
                string dataAtual = DateTime.Now.ToString("yyyyMMdd");

                StreamWriter lObjEscreveTexto = new StreamWriter(@"C:\LogHermes\Service_Post_API\Communication_LOG_"+ dataAtual+".txt", true);

                if (RequestRest.statusCode == "200")
                {
                    situation = "Success | Delivery Code: " + order.Delivery + " Response code serve: " + RequestRest.statusCode;
                }
                else
                {
                    situation = "Fail | message returned server : " + order.Error.Message + "|  Code response server : " + RequestRest.statusCode;
                }


                lObjEscreveTexto.WriteLine(string.Concat( "Postagem do pedido "+ pedido + ", com status: "+ situation, ", Hora Execução: "+ DateTime.Now.AddHours(-3).ToString("HH:mm"), Environment.NewLine));

                lObjEscreveTexto.Flush();
                lObjEscreveTexto.Close();
                lObjEscreveTexto.Dispose();                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void HermesLogService( string date=null, string comportamentoServico=null)
        {
            try
            {
                //grava quando o serviço incia, para ou um ciclo de execução.
                if (date != string.Empty && comportamentoServico != string.Empty)
                {
                    StreamWriter lObjEscreveTexto = new StreamWriter(@"C:\LogHermes\Log_Servico_"+DateTime.Now.ToString("yyyy-MM-dd")+".txt", true);


                    lObjEscreveTexto.WriteLine(string.Concat(date, " - ", comportamentoServico, Environment.NewLine));

                    lObjEscreveTexto.Flush();
                    lObjEscreveTexto.Close();
                    lObjEscreveTexto.Dispose();
                    #region original
                    //string pathFile = @"C:\LogHermes\Service_Status_Exec\" + DateTime.Now.ToString("yyyyMMdd");
                    //System.IO.Directory.CreateDirectory(pathFile);

                    //pathFile = pathFile + "\\" + DateTime.Now.ToString("yyyyMMddTHHmm") + ".txt";

                    //if (!File.Exists(pathFile))
                    //{
                    //    StreamWriter st = File.AppendText(pathFile);

                    //    st.Write("####  HERMES - Log do Status de execução do serviço ##### " + DateTime.Now.ToString("ddMMMMyyyy") + "\n");
                    //    st.Close();
                    //}

                    //StreamWriter s = File.AppendText(pathFile);

                    //s.WriteLine("Registro da Hora de execução foi em " + date + ", o status do serviço: " + comportamentoServico);
                    //s.Close();
                    #endregion
                }
            }
            catch (Exception ex )
            {
                throw ex;
            }
            
        }


        public static void RecordLogsGeneric(string stringLog, string path)
        {
            StreamWriter lObjEscreveTexto = new StreamWriter(path, true);
            lObjEscreveTexto.WriteLine(string.Concat(stringLog, Environment.NewLine));

            lObjEscreveTexto.Flush();
            lObjEscreveTexto.Close();
            lObjEscreveTexto.Dispose();
        }
    }
}
