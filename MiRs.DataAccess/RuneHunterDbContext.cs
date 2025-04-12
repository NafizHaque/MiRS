using Microsoft.EntityFrameworkCore;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.RuneHunterData;
using MiRs.Domain.Entities.User;

namespace MiRs.DataAccess
{
    public class RuneHunterDbContext :DbContext
    {
        public RuneHunterDbContext(DbContextOptions<RuneHunterDbContext> options) : base(options)
        {
        }

        public DbSet<RHUser> Users { get; set; }

        public DbSet<RHUserToTeam> UserToTeams { get; set; }

        public DbSet<GuildTeam> GuildTeams{ get; set; }

        public DbSet<GuildEvent> GuildEvents { get; set; }

        public DbSet<GuildEventTeam> GuildEventTeam { get; set; }

        public DbSet<GuildTeamLevelProgress> GuildTeamsLevelProgress { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<LevelTask> LevelTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// Configures the primary key for RHUser.
            /// </summary>
            modelBuilder.Entity<RHUser>()
                .HasKey(u => u.Id);

            /// <summary>
            /// Configures the one-to-many relationship between RHUser and RHUserToTeam.
            /// </summary>
            modelBuilder.Entity<RHUser>()
                .HasMany(u => u.UserToTeams)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId);

            /// <summary>
            /// Configures the primary key for RHUser.
            /// </summary>
            modelBuilder.Entity<RHUserToTeam>()
                .HasKey(ut => ut.Id);

            /// <summary>
            /// Configures the many-to-one relationship from RHUserToTeam to Team.
            /// </summary>
            modelBuilder.Entity<RHUserToTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.UsersInTeam)
                .HasForeignKey(ut => ut.TeamId);

            /// <summary>
            /// Configures the primary key for RHUser.
            /// </summary>
            modelBuilder.Entity<GuildEventTeam>()
                .HasKey(et => et.Id);

            /// <summary>
            /// Configures the one-to-many relationship between GuildEvent and GuildEventTeam.
            /// </summary>
            modelBuilder.Entity<GuildEventTeam>()
                .HasOne(et => et.Event)
                .WithMany(e => e.EventTeams)
                .HasForeignKey(et => et.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configures the one-to-many relationship between Team and GuildEventTeam.
            /// </summary>
            modelBuilder.Entity<GuildEventTeam>()
                .HasOne(et => et.Team)
                .WithMany(t => t.EventTeams)
                .HasForeignKey(et => et.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configures GuildTeamLevelProgress entity and its relationships.
            /// </summary>
            modelBuilder.Entity<GuildTeamLevelProgress>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<GuildTeamLevelProgress>()
                .HasOne(p => p.GuildEventTeam)
                .WithMany(et => et.LevelProgresses)
                .HasForeignKey(p => p.GuildEventTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GuildTeamLevelProgress>()
                .HasOne(p => p.Level)
                .WithOne()
                .HasForeignKey<GuildTeamLevelProgress>(p => p.LevelId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configures Categories entity and its relationships.
            /// </summary>
            modelBuilder.Entity<Category>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Category>()
                .HasMany(p => p.LevelTasks)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
