using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DataLayer.Models.Cards
{
    public class Card
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
    }
}