using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grader.DAL.Db;
using King.FlightSearch.DAL.Entities;
using King.FlightSearch.DAL.Repository.Dto;

namespace King.FlightSearch.DAL.Repository
{
    public class FlightEntryRepository : RepositoryBase<FlightEntry, FlightEntryDTO>
    {
        public FlightEntryRepository(FlightDbContext dbContext, FlightEntryMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public List<FlightEntryDTO> GetCached(string searchHash)
        {
            return this.DbContext.Flights
                .Where(p => p.ItineraryEntry.SearchEntry.SearchEntryHash == searchHash)
                .OrderBy(p => p.ItineraryEntry.TotalPrice)
                .Select(this.Mapper.SelectorExpression)
                .ToList();
        }
    }
}
