namespace MyShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MyShop.Data.MyShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MyShop.Data.MyShopDbContext context)
        {
            //CreateUser(context);
        }

        private void CreateUser(MyShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MyShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "khanhle0301.it@gmail.com",
                Email = "khanhle0301.it@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Lê Khanh"
            };
            manager.Create(user, "123654$");
        }
    }
}