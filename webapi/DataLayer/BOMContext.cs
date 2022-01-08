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

        public DbSet<CardDeck> CardDeck { get; set; } 

        public DbSet<UserMageGame> UserMageGames { get; set; }

        public BOMContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardDeck>().HasKey(x => new { x.CardID, x.DeckID});

            modelBuilder.Entity<CardDeck>()
                .HasOne(cd => cd.Card)
                .WithMany(c => c.Decks)
                .HasForeignKey(cd => cd.CardID);
            modelBuilder.Entity<CardDeck>()
                .HasOne(cd => cd.Deck)
                .WithMany(d => d.Cards)
                .HasForeignKey(cd => cd.DeckID);

            modelBuilder.Entity<User>()
                .HasOne<Deck>(u => u.Deck)
                .WithOne(d => d.User)
                .HasForeignKey<Deck>(d => d.UserID);

        }
    }
}