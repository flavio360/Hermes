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
                bool exec = VerificarHoraExecucao();
                try
                {
                    if (exec==true)
                    {
                        RecordLog log = new RecordLog();

                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                        var statusExec = "ciclo EXECUTADO";

                        EventLog.WriteEntry(statusExec, EventLogEntryType.Information);

                        //Grava o ciclo de execução
                        log.HermesLogService(string.Empty, date, statusExec);

                        //serviço de envio dos pedidos irlink para Interlog
                        InicioServico();

                        timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                        timer.Interval = 5000; //number in milisecinds  
                        timer.Enabled = true;
                        exec = false;
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
            //WriteToFile("Service is stopped at " + DateTime.Now);

        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //WriteToFile("Service is recall at " + DateTime.Now);
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
                var t_exec = DateTime.Now.AddHours(-3).ToString("HH:mm");

                if (t_exec == "06:30"|| t_exec == "12:30"|| t_exec == "18:30"|| t_exec == "00:30")
                {
                    exec = true; 
                }
                else
                {

                RecordLog log = new RecordLog();
                var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                var statusExec = "ciclo NÃO EXECUTADO";
                
                //Grava o ciclo de execução
                log.HermesLogService(string.Empty, date, statusExec);
                
                //tempo que a Thead fica pausada até a próxima execução.
                int minutosSleep = 59;
                Thread.Sleep(minutosSleep * 60 * 1000);
                }

            return exec;
        }

        public void StartDebug(string[] args)
        {
            OnStart(args);
        }

    }
}
