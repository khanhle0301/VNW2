﻿using Microsoft.AspNet.Identity.EntityFramework;
using MyShop.Model.Models;
using System.Data.Entity;

namespace MyShop.Data
{
    public class MyShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyShopDbContext() : base("MyShopConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CapBac> CapBacs { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<CongTy> CongTys { set; get; }
        public DbSet<CongTyNganhNghe> CongTyNganhNghes { set; get; }
        public DbSet<CongTyPhucLoi> CongTyPhucLois { set; get; }
        public DbSet<KyNang> KyNangs { set; get; }
        public DbSet<NganhNghe> NganhNghes { set; get; }
        public DbSet<PhucLoi> PhucLois { set; get; }
        public DbSet<Tinh> Tinhs { set; get; }
        public DbSet<TinTuyenDung> TinTuyenDungs { set; get; }
        public DbSet<TinTuyenDungKyNang> TinTuyenDungKyNangs { set; get; }
        public DbSet<TinTuyenDungNganhNghe> TinTuyenDungNganhNghes { set; get; }
        public DbSet<TinTuyenDungTinh> TinTuyenDungTinhs { set; get; }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }

        public static MyShopDbContext Create()
        {
            return new MyShopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}