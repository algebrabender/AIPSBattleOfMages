using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.DataLayer.Models
{
    [Table("Mage")]
    public class Mage 
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Type")]
        public string Type { get; set; }

        
        public virtual List<PlayerState> PlayerStates { get; set; }

    }

}