using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.DAL.Repository.Dto.Infrastructure;
using King.FlightSearch.DAL.Entities;
using King.FlightSearch.DAL.Shared;

namespace King.FlightSearch.DAL.Repository.Dto
{
    public class FlightEntryDTO : EntityBaseDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public Guid ItineraryEntryId { get; set; }
        public Guid ItineraryEntrySearchEntryId { get; set; }
        public Guid ItineraryEntrySearchEntryAirportFromId { get; set; }
        public string ItineraryEntrySearchEntryAirportFromIATA { get; set; }
        public string ItineraryEntrySearchEntryAirportFromName { get; set; }
        public Guid ItineraryEntrySearchEntryAirportToId { get; set; }
        public string ItineraryEntrySearchEntryAirportToIATA { get; set; }
        public string ItineraryEntrySearchEntryAirportToName { get; set; }
        public int ItineraryEntrySearchEntryAdultsCount { get; set; }
        public int ItineraryEntrySearchEntryChildrenCount { get; set; }
        public Currency ItineraryEntrySearchEntryCurrency { get; set; }
        public string ItineraryEntrySearchEntrySearchEntryHash { get; set; }
        public decimal ItineraryEntryTotalPrice { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int StopsCount { get; set; }
        public FlightType Type { get; set; }
    }

    public class FlightEntryMapper : MapperBase<FlightEntry, FlightEntryDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        private EntityBaseMapper _entityBaseMapper = new EntityBaseMapper();
        public override Expression<Func<FlightEntry, FlightEntryDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<FlightEntry, FlightEntryDTO>>)(p => new FlightEntryDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ItineraryEntryId = p.ItineraryEntryId,
                    ItineraryEntrySearchEntryId =  p.ItineraryEntry.SearchEntryId ,
                    ItineraryEntrySearchEntryAirportFromId =  p.ItineraryEntry.SearchEntry.AirportFromId ,
                    ItineraryEntrySearchEntryAirportFromIATA = p.ItineraryEntry != null && p.ItineraryEntry.SearchEntry != null && p.ItineraryEntry.SearchEntry.AirportFrom != null ? p.ItineraryEntry.SearchEntry.AirportFrom.IATA : null,
                    ItineraryEntrySearchEntryAirportFromName = p.ItineraryEntry != null && p.ItineraryEntry.SearchEntry != null && p.ItineraryEntry.SearchEntry.AirportFrom != null ? p.ItineraryEntry.SearchEntry.AirportFrom.Name : null,
                    ItineraryEntrySearchEntryAirportToId = p.ItineraryEntry.SearchEntry.AirportToId ,
                    ItineraryEntrySearchEntryAirportToIATA = p.ItineraryEntry != null && p.ItineraryEntry.SearchEntry != null && p.ItineraryEntry.SearchEntry.AirportTo != null ? p.ItineraryEntry.SearchEntry.AirportTo.IATA : null,
                    ItineraryEntrySearchEntryAirportToName = p.ItineraryEntry != null && p.ItineraryEntry.SearchEntry != null && p.ItineraryEntry.SearchEntry.AirportTo != null ? p.ItineraryEntry.SearchEntry.AirportTo.Name : null,
                    ItineraryEntrySearchEntryAdultsCount =  p.ItineraryEntry.SearchEntry.AdultsCount ,
                    ItineraryEntrySearchEntryChildrenCount =  p.ItineraryEntry.SearchEntry.ChildrenCount ,
                    ItineraryEntrySearchEntryCurrency =  p.ItineraryEntry.SearchEntry.Currency ,
                    ItineraryEntrySearchEntrySearchEntryHash = p.ItineraryEntry != null && p.ItineraryEntry.SearchEntry != null ? p.ItineraryEntry.SearchEntry.SearchEntryHash : null,
                    ItineraryEntryTotalPrice = p.ItineraryEntry.TotalPrice ,
                    DepartureTime = p.DepartureTime,
                    ArrivalTime = p.ArrivalTime,
                    StopsCount = p.StopsCount,
                    Type = p.Type,

                })).MergeWith(this._entityBaseMapper.SelectorExpression);
            }
        }

        public override void MapToModel(FlightEntryDTO dto, FlightEntry model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ItineraryEntryId = dto.ItineraryEntryId;
            model.DepartureTime = dto.DepartureTime;
            model.ArrivalTime = dto.ArrivalTime;
            model.StopsCount = dto.StopsCount;
            model.Type = dto.Type;
            this._entityBaseMapper.MapToModel(dto, model);
        }
    }
}
