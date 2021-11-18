using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Hermes.Teste
{
    public class DARF
    {
        
        public static void GetSolicitacaoDarfByManifesto(string manifesto)
        {
            
            List<SolicitaDARF> objsOrders = new List<SolicitaDARF>();
            var TipoXML = "DARF";
            var DtHorarioEnvioArquivo = DateTime.Now.ToString("yyyy-MM-ddThh:mm");
            var Cnpj = "68661933000163";
            var DtHorario = DateTime.Now.ToString("yyyy-MM-dd");

            using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
            {
                var query = "select top 3  a.Codigo as numeroRemessa ,e.ManifestoNumber as numeroManifesto from HarpiaHouse a inner join	HarpiaBagsDetailHarpiaHouse b on a.Id = b.HarpiaHouseId inner join	HarpiaMasterHarpiaBagsDetail c on c.HarpiaBagsDetailId = b.HarpiaBagsDetailId " +
                            " left join HarpiaMasterCustomsGroup d on d.MasterId = c.HarpiaMasterId left join CustomsGroup e on e.Id = d.GroupId where e.ManifestoNumber is not null";

                db.Open();

                IEnumerable retorno = db.Query<SolicitaDARF>(query);

                db.Close();

                string nManifesto=string.Empty;


                foreach (dynamic item in retorno)
                {
                    nManifesto =  item.NumeroManifesto;
                    break;
                }


                XElement xml = new XElement("solicitacaoDarf",
                               new XElement("transmissao",
                               new XElement("tipoXML", TipoXML),
                               new XElement("dtHorarioEnvioArquivo", DtHorarioEnvioArquivo)),
                               new XElement("declarante",
                               new XElement("empresa",
                               new XElement("cnpj", Cnpj))),
                               new XElement("darf",
                               new XElement("manifestos",
                               new XElement("manifesto",
                               new XElement("numeroManifesto", nManifesto),
                               new XElement("remessas")))));

                foreach (dynamic item in retorno)
                {
                    XElement novoElemento = new XElement("remessa",
                                            new XElement("numeroRemessa", item.NumeroRemessa),
                                            new XElement("impostoImportacao", "S"));

                    xml.Element("darf").Element("manifestos").Element("manifesto").Element("remessas").Add(novoElemento);                   
                }

                xml.Save(@"c:\0XML\1109_SolicitarDarf_" + DtHorario + ".xml");


                //var spath = @"c:\0XML\1109_SolicitarDarf_" + DtHorario + ".xml";


                string TESTE = "";




            

                


            }

        }
    }
}
