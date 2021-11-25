using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.BLL.API
{

    public static class InterLogConfig
    {
        //private static string urlBase = @"https://www.sicloweb.com.br/api/v1/delivery/add";
        private static string _urlBase = @"https://www.sicloweb.com.br/api/v1/delivery/remessas/add";
        private static string _token = "3806734b256c27e41ec2c6bffa26d9e7";
        private static string _codgrupoproduto = "12";

        public static string urlBase
        {
            get
            {
                return _urlBase;
            }
        }

        public static string token
        {
            get
            {
                return _token;
            }
        }

        public static string codgrupoproduto
        {
            get
            {
                return _codgrupoproduto;
            }
        }
    }
}
