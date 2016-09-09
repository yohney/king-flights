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
    public class ItineraryEntryRepository : RepositoryBase<ItineraryEntry, ItineraryEntryDTO>
    {
        public ItineraryEntryRepository(FlightDbContext dbContext, ItineraryEntryMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
