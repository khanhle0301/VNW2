using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface IKyNangRepository : IRepository<KyNang>
    {
    }

    public class KyNangRepository : RepositoryBase<KyNang>, IKyNangRepository
    {
        public KyNangRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}