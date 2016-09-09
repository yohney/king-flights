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
    public class AirportRepository : RepositoryBase<Airport, AirportDTO>
    {
        public AirportRepository(FlightDbContext dbContext, AirportMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public List<AirportDTO> FetchForAutocomplete(string query)
        {
            return this.DbContext.Airports
                .Where(p => p.IATA.Contains(query) || p.Name.Contains(query) || p.CountryCode.Contains(query))
                .OrderBy(p => p.Name)
                .Select(this.Mapper.SelectorExpression)
                .ToList();
        }

        public override void InsertOrUpdate(AirportDTO dto, bool autoSave = true)
        {
            var existingAirportId = this.DbContext.Airports
                .Where(p => p.IATA == dto.IATA)
                .Select(p => p.Id)
                .FirstOrDefault();

            dto.Id = existingAirportId;

            base.InsertOrUpdate(dto, autoSave);
        }

        public string GetAirportCode(Guid airportId)
        {
            return this.DbContext.Airports
                .Where(p => p.Id == airportId)
                .Select(p => p.IATA)
                .FirstOrDefault();
        }
    }
}
