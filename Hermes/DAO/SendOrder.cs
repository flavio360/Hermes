using Hermes.APP;
using Hermes.DTO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO
{
    public class SendOrder
    {
        public static  List<Order> SendTracking(List<Order> order)
        {
            try
            {
                RequestRest postTracking = new RequestRest();

                foreach (dynamic item in order)
                {                    
                    OrderResponse trackingRetorno = postTracking.PostFormData(item);
                        
                    RecordLog.LogPOST(trackingRetorno, item.Pedido);

                    if (trackingRetorno.Error.Code=="200")
                    {
                        //implantar o inseert na tabela CheckpointSended
                    }
                }
            }

            catch (System.Exception ex)
            {
                RecordLog.ErrorLogRecording(ex.ToString());
            }
            return null;
        }
    }
}
