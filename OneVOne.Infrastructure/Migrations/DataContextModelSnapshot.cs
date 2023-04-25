﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneVOne.GameService.Infrastructure;

#nullable disable

namespace OneVOne.GameService.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OneVOne.Core.Entities.Game", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("GameTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PlayerOneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerOneStatisticsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("PlayerOneTotalScore")
                        .HasColumnType("tinyint");

                    b.Property<bool>("PlayerOneWin")
                        .HasColumnType("bit");

                    b.Property<Guid>("PlayerTwoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerTwoStatisticsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("PlayerTwoTotalScore")
                        .HasColumnType("tinyint");

                    b.Property<bool>("PlayerTwoWin")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerOneStatisticsId");

                    b.HasIndex("PlayerTwoId");

                    b.HasIndex("PlayerTwoStatisticsId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Person", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.PlayByPlayStatistics", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Block")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Foul")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Rebound")
                        .HasColumnType("tinyint");

                    b.Property<byte>("ScorePoint")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Steal")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("PlayByPlayStatistics");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Player", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte?>("Athleticism")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("Defending")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("InsideScoring")
                        .HasColumnType("tinyint");

                    b.Property<bool?>("IsAttacker")
                        .HasColumnType("bit");

                    b.Property<string>("NbaPlayerPageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("OutsideScoring")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte?>("Playmaking")
                        .HasColumnType("tinyint");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Rebounding")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique()
                        .HasFilter("[PersonId] IS NOT NULL");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.PlayerImage", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique()
                        .HasFilter("[PlayerId] IS NOT NULL");

                    b.ToTable("PlayerImages");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Team", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Game", b =>
                {
                    b.HasOne("OneVOne.Core.Entities.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneVOne.Core.Entities.PlayByPlayStatistics", "PlayerOneStatistics")
                        .WithMany()
                        .HasForeignKey("PlayerOneStatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneVOne.Core.Entities.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneVOne.Core.Entities.PlayByPlayStatistics", "PlayerTwoStatistics")
                        .WithMany()
                        .HasForeignKey("PlayerTwoStatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerOne");

                    b.Navigation("PlayerOneStatistics");

                    b.Navigation("PlayerTwo");

                    b.Navigation("PlayerTwoStatistics");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Player", b =>
                {
                    b.HasOne("OneVOne.Core.Entities.Person", "Person")
                        .WithOne()
                        .HasForeignKey("OneVOne.Core.Entities.Player", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneVOne.Core.Entities.Team", null)
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.PlayerImage", b =>
                {
                    b.HasOne("OneVOne.Core.Entities.Player", null)
                        .WithOne("PlayerImage")
                        .HasForeignKey("OneVOne.Core.Entities.PlayerImage", "PlayerId");
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Player", b =>
                {
                    b.Navigation("PlayerImage")
                        .IsRequired();
                });

            modelBuilder.Entity("OneVOne.Core.Entities.Team", b =>
                {
                    b.Navigation("TeamPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
