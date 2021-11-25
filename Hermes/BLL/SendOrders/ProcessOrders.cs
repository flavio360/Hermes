using Hermes.APP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;
using System.ServiceProcess;
using System.Threading;
using Hermes.DTO.API;
using Hermes.DAO;

namespace Hermes.BLL.SendOrders
{
    public class ProcessOrders
    {

        public static void ProcessOrdersSS()
        {
            int minutosSleep = 25;
            Timer timer = new Timer();

            while (true)
            {
                RecordLog log = new RecordLog();
                bool exec = false;
                string paramExec = "|6|12|18|00|"; //horários de execução

                var p = new ServiceControlExecutation();
                exec = p.ValidadtionHourExec(paramExec);

                try
                {
                    if (exec == true)
                    {
                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                        var statusExec = "ciclo EXECUTADO ! ";

                        //Grava o ciclo de execução
                        log.HermesLogService(date, statusExec);

                        //serviço de envio dos pedidos irlink para Interlog
                        List<Order> OrdersPost = new List<Order>();
                        OrdersPost = LoadOrders.LoadOrdersSend();

                        if (OrdersPost.Count > 0)
                        {
                            SendOrder.SendOrders(OrdersPost);
                        }


                        Thread.Sleep(minutosSleep * 60 * 1000);

                        
                        //timer.Interval = 5000; //number in milisecinds  
                        timer.Interval = (minutosSleep * 60 * 1000);
                        exec = false;
                    }
                    else
                    {
                        var date = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm");
                        var statusExec = "ciclo NÃO EXECUTADO ";

                        //Grava o ciclo de execução
                        log.HermesLogService(date, statusExec);

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
    }
}
