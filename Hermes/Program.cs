using System.ServiceProcess;


namespace Hermes
{
    static class Program
    {
        static void Main()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {

                //DEBUG ENVIO TRACK PARA AIRLINK
                //HermesTrack Hservice = new HermesTrack();
                //Hservice.StartDebugTrack(new string[2]);

                //DEBUG ENVIO DE PEDIDOS PARA INTERLOG
                Hermes service = new Hermes();
                service.StartDebug(new string[2]);

                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Hermes(),new HermesTrack() };
                ServiceBase.Run(ServicesToRun);
            }
        }        
    }
}
