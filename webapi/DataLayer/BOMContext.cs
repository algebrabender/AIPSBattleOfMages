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

        public DbSet<PlayerState> PlayerStates { get; set; }

        public BOMContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardDeck>().HasKey(x => new { x.CardID, x.DeckID});

            modelBuilder.Entity<CardDeck>()
                .HasOne(cd => cd.Deck)
                .WithMany(d => d.Cards)
                .HasForeignKey(cd => cd.DeckID);

            modelBuilder.Entity<User>()
                .HasOne<PlayerState>(u => u.PlayerState)
                .WithOne(p => p.User)
                .HasForeignKey<PlayerState>(d => d.UserID);

            modelBuilder.Entity<Deck>()
                .HasOne<PlayerState>(d => d.PlayerState)
                .WithOne(p => p.Deck)
                .HasForeignKey<PlayerState>(p => p.DeckID);

            modelBuilder.Entity<PlayerState>()
                .HasKey(nameof(PlayerState.GameID), nameof(PlayerState.UserID));

        }
    }
}