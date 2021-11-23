using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Npgsql;
using System;
using System.Data;
using Hermes.DAO.SSManagerNew.Track;

namespace Hermes.APP
{
    public  class LoadTrackingDAO
    {
        #region query carrega os pedidos para gravar na tabela pedidomaster  da airlink

        private static string query = "" +
            "select "+
                "b.Id, b.CreatedDate, b.Codigo,g.[Description] as DestMunicipio, b.RemAddress,b.RemCep,b.RemEstado,b.RemNombre, b.DestAddress, b.DestCep, f.Name as DestEstado, " +
                " b.DestDocumento, b.CreatedDate, B.DestNombre " +
                "from " +
                "    HarpiaHouse b " +
                "left join " +
                "    CheckpointSended c on c.HarpiaCodigo = b.Codigo " +
                "left join " +
                "    HarpiaState f on f.Id = b.DestEstado " +
                "left join " +
                "    HarpiaMunicipality g  on g.Id = b.DestMunicipio " +
                "where " +
                "    c.HarpiaCodigo is null";
        #endregion

        #region query que carrega os tracks pendentes de envio
        private static string queryTrack =
            "select c.Id_pedido as CheckPointSended_Id, f.CodeDestino as CodeStaus ,a.HouseId, a.id as CheckpointId,a.Created,a.[Date],c.PedidoidAirLink " +
            "from[Checkpoint] a left join HarpiaHouse b on a.HouseId = b.Id " +
            "left join CheckpointSended c on b.Codigo = c.HarpiaCodigo " +
            "left join CheckpointTrackingSended d on a.Id = d.CheckpointId " +
            "left join TypeCheckpoint e on a.Code = e.Code " +
            "left join  CheckpointDeXPara f on e.Id = f.CodeOrigin " +
            "where d.CheckpointId is null and c.PedidoidAirLink is not null   order by c.Id_pedido, f.CodeDestino ";
        #endregion


        #region Carrega os pedidos pendentes para gravar na pedidomaster da airlink
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
                            CreatedDate = item.CreatedDate,
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
        #endregion

        public List<SendTrack> SelectTracking()
        {
            List<SendTrack> objTrackings = new List<SendTrack>();
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["HConnectionString"].ConnectionString))
                {
                    db.Open();

                    IEnumerable tracking = db.Query<SendTrack>(queryTrack);

                    db.Close();

                    foreach (dynamic item in tracking)
                    {
                        objTrackings.Add(new SendTrack()
                        {
                            CheckPointSended_Id = item.CheckPointSended_Id,
                            HouseId = item.HouseId,
                            CodeStaus = item.CodeStaus,
                            Created = item.Created,
                            CheckpointId = item.CheckpointId,
                            PedidoidAirLink = item.PedidoidAirLink
                        });;
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
