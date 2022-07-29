using Banking.Core.Interfaces.Repositories;
using Banking.Core.Aggregate.Entities;
using Banking.Infrastructure.Data;

namespace Banking.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BankingContext dataContext) : base(dataContext) {} 

        public User? GetUserByLogin(string login)
        {
            return dbSet.Where(u => u.Login == login)
                        .Where(u => u.IsActive)
                                .FirstOrDefault();
        }

        public async Task<int> Register(User user)
        {
            var existingUser = dbSet.Where(u => u.Login == user.Login)
                                        .FirstOrDefault();
            
            if (existingUser != null)
            {
                throw new Exception("User Login already exists");
            }
            await dbSet.AddAsync(user);

            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}