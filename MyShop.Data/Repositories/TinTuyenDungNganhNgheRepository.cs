using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ITinTuyenDungNganhNgheRepository : IRepository<TinTuyenDungNganhNghe>
    {
    }

    public class TinTuyenDungNganhNgheRepository : RepositoryBase<TinTuyenDungNganhNghe>, ITinTuyenDungNganhNgheRepository
    {
        public TinTuyenDungNganhNgheRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}