﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.DataLayer;

namespace webapi.Migrations
{
    [DbContext(typeof(BOMContext))]
    [Migration("20220316135415_V9")]
    partial class V9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("webapi.DataLayer.Models.Cards.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Air")
                        .HasColumnType("int")
                        .HasColumnName("Air");

                    b.Property<int>("Damage")
                        .HasColumnType("int")
                        .HasColumnName("Damage");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<int>("Earth")
                        .HasColumnType("int")
                        .HasColumnName("Earth");

                    b.Property<int>("Fire")
                        .HasColumnType("int")
                        .HasColumnName("Fire");

                    b.Property<int>("Ice")
                        .HasColumnType("int")
                        .HasColumnName("Ice");

                    b.Property<int>("ManaCost")
                        .HasColumnType("int")
                        .HasColumnName("ManaCost");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("ID");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Cards.CardDeck", b =>
                {
                    b.Property<int>("CardID")
                        .HasColumnType("int")
                        .HasColumnName("CardID");

                    b.Property<int>("DeckID")
                        .HasColumnType("int")
                        .HasColumnName("DeckID");

                    b.Property<int>("NumberInDeck")
                        .HasColumnType("int")
                        .HasColumnName("NumberInDeck");

                    b.HasKey("CardID", "DeckID");

                    b.HasIndex("DeckID");

                    b.ToTable("CardDeck");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Cards.Deck", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfCards")
                        .HasColumnType("int")
                        .HasColumnName("NumberOfCards");

                    b.HasKey("ID");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedGameUserID")
                        .HasColumnType("int")
                        .HasColumnName("CreatedGameUserID");

                    b.Property<int>("NumOfPlayers")
                        .HasColumnType("int")
                        .HasColumnName("NumOfPlayers");

                    b.Property<int?>("TerrainID")
                        .HasColumnType("int");

                    b.Property<int>("WhoseTurnID")
                        .HasColumnType("int")
                        .HasColumnName("WhoseTurnID");

                    b.Property<int>("WinnerUserID")
                        .HasColumnType("int")
                        .HasColumnName("WinnerUserID");

                    b.HasKey("ID");

                    b.HasIndex("TerrainID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Mage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("ID");

                    b.ToTable("Mage");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.PlayerState", b =>
                {
                    b.Property<int>("GameID")
                        .HasColumnType("int")
                        .HasColumnName("GameID");

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("DeckID")
                        .HasColumnType("int");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("int")
                        .HasColumnName("HealthPoints");

                    b.Property<int>("ID")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<int>("MageID")
                        .HasColumnType("int")
                        .HasColumnName("MageID");

                    b.Property<int>("ManaPoints")
                        .HasColumnType("int")
                        .HasColumnName("ManaPoints");

                    b.Property<int>("TurnOrder")
                        .HasColumnType("int");

                    b.HasKey("GameID", "UserID");

                    b.HasIndex("DeckID")
                        .IsUnique();

                    b.HasIndex("MageID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("PlayerState");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Terrain", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("ID");

                    b.ToTable("Terrain");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Salt");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Tag");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Username");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Cards.CardDeck", b =>
                {
                    b.HasOne("webapi.DataLayer.Models.Cards.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.DataLayer.Models.Cards.Deck", "Deck")
                        .WithMany("Cards")
                        .HasForeignKey("DeckID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Game", b =>
                {
                    b.HasOne("webapi.DataLayer.Models.Terrain", "Terrain")
                        .WithMany("Games")
                        .HasForeignKey("TerrainID");

                    b.Navigation("Terrain");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.PlayerState", b =>
                {
                    b.HasOne("webapi.DataLayer.Models.Cards.Deck", "Deck")
                        .WithOne("PlayerState")
                        .HasForeignKey("webapi.DataLayer.Models.PlayerState", "DeckID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.DataLayer.Models.Game", null)
                        .WithMany("PlayerStates")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.DataLayer.Models.Mage", "Mage")
                        .WithMany("PlayerStates")
                        .HasForeignKey("MageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.DataLayer.Models.User", "User")
                        .WithOne("PlayerState")
                        .HasForeignKey("webapi.DataLayer.Models.PlayerState", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");

                    b.Navigation("Mage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Cards.Deck", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("PlayerState");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Game", b =>
                {
                    b.Navigation("PlayerStates");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Mage", b =>
                {
                    b.Navigation("PlayerStates");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.Terrain", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("webapi.DataLayer.Models.User", b =>
                {
                    b.Navigation("PlayerState");
                });
#pragma warning restore 612, 618
        }
    }
}
