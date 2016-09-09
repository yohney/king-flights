using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.Services.External.Airports;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace King.FlightSearch.Services.External.Tests
{
    [TestClass]
    public class AirportApiServiceTests
    {
        [TestMethod]
        public void TestFetchAirports()
        {
            var service = new AirportApiService();
            var apiResponse = service.GetAirports();
            Assert.IsTrue(apiResponse.IsSuccess);

            var airports = apiResponse.Data.response;
            Assert.IsNotNull(airports);
            Assert.IsTrue(airports.Count > 5000);
            Assert.IsTrue(airports.All(p => !string.IsNullOrWhiteSpace(p.code)));
            Assert.IsTrue(airports.All(p => !string.IsNullOrWhiteSpace(p.country_code)));
            Assert.IsTrue(airports.All(p => !string.IsNullOrWhiteSpace(p.name)));
        }
    }
}
