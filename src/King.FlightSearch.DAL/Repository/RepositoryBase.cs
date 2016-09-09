using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grader.DAL.Db;
using King.FlightSearch.DAL.Entities;
using King.FlightSearch.DAL.Repository.Dto;
using King.FlightSearch.DAL.Repository.Dto.Infrastructure;

namespace King.FlightSearch.DAL.Repository
{
    public class RepositoryBase<TEntity, TDto>
        where TDto: EntityBaseDTO
        where TEntity: EntityBase, new()
    {
        protected FlightDbContext DbContext { get; }
        protected MapperBase<TEntity, TDto> Mapper { get; }

        public RepositoryBase(FlightDbContext dbContext, MapperBase<TEntity, TDto> mapper)
        {
            this.Mapper = mapper;
            this.DbContext = dbContext;
        }

        public virtual void InsertOrUpdate(TDto dto, bool autoSave = true)
        {
            if (dto.Id == Guid.Empty)
                dto.Id = Guid.NewGuid();

            var entity = this.DbContext.Set<TEntity>().Find(dto.Id);

            if(entity == null)
            {
                entity = new TEntity();
                entity.DateCreated = DateTime.Now;
                entity.Id = dto.Id;
                this.Mapper.MapToModel(dto, entity);

                this.DbContext.Set<TEntity>().Add(entity);

                if(autoSave)
                    this.DbContext.SaveChanges();

            }
            else
            {
                entity.DateModified = DateTime.Now;
                this.Mapper.MapToModel(dto, entity);

                if (autoSave)
                    this.DbContext.SaveChanges();
            }
        }

        public void Save()
        {
            this.DbContext.SaveChanges();
        }
    }
}
