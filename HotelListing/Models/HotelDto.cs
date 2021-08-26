using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CreateHotelDto
    {
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel Name Is Too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Address Name Is Too Long")]
        public string Address { get; set; }

        [Required]
        [Range(1,5)]
        public double Rating { get; set; }

        //[Required]
        public int CountryId { get; set; }

    }

    public class UpdateHotelDto : CreateHotelDto
    {
        
    }

    public class HotelDto : CreateHotelDto
    {
        public int Id { get; set; }

        // A DTO should not reference a domain object directly
        // Dtos talk to Dtos, and domain obj to domain obj
        // Automapper is the only thing connecting them
        public CountryDto Country { get; set; }
    }
}
