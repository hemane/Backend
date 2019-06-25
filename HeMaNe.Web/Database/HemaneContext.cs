using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Web.Database.Models;
using HeMaNe.Web.Helpers;
using Microsoft.Extensions.Options;

namespace HeMaNe.Web.Database
{
    public class HemaneContext : DbContext
    {
        private readonly IOptions<AppSettings> _appSettings;

        public HemaneContext(DbContextOptions x, IOptions<AppSettings> appSettings) : base(x)
        {
            _appSettings = appSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(this._appSettings.Value.ConnectionString);
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchTeam> MatchTeams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchTeam>(e =>
            {
                e.HasKey(t => new { t.TeamId, t.MatchId });
                e.HasOne(t => t.Team)
                    .WithMany(t => t.MatchTeams)
                    .HasForeignKey(t => t.TeamId);

                e.HasOne(t => t.Match)
                    .WithMany(t => t.Teams)
                    .HasForeignKey(t => t.MatchId);
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
