using System;
using System.Collections.Generic;
using System.Text;
using Meet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public
        ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<CarMeet> CarMeets { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
           new IdentityRole
           {
               Name = "Car Guy",
               NormalizedName = "CARGUY"
           }
            );
        }
    }
}
