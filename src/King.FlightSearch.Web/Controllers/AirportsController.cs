using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using King.FlightSearch.Services;
using King.FlightSearch.Web.Models;

namespace King.FlightSearch.Web.Controllers
{
    public class AirportsController : ApiController
    {
        private FlightService _flightService;

        public AirportsController(FlightService flightService)
        {
            this._flightService = flightService;
        }

        [HttpGet]
        [Route("api/airports/suggest/{term}")]
        public List<SuggestItem> AirportsSuggest(string term)
        {
            return this._flightService.GetAirports(term).Data
                .Select(p => new SuggestItem()
                {
                    label = $"{p.Name} ({p.IATA})",
                    value = p.Id.ToString()
                })
                .ToList();
        }
    }
}
