namespace Trolltampen.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Trolltampen.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Trolltampen.DAL.TTDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Trolltampen.DAL.TTDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //if (!WebMatrix.WebData.WebSecurity.Initialized)
            //{
            //    WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("TrolltampenLive", "UserProfiles", "UserId", "UserName", true);
            //}
            //context.UserProfiles.Add(new UserProfile() { UserName = "And", FirstName = "Andriy", LastName = "Lakhno", Email = "andriy.l.a@gmail.com", IsActive = true });
            //context.SaveChanges();
            //WebMatrix.WebData.WebSecurity.CreateAccount("And", "1234");
        }
    }
}
