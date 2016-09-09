using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.DAL.Shared;
using King.FlightSearch.Services.External.Airports;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace King.FlightSearch.Services.External.Tests
{
    [TestClass]
    public class FlightApiServiceTests
    {
        [TestMethod]
        public void TestFetchFlights_ZAG_SVQ()
        {
            var searchModel = new FlightApiRequestModel();
            searchModel.Adults = 2;
            searchModel.AirportFromCode = "ZAG";
            searchModel.AirportToCode = "SVQ";
            searchModel.Currency = Currency.EUR.ToString().ToUpper();
            searchModel.FormattedDepartureDate = "2016-11-01";
            searchModel.FormattedReturnDate = "2016-11-08";

            var service = new FlightApiService();
            var apiResponse = service.FindFlights(searchModel);
            Assert.IsTrue(apiResponse.IsSuccess);

            Assert.IsNotNull(apiResponse.Data);
        }

        [TestMethod]
        public void TestFetchFlights_ZAG_ZZZ_BadRequest()
        {
            var searchModel = new FlightApiRequestModel();
            searchModel.Adults = 2;
            searchModel.AirportFromCode = "ZAG";
            searchModel.AirportToCode = "ZZZ";
            searchModel.Currency = Currency.EUR.ToString().ToUpper();
            searchModel.FormattedDepartureDate = "2016-11-01";
            searchModel.FormattedReturnDate = "2016-11-08";

            var service = new FlightApiService();
            var apiResponse = service.FindFlights(searchModel);
            Assert.IsFalse(apiResponse.IsSuccess);
            Assert.IsNull(apiResponse.Data);
        }
    }
}
