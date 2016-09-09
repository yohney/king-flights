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
    public class SearchEntryDTO : EntityBaseDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public Guid AirportFromId { get; set; }
        public Guid AirportToId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public Currency Currency { get; set; }
        public string SearchEntryHash { get; set; }
    }

    public class SearchEntryMapper : MapperBase<SearchEntry, SearchEntryDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        private EntityBaseMapper _entityBaseMapper = new EntityBaseMapper();
        public override Expression<Func<SearchEntry, SearchEntryDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<SearchEntry, SearchEntryDTO>>)(p => new SearchEntryDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    AirportFromId = p.AirportFromId,
                    AirportToId = p.AirportToId,
                    DepartureDate = p.DepartureDate,
                    ReturnDate = p.ReturnDate,
                    AdultsCount = p.AdultsCount,
                    ChildrenCount = p.ChildrenCount,
                    Currency = p.Currency,
                    SearchEntryHash = p.SearchEntryHash,

                })).MergeWith(this._entityBaseMapper.SelectorExpression);
            }
        }

        public override void MapToModel(SearchEntryDTO dto, SearchEntry model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.AirportFromId = dto.AirportFromId;
            model.AirportToId = dto.AirportToId;
            model.DepartureDate = dto.DepartureDate;
            model.ReturnDate = dto.ReturnDate;
            model.AdultsCount = dto.AdultsCount;
            model.ChildrenCount = dto.ChildrenCount;
            model.Currency = dto.Currency;
            model.SearchEntryHash = dto.SearchEntryHash;
            this._entityBaseMapper.MapToModel(dto, model);
        }
    }
}
