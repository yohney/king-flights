using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King.FlightSearch.Services.External.Airports
{
    public class FlightApiRequestModel
    {
        public string AirportFromCode { get; set; }
        public string AirportToCode { get; set; }

        /// <summary>
        /// In format yyyy-MM-dd
        /// </summary>
        public string FormattedDepartureDate { get; set; }

        /// <summary>
        /// In format yyyy-MM-dd
        /// </summary>
        public string FormattedReturnDate { get; set; }

        public int Adults { get; set; }
        public int Children { get; set; }

        public string Currency { get; set; }
    }
}
