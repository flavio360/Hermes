using Npgsql;
using System;
using System.Collections.Generic;
using Hermes.DAO.Track.Airlinkexpress;
using Dapper;
using Hermes.DAO.Utilidades;
using System.Collections;

namespace Hermes.DAO.Track.AirLink
{
    public class PedidomestreDAO
    {
        #region Propriedades private

        private static string connString = ConnString.ConnStringa;

        private static string query = "insert into pedidomestre (clienteid, produtoid, tipo, documento, remrazaosocial, remendereco, remmunicipio,desrazaosocial, desendereco, desbairro, desmunicipio,  descep, descgc,desuf, dtentrada)values";
        private static string querySe = "select pedidoid,documento from  pedidomestre where documento in (";
        #endregion

        public void InsertPedidomestre(List<LoadTracking> trackings)
        {            
            List<Pedidomestre> objRecordDB = new List<Pedidomestre>();       
            try
            {
                foreach (var item in trackings)
                {
                    objRecordDB.Add(new Pedidomestre()
                    {
                        Clienteid = "3",
                        Produtoid = "1",
                        Tipo = "1",
                        Documento = item.Codigo.Trim(),
                        Remrazaosocial = item.RemNombre.Trim(),
                        Remendereco = item.RemAddress.Replace(",", " ").Trim(),
                        Remmunicipio = "N/D",
                        Desrazaosocial = item.DestNombre.Trim(),
                        Desendereco = item.DestAddress.Replace(",", " ").Trim(),
                        Desbairro = "N/D",
                        Desmunicipio = item.DestMunicipio != string.Empty ? item.DestMunicipio : "N/D",
                        Desuf = item.DestEstado.Trim(),
                        Descgc = item.DestDocumento.Trim(),
                        Descep = item.DestCep.Replace("-", "").Trim(),
                        Dtentrada = Convert.ToDateTime(item.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss") //"2018-08-29 18:26:24"
                    }); ; ;
                }
                
                //adiciona todos itens para o insert
                foreach (var item in objRecordDB)
                {
                    query = query + @"('" + item.Clienteid + "','" + item.Produtoid + "','" + item.Tipo + "','" + item.Documento + "','" + item.Remrazaosocial + "','" + item.Remendereco + "','" +
                         item.Remmunicipio + "','" + item.Desrazaosocial + "','" + item.Desendereco + "','" + item.Desbairro + "','" + item.Desmunicipio+ "','" +
                         item.Descep + "','" + item.Descgc + "','" +  item.Desuf + "','" + item.Dtentrada + "'),";
                }

                //remove o ultimo caractere da string de insert
                query = query.Remove(query.Length - 1);

                //using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();                    

                    //insert
                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, pgsqlConnection))
                    {
                        var ret = pgsqlcommand.ExecuteNonQuery();
                    }
                    pgsqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pedidomestre> SelectPedidomestre(List<LoadTracking> trackings=null, string code=null)
        {            
            try
            {
                List<Pedidomestre> objTrack = new List<Pedidomestre>();

                querySe = "select pedidoid,documento from  pedidomestre where documento in (";
                foreach (var item in trackings)
                {
                    querySe = querySe + "'"+item.Codigo+"'," ; 
                }

                querySe = querySe.Remove(querySe.Length - 1);
                querySe = querySe + ")";

                NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=adm#56@dba;Database=airlinkexpress");

                conn.Open();

                IEnumerable tracking = conn.Query<Pedidomestre>(querySe);

                conn.Close();

                foreach (dynamic item in tracking)
                {
                    objTrack.Add(new Pedidomestre()
                    {
                        Clienteid = item.Clienteid,
                        Documento = item.Documento,
                        PedidoId = item.PedidoId,
                        Desbairro = item.Desbairro,
                        Descep = item.Descep,
                        Descgc = item.Descgc,
                        Desendereco = item.Desendereco,
                        Desmunicipio = item.Desmunicipio,
                        Desrazaosocial = item.Desrazaosocial,
                        Desuf = item.Desuf,
                        Dtentrada = item.Dtentrada,
                        Produtoid = item.Produtoid,
                        Remendereco = item.Remendereco,
                        Remmunicipio = item.Remmunicipio,
                        Remrazaosocial = item.Remrazaosocial,
                        Tipo = item.Tipo
                    });
                }

                return objTrack;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }    
    }
}
