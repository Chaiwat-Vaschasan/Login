using System;
using Login.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Login.Database
{

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }

        public DbSet<Movie> Movie {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>().HasKey(a => a.AccountID);
            builder.Entity<Account>().HasIndex(a => a.UserName).IsUnique();
            builder.Entity<Account>().HasIndex(a => a.Password).IsUnique();
            builder.Entity<Account>().HasIndex(a => a.Email).IsUnique();
            builder.Entity<Movie>().HasKey(m => m.MovieID);
        }
    }
}