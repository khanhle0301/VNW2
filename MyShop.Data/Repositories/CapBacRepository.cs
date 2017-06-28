using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ICapBacRepository : IRepository<CapBac>
    {
    }

    public class CapBacRepository : RepositoryBase<CapBac>, ICapBacRepository
    {
        public CapBacRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}