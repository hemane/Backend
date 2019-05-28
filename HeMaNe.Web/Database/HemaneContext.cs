using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Database
{
    public class HemaneContext : DbContext
    {
        public HemaneContext(DbContextOptions x) : base(x)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=hemane;Uid=root;"); // ;Pwd=myPassword
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
