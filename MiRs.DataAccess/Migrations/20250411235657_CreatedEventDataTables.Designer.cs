﻿// <auto-generated />
using System;
using MiRs.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    [DbContext(typeof(RuneHunterDbContext))]
    [Migration("20250411235657_CreatedEventDataTables")]
    partial class CreatedEventDataTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EventActive")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("EventEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EventStart")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Eventname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuildId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GuildEvent");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildEventTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("TeamId");

                    b.ToTable("GuildEventTeam");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GuildId")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GuildTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PreviousUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUserToTeam", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("UserToTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildEventTeam", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunter.GuildEvent", "Event")
                        .WithMany("EventTeams")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiRs.Domain.Entities.RuneHunter.GuildTeam", "Team")
                        .WithMany("EventTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUserToTeam", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunter.GuildTeam", "Team")
                        .WithMany("UsersInTeam")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiRs.Domain.Entities.RuneHunter.RHUser", "User")
                        .WithMany("UserToTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildEvent", b =>
                {
                    b.Navigation("EventTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeam", b =>
                {
                    b.Navigation("EventTeams");

                    b.Navigation("UsersInTeam");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUser", b =>
                {
                    b.Navigation("UserToTeams");
                });
#pragma warning restore 612, 618
        }
    }
}
