using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ITinTuyenDungKyNangRepository : IRepository<TinTuyenDungKyNang>
    {
    }

    public class TinTuyenDungKyNangRepository : RepositoryBase<TinTuyenDungKyNang>, ITinTuyenDungKyNangRepository
    {
        public TinTuyenDungKyNangRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}