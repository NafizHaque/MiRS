using Microsoft.EntityFrameworkCore;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.RuneHunterData;

namespace MiRs.DataAccess
{
    public class RuneHunterDbContext : DbContext
    {
        public RuneHunterDbContext(DbContextOptions<RuneHunterDbContext> options) : base(options)
        {
        }

        public DbSet<RHUser> Users { get; set; }

        public DbSet<RHUserToTeam> UserToTeams { get; set; }

        public DbSet<GuildTeam> GuildTeams { get; set; }

        public DbSet<GuildEvent> GuildEvents { get; set; }

        public DbSet<GuildEventTeam> GuildEventTeam { get; set; }

        public DbSet<GuildTeamCategoryProgress> GuildTeamCategoryProgress { get; set; }

        public DbSet<GuildTeamCategoryLevelProgress> GuildTeamCategoryLevelProgress { get; set; }

        public DbSet<GuildTeamLevelTaskProgress> GuildTeamLevelTaskProgress { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<LevelTask> LevelTasks { get; set; }

        public DbSet<RHUserRawLoot> UserRawLoot { get; set; }

        public DbSet<RunescapeLootAlias> RunescapeLootAlias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /// <summary>
            /// Configures the primary key for RHUserRawLoot.
            /// </summary>
            modelBuilder.Entity<RHUserRawLoot>()
                .HasKey(u => u.Id);

            /// <summary>
            /// Configures the primary key for RHUser.
            /// </summary>
            modelBuilder.Entity<RHUser>()
                .HasKey(u => u.UserId);

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
            /// Configures GuildTeamCategoryProgress entity and its relationships.
            /// </summary>
            modelBuilder.Entity<GuildTeamCategoryProgress>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<GuildTeamCategoryProgress>()
                .HasOne(p => p.GuildEventTeam)
                .WithMany(l => l.CategoryProgresses)
                .HasForeignKey(p => p.GuildEventTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GuildTeamCategoryProgress>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            /// <summary>
            /// Configures GuildTeamCategoryProgress entity and its relationships.
            /// </summary>
            modelBuilder.Entity<GuildTeamCategoryLevelProgress>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<GuildTeamCategoryLevelProgress>()
                .HasOne(p => p.CategoryProgress)
                .WithMany(p => p.CategoryLevelProcess)
                .HasForeignKey(p => p.CategoryProgressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GuildTeamCategoryLevelProgress>()
                .HasOne(p => p.Level)
                .WithMany()
                .HasForeignKey(p => p.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GuildTeamCategoryLevelProgress>()
                .HasMany(p => p.LevelTaskProgress)
                .WithOne(p => p.CategoryLevelProgress)
                .HasForeignKey(p => p.CategoryLevelProcessId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configures GuildTeamLevelTaskProgress entity and its relationships.
            /// </summary>
            modelBuilder.Entity<GuildTeamLevelTaskProgress>()
                .HasOne(p => p.LevelTask)
                .WithMany()
                .HasForeignKey(p => p.LevelTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            /// <summary>
            /// Configures Categories entity and its relationships.
            /// </summary>
            modelBuilder.Entity<Category>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Category>()
                .HasMany(p => p.Level)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configures level entity and its relationships.
            /// </summary>
            modelBuilder.Entity<Level>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Level>()
                .HasMany(p => p.LevelTasks)
                .WithOne(p => p.LevelParent)
                .HasForeignKey(p => p.LevelId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configures the primary key for RunescapeLootAlias.
            /// </summary>
            modelBuilder.Entity<RunescapeLootAlias>()
                .HasKey(u => u.Id);
        }
    }
}
