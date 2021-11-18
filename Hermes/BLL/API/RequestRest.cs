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
        public static string statusCode;
        public static string msgRet;
        private static string urlBase = @"https://www.sicloweb.com.br/api/v1/delivery/add";
        private static string token = "3806734b256c27e41ec2c6bffa26d9e7";
        private RestClient cliente;
        private RestRequest requisicao;

        #region agrupa
        //public RequestRest(string urlBase)
        //{
        //    if (urlBase == null)
        //        throw new InvalidOperationException("URL é obrigatório");

        //    cliente = new RestClient(urlBase);
        //    requisicao = new RestRequest();
        //}

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

        public OrderResponse PostFormData( Order body)
        {

            OrderResponse objretorno;


            var client = new RestClient(urlBase);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            request.AlwaysMultipartFormData = true;
            request.AddParameter("token", token);
            request.AddParameter("destinatario", body.Destinatario);
            request.AddParameter("pedido", body.Pedido);
            request.AddParameter("ceporigem", body.Ceporigem.Replace("-","").Replace(" ","").PadLeft(8,'0'));
            request.AddParameter("endereco", body.Endereco);
            request.AddParameter("numero", body.Numero);
            request.AddParameter("complemento",body.Complemento);
            request.AddParameter("bairro",body.Bairro == string.Empty? "_": body.Bairro);
            request.AddParameter("cidade", body.Cidade);
            request.AddParameter("celular", body.Celular);
            request.AddParameter("cep", body.Cep);
            request.AddParameter("UF", body.UF);
            request.AddParameter("Cpf_cnpj", body.Cpf_cnpj);
            request.AddParameter("Insc_est", body.Insc_est);
            request.AddParameter("Email", body.Email);
            request.AddParameter("Referencia", body.Referencia);
            request.AddParameter("Telefone", body.Telefone);
            request.AddParameter("Telefone1", body.Telefone1);
            request.AddParameter("Telefone2", body.Telefone2);
            request.AddParameter("codproduto", "1");
            request.AddParameter("comprimento", "0.001");
            request.AddParameter("largura", "0.001");
            request.AddParameter("altura", "0.001");
            request.AddParameter("peso", body.Peso);
            request.AddParameter("quantidade", body.Quantidade);
            request.AddParameter("mao_propria", "0");
            request.AddParameter("nome_mao_propria", "");
            request.AddParameter("seguro", "0");
            request.AddParameter("com_ar", "1");
            request.AddParameter("nrnota", "");
            request.AddParameter("Vlr_entrega", body.Vlrentrega);
            request.AddParameter("HAWBHouse", body.HAWBHouse);
            request.AddParameter("MAWB", body.MAWB);
            request.AddParameter("CodNCM", body.CodNCM);
            request.AddParameter("Incoterms", body.Incoterms);

            try
            {
                IRestResponse response = client.Execute(request);

                objretorno = JsonConvert.DeserializeObject<OrderResponse>(response.Content);
                statusCode = Convert.ToInt32(response.StatusCode).ToString();
                msgRet = response.StatusDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }        

            return objretorno;            
        } 
        

    }
}
