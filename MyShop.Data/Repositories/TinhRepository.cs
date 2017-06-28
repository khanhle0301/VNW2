using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ITinhRepository : IRepository<Tinh>
    {
    }

    public class TinhRepository : RepositoryBase<Tinh>, ITinhRepository
    {
        public TinhRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}