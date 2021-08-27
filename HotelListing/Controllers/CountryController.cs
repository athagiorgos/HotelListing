using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Controllers.Data;
using HotelListing.Data;

namespace HotelListing.Controllers
{

    // Attribute based mapping
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            try
            {
                var countries = await _unitOfWork.CountryRepository.GetPagedList(requestParams);
                // Mapping the Country objects to a list of CountryDto objects
                var results = _mapper.Map<IList<CountryDto>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }


        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.CountryRepository.Get(q => q.Id == id, new List<string> {"Hotels"});
                // Mapping the Country objects to a list of CountryDto objects
                var result = _mapper.Map<CountryDto>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }


        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDto countryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(countryDto); 
                await _unitOfWork.CountryRepository.Insert(country);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
        
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDto updateCountryDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _unitOfWork.CountryRepository.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(updateCountryDto, country);
                _unitOfWork.CountryRepository.Update(country);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = await _unitOfWork.CountryRepository.Get(q => q.Id == id);
                if (hotel ==  null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.CountryRepository.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
    }
}
