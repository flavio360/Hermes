using Hermes.DAO.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hermes.DTO.API
{
    public class Order : Authentication
    {
        //public string Pedido { get; set; }
        //[JsonPropertyName("pedido")]
        //public string Codigo { get; set; }

        [JsonPropertyName("destinatario")]
        public string Destinatario { get; set; }

        [JsonPropertyName("pedido")]
        public string Pedido { get; set; }

        [JsonPropertyName("ceporigem")]
        public string Ceporigem { get; set; }

        [JsonPropertyName("insc_est")]
        public string Inscest { get; set; }

        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("complemento")]

        public string Complemento { get; set; }

        [JsonPropertyName("bairro")]

        public string Bairro { get; set; }

        [JsonPropertyName("cidade")]

        public string Cidade { get; set; }

        [JsonPropertyName("celular")]
        public string Celular { get; set; }

        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("numero")]
        public string UF { get; set; }

        [JsonPropertyName("cpf_cnpj")]
        public string Cpf_cnpj { get; set; }

        [JsonPropertyName("insc_est")]
        public string Insc_est { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("referencia")]
        public string Referencia { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("telefone1")]
        public string Telefone1 { get; set; }

        [JsonPropertyName("telefone2")]
        public string Telefone2 { get; set; }

        [JsonPropertyName("codproduto")]
        public string Codproduto { get; set; }

        [JsonPropertyName("comprimento")]
        public string Comprimento { get; set; }

        [JsonPropertyName("largura")]
        public string Largura { get; set; }

        [JsonPropertyName("altura")]
        public string Altura { get; set; }

        [JsonPropertyName("peso")]
        public string Peso { get; set; }

        [JsonPropertyName("quantidade")]
        public string Quantidade { get; set; }

        [JsonPropertyName("mao_propria")]
        public string Mao_propria { get; set; }

        [JsonPropertyName("nome_mao_propria")]
        public string Nomemaopropria { get; set; }

        [JsonPropertyName("seguro")]
        public string Seguro { get; set; }

        [JsonPropertyName("com_ar")]
        public string Com_ar { get; set; }

        [JsonPropertyName("nrnota")]
        public string Nrnota { get; set; }

        [JsonPropertyName("vlr_entrega")]
        public string Vlrentrega { get; set; }

        [JsonPropertyName("HAWB/House")]
        public string HAWBHouse { get; set; }

        [JsonPropertyName("MAWB/Master")]
        public string MAWB { get; set; }


        [JsonPropertyName("Cód. NCM")]
        public string CodNCM { get; set; }

        [JsonPropertyName("incoterms")]
        public string Incoterms { get; set; }

        [JsonPropertyName("odentregacliente")]
        public string Codentregacliente { get; set; }

        [JsonPropertyName("codgrupoproduto")]
        public string Codgrupoproduto { get; set; }

    }
}
