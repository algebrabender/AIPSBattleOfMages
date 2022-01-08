using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.DataLayer.Models.Cards
{
    public class Deck
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("NumberOfCards")]
        public int NumberOfCards { get; set; }

        public virtual List<CardDeck> Cards { get; set; }

        [JsonIgnore]
        public PlayerState PlayerState { get; set; }
    }
}