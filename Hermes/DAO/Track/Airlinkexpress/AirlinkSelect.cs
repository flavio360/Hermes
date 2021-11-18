using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using Npgsql;

using System.Data;

namespace Hermes.DAO.Track.AirLink
{
    public class AirlinkSelect
    {
        private static string queryAir = "select * from  type_checkpoint;";
        public IEnumerable SelectAirlink()
        {
            IEnumerable tracking;
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=adm#56@dba;Database=airlinkexpress");

                conn.Open();

                NpgsqlCommand command = new NpgsqlCommand("select code from type_checkpoint", conn);

                tracking = conn.Query<LoadTracking>(queryAir);

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tracking;
        }
    }
}
