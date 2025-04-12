using Microsoft.EntityFrameworkCore;
using MiRs.Domain.Entities.RuneHunter;
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

        public DbSet<GuildEvent> GuildEvent { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RHUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<RHUser>()
                .HasMany(u => u.UserToTeams)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId);


            modelBuilder.Entity<RHUserToTeam>()
                .HasKey(ut => new { ut.UserId, ut.TeamId });

            modelBuilder.Entity<RHUserToTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.UsersInTeam)
                .HasForeignKey(ut => ut.TeamId);

        }
    }
}
