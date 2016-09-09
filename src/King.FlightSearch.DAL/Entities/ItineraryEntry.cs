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
    public class ItineraryEntry : EntityBase
    {
        [ForeignKey("SearchEntry")]
        public Guid SearchEntryId { get; set; }
        public SearchEntry SearchEntry { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
