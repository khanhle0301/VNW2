using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ICongTyRepository : IRepository<CongTy>
    {
    }

    public class CongTyRepository : RepositoryBase<CongTy>, ICongTyRepository
    {
        public CongTyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}