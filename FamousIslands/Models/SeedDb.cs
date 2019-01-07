using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousIslands.Models
{
    public static class SeedDb
    {
        public static void SeedDatabase(this ApplicationDbContext context)
        {
            if (context.Countries.Any())
            {
                return;
            }

            // init seed data
            var countries = new List<Country>()
            {
                new Country()
                {
                     Name = "Thailand",
                     Continent = "Asia",
                     Islands = new List<Island>()
                     {
                         new Island() {
                             Name = "KOH SAMUI",
                             Description = "Best island in Thailand for families and first-timers, honeymooners and yoga bunnies."
                         },
                          new Island() {
                             Name = "KOH TAO",
                             Description = "Best island in Thailand for Scuba diving."
                          },
                            new Island() {
                             Name = "KOH PHA NGAN",
                             Description = "Best island in Thailand for: Hedonists and hippies."
                          },
                     }
                },
                new Country()
                {
                     Name = "Greece",
                     Continent = "Europa",
                     Islands = new List<Island>()
                     {
                         new Island() {
                             Name = "Santorini",
                             Description = "The best beaches: Red Beach, named after the unique lava colored sand, White Beach, Kamari and Perissa."
                         },
                          new Island() {
                             Name = "Mykonos",
                             Description = "Mykonos is the best choice for those who want to party."
                          },
                     }
                },
                new Country()
                {
                     Name = "Maldives",
                     Continent = "Asia",
                     Islands = new List<Island>()
                     {
                         new Island() {
                             Name = "Malé",
                             Description = "Malé is the best island in Maldives for luxury-seeker honeymoon couples. "
                         },
                          new Island() {
                             Name = "Biyadhoo",
                             Description = "Its sparkling waters and wide range of watersports are what make the island so popular."
                          },
                     }
                }


            };

            context.Countries.AddRange(countries);
            context.SaveChanges();  //executes statements
        }
    }
}
