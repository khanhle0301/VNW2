namespace MyShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ApplicationRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        BirthDay = c.DateTime(),
                        IsSystem = c.Boolean(nullable: false),
                        IsCustomer = c.Boolean(nullable: false),
                        NhanMail = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.CapBacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CongTyNganhNghes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CongTyId = c.Int(nullable: false),
                        NganhNgheId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CongTys", t => t.CongTyId, cascadeDelete: true)
                .ForeignKey("dbo.NganhNghes", t => t.NganhNgheId, cascadeDelete: true)
                .Index(t => t.CongTyId)
                .Index(t => t.NganhNgheId);
            
            CreateTable(
                "dbo.CongTys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        QuyMo = c.String(maxLength: 256),
                        DienThoai = c.String(maxLength: 256),
                        DiaChi = c.String(maxLength: 256),
                        ThongTin = c.String(),
                        Logo = c.String(maxLength: 256),
                        HinhAnh = c.String(storeType: "xml"),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NganhNghes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(maxLength: 256),
                        ParentId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CongTyPhucLois",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CongTyId = c.Int(nullable: false),
                        PhucLoiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CongTys", t => t.CongTyId, cascadeDelete: true)
                .ForeignKey("dbo.PhucLois", t => t.PhucLoiId, cascadeDelete: true)
                .Index(t => t.CongTyId)
                .Index(t => t.PhucLoiId);
            
            CreateTable(
                "dbo.PhucLois",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KyNangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tinhs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TinTuyenDungKyNangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TinTuyenDungId = c.Int(nullable: false),
                        KyNangId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KyNangs", t => t.KyNangId, cascadeDelete: true)
                .ForeignKey("dbo.TinTuyenDungs", t => t.TinTuyenDungId, cascadeDelete: true)
                .Index(t => t.TinTuyenDungId)
                .Index(t => t.KyNangId);
            
            CreateTable(
                "dbo.TinTuyenDungs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChucDanh = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(maxLength: 256),
                        CapBacId = c.Int(nullable: false),
                        TuLuong = c.Int(nullable: false),
                        DenLuong = c.Int(nullable: false),
                        HienThiLuong = c.Boolean(nullable: false),
                        MoTa = c.String(),
                        YeuCau = c.String(),
                        NguoiLienHe = c.String(maxLength: 256),
                        Email = c.String(maxLength: 256),
                        CongTyId = c.Int(nullable: false),
                        ViewCount = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CapBacs", t => t.CapBacId, cascadeDelete: true)
                .ForeignKey("dbo.CongTys", t => t.CongTyId, cascadeDelete: true)
                .Index(t => t.CapBacId)
                .Index(t => t.CongTyId);
            
            CreateTable(
                "dbo.TinTuyenDungNganhNghes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TinTuyenDungId = c.Int(nullable: false),
                        NganhNgheId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NganhNghes", t => t.NganhNgheId, cascadeDelete: true)
                .ForeignKey("dbo.TinTuyenDungs", t => t.TinTuyenDungId, cascadeDelete: true)
                .Index(t => t.TinTuyenDungId)
                .Index(t => t.NganhNgheId);
            
            CreateTable(
                "dbo.TinTuyenDungTinhs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TinTuyenDungId = c.Int(nullable: false),
                        TinhId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tinhs", t => t.TinhId, cascadeDelete: true)
                .ForeignKey("dbo.TinTuyenDungs", t => t.TinTuyenDungId, cascadeDelete: true)
                .Index(t => t.TinTuyenDungId)
                .Index(t => t.TinhId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TinTuyenDungTinhs", "TinTuyenDungId", "dbo.TinTuyenDungs");
            DropForeignKey("dbo.TinTuyenDungTinhs", "TinhId", "dbo.Tinhs");
            DropForeignKey("dbo.TinTuyenDungNganhNghes", "TinTuyenDungId", "dbo.TinTuyenDungs");
            DropForeignKey("dbo.TinTuyenDungNganhNghes", "NganhNgheId", "dbo.NganhNghes");
            DropForeignKey("dbo.TinTuyenDungKyNangs", "TinTuyenDungId", "dbo.TinTuyenDungs");
            DropForeignKey("dbo.TinTuyenDungs", "CongTyId", "dbo.CongTys");
            DropForeignKey("dbo.TinTuyenDungs", "CapBacId", "dbo.CapBacs");
            DropForeignKey("dbo.TinTuyenDungKyNangs", "KyNangId", "dbo.KyNangs");
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.CongTyPhucLois", "PhucLoiId", "dbo.PhucLois");
            DropForeignKey("dbo.CongTyPhucLois", "CongTyId", "dbo.CongTys");
            DropForeignKey("dbo.CongTyNganhNghes", "NganhNgheId", "dbo.NganhNghes");
            DropForeignKey("dbo.CongTyNganhNghes", "CongTyId", "dbo.CongTys");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.TinTuyenDungTinhs", new[] { "TinhId" });
            DropIndex("dbo.TinTuyenDungTinhs", new[] { "TinTuyenDungId" });
            DropIndex("dbo.TinTuyenDungNganhNghes", new[] { "NganhNgheId" });
            DropIndex("dbo.TinTuyenDungNganhNghes", new[] { "TinTuyenDungId" });
            DropIndex("dbo.TinTuyenDungs", new[] { "CongTyId" });
            DropIndex("dbo.TinTuyenDungs", new[] { "CapBacId" });
            DropIndex("dbo.TinTuyenDungKyNangs", new[] { "KyNangId" });
            DropIndex("dbo.TinTuyenDungKyNangs", new[] { "TinTuyenDungId" });
            DropIndex("dbo.CongTyPhucLois", new[] { "PhucLoiId" });
            DropIndex("dbo.CongTyPhucLois", new[] { "CongTyId" });
            DropIndex("dbo.CongTyNganhNghes", new[] { "NganhNgheId" });
            DropIndex("dbo.CongTyNganhNghes", new[] { "CongTyId" });
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropTable("dbo.TinTuyenDungTinhs");
            DropTable("dbo.TinTuyenDungNganhNghes");
            DropTable("dbo.TinTuyenDungs");
            DropTable("dbo.TinTuyenDungKyNangs");
            DropTable("dbo.Tinhs");
            DropTable("dbo.KyNangs");
            DropTable("dbo.Errors");
            DropTable("dbo.PhucLois");
            DropTable("dbo.CongTyPhucLois");
            DropTable("dbo.NganhNghes");
            DropTable("dbo.CongTys");
            DropTable("dbo.CongTyNganhNghes");
            DropTable("dbo.CapBacs");
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationRoles");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
