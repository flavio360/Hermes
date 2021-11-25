using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.DAO.SSManagerNew.Track
{
    [Table("CheckpointTrackingSended")]
    public class CheckpointTrackingSended
    {
        [Column("Id_track")]
        public string Id_track { get; set; }

        [Column("CheckpointSended_ID")]
        public string CheckpointSended_ID { get; set; }

        [Column("CodeStatus")]
        public string CodeStatus { get; set; }

        [Column("HouseId")]
        public string HouseId { get; set; }

        [Column("CheckpointId")]
        public string CheckpointId { get; set; }
    }
}
