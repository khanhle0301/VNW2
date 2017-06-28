using AutoMapper;
using MyShop.Model.Models;
using MyShop.Web.Models;

namespace MyShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<NganhNghe, NganhNgheViewModel>();
            Mapper.CreateMap<Tinh, TinhViewModel>();
            Mapper.CreateMap<CapBac, CapBacViewModel>();
            Mapper.CreateMap<PhucLoi, PhucLoiViewModel>();
            Mapper.CreateMap<KyNang, KyNangViewModel>();

            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}