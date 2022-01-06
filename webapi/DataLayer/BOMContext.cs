using Microsoft.EntityFrameworkCore;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;

namespace webapi.DataLayer
{
    public class BOMContext : DbContext
    {

        public DbSet<User> Users { get; set;}
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Terrain> Terrains {get; set; }
        public DbSet<Card> Cards { get; set; }
        public BOMContext(DbContextOptions options) : base(options)
        {

        }
    }
}