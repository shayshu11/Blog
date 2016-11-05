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

            //context.Fans.AddOrUpdate((f => f.ID), new Models.Fan()
            //{
            //    ID = 1,
            //    Email = "ofri@gmail.com",
            //    Password = "Ofri1111"
            //});

            //context.Fans.AddOrUpdate((f => f.ID), new Models.Fan()
            //{
            //    ID = 2,
            //    Email = "shayshu11@gmail.com",
            //    Password = "Hi1234"
            //});

            //context.Permissions.AddOrUpdate(new Models.Permission()
            //{
            //    id = 0,
            //    type = Models.PermissionType.ADMIN
            //});

            //context.Permissions.AddOrUpdate(new Models.Permission()
            //{
            //    id = 1,
            //    type = Models.PermissionType.USER
            //});

            //context.Fans.AddOrUpdate(new Models.Fan()
            //{
            //    FirstName = "Ofri",
            //    LastName = "Sherf",
            //    Gender = Models.Gender.FEMALE,
            //    DateOfBirth = new DateTime(1994, 1, 30),
            //    permissionId = 0,
            //    Email = "ofri@gmail.com",
            //    Password = "Ofri1111"
            //});

            //context.Fans.AddOrUpdate(new Models.Fan()
            //{
            //    FirstName = "Roni",
            //    LastName = "Cohen",
            //    Gender = Models.Gender.FEMALE,
            //    DateOfBirth = new DateTime(1994, 1, 30),
            //    permissionId = 1,
            //    Email = "roni@gmail.com",
            //    Password = "Ofri1111"
            //});

            //context.Fans.AddOrUpdate(new Models.Fan()
            //{
            //    FirstName = "Shay",
            //    LastName = "Holzman",
            //    Gender = Models.Gender.FEMALE,
            //    permissionId = 0,
            //    Email = "shayshu11@gmail.com",
            //    Password = "Hi1234"
            //});

            //context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            //{
            //    ID = 0,
            //    Content = "Hellooooooooooooooooooooooooooooooooooooooooo",
            //    Title = "hi",
            //    WriterId = 7,
            //    PostDate = new DateTime(2016, 10, 13, 12, 30, 0),
            //});

            context.Locations.AddOrUpdate(new Models.Location() { 
                Latitude = 31.969738,
                Longitude = 34.772787,
                Name = "Colman"
            });
        }
    }
}
