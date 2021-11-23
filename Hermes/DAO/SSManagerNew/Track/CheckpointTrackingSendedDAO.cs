using Dapper;
using Hermes.DAO.Utilidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO.SSManagerNew.Track
{
    public class CheckpointTrackingSendedDAO
    {
        private static string queryInsert = "insert into CheckpointTrackingSended (CheckpointSended_ID,CodeStatus,HouseId,CheckpointId) values ";
        public static void InsertCheckpointTrackingSended(List<SendTrack> sendTracks)
        {

            try
            {
                foreach (var item in sendTracks)
                {
                    queryInsert = queryInsert + @"('" + item.CheckPointSended_Id + "','" + item.CodeStaus + "','" + item.HouseId + "','" + item.CheckpointId + "'),";

                }

                queryInsert = RemoveUltimoCaractere.RemoveUltimoCaractereString(queryInsert);

                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["HConnectionString"].ConnectionString))
                {
                    db.Open();

                    var retorno = db.Execute(queryInsert);

                    db.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
