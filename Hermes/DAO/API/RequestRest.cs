using Hermes.ADO;
using Hermes.APP;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hermes.DTO.API
{
    public class RequestRest
    {
        
        private static string urlBase = @"https://www.sicloweb.com.br/api/v1/delivery/add";
        private RestClient cliente;
        private RestRequest requisicao;

        #region agrupa
        public RequestRest(string urlBase)
        {
            if (urlBase == null)
                throw new InvalidOperationException("URL é obrigatório");

            cliente = new RestClient(urlBase);
            requisicao = new RestRequest();
        }

        public void addHeader(Dictionary<string, string> header)
        {
            if (header != null)
                header.ToList().ForEach(x => requisicao.AddHeader(x.Key, x.Value));
        }
        public void addCookie()
        {
            cliente.CookieContainer = new CookieContainer();
        }


        //public T post<T>(string urlPath)
        //{
        //    return post<T>(urlPath, DataFormat.Json, null);
        //}


        public T Post<T>(string urlPath, DataFormat dataformat, object body = null)
        {
            requisicao.Resource = urlPath ?? throw new InvalidOperationException("Caminho da URL é obrigatório");
            requisicao.Method = Method.POST;
            requisicao.RequestFormat = dataformat;

            if (body != null) if (dataformat == DataFormat.Json) requisicao.AddJsonBody(body); else requisicao.AddXmlBody(body);

            IRestResponse response = cliente.Execute(requisicao);
            if (response.Content == null)
                throw new InvalidOperationException("Resposta vazia.");
            if (!new HttpStatusCode[] { HttpStatusCode.Accepted, HttpStatusCode.Created, HttpStatusCode.OK, HttpStatusCode.PartialContent }.Contains(response.StatusCode))
                throw new InvalidOperationException("Resposta inválida: Código: " + response.StatusCode + " objeto: " + response.Content);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public string Post(string urlPath, DataFormat dataformat, object body = null)
        {
            requisicao.Resource = urlPath ?? throw new InvalidOperationException("Caminho da URL é obrigatório");
            requisicao.Method = Method.POST;
            requisicao.RequestFormat = dataformat;

            if (body != null) if (dataformat == DataFormat.Json) requisicao.AddJsonBody(body); else requisicao.AddXmlBody(body);

            IRestResponse response = cliente.Execute(requisicao);

            return response.Content;
        }
        #endregion





        public string PostFormData(string urlPath, Order body)
        {
            var client = new RestClient(urlBase);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("token", "3806734b256c27e41ec2c6bffa26d9e7");
            request.AddParameter("destinatario", body.Destinatario);
            request.AddParameter("pedido", body.Pedido);
            request.AddParameter("ceporigem", body.Ceporigem);
            request.AddParameter("endereco", "Rua ficticia 1500");
            request.AddParameter("numero", "");
            request.AddParameter("complemento", "Bairro ficticio");
            request.AddParameter("bairro", " ");
            request.AddParameter("cidade", "JUNDIAI");
            request.AddParameter("celular", "");
            request.AddParameter("cep", "13214530");
            request.AddParameter("UF", "SP");
            request.AddParameter("Cpf_cnpj", "12644526833");
            request.AddParameter("Insc_est", "");
            request.AddParameter("Email", "mn.ogassawala@gmail.com");
            request.AddParameter("Referencia", "");
            request.AddParameter("Telefone", "");
            request.AddParameter("Telefone1", "");
            request.AddParameter("Telefone2", "");
            request.AddParameter("codproduto", "1");
            request.AddParameter("comprimento", "0.001");
            request.AddParameter("largura", "0.001");
            request.AddParameter("altura", "0.001");
            request.AddParameter("peso", "12.65");
            request.AddParameter("quantidade", "1");
            request.AddParameter("mao_propria", "0");
            request.AddParameter("nome_mao_propria", "");
            request.AddParameter("seguro", "0");
            request.AddParameter("com_ar", "1");
            request.AddParameter("nrnota", "");
            request.AddParameter("Vlr_entrega", "");
            request.AddParameter("HAWBHouse", "");
            request.AddParameter("MAWB", "");
            request.AddParameter("CodNCM", "");
            request.AddParameter("Incoterms", "DDP");
            IRestResponse response = client.Execute(request);


            OrderResponse orderResponse = new OrderResponse();
            orderResponse.Delivery = response.

            return orderResponse;
        }

        //meu
        public static  List<Order> SendTracking(List<Order> order) 
        {
            try
            {   
                foreach (dynamic item in order)
                {                    
                    RequestRest postTracking = new RequestRest(urlBase);
                    //OrderResponse trackingRetorno = postTracking.Post(urlBase, RestSharp.DataFormat.Json, item);                    
                    var trackingRetorno = postTracking.PostFormData(urlBase, item);                    
                }
            }

            catch (System.Exception ex)
            {
                RecordLog.RecordLogSended(ex.ToString());
            }
            return null;
        }
    }
}
