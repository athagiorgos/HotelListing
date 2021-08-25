using System.Collections.Generic;

namespace HotelListing.Controllers.Data
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        // in our unit of work we can look for our country and include the list 
        // of hotels
        public virtual IList<Hotel> Hotels { get; set; }
    }
}
