using Hermes.APP;
using Hermes.BLL;
using Hermes.BLL.Utilidades;
using Hermes.DAO;
using Hermes.DTO.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Hermes
{
    public partial class Hermes : ServiceBase
    {
        private int minutosSleep = 25;
        Timer timer = new Timer();
        public Hermes()
        {            
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            //valida a hora de execução do serviço. 
            while (true)
            {
                RecordLog log = new RecordLog();
                bool exec = false;
                string paramExec = "|6|12|18|00|";

                var p = new ServiceControlExecutation();
                exec = p.ValidadtionHourExec(paramExec);

                try
                {                  
                    if (exec == true)
                    { 
                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                        var statusExec = "ciclo EXECUTADO ! ";

                        //Grava o ciclo de execução
                        log.HermesLogService( date, statusExec);

                        //serviço de envio dos pedidos irlink para Interlog
                        InicioServico();

                        Thread.Sleep(minutosSleep * 60 * 1000);

                        timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                        //timer.Interval = 5000; //number in milisecinds  
                        timer.Interval = (minutosSleep * 60 * 1000);
                        timer.Enabled = true;
                        exec = false;
                    }
                    else
                    {                        
                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                        var statusExec = "ciclo NÃO EXECUTADO " ;

                        //Grava o ciclo de execução
                        log.HermesLogService( date, statusExec);

                        //tempo que a Thead fica pausada até a próxima execução.

                        Thread.Sleep(minutosSleep * 60 * 1000);
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }  
        }
        protected override void OnStop()
        {
            string parada = "Serviço parado em: " + DateTime.Now.ToString();
            EventLog.WriteEntry(parada, EventLogEntryType.Information);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            string parada = "Tempo Decorrido em: " + DateTime.Now.ToString();
            EventLog.WriteEntry(parada, EventLogEntryType.Information);
        }

        public void InicioServico()
        {
            List<Order> OrdersPost = new List<Order>();
            OrdersPost = LoadOrders.LoadOrdersSend();

            if (OrdersPost.Count>0)
            {
                SendOrder.SendOrders(OrdersPost);
            }
            else
            {
                return;
            }        
        }

        public void StartDebug(string[] args)
        {
            OnStart(args);
        }

    }
}
