using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.DAL.Repository;
using King.FlightSearch.DAL.Repository.Dto;
using King.FlightSearch.DAL.Shared;
using King.FlightSearch.Services.External.Airports;

namespace King.FlightSearch.Services
{
    public class FlightSearchModel
    {
        public Guid AirportFromId { get; set; }
        public Guid AirportToId { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }

        public Currency Currency { get; set; }
    }
}
