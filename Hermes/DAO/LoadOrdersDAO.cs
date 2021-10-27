using Dapper;
using Hermes.APP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.DTO.API;
using System.ComponentModel;

namespace Hermes.DTO.API
{
    public class LoadOrdersDAO
    {
        private static string token = "3806734b256c27e41ec2c6bffa26d9e7";


        public static List<Order> LoadOrders() 
        {
            List<Order> objsOrders = new List<Order>();
            
            var query = "SELECT TOP 2 " +
                        "	A.Codigo as Pedido,A.DestNombre AS Destinatario,A.DestAddress AS Endereco,A.DestComplemento AS Complemento,DestTelephone AS Telefone, A.DestCep AS Cep,C.Name AS UF, " +
                        "	D.[Description] AS Cidade,A.RemCep AS Ceporigem, A.DestDocumento AS Cpf_cnpj, A.DestEmail AS Email, A.Incoterm AS Incoterms, A.TotalPackageQuantity AS Quantidade,A.TotalWeight AS Peso, " +
                        "	a.CommercialValueTotal AS Vlrentrega " +
                        "FROM " +
                        "	HarpiaHouse A " +
                        "LEFT JOIN " +
                        "	CheckpointSended B ON A.Codigo = B.HarpiaCodigo " +
                        "LEFT JOIN " +
                        "	HarpiaState C ON A.DestEstado = C.Id " +
                        "LEFT JOIN " +
                        "	HarpiaMunicipality D ON A.DestMunicipio = D.Id " +
                        "WHERE " +
                        "	B.Sended IS NULL " +
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
                            Token = token,
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
    }
}
