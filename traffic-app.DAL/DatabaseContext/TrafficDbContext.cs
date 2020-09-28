using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.DatabaseContext
{
    public class TrafficDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TrafficDbContext(DbContextOptions<TrafficDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(m => m.DeletedAt != null );
        }
    }
}
