using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Domain.Common
{
    public interface IPatternsFramework<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(List<TEntity> entities);

        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);
        Task RemoveAsync(int id);

        Task RemoveRangeAsync(string ids);
        Task<int> SaveChangesAsync();

        Task UpdateAsync(TEntity entity);
    }

}