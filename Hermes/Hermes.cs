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
using System.Threading.Tasks;


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
            inicioServico();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            //WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //WriteToFile("Service is recall at " + DateTime.Now);
        }        

        public void inicioServico()
        {
            List<Order> OrdersPost = new List<Order>();

            OrdersPost = LoadOrders.LoadOrdersSend();

            SendOrder.SendOrders(OrdersPost);
        }
    }
}
