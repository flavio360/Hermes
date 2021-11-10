using Dapper;
using Hermes.ADO;
using Hermes.APP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO
{
    public class RecordOrderSended
    {
        public static void RecordOrderSended_(List<Tracking> order)
        {
            //ALTERAR PARA UPDATE
            var query = "insert into OrderSended  values (@IdHarpiaCodigo)";
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ConnectionString))
                {
                    db.Open();
                    foreach (var item in order)
                    {
                        db.Execute(query, new { IdHarpiaCodigo = item.Codigo});
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                RecordLog.ErrorLogRecording(ex.ToString());
            }
        }
    }
}
