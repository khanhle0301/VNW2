using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface INganhNgheRepository : IRepository<NganhNghe>
    {
    }

    public class NganhNgheRepository : RepositoryBase<NganhNghe>, INganhNgheRepository
    {
        public NganhNgheRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}