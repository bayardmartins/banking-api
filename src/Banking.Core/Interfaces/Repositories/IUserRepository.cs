using Banking.Core.Aggregate.Entities;

namespace Banking.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User? GetUserByLogin(string userName);
        Task<int> Register(User user);
    }
}