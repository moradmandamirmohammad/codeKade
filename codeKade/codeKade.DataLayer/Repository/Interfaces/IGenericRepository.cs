using codeKade.DataLayer.Entities.Common;

namespace codeKade.DataLayer.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        Task AddEntity(TEntity entity);

        Task<TEntity> GetByID(long ID);

        IQueryable<TEntity> GetEntityQuery();

        void EditEntity(TEntity entity);

        void DeleteEntity(TEntity entity);

        Task DeleteEntity(long ID);

        Task DeletePermanentEntity(long ID);

        void DeletePermanentEntit(TEntity entity);

        Task SaveChanges();
    }
}
