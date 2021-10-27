using Hermes.ADO;
using Hermes.APP;
using Hermes.DAO;
using Hermes.DTO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Hermes()
            //};
            //ServiceBase.Run(ServicesToRun);

            {
                //remover a partir daqui e descomentar acima
#if (!DEBUG)

            ServiceBase[] ServicesToRun;

          ServicesToRun = new ServiceBase[]

          {

              new Service1()

          };

          ServiceBase.Run(ServicesToRun);

#else

                // Debug code: Permite debugar um código sem se passar por um Windows Service.

                // Defina qual método deseja chamar no inicio do Debug (ex. MetodoRealizaFuncao)

                // Depois de debugar basta compilar em Release e instalar para funcionar normalmente.

                //Hermes service = new Hermes();

                // Chamada do seu método para Debug.

                //service.RequestTrackingSS();
                
                List<Order> a = new List<Order>();
                a = LoadOrdersDAO.LoadOrders();
                
                SendOrder.SendTracking(a);



                RequestTracking.RequestTrackingSS();

            // Coloque sempre um breakpoint para o ponto de parada do seu código.

                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                
#endif
                
        }

        }
    }
}
