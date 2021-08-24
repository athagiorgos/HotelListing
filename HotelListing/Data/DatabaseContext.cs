using HotelListing.Configurations.Entities;
using HotelListing.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Controllers.Data
{

    //This class represents the bridge between our application and our database
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {}

        // This property of type DbSet<> is used to define the table to be created based
        // on the object that we pass inside the <>.
        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        // Seeding Data on Startup of the application with Entity Framework Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}


// Everything else for connecting the database such as 
// ConnectionString wiil be defined inside the appsettings.json