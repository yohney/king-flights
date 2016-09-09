using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using King.FlightSearch.DAL.Repository;
using King.FlightSearch.DAL.Repository.Dto;
using King.FlightSearch.DAL.Shared;
using King.FlightSearch.Services.External.Airports;
using King.FlightSearch.Services.External.Amadeus;
using King.FlightSearch.Services.Infrastructure;

namespace King.FlightSearch.Services
{
    public class FlightService : ServiceBase
    {
        private AirportRepository _airportRepository;
        private FlightEntryRepository _flightRepository;
        private AirportApiService _airportApiService;
        private FlightApiService _flightApiService;
        private SearchEntryRepository _searchEntryRepository;
        private ItineraryEntryRepository _itineraryEntryRepository;

        public FlightService(
            AirportRepository airportRepository,
            FlightEntryRepository flightRepository,
            AirportApiService airportApiService,
            FlightApiService flightApiService,
            SearchEntryRepository searchEntryRepository, ItineraryEntryRepository itineraryEntryRepository)
        {
            this._itineraryEntryRepository = itineraryEntryRepository;
            this._searchEntryRepository = searchEntryRepository;
            this._flightApiService = flightApiService;
            this._airportApiService = airportApiService;
            this._flightRepository = flightRepository;
            this._airportRepository = airportRepository;

        }

        public ServiceResult<List<AirportDTO>> GetAirports(string query)
        {
            return this.Ok(this._airportRepository.FetchForAutocomplete(query));
        }

        public ServiceResult SyncAirports()
        {
            var apiResponse = this._airportApiService.GetAirports();
            if (!apiResponse.IsSuccess)
                return this.Error(apiResponse.ResponseMessage);

            var allAirports = apiResponse.Data.response
                .Select(p => new AirportDTO()
                {
                    CountryCode = p.country_code,
                    IATA = p.code,
                    Name = p.name
                })
                .ToList();

            foreach (var a in allAirports)
                this._airportRepository.InsertOrUpdate(a, autoSave: false);

            this._airportRepository.Save();

            return this.Ok();
        }

        public ServiceResult<List<FlightEntryDTO>> Search(FlightSearchModel model)
        {
            var searchHash = model.AirportFromId + "." +
                model.AirportToId + "." +
                model.DepartureDate.ToShortDateString() + "." +
                model.ReturnDate?.ToShortDateString() + "." +
                model.AdultsCount + "." +
                model.ChildrenCount + "." +
                model.Currency;

            var cachedResults = this._flightRepository.GetCached(searchHash);

            if (cachedResults.Count > 0)
                return this.Ok(cachedResults);

            var requestModel = new FlightApiRequestModel();
            requestModel.AirportFromCode = this._airportRepository.GetAirportCode(model.AirportFromId);
            requestModel.AirportToCode = this._airportRepository.GetAirportCode(model.AirportToId);
            requestModel.Adults = model.AdultsCount;
            requestModel.Children = model.ChildrenCount;
            requestModel.Currency = model.Currency.ToString().ToUpper();
            requestModel.FormattedDepartureDate = model.DepartureDate.ToString("yyyy-MM-dd");
            requestModel.FormattedReturnDate = model.ReturnDate?.ToString("yyyy-MM-dd");

            var flightApiResult = this._flightApiService.FindFlights(requestModel);
            if (!flightApiResult.IsSuccess)
                return this.Error<List<FlightEntryDTO>>(flightApiResult.ResponseMessage);

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var searchEntry = new SearchEntryDTO();
                searchEntry.Id = Guid.NewGuid();
                searchEntry.SearchEntryHash = searchHash;
                searchEntry.AdultsCount = model.AdultsCount;
                searchEntry.ChildrenCount = model.ChildrenCount;
                searchEntry.AirportFromId = model.AirportFromId;
                searchEntry.AirportToId = model.AirportToId;
                searchEntry.DepartureDate = model.DepartureDate;
                searchEntry.ReturnDate = model.ReturnDate;
                searchEntry.Currency = model.Currency;
                this._searchEntryRepository.InsertOrUpdate(searchEntry);

                var usCulture = new CultureInfo("en-us");

                foreach (var fareItem in flightApiResult.Data.results)
                {
                    var price = decimal.Parse(fareItem.fare.total_price, usCulture.NumberFormat);

                    var itineraryEntry = new ItineraryEntryDTO();
                    itineraryEntry.Id = Guid.NewGuid();
                    itineraryEntry.SearchEntryId = searchEntry.Id;
                    itineraryEntry.TotalPrice = price;
                    this._itineraryEntryRepository.InsertOrUpdate(itineraryEntry);

                    foreach (var outbound in fareItem.itineraries.Select(i => i.outbound))
                    {
                        var flightEntry = this.GetFlightEntry(itineraryEntry.Id, outbound.flights, FlightType.Outbound);
                        this._flightRepository.InsertOrUpdate(flightEntry);
                    }

                    foreach (var inbound in fareItem.itineraries.Select(i => i.inbound))
                    {
                        var flightEntry = this.GetFlightEntry(itineraryEntry.Id, inbound.flights, FlightType.Inbound);
                        this._flightRepository.InsertOrUpdate(flightEntry);
                    }
                }
                scope.Complete();
            }

            return this.Search(model);
        }

        private FlightEntryDTO GetFlightEntry(Guid itineraryId, List<Flight> flights, FlightType flightType)
        {
            var flightEntry = new FlightEntryDTO();
            flightEntry.ItineraryEntryId = itineraryId;
            flightEntry.StopsCount = flights.Count - 1;
            flightEntry.DepartureTime = flights.Select(p => DateTime.ParseExact(p.departs_at, "yyyy-MM-ddTHH:mm", null)).Min();
            flightEntry.ArrivalTime = flights.Select(p => DateTime.ParseExact(p.departs_at, "yyyy-MM-ddTHH:mm", null)).Max();
            flightEntry.Type = flightType;
            return flightEntry;
        }
    }
}