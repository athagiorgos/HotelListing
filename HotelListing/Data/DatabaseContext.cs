using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers.Data
{

    //This class represents the bridge between our application and our database
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        // This property of type DbSet<> is used to define the table to be created based
        // on the object that we pass inside the <>.
        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }



        // Seeding Data on Startup of the application with Entity Framework Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Greece",
                    ShortName = "GR"
                },
                new Country
                {
                    Id = 2,
                    Name = "United States of America",
                    ShortName = "US"
                },
                new Country
                {
                    Id = 3,
                    Name = "France",
                    ShortName = "FR"
                }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Cavo Taggoo",
                    Address = "Mykonos",
                    CountryId = 1,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Los Angeles Luxury Hotel",
                    Address = "Los Angeles",
                    CountryId = 2,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hotel Lutetia",
                    Address = "45 Bd Raspail",
                    CountryId = 3,
                    Rating = 4.5
                }
            );
        }
    }
}


// Everything else for connecting the database such as 
// ConnectionString wiil be defined inside the appsettings.json