using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using webapi.DataLayer.Models.Cards;

namespace webapi.DataLayer.Models
{
    [Table("User")]
    public class User 
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Salt")]
        public string Salt { get; set; }

        [Column("Tag")]
        public string Tag { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        public int MageID { get; set; }
        
        [JsonIgnore]
        public Mage Mage {get; set; }

        public int GameID { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }
        
        [JsonIgnore]
        public Deck Deck { get; set; }

    }

}