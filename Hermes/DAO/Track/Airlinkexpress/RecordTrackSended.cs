using Hermes;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;


namespace Hermes.APP
{
    public class RecordTrackSend
    {
        public static void RecordTrackSendList(List<LoadTracking> list) 
        {
            //ALTERAR PARA UPDATE
            var query = "insert into CheckpointSended values (@IdHarpiaCodigo, @Code)"; 
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
                {
                    db.Open();
                    foreach (var item in list)
                    {
                        db.Execute(query, new { IdHarpiaCodigo = item.Codigo.ToString(), Code = item.Code });
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                RecordLog.ErrorLogRecording(ex.ToString());
            }           
        }
    }
}
