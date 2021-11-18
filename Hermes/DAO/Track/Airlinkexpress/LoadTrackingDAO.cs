using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Npgsql;
using System;
using System.Data;

namespace Hermes.APP
{
    public  class LoadTrackingDAO
    {
 
        private static string query = "select top 5" +
                                            "b.Codigo, e.CodeDestino,e.CodeOrigin,d.Id, d.Code, b.RemAddress,b.RemCep,b.RemEstado,b.RemNombre, b.DestAddress, b.DestCep, " +
                                            "g.[Description] as DestMunicipio,b.DestDocumento, a.Created, B.DestNombre, f.Name as DestEstado " +
                                      "from " +
                                      "    [Checkpoint] a " +
                                      "left join " +
                                      "    HarpiaHouse b on a.HouseId = b.Id " +
                                      "left join " +
                                      "    CheckpointSended c on a.Id = Checkpoint_id " +
                                      "left join " +
                                      "    TypeCheckpoint d on a.code = d.Code " +
                                      "left join " +
                                      "    CheckpointDeXPara e on e.CodeOrigin = d.id " +
                                      "left join " +
                                            "HarpiaState f on f.Id = b.DestEstado " +
                                       "left join " +
                                             "HarpiaMunicipality g on g.Id = b.DestMunicipio " +
                                      "where " +
                                      "    c.Checkpoint_id is null ";

        public List<LoadTracking> LoadTrackingSS()
        {
            List<LoadTracking> objTrackings = new List<LoadTracking>();
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["HConnectionString"].ConnectionString))
                {
                    db.Open();

                    IEnumerable tracking = db.Query<LoadTracking>(query);

                    db.Close();

                    foreach (dynamic item in tracking)
                    {
                        objTrackings.Add(new LoadTracking() 
                        {
                            DestDocumento = item.DestDocumento,
                            DestMunicipio = item.DestMunicipio,
                            DestEstado = item.DestEstado,
                            DestCep = item.DestCep,
                            DestAddress = item.DestAddress,
                            RemNombre = item.RemNombre,
                            RemEstado = item.RemEstado,
                            RemCep = item.RemCep,
                            Codigo = item.Codigo,
                            Code = item.Code,
                            Created = item.Created,
                            CodeDestino = item.CodeDestino,
                            RemAddress = item.RemAddress,
                            DestNombre = item.DestNombre
                        });
                    }
                }             
            }
            
            catch (System.Exception ex)
            {             
                //RecordLog.ErrorLogRecording(ex.ToString());
            }

            return objTrackings;
        }


       
    }
}
