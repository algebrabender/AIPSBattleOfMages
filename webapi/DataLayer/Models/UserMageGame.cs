using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.DataLayer.Models
{
    [Table("UserMageGame")]
    public class UserMageGame
    {
        [Column("UserID")]
        public int UserID { get; set; }

        [Column("MageID")]
        public int MageID { get; set; }
    
        [Column("GameID")]
        public int GameID { get; set; }

        [Column("ManaPoints")]
        public int ManaPoints { get; set; }

        [Column("HealthPoints")]
        public int HealthPoints { get; set; }
    }
}