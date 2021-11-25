using Hermes.APP;
using Hermes.BLL.Utilidades;
using Hermes.DAO.SSManager;
using Hermes.DAO.SSManagerNew;
using Hermes.DAO.SSManagerNew.Track;
using Hermes.DAO.Track.AirLink;
using Hermes.DAO.Track.Airlinkexpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Hermes.BLL.SendTrack
{
    public class Tracking
    {
        private static int minutosSleep = 5;
        private static string pathLog = @"C:\LogHermes\EnvioTrack\";
        public static void TrackingAirlink()
        {
            string fileName; 
            Timer timer = new Timer();
            bool exec = false;
            string paramExec = "6|12|18|00";
            string stringlog;

            LoadTrackingDAO load = new LoadTrackingDAO();
            PedidomestreDAO mestre = new PedidomestreDAO();
            CheckpointSendedDAO send = new CheckpointSendedDAO();
            Checkpoint_airlinkDAO check = new Checkpoint_airlinkDAO();
            GenericLogRecord log = new GenericLogRecord();

            try
            {
                while (true)
                {
                    var p = new ServiceControlExecutation();
                    exec = p.ValidadtionHourExec(paramExec);

                    if (exec)
                    {
                        //1 recupera os itens para o primeiro envio, etapa que valida se já foi feito o primeiro envio
                        var objloaded = load.LoadTrackingSS();

                        if (objloaded.Count > 0)
                        {
                            fileName = "Enviados_";
                            stringlog = "1- Total de " + objloaded.Count + " para envio, registro efetuado em : " + DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                            log.RecordLog(stringlog, pathLog, fileName);

                            #region Valida pedidos carregados que já foram gravados na pedidomestre
                            //carrega itens para comparar se há algum repetido

                            //List<Pedidomestre> pedidomestresGravados = mestre.SelectPedidomestre(objloaded);


                            //LoadTracking l = new LoadTracking();
                            //if (pedidomestresGravados.Count > 0)
                            //{
                            //    int i = 0;
                            //    string[] arr = new string[pedidomestresGravados.Count];
                            //    int []index = new int[pedidomestresGravados.Count];
                            //    //-----------------------------
                            //    foreach (var itemP in pedidomestresGravados)
                            //    {                                    
                            //        arr[i] = itemP.Documento;
                            //        i++;
                            //    }

                            //    i = 0;

                            //    objloaded = objloaded.OrderBy(x => x.Codigo).ToList();

                            //    foreach (var t in objloaded)
                            //    {
                                    
                            //        index[i] = objloaded.FindIndex(item => t.Codigo == arr[i]);
                            //    }

                            //    if (index.Length > 0)
                            //    {
                            //        objloaded.RemoveAt(index[i]);
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

                            objloaded.Clear();
                            objtrk.Clear();
                            pedidomestres.Clear();

                            Thread.Sleep(minutosSleep * 60 * 1000);
                        }
                        else
                        {
                            var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");

                            //Grava o ciclo de execução
                            fileName = "Registro_Execucao_";
                            stringlog = "Serviço de envio de tracking| ciclo NÃO EXECUTADO, não existe tracking no momento da execução, registro efetuado em: " + date;
                            log.RecordLog(stringlog, pathLog, fileName);
                            //tempo que a Thead fica pausada até a próxima execução.

                            Thread.Sleep(minutosSleep * 60 * 1000);
                        }
                    }
                    else
                    {
                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");

                        //Grava o ciclo de execução
                        fileName = "Registro_Execucao_";
                        stringlog  = "Fora da horario de execução, registro efetuado em: " + date;
                        log.RecordLog(stringlog, pathLog, fileName);
                        //tempo que a Thead fica pausada até a próxima execução.

                        Thread.Sleep(minutosSleep * 60 * 1000);
                    }

                    exec = false;
                }
            }

            catch (Exception ex)
            {
                stringlog = ex.ToString() + " Registrado em : " + DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                fileName = "Erro_Execucao_";

                log.RecordLog(stringlog, pathLog, fileName);
            }            
        }
    }
}
