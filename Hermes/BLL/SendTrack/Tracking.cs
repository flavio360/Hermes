using Hermes.APP;
using Hermes.BLL.Utilidades;
using Hermes.DAO.SSManager;
using Hermes.DAO.SSManagerNew.Track;
using Hermes.DAO.Track.AirLink;
using Hermes.DAO.Track.Airlinkexpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.BLL.SendTrack
{
    public class Tracking
    {
        public static void TrackingAirlink()
        {
            LoadTrackingDAO load = new LoadTrackingDAO();
            PedidomestreDAO mestre = new PedidomestreDAO();
            CheckpointSendedDAO send = new CheckpointSendedDAO();
            Checkpoint_airlinkDAO check = new Checkpoint_airlinkDAO();
            bool exec = false;

            try
            {                
                exec = TrataString.VerificarHoraExecucao();
                if (true)
                {
                    //1 recupera os itens para o primeiro envio, etapa que valida se já foi feito o primeiro envio
                    var objloaded = load.LoadTrackingSS();

                    if (objloaded.Count > 0)
                    {
                        #region Valida pedidos carregados que já foram gravados na pedidomestre
                        //carrega itens para comparar se há algum repetido
                        //List<Pedidomestre> pedidomestresGravados = mestre.SelectPedidomestre(objloaded);


                        //if (pedidomestresGravados.Count > 0)
                        //{
                        //    LoadTracking l = new LoadTracking();
                        //    foreach (dynamic t in pedidomestresGravados)
                        //    {
                        //        l.Codigo = t.Documento;
                        //        objloaded.RemoveAll(x => x.Codigo == l.Codigo);
                        //    }
                        //}

                        #endregion

                        //2 faz insert dos itens que dewvem ser enviados na pedidomsetre, só deve fazer insert uma vez .
                        mestre.InsertPedidomestre(objloaded);

                        //3 recupera o que foi gravado
                        List<Pedidomestre> pedidomestres = mestre.SelectPedidomestre(objloaded);

                        //4 Registra na ssmanager tudo que foi gravado na pedidomestre da airlink
                        send.InsertCheckpointSended(pedidomestres);

                        //5 recupera os efetivamente os tracking disponiveis
                        var objtrk = load.SelectTracking();

                        //6 grava na base airlink os tracking dos pedidos
                        check.InsertCheckpoint_airlink(objtrk);

                        //7 grava os pedidos enviados 
                        CheckpointTrackingSendedDAO.InsertCheckpointTrackingSended(objtrk);
                    }
                }
                else
                {
                    //var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                    //var statusExec = "ciclo NÃO EXECUTADO ";

                    ////Grava o ciclo de execução
                    //log.HermesLogService(string.Empty, date, statusExec);

                    ////tempo que a Thead fica pausada até a próxima execução.

                    //Thread.Sleep(minutosSleep * 60 * 1000);
                }
                exec = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
