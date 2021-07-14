using System;
using FirstMVCProject.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCProject
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options):base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new CompanyMapping());
        }
    }
}
