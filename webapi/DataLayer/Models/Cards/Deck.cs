using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DataLayer.Models.Cards
{
    public class Deck
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("NumberOfCards")]
        public int NumberOfCards { get; set; }

        public virtual List<Card> Cards { get; set; }
    }
}