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
    public class SearchEntryRepository : RepositoryBase<SearchEntry, SearchEntryDTO>
    {
        public SearchEntryRepository(FlightDbContext dbContext, SearchEntryMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
