using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using Npgsql;
using Hermes.DAO.Utilidades;
using Hermes.DAO.SSManagerNew.Track;

namespace Hermes.DAO.Track.Airlinkexpress
{
    public class Checkpoint_airlinkDAO
    {
        private static string querySelec = "select  id from checkpoint_airlink order by id desc limit 1 ";
        private static string queryInsert = "insert into  checkpoint_airlink (id,type_checkpoint_id,pedido_id,data_cadastro,data_ocorrencia,observacao) values";
        private static string connString = ConnString.ConnStringa;
        int id;
        public int SelectCheckpoint_airlink()
        {
            IEnumerable tracking;
            
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(ConnString.ConnStringa);

                conn.Open();

                NpgsqlCommand command = new NpgsqlCommand("select code from type_checkpoint", conn);

                //tracking = conn.Query<LoadTracking>(querySelec);
                tracking = conn.Query(querySelec);

                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            foreach (dynamic item in tracking)
            {
                id = item.id;
            }

            return id;
        }


        public  void  InsertCheckpoint_airlink(List<SendTrack> obj)
        {
            int ultID = SelectCheckpoint_airlink()+1;

            foreach (var item in obj)
            {
                queryInsert = queryInsert + @"('" + ultID + "','" + item.CodeStaus + "','" + item.PedidoidAirLink.Replace(" ","") + "','" + "now()" + "','" + item.Created + "','" + "'),";

                ultID++;
            }


            queryInsert = RemoveUltimoCaractere.RemoveUltimoCaractereString(queryInsert);


            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
            {
                //Abra a conexão com o PgSQL                  
                pgsqlConnection.Open();

                //insert
                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(queryInsert, pgsqlConnection))
                {
                    var ret = pgsqlcommand.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
            //return null;
        }
    }
}
