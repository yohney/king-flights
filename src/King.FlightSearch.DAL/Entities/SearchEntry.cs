using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.DAL.Shared;

namespace King.FlightSearch.DAL.Entities
{
    public class SearchEntry : EntityBase
    {
        [ForeignKey("AirportFrom")]
        public Guid AirportFromId { get; set; }
        public Airport AirportFrom { get; set; }

        [ForeignKey("AirportTo")]
        public Guid AirportToId { get; set; }
        public Airport AirportTo { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }

        public Currency Currency { get; set; }

        [MaxLength(255)]
        public string SearchEntryHash { get; set; }
    }
}
