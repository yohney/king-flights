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
    public class FlightEntry : EntityBase
    {
        [ForeignKey("ItineraryEntry")]
        public Guid ItineraryEntryId { get; set; }
        public ItineraryEntry ItineraryEntry { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int StopsCount { get; set; }
        public FlightType Type { get; set; }
    }
}
