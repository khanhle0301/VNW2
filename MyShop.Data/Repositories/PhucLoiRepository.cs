using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface IPhucLoiRepository : IRepository<PhucLoi>
    {
    }

    public class PhucLoiRepository : RepositoryBase<PhucLoi>, IPhucLoiRepository
    {
        public PhucLoiRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}