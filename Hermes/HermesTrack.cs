using Hermes.BLL.SendTrack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    partial class HermesTrack : ServiceBase
    {
        public HermesTrack()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //Envia tracking da Airlink.
                Tracking.TrackingAirlink();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        public void StartDebugTrack(string[] args)
        {
            OnStart(args);
        }
    }
}
