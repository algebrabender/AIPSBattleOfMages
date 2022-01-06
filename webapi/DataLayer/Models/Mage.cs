using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DataLayer.Models
{
    [Table("Mage")]
    public class Mage 
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Type")]
        public string Type { get; set; }

        public virtual List<User> Users { get; set; }

    }

}