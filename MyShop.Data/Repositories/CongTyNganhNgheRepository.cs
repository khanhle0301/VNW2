using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ICongTyNganhNgheRepository : IRepository<CongTyNganhNghe>
    {
    }

    public class CongTyNganhNgheRepository : RepositoryBase<CongTyNganhNghe>, ICongTyNganhNgheRepository
    {
        public CongTyNganhNgheRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}