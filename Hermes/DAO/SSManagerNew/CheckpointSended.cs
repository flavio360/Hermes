using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO.SSManager
{
    [Table("CheckpointSended")]
    public class CheckpointSended
    {
        [Column("HarpiaCodigo")]
        public string HarpiaCodigo { get; set; }

        [Column("Code")]
        public string Code { get; set; }

        [Column("Checkpoint_id")]
        public string Checkpoint_id { get; set; }

        [Column("Pedidoid")]
        public string Pedidoid { get; set; }
    }
}
