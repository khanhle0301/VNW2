using MyShop.Data.Infrastructure;
using MyShop.Model.Models;

namespace MyShop.Data.Repositories
{
    public interface ICongTyPhucLoiRepository : IRepository<CongTyPhucLoi>
    {
    }

    public class CongTyPhucLoiRepository : RepositoryBase<CongTyPhucLoi>, ICongTyPhucLoiRepository
    {
        public CongTyPhucLoiRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}