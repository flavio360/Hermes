using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Teste
{
    public   class SolicitaDARF
    {
        public string TipoXML { get; set; }
        public string DtHorarioEnvioArquivo { get; set; }
        public string Cnpj { get; set; }
        public string NumeroManifesto { get; set; }
        public string ImpostoImportacao { get; set; }
        public string NumeroRemessa { get; set; }
        public float ValorMultaMora { get; set; }
        public float valorJurosMora { get; set; }
        public float ValorJurosOficio { get; set; }
        public string BaseLegalMulta { get; set; }
    }
}
