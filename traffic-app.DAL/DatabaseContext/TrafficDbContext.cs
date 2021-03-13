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
         /*
           dotnet ef --startup-project ../traffic-app.API migrations add passengeruserpost --context TrafficDbContext
           dotnet ef --startup-project ../traffic-app.API database update postmigration --context TrafficDbContext
           dotnet ef --startup-project ../traffic-app.API database update  --context TrafficDbContext
         */ 
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostUser> PostUsers { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<OnTheWayDriverPost> OnTheWayDriverPosts { get; set; }
        public DbSet<OnTheWayPassengerPost> OnTheWayPassengerPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.Entity<User>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<Post>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<PostUser>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<PostImage>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<Message>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<OnTheWayDriverPost>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<OnTheWayPassengerPost>().HasQueryFilter(m => m.DeletedAt == null);
        }
    }
}
