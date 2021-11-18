using Npgsql;
using System;
using System.Collections.Generic;
using Hermes.DAO.Track.Airlinkexpress;
using Dapper;

namespace Hermes.DAO.Track.AirLink
{
    public class PedidomestreDAO
    {
        private static string connString = "Server=localhost;Port=5432;User Id = postgres; Password=adm#56@dba;Database=airlinkexpress";

        private static string valuesInsert = "insert into pedidomestre (clienteid, produtoid, tipo, documento, remrazaosocial, remendereco, remmunicipio,desrazaosocial, desendereco, desbairro, desmunicipio,  descep, descgc,desuf, dtentrada)values";
        public void RecordTracksAirlink(List<LoadTracking> trackings)
        {
            List<Pedidomestre> objRecordDB = new List<Pedidomestre>();
        //IEnumerable tracking;
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
                        Remendereco = item.RemAddress.Replace(","," ").Trim(),
                        Remmunicipio = "N/D",
                        Desrazaosocial = item.DestNombre.Trim(),
                        Desendereco = item.DestAddress.Replace(",", " ").Trim(),
                        Desbairro = "N/D",
                        Desmunicipio = item.DestMunicipio != string.Empty ? item.DestMunicipio : "N/D",
                        Desuf = item.DestEstado.Trim(),
                        Descgc = item.DestDocumento.Trim(),
                        Descep = item.DestCep.Replace("-", "").Trim(),
                    });
                }
                

                foreach (var item in objRecordDB)
                {
                    valuesInsert = valuesInsert + @"('" + item.Clienteid + "','" + item.Produtoid + "','" + item.Tipo + "','" + item.Documento + "','" + item.Remrazaosocial + "','" + item.Remendereco + "','" +
                         item.Remmunicipio + "','" + item.Desrazaosocial + "','" + item.Desendereco + "','" + item.Desbairro + "','" + item.Desmunicipio+ "','" +
                         item.Descep + "','" + item.Descgc + "','" +  item.Desuf + "'," + "now()"+"),";
                }

                valuesInsert = valuesInsert.Remove(valuesInsert.Length - 1);


                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdInserir = valuesInsert;

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
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
    
    }
}
