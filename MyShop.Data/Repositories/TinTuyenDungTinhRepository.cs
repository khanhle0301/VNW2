using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ITinTuyenDungTinhRepository : IRepository<TinTuyenDungTinh>
    {
    }

    internal class TinTuyenDungTinhRepository : RepositoryBase<TinTuyenDungTinh>, ITinTuyenDungTinhRepository
    {
        public TinTuyenDungTinhRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}