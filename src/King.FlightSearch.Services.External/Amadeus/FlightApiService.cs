using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.Services.External.Amadeus;
using Newtonsoft.Json;
using RestSharp;

namespace King.FlightSearch.Services.External.Airports
{
    //https://sandbox.amadeus.com/travel-innovation-sandbox/apis/get/flights/low-fare-search
    public class FlightApiService
    {
        private string Key
            => ConfigurationManager.AppSettings["amadeus-api-key"];

        private string Resource
            => ConfigurationManager.AppSettings["amadeus-api-flights-resource"];

        private string BaseUrl
            => ConfigurationManager.AppSettings["amadeus-api-flights-base-url"];

        public ExternalApiResult<FlightApiResult> FindFlights(FlightApiRequestModel searchRequest)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(this.BaseUrl);

            var request = new RestRequest(this.Resource, Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("apikey", this.Key);
            request.AddQueryParameter("origin", searchRequest.AirportFromCode);
            request.AddQueryParameter("destination", searchRequest.AirportToCode);
            request.AddQueryParameter("departure_date", searchRequest.FormattedDepartureDate);
            request.AddQueryParameter("adults", searchRequest.Adults.ToString());
            request.AddQueryParameter("children", searchRequest.Children.ToString());
            request.AddQueryParameter("currency", searchRequest.Currency);

            if (!string.IsNullOrWhiteSpace(searchRequest.FormattedReturnDate))
                request.AddQueryParameter("return_date", searchRequest.FormattedReturnDate);

            var response = client.Execute<FlightApiResult>(request);

            return ExternalApiResult.FromResponse(response);
        }
    }
}
