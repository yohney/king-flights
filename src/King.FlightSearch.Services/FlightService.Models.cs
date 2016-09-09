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
    public class ItineraryModel
    {
        public decimal Price { get; set; }

        public string DepartureAirport
            => this.InboundFlights.Concat(OutboundFlights).First().ItineraryEntrySearchEntryAirportFromName;

        public string ArrivalAirport
            => this.InboundFlights.Concat(OutboundFlights).First().ItineraryEntrySearchEntryAirportToName;

        public string Currency
            => this.InboundFlights.Concat(OutboundFlights).First().ItineraryEntrySearchEntryCurrency.ToString();

        public int Adults
            => this.InboundFlights.Concat(OutboundFlights).First().ItineraryEntrySearchEntryAdultsCount;

        public int Children
            => this.InboundFlights.Concat(OutboundFlights).First().ItineraryEntrySearchEntryChildrenCount;

        public List<FlightEntryDTO> InboundFlights { get; set; }
        public List<FlightEntryDTO> OutboundFlights { get; set; }

        public ItineraryModel()
        {
            this.InboundFlights = new List<FlightEntryDTO>();
            this.OutboundFlights = new List<FlightEntryDTO>();
        }
    }

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
