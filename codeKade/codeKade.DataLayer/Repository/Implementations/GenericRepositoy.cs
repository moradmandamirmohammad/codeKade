using Microsoft.EntityFrameworkCore;
using codeKade.DataLayer.Context;
using codeKade.DataLayer.Entities.Common;
using codeKade.DataLayer.Repository.Interfaces;

namespace codeKade.DataLayer.Repository.Implementations
{
    public class GenericRepositoy<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _DbSet;
        public GenericRepositoy(ApplicationDbContext context)
        {
            _context = context;
            _DbSet = _context.Set<TEntity>();
        }

        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
            await _DbSet.AddAsync(entity);
        }


        public void DeleteEntity(TEntity entity)
        {
            if (entity != null)
            {
                entity.IsDelete = true;
                EditEntity(entity);
            }

        }

        public async Task DeleteEntity(long ID)
        {
            var item = await GetByID(ID);
            if (item != null)
            {
                item.IsDelete = true;
                EditEntity(item);
            }
        }

        public void DeletePermanentEntit(TEntity entity)
        {
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }


        public async Task DeletePermanentEntity(long ID)
        {
            var item = await GetByID(ID);
            if (item != null)
            {
                _context.Remove(item);
            }
        }

        public async ValueTask DisposeAsync()
       {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }

        public void EditEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _DbSet.Update(entity);
        }

       

        public async Task<TEntity> GetByID(long ID)
        {
            var item = await _DbSet.SingleOrDefaultAsync(j => j.ID == ID);
            return item;
        }

        public IQueryable<TEntity> GetEntityQuery()
        {
            return _DbSet.AsQueryable();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
