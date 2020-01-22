using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GamesProcess.Data;

namespace GamesProcess.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20200122180509_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GamesProcess.Models.Event", b =>
                {
                    b.Property<int>("EventID");

                    b.Property<DateTime>("Date");

                    b.Property<int>("EventNumber");

                    b.Property<int>("GameID");

                    b.Property<string>("MachineValues");

                    b.Property<string>("WinningValues");

                    b.HasKey("EventID");

                    b.HasIndex("GameID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("GamesProcess.Models.Game", b =>
                {
                    b.Property<int>("ID");

                    b.Property<int>("GamesClassID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("GamesClassID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GamesProcess.Models.GamesClass", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.ToTable("GamesClass");
                });

            modelBuilder.Entity("GamesProcess.Models.Event", b =>
                {
                    b.HasOne("GamesProcess.Models.Game")
                        .WithMany("Events")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamesProcess.Models.Game", b =>
                {
                    b.HasOne("GamesProcess.Models.GamesClass")
                        .WithMany("Games")
                        .HasForeignKey("GamesClassID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
