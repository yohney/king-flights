using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King.FlightSearch.DAL.Entities
{
    public class Airport : EntityBase
    {
        public string IATA { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }
}
