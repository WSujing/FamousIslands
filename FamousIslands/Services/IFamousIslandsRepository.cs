using FamousIslands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousIslands.Services
{
    public interface IFamousIslandsRepository
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id, bool includeIsland);
        IEnumerable<Island> GetIslands(int countryId);
        Island GetIsland(int countryId, int id);
        void AddIsland(int countryId, Island island);
        void DeleteIsland(Island island);
        bool CountryExist(int id);
        bool Save();
    }
}
