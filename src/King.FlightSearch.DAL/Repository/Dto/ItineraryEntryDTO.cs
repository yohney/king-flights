using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.DAL.Repository.Dto.Infrastructure;
using King.FlightSearch.DAL.Entities;

namespace King.FlightSearch.DAL.Repository.Dto
{
    public class ItineraryEntryDTO : EntityBaseDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public Guid SearchEntryId { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ItineraryEntryMapper : MapperBase<ItineraryEntry, ItineraryEntryDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        private EntityBaseMapper _entityBaseMapper = new EntityBaseMapper();
        public override Expression<Func<ItineraryEntry, ItineraryEntryDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<ItineraryEntry, ItineraryEntryDTO>>)(p => new ItineraryEntryDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    SearchEntryId = p.SearchEntryId,
                    TotalPrice = p.TotalPrice,

                })).MergeWith(this._entityBaseMapper.SelectorExpression);
            }
        }

        public override void MapToModel(ItineraryEntryDTO dto, ItineraryEntry model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.SearchEntryId = dto.SearchEntryId;
            model.TotalPrice = dto.TotalPrice;
            this._entityBaseMapper.MapToModel(dto, model);
        }
    }
}
