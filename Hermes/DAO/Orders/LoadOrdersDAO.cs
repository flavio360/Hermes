using Dapper;
using Hermes.APP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace Hermes.DTO.API
{
    public static class LoadOrders
    {

        public static List<Order> LoadOrdersSend() 
        {

            List<Order> objsOrders = new List<Order>();
            var dataCorte = "'2021-11-10'";


            var query = "SELECT " +
                        "	A.Codigo as Pedido,A.DestNombre AS Destinatario,A.DestAddress AS Endereco,A.DestComplemento AS Complemento,DestTelephone AS Telefone, A.DestCep AS Cep,C.Name AS UF, " +
                        "	D.[Description] AS Cidade,A.RemCep AS Ceporigem, A.DestDocumento AS Cpf_cnpj, A.DestEmail AS Email, A.Incoterm AS Incoterms, A.TotalPackageQuantity AS Quantidade,A.TotalWeight AS Peso, " +
                        "	a.CommercialValueTotal AS Vlrentrega " +
                        "FROM " +
                        "	HarpiaHouse A " +
                        "LEFT JOIN " +
                        "	OrderSended B ON A.Codigo = B.HarpiaCodigo " +
                        "LEFT JOIN " +
                        "	HarpiaState C ON A.DestEstado = C.Id " +
                        "LEFT JOIN " +
                        "	HarpiaMunicipality D ON A.DestMunicipio = D.Id " +
                        "WHERE " +
                        "	B.HarpiaCodigo IS NULL AND " +
                        "   A.CreatedDate >" + dataCorte + 
                        "ORDER BY " +
                        "	A.Codigo";

            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
                {
                    db.Open();

                   
                    IEnumerable retorno  = db.Query<Order>(query);

                    db.Close();

                    foreach (dynamic item in retorno)
                    {
                        objsOrders.Add(new Order()
                        {
                            Token = string.Empty,
                            Destinatario = item.Destinatario,
                            Endereco = item.Endereco,
                            Numero = string.Empty,
                            Complemento = item.Complemento,
                            Bairro = string.Empty,
                            Cidade = item.Cidade,
                            Celular = string.Empty,
                            Cep = item.Cep,
                            UF = item.UF,
                            Ceporigem = item.Ceporigem,
                            Cpf_cnpj = item.Cpf_cnpj,
                            Inscest = string.Empty,
                            Email = item.Email,
                            Referencia = string.Empty,
                            Telefone = item.Telefone,
                            Telefone1 = string.Empty,
                            Telefone2 = string.Empty,
                            Codproduto = "1",
                            Comprimento = string.Empty,
                            Largura = string.Empty,
                            Altura = string.Empty,
                            Peso = item.Peso,
                            Quantidade = item.Quantidade,
                            Mao_propria = "0",
                            Nomemaopropria = string.Empty,
                            Seguro = "0",
                            Com_ar = "1",
                            Nrnota = string.Empty,
                            Pedido = item.Pedido,
                            Vlrentrega = item.Vlrentrega,
                            HAWBHouse = string.Empty,
                            MAWB = string.Empty,
                            CodNCM = string.Empty,
                            Incoterms = item.Incoterms
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.ErrorLogRecording(ex.ToString());
            }  
             return objsOrders ;
        }

        public static void RecordSendedOrder(string order, string delivery)
        {
            try
            {
                var dataEnvio = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");                

                var query = "insert into OrderSended values(@HarpiaCodigo,@DataEnvio,@Delivery)";
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
                {
                    db.Open();

                    db.Execute(query, new { HarpiaCodigo = order , DataEnvio = dataEnvio, Delivery = delivery });

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
