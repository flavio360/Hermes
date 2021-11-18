using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO.Track.Airlinkexpress
{
    [Table("pedidomestre")]
    public  class Pedidomestre
    {
        [Column("clienteid")]
        public string Clienteid { get; set; }

        [Column("produtoid")]
        public string Produtoid { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("documento")]
        public string Documento { get; set; }

        [Column("remrazaosocial")]
        public string Remrazaosocial { get; set; }

        [Column("remendereco")]
        public string Remendereco { get; set; }

        [Column("remmunicipio")]
        public string Remmunicipio { get; set; }

        [Column("desrazaosocial")]
        public string Desrazaosocial { get; set; }

        [Column("desendereco")]
        public string Desendereco { get; set; }

        [Column("desbairro")]
        public string Desbairro { get; set; }

        [Column("desmunicipio")]
        public string Desmunicipio { get; set; }

        [Column("desuf")]
        public string Desuf { get; set; }

        [Column("descep")]
        public string Descep { get; set; }

        [Column("descgc")]
        public string Descgc { get; set; }

        [Column("dtentrada")]
        public string Dtentrada { get; set; }
    }
}
