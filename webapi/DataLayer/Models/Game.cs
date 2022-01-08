using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.DataLayer.Models
{
    [Table("Game")]
    public class Game 
    {
        [Column("ID")]
        public int ID { get; set; }

        [Range(1, 4)]
        [Column("NumOfPlayers")]
        public int NumOfPlayers { get; set; }

        [Column("CreatedGameUserID")]
        public int CreatedGameUserID { get; set; }

        [Column("WinnerUserID")]
        public int WinnerUserID { get; set; }

        [Column("WhoseTurnID")]
        public int WhoseTurnID { get; set; }

        public virtual List<PlayerState> PlayerStates { get; set; }

        [JsonIgnore]
        public Terrain Terrain { get; set; }
    }

}