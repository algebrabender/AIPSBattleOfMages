using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DataLayer.Models.Cards
{
    [Table("CardDeck")]
    public class CardDeck
    {
        [Column("CardID")]
        public int CardID { get; set; }

        [Column("Card")]
        public Card Card { get; set; }

        [Column("DeckID")]
        public int DeckID { get; set; }


        [Column("Deck")]
        public Deck Deck { get; set; }
        
    }
}