using System.Collections.Generic;
using System.Data.SqlClient;
using Hermes.ADO;
using Dapper;
using System.Configuration;
using System.Collections;
using Hermes.DTO.API;

namespace Hermes.APP
{
    public  class RequestTracking
    {  

        private static string query = "SELECT b.Codigo, A.Code,A.Created FROM [Checkpoint] A " +
                                      "INNER JOIN HarpiaHouse B on A.HouseId = B.Id LEFT JOIN " +
                                      "CheckpointSended C on B.Codigo  = C.IdHarpiaCodigo AND A.Code = C.code " +
                                      "WHERE A.FechaEnvioGeodis BETWEEN GETDATE() - 6 AND GETDATE() AND C.IdHarpiaCodigo IS NULL";

        public static void RequestTrackingSS()
        {            
            try
            {
                List<Tracking> objTrackings = new List<Tracking>();

                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
                {
                    db.Open();

                    IEnumerable tracking = db.Query<Tracking>(query);

                    db.Close();

                    foreach (dynamic item in tracking)
                    {
                        objTrackings.Add(new Tracking() 
                        { 
                        Code = item.Code,
                        Codigo = item.Codigo,
                        Created = item.Created.ToString()
                        });
                    }
                    RecordLog.RecordLogSended(objTrackings);
                    
                    if (objTrackings.Count != 0)
                    {
                        //CommunicationInterLog.SendTracking();
                    }

                    if (objTrackings.Count!=0)
                    {
                        RecordTrackSend.RecordTrackSendList(objTrackings);
                    }
                }
            }
            catch (System.Exception ex)
            {             
                RecordLog.RecordLogSended(ex.ToString());
            }            
        }
    }
}
