using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using webapi.DataLayer.Models.Cards;

namespace webapi.DataLayer.Models
{
    [Table("PlayerState")]
    public class PlayerState
    {
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("UserID")]
        public int UserID { get; set; }

        //[JsonIgnore]
        public User User { get; set; }

        public  int TurnOrder { get; set; }

        [Column("MageID")]
        public int MageID { get; set; }

        [JsonIgnore]
        public Mage Mage { get; set; }
    
        [Column("GameID")]
        public int GameID { get; set; }

        [JsonIgnore]
        public Deck Deck { get; set; }

        public int DeckID { get; set; }

        [Column("ManaPoints")]
        public int ManaPoints { get; set; }

        [Column("HealthPoints")]
        public int HealthPoints { get; set; }
    }
}