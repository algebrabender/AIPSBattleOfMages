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

        [Column("NumOfPlayers")]
        public int NumOfPlayers { get; set; }

        [Column("CreatedGameUserID")]
        public int CreatedGameUserID { get; set; }

        [Column("WinnerUserID")]
        public int WinnerUserID { get; set; }

        [Column("WhoseTurnID")]
        public int WhoseTurnID { get; set; }

        public virtual List<User> Users { get; set; }

        [JsonIgnore]
        public Terrain Terrain { get; set; }
    }

}