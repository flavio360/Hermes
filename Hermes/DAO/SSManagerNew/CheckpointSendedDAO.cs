using Dapper;
using Hermes.APP;
using Hermes.DAO.Track.Airlinkexpress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Hermes.DAO.SSManager
{
    public class CheckpointSendedDAO
    {
        private string queryInsert = "insert into CheckpointSended (HarpiaCodigo,Code,Checkpoint_id,Pedidoid) values  ";
        public void InsertCheckpointSended(List<Pedidomestre> sended)
        {
            try
            {
                queryInsert = "insert into CheckpointSended (HarpiaCodigo,PedidoidAirLink,DataEnvio) values  ";
                foreach (var item in sended)
                {
                    queryInsert = queryInsert + "('" + item.Documento + "','" +  item.PedidoId + "','" + DateTime.Now + "'),"; 
                }

                queryInsert = queryInsert.Remove(queryInsert.Length - 1);


                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
                {
                    db.Open();

                    var retorno = db.Execute(queryInsert) ;

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
