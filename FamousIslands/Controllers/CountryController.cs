using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FamousIslands.Dtos;
using FamousIslands.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamousIslands.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase

    {
        private IFamousIslandsRepository _famousIslandsRepository;
        public CountryController(IFamousIslandsRepository famousIslandsRepository)
        {
            _famousIslandsRepository = famousIslandsRepository;
        }

        [HttpGet()]
        public IActionResult GetCountries()
        {
            var countries = _famousIslandsRepository.GetCountries();
            var countriesDto = Mapper.Map<IEnumerable<CountryWithoutIslandDto>>(countries);

            return Ok(countriesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int id, bool includeIsland=false)
        {
            var country = _famousIslandsRepository.GetCountry(id, includeIsland);

            if (country == null)
                return NotFound();

            if (includeIsland)
            {
                var countryDto = Mapper.Map<CountryDto>(country);

                return Ok(countryDto);
            }

            var countryWithIslandDto = Mapper.Map<CountryWithoutIslandDto>(country);
            return Ok(countryWithIslandDto);
        }


        //[HttpGet()]
        //public IActionResult GetCountriesWithoutIsland()
        //{
        //    var countries = _famousIslandsRepository.GetCountries();
        //    var countriesDto = new List<CountryWithoutIslandDto>();

        //    foreach (var country in countries)
        //    {
        //        countriesDto.Add(new CountryWithoutIslandDto
        //        {
        //            Id = country.Id,
        //            Name = country.Name,
        //            Continent = country.Continent,
        //        });

        //    }

        //    return Ok(countriesDto);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetCountry(int id, bool includeIsland = false)
        //{
        //    var country = _famousIslandsRepository.GetCountry(id, includeIsland);

        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    if (includeIsland)
        //    {
        //        var countryDto = new CountryDto()
        //        {
        //            Id = country.Id,
        //            Name = country.Name,
        //            Continent = country.Continent
        //        };

        //        foreach (var island in country.Islands)
        //        {
        //            countryDto.Islands.Add(new IslandDto()
        //            {
        //                Id = island.Id,
        //                Name = island.Name,
        //                Description = island.Description
        //            });
        //        }

        //        return Ok(countryDto);
        //    }
        //    var countryWithoutIslandDto = new CountryWithoutIslandDto()
        //    {
        //        Id = country.Id,
        //        Name = country.Name,
        //        Continent = country.Continent
        //    };
        //    return Ok(countryWithoutIslandDto);
        //}
    }
}