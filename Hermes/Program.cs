using System.ServiceProcess;

namespace Hermes
{
    static class Program
    {
        static void Main()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Hermes service = new Hermes();
                service.StartDebug(new string[2]);
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Hermes() };
                ServiceBase.Run(ServicesToRun);
            }
        }        
    }
}
