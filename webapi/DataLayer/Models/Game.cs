using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.DataLayer.Models
{
    [Table("Game")]
    public class Game 
    {
        [Column("ID")]
        public int ID { get; set; }

        public virtual List<User> Users { get; set; }

        [JsonIgnore]
        public Terrain Terrain { get; set; }
        

    }

}