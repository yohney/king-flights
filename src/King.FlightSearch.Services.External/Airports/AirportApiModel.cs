using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King.FlightSearch.Services.External.Airports
{
    public class AirportApiItem
    {
        public string code { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
    }

    public class AirportApiResponseModel
    {
        public Request request { get; set; }
        public List<AirportApiItem> response { get; set; }
    }

    public class Key
    {
        public int id { get; set; }
        public string api_key { get; set; }
        public string type { get; set; }
        public object expired { get; set; }
        public string registered { get; set; }
        public int affiliate_account { get; set; }
        public int limits_by_hour { get; set; }
        public int limits_by_minute { get; set; }
        public int usage_by_hour { get; set; }
        public int usage_by_minute { get; set; }
    }

    public class Params
    {
        public string lang { get; set; }
    }

    public class Device
    {
        public string type { get; set; }
    }

    public class Agent
    {
        public string browser { get; set; }
        public string os { get; set; }
        public string platform { get; set; }
    }

    public class Client
    {
        public string country_code { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public double lat { get; set; }
        public int lng { get; set; }
        public string ip { get; set; }
        public Device device { get; set; }
        public Agent agent { get; set; }
    }

    public class Request
    {
        public string lang { get; set; }
        public string currency { get; set; }
        public int time { get; set; }
        public int id { get; set; }
        public string server { get; set; }
        public int pid { get; set; }
        public Key key { get; set; }
        public Params @params { get; set; }
        public int version { get; set; }
        public string method { get; set; }
        public Client client { get; set; }
    }
}
