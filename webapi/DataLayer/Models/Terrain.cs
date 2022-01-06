using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DataLayer.Models
{
    [Table("Terrain")]
    public class Terrain 
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Type")]
        public string Type { get; set; }

        public virtual List<Game> Games {get; set;}

    }

}