using Microsoft.EntityFrameworkCore;
using Banking.Infrastructure.Data;
using Banking.Core.Aggregate.Entities;
using Banking.Core.Interfaces.Repositories;

namespace Banking.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        internal readonly BankingContext _context;
        internal DbSet<T> dbSet;
        public BaseRepository(BankingContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }
        public async virtual Task<T?> Get(int id)
        {
            return await dbSet.Where(item => item.IsActive)
                                .Where(item => item.Id == id)
                                .FirstOrDefaultAsync();
        }
        public async virtual Task<IEnumerable<T>> GetAll()
        { 
            return await dbSet.Where(item => item.IsActive)
                                .ToListAsync<T>();
        }
        public async virtual Task<int> Add(T item)
        {
            await dbSet.AddAsync(item);
            await _context.SaveChangesAsync();  
            return item.Id;  
        }

        public async virtual Task<int?> SetIsActivate(int id, bool isActive)
        {
            var itemToSetIsActive = await dbSet.FindAsync(id);
            if (itemToSetIsActive == null) {return null;};
            itemToSetIsActive.IsActive = isActive;
            dbSet.Update(itemToSetIsActive);
            await _context.SaveChangesAsync();
            return id;
        }

        public async virtual Task<int?> Delete(int id)
        {
            var itemToDelete = await dbSet.FindAsync(id);
            if (itemToDelete == null)
            {
                return null;
            }
            dbSet.Remove(itemToDelete);
            await _context.SaveChangesAsync();
            return id;
        }
        public virtual async Task<int?> Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}