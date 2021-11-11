using Hermes.ADO;
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
        public static  List<Order> SendOrders(List<Order> order)
        {
            try
            {
                List<Tracking> tracking = new List<Tracking>();
                RequestRest postOrder = new RequestRest();

                foreach (dynamic item in order)
                {  
                    OrderResponse orderResponse = postOrder.PostFormData(item);
                     
                    RecordLog.LogPOST(orderResponse, item.Pedido);

                    if (orderResponse.Error.Code=="200")
                    {
                        LoadOrders.RecordSendedOrder(item.Pedido);
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
