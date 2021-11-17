using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Runtime.InteropServices;
using Hermes.ADO;
using System.Data.SqlClient;
using System.Data;
using Hermes.APP;
using Hermes.DTO.API;
using Hermes.DAO;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Timer = System.Timers.Timer;
using System.Threading;

namespace Hermes
{
    public partial class Hermes : ServiceBase
    {
        private int minutosSleep = 15;
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

                exec = VerificarHoraExecucao();

                try
                {                  
                    if (exec == true)
                    { 
                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                        var statusExec = "ciclo EXECUTADO ! ";

                        //EventLog.WriteEntry(statusExec, EventLogEntryType.Information);

                        //Grava o ciclo de execução
                        log.HermesLogService(string.Empty, date, statusExec);

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
                        log.HermesLogService(string.Empty, date, statusExec);

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

        public bool VerificarHoraExecucao()
        {
            bool exec = false;

                //var t_exec = DateTime.Now.AddHours(-3).Hour.ToString();
                var t_exec = DateTime.Now.AddHours(-3).ToString("HH");

                if (t_exec == "06"|| t_exec == "12"|| t_exec == "18"|| t_exec == "00" || t_exec == "08")
                {
                    exec = true; 
                }

            return exec;
        }

        public void StartDebug(string[] args)
        {
            OnStart(args);
        }

    }
}
