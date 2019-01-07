using FamousIslands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousIslands.Dtos
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Continent { get; set; }

        public int IslandCount
        {
            get
            {
                return Islands.Count;
            }
        }

        public ICollection<IslandDto> Islands { get; set; } = new List<IslandDto>();
    }
}
