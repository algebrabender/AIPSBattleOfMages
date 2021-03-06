using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.DataLayer.Models.Cards
{
    [Table("Card")]
    public class Card
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Title")]
        public string Title { get; set;}

        [Column("Description")]
        public string Description { get; set;}

        [Column("Type")]
        public string Type {get; set; }

        [Column("ManaCost")]
        public int ManaCost { get; set; }

        [Column("Damage")]
        public int Damage {get; set; }

        [Column("Fire")]
        public int Fire { get; set; }

        [Column("Ice")]
        public int Ice { get; set; }

        [Column("Air")]
        public int Air { get; set; }

        [Column("Earth")]
        public int Earth { get; set; }
    }
}