using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FamousIslands.Dtos;
using FamousIslands.Models;
using FamousIslands.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamousIslands.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class IslandController : ControllerBase
    {
        private IFamousIslandsRepository _famousIslandsRepository;
        public IslandController(IFamousIslandsRepository famousIslandsRepository)
        {
            _famousIslandsRepository = famousIslandsRepository;
        }

        [HttpGet("{countryId}/Island")]
        public IActionResult GetIslands(int countryId)
        {
            if (!_famousIslandsRepository.CountryExist(countryId))
                return NotFound();
            var Islands = _famousIslandsRepository.GetIslands(countryId);
            var IslandsDto = Mapper.Map<IEnumerable<IslandDto>>(Islands);

            return Ok(IslandsDto);
        }

        //[HttpGet("{countryId}/Island")]
        //public IActionResult GetIslands(int countryId)
        //{
        //    if (! _famousIslandsRepository.CountryExist(countryId))
        //        return NotFound();

        //    var islands = _famousIslandsRepository.GetIslands(countryId);
        //    var islandsDto = new List<IslandDto>();

        //    foreach (var island in islands)
        //    {
        //        islandsDto.Add(new IslandDto() {                 
        //            Id = island.Id,
        //            Name = island.Name,
        //            Description = island.Description
        //        });
        //    }

        //    return Ok(islandsDto);           
        //}

        [HttpGet("{countryId}/Island/{id}", Name ="GetIsland")]
        public IActionResult GetIsland(int countryId, int id)
        {
            if (!_famousIslandsRepository.CountryExist(countryId))
                return NotFound();

            var island = _famousIslandsRepository.GetIsland(countryId, id);

            if (island == null)
                return NotFound();

            var islandDto = Mapper.Map<IslandDto>(island);

            return Ok(islandDto);
        }

        //[HttpGet("{countryId}/Island/{id}", Name ="GetIsland")]
        //public IActionResult GetIsland(int countryId, int id)
        //{
        //    if (!_famousIslandsRepository.CountryExist(countryId))
        //        return NotFound();

        //    var island = _famousIslandsRepository.GetIsland(countryId, id);

        //    if (island == null)
        //        return NotFound();

        //    var islandDto = new IslandDto()
        //    {
        //        Id = island.Id,
        //        Name = island.Name,
        //        Description = island.Description
        //    };

        //    return Ok(islandDto);
        //} 
        
        [HttpPost("{countryId}/Island")]
        public IActionResult PostIsland(int countryId, [FromBody]IslandWithoutIdDto islandDto)
        {
            if (!_famousIslandsRepository.CountryExist(countryId))
                return NotFound();

            if (islandDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();


            var island = Mapper.Map<Island>(islandDto);

            _famousIslandsRepository.AddIsland(countryId, island);

            if (!_famousIslandsRepository.Save())
                return StatusCode(500, "An error happened while handling your request.");

            var createdIsland = Mapper.Map<IslandDto>(island);
            return CreatedAtRoute("GetIsland", new { id = createdIsland.Id }, createdIsland );            
        }

        [HttpPut("{countryId}/Island/{id}")]
        public IActionResult PutIsland(int countryId, int id, [FromBody]IslandDto islandDto)
        {
            if (!_famousIslandsRepository.CountryExist(countryId))
                return NotFound();

            if (islandDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var island = _famousIslandsRepository.GetIsland(countryId, id);

            if (island == null)
                return NotFound();

            Mapper.Map(islandDto, island);

            if (!_famousIslandsRepository.Save())
                return StatusCode(500, "An error happened while handling your request.");

            return NoContent();
        }

        [HttpDelete("{countryId}/Island/{id}")]
        public IActionResult DeleteIsland(int countryId, int id)
        {
            if (!_famousIslandsRepository.CountryExist(countryId))
                return NotFound();

            var island = _famousIslandsRepository.GetIsland(countryId, id);

            if (island == null)
                return NotFound();

            _famousIslandsRepository.DeleteIsland(island);

            if (!_famousIslandsRepository.Save())
                return StatusCode(500, "An error happened while handling your request.");

            return NoContent();
        }
    }
}