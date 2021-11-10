using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hermes.DAO.API
{
    public class Authentication
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
