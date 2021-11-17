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
        public static   List<Order> SendOrders(List<Order> order)
        {
            try
            {
                List<Tracking> tracking = new List<Tracking>();
                RequestRest postOrder = new RequestRest();

                foreach (dynamic item in order)
                {  
                    //Envia pedido para Interlog
                    OrderResponse orderResponse = postOrder.PostFormData(item);          

                    if (RequestRest.statusCode == "200")
                    {
                        //Grava na base o pedido que foi enviado para a Interlog
                        LoadOrders.RecordSendedOrder(item.Pedido,orderResponse.Delivery);
                    }

                    var codigoRet = RequestRest.statusCode;
                   
                    //grava Log do response da API Interlog
                    RecordLog.LogPOST(orderResponse, item.Pedido, codigoRet);
                }               
            }
            catch (System.Exception ex)
            {
                RecordLog.ErrorLogRecording(ex.ToString(),string.Empty, string.Empty);
            }
            return null;
        }
    }
}
