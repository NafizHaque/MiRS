﻿// <auto-generated />
using System;
using MiRs.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    [DbContext(typeof(RuneHunterDbContext))]
    partial class RuneHunterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<decimal>("GuildId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GuildEvents");
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

                    b.Property<decimal>("GuildId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GuildTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryLevelProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryProgressId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryProgressId");

                    b.HasIndex("LevelId");

                    b.ToTable("GuildTeamCategoryLevelProgress");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("GuildEventTeamId")
                        .HasColumnType("int");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GuildEventTeamId");

                    b.ToTable("GuildTeamCategoryProgress");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamLevelTaskProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryLevelProcessId")
                        .HasColumnType("int");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("LevelTaskId")
                        .HasColumnType("int");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryLevelProcessId");

                    b.HasIndex("LevelTaskId");

                    b.ToTable("GuildTeamLevelTaskProgress");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUser", b =>
                {
                    b.Property<decimal>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("UserId"));

                    b.Property<string>("PreviousUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUserToTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<decimal>("UserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("UserToTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Levelnumber")
                        .HasColumnType("int");

                    b.Property<int>("Unlock")
                        .HasColumnType("int");

                    b.Property<string>("UnlockDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Level");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.LevelTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Goal")
                        .HasColumnType("int");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("LevelTasks");
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

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryLevelProgress", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryProgress", "CategoryProgress")
                        .WithMany("CategoryLevelProcess")
                        .HasForeignKey("CategoryProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiRs.Domain.Entities.RuneHunterData.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CategoryProgress");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryProgress", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunterData.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MiRs.Domain.Entities.RuneHunter.GuildEventTeam", "GuildEventTeam")
                        .WithMany("CategoryProgresses")
                        .HasForeignKey("GuildEventTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("GuildEventTeam");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamLevelTaskProgress", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryLevelProgress", "CategoryLevelProgress")
                        .WithMany("LevelTaskProgress")
                        .HasForeignKey("CategoryLevelProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiRs.Domain.Entities.RuneHunterData.LevelTask", "LevelTask")
                        .WithMany()
                        .HasForeignKey("LevelTaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CategoryLevelProgress");

                    b.Navigation("LevelTask");
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

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.Level", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunterData.Category", "Category")
                        .WithMany("Level")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.LevelTask", b =>
                {
                    b.HasOne("MiRs.Domain.Entities.RuneHunterData.Level", "LevelParent")
                        .WithMany("LevelTasks")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LevelParent");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildEvent", b =>
                {
                    b.Navigation("EventTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildEventTeam", b =>
                {
                    b.Navigation("CategoryProgresses");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeam", b =>
                {
                    b.Navigation("EventTeams");

                    b.Navigation("UsersInTeam");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryLevelProgress", b =>
                {
                    b.Navigation("LevelTaskProgress");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.GuildTeamCategoryProgress", b =>
                {
                    b.Navigation("CategoryLevelProcess");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunter.RHUser", b =>
                {
                    b.Navigation("UserToTeams");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.Category", b =>
                {
                    b.Navigation("Level");
                });

            modelBuilder.Entity("MiRs.Domain.Entities.RuneHunterData.Level", b =>
                {
                    b.Navigation("LevelTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
