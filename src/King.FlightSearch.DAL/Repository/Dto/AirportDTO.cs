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
    public class AirportDTO : EntityBaseDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public string IATA { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }

    public class AirportMapper : MapperBase<Airport, AirportDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        private EntityBaseMapper _entityBaseMapper = new EntityBaseMapper();
        public override Expression<Func<Airport, AirportDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Airport, AirportDTO>>)(p => new AirportDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    IATA = p.IATA,
                    Name = p.Name,
                    CountryCode = p.CountryCode,

                })).MergeWith(this._entityBaseMapper.SelectorExpression);
            }
        }

        public override void MapToModel(AirportDTO dto, Airport model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.IATA = dto.IATA;
            model.Name = dto.Name;
            model.CountryCode = dto.CountryCode;
            this._entityBaseMapper.MapToModel(dto, model);
        }
    }
}
