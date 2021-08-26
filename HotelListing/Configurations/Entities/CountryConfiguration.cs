using HotelListing.Controllers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}
