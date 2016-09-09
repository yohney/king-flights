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
    public class EntityBaseDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public Guid Id { get; set; }
    }

    public class EntityBaseMapper : MapperBase<EntityBase, EntityBaseDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<EntityBase, EntityBaseDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<EntityBase, EntityBaseDTO>>)(p => new EntityBaseDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    Id = p.Id,

                }));
            }
        }

        public override void MapToModel(EntityBaseDTO dto, EntityBase model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.Id = dto.Id;

        }
    }
}
