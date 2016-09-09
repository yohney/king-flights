using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.Services.External;
using Newtonsoft.Json;
using RestSharp;

namespace King.FlightSearch.Services.External.Airports
{
    // http://iatacodes.org/
    public class AirportApiService
    {
        private string Key
            => ConfigurationManager.AppSettings["iata-api-key"];

        private string BaseUrl
            => ConfigurationManager.AppSettings["iata-api-airport-base-url"];

        private string FullUrl
            => ConfigurationManager.AppSettings["iata-api-airport-resource-url"];

        public ExternalApiResult<AirportApiResponseModel> GetAirports()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(this.BaseUrl);

            var request = new RestRequest(this.FullUrl, Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("api_key", this.Key, ParameterType.QueryString);

            var response = client.Execute<AirportApiResponseModel>(request);
            return ExternalApiResult.FromResponse(response);
        }
    }
}
