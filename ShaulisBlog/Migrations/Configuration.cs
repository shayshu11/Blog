namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShaulisBlog.Models.ShaulisBlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShaulisBlog.Models.ShaulisBlogContext context)
        {
            //This method will be called after migrating to the latest version.


          context.Permissions.AddOrUpdate(new Models.Permission()
          {
              id = 0,
              type = "Admin",
              canComment = true,
              canDeleteAllComments = true,
              canDeletePost = true,
              canDeleteSelfComment = true,
              canPost = true
          });

            context.Permissions.AddOrUpdate(new Models.Permission()
            {
                id = 1,
                type = "User",
                canComment = true,
                canDeleteAllComments = false,
                canDeletePost = false,
                canDeleteSelfComment = true,
                canPost = false
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Ofri",
                LastName = "Sherf",
                Gender = Models.Gender.FEMALE,
                DateOfBirth = new DateTime(1994, 1, 30),
                Seniority = 2,
                ID = 1,
                permissionId = 0
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Shay",
                LastName = "Holzman",
                Gender = Models.Gender.FEMALE,
                DateOfBirth = new DateTime(1994, 11, 11),
                Seniority = 1,
                ID = 2,
                permissionId = 0
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                ID = 0,
                Content = "Hellooooooooooooooooooooooooooooooooooooooooo",
                Title = "hi",
                WriterId = 1,
                PostDate = new DateTime(2016, 10, 13, 12, 30, 0),
            });
        }
    }
}
