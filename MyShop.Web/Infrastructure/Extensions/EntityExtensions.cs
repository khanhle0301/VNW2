using MyShop.Model.Models;
using MyShop.Web.Models;
using System;

namespace MyShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
            appGroup.Description = appGroupViewModel.Description;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {
            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
            appUser.Address = appUserViewModel.Address;
        }

        public static void UpdateNganhNghe(this NganhNghe nganhNghe, NganhNgheViewModel nganhNgheVm)
        {
            nganhNghe.Id = nganhNgheVm.Id;
            nganhNghe.Ten = nganhNgheVm.Ten;
            nganhNghe.Alias = nganhNgheVm.Alias;
            nganhNghe.ParentId = nganhNgheVm.ParentId;
            nganhNghe.CreatedDate = nganhNgheVm.CreatedDate;
            nganhNghe.CreatedBy = nganhNgheVm.CreatedBy;
            nganhNghe.UpdatedDate = nganhNgheVm.UpdatedDate;
            nganhNghe.UpdatedBy = nganhNgheVm.UpdatedBy;
            nganhNghe.MetaKeyword = nganhNgheVm.MetaKeyword;
            nganhNghe.MetaDescription = nganhNgheVm.MetaDescription;
            nganhNghe.Status = nganhNgheVm.Status;
        }

        public static void UpdateCapBac(this CapBac capbac, CapBacViewModel capbacVm)
        {
            capbac.Id = capbacVm.Id;
            capbac.Ten = capbacVm.Ten;
            capbac.Status = capbacVm.Status;
        }

        public static void UpdatePhucLoi(this PhucLoi pl, PhucLoiViewModel plVm)
        {
            pl.Id = plVm.Id;
            pl.Ten = plVm.Ten;
            pl.Status = plVm.Status;
        }

        public static void UpdateKyNang(this KyNang kn, KyNangViewModel knVm)
        {
            kn.Id = knVm.Id;
            kn.Ten = knVm.Ten;
            kn.Status = knVm.Status;
        }
    }
}