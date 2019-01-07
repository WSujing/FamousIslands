using FamousIslands.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousIslands.Services
{
    public class FamousIslandsRepository: IFamousIslandsRepository
    {
        private ApplicationDbContext _context;
        public FamousIslandsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.ToList().OrderBy(c => c.Name);
            //return _context.Countries.Include(c => c.Islands).ToList().OrderBy(c => c.Name);
        }

        public Country GetCountry(int id, bool includeIsland)
        {
            if (includeIsland)
            {
                return _context.Countries.Include(c => c.Islands).Where(c => c.Id == id).FirstOrDefault();
            }

            return _context.Countries.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Island> GetIslands(int countryId)
        {
            return _context.Islands.ToList().Where(i => i.CountryId == countryId);
        }

        public Island GetIsland(int countryId, int id)
        {
            return _context.Islands.Where(i => i.CountryId == countryId && i.Id == id).FirstOrDefault();
        }

        public void AddIsland(int countryId, Island island)
        {
            var country = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
            country.Islands.Add(island);
        }

        public void DeleteIsland(Island island)
        {
            _context.Islands.Remove(island);
        }

        public bool CountryExist(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }

}


