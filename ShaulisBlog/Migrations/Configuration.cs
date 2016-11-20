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
            // This method will be called after migrating to the latest version.
            context.Comments.RemoveRange(context.Comments);
            context.BlogPosts.RemoveRange(context.BlogPosts);
            context.Fans.RemoveRange(context.Fans);
            context.Permissions.RemoveRange(context.Permissions);
            context.Locations.RemoveRange(context.Locations);

            context.Locations.AddOrUpdate(new Models.Location()
            {
                Latitude = 31.969738,
                Longitude = 34.772787,
                Name = "Colman"
            });

            context.Permissions.AddOrUpdate(new Models.Permission()
            {
                type = Models.PermissionType.ADMIN
            });

            context.Permissions.AddOrUpdate(new Models.Permission()
            {
                type = Models.PermissionType.USER
            });

            context.SaveChanges();

            int adminPermId = context.Permissions.First(p => p.type == Models.PermissionType.ADMIN).id;
            int userPermId = context.Permissions.First(p => p.type == Models.PermissionType.USER).id;

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Shay",
                LastName = "Holzman",
                Gender = Models.Gender.FEMALE,
                permissionId = adminPermId,
                Email = "shayshu11@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("Hi1234567!@#", "shayshu11@gmail.com"),
                CreationDate = new DateTime(2016, 6, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 11, 11, 1, 10, 0, 0)
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Ofri",
                LastName = "Sherf",
                Gender = Models.Gender.FEMALE,
                permissionId = adminPermId,
                Email = "ofri@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("Ofri1111", "ofri@gmail.com"),
                CreationDate = new DateTime(2016, 6, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 1, 30, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Roni",
                LastName = "Cohen",
                Gender = Models.Gender.FEMALE,
                permissionId = adminPermId,
                Email = "roni@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("Roni1111", "roni@gmail.com"),
                CreationDate = new DateTime(2016, 7, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 1, 30, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Gal",
                LastName = "Raveh",
                Gender = Models.Gender.FEMALE,
                permissionId = adminPermId,
                Email = "galr@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "galr@gmail.com"),
                CreationDate = new DateTime(2016, 7, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Gal",
                LastName = "Cohen",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "galco@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "galco@gmail.com"),
                CreationDate = new DateTime(2016, 8, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Noa",
                LastName = "Levi",
                Gender = Models.Gender.FEMALE,
                permissionId = userPermId,
                Email = "noale@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "noale@gmail.com"),
                CreationDate = new DateTime(2016, 8, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Sapir",
                LastName = "Goren",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "sapsap@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "sapsap@gmail.com"),
                CreationDate = new DateTime(2016, 8, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1994, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Tami",
                LastName = "Holzman",
                Gender = Models.Gender.FEMALE,
                permissionId = userPermId,
                Email = "tami@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("tami2910", "tami@gmail.com"),
                CreationDate = new DateTime(2016, 9, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1961, 10, 29, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Ron",
                LastName = "Kiewe",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "ronki@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "ronki@gmail.com"),
                CreationDate = new DateTime(2016, 9, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1996, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Tomer",
                LastName = "Slutzki",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "tomersu@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "tomersu@gmail.com"),
                CreationDate = new DateTime(2016, 9, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1996, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Snir",
                LastName = "Zarchi",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "snirki@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "snirki@gmail.com"),
                CreationDate = new DateTime(2016, 9, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1996, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Shunit",
                LastName = "Tubul",
                Gender = Models.Gender.FEMALE,
                permissionId = userPermId,
                Email = "shun@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "shun@gmail.com"),
                CreationDate = new DateTime(2016, 10, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1997, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Derek",
                LastName = "Sheperd",
                Gender = Models.Gender.FEMALE,
                permissionId = userPermId,
                Email = "derek@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "derek@gmail.com"),
                CreationDate = new DateTime(2016, 10, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1997, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Pavel",
                LastName = "Sobutko",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "pavel@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "pavel@gmail.com"),
                CreationDate = new DateTime(2016, 10, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1992, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Yoad",
                LastName = "Tewel",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "yoad@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "yoad@gmail.com"),
                CreationDate = new DateTime(2016, 11, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1995, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Rotem",
                LastName = "Tewel",
                Gender = Models.Gender.FEMALE,
                permissionId = userPermId,
                Email = "rotemi@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "rotemi@gmail.com"),
                CreationDate = new DateTime(2016, 11, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1995, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Jack",
                LastName = "Levi",
                Gender = Models.Gender.MALE,
                permissionId = userPermId,
                Email = "jack@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "jack@gmail.com"),
                CreationDate = new DateTime(2016, 11, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1995, 9, 11, 0, 0, 0, 0),
            });

            context.Fans.AddOrUpdate(new Models.Fan()
            {
                FirstName = "Jenni",
                LastName = "Grey",
                Gender = Models.Gender.FEMALE,
                permissionId = userPermId,
                Email = "jennig@gmail.com",
                Password = ShaulisBlog.Controllers.FansController.HashString("gal12345678", "jennig@gmail.com"),
                CreationDate = new DateTime(2016, 11, 6, 0, 0, 0, 0),
                DateOfBirth = new DateTime(1995, 9, 11, 0, 0, 0, 0),
            });

            context.SaveChanges();

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "Hello",
                Title = "hi",
                WriterId = context.Fans.First(f => f.FirstName == "Shay").ID,
                PostDate = new DateTime(2016, 6, 13, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 6, 13, 12, 30, 0, 0)
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "Hello darkness my old friend",
                Title = "hi there",
                WriterId = context.Fans.First(f => f.FirstName == "Ofri").ID,
                PostDate = new DateTime(2016, 6, 14, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 6, 14, 12, 30, 0, 0)
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "Hello, it's me",
                Title = "Adele",
                WriterId = context.Fans.First(f => f.Email == "galco@gmail.com").ID,
                PostDate = new DateTime(2016, 7, 15, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 7, 15, 12, 30, 0, 0)
            });


            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "Derek Sheperd is dead!!!",
                Title = "OMG",
                WriterId = context.Fans.First(f => f.Email == "galr@gmail.com").ID,
                PostDate = new DateTime(2016, 8, 15, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 8, 15, 12, 30, 0, 0)
            });


            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "math science history unraveling the mystery that all started with a big bang",
                Title = "Big bang",
                WriterId = context.Fans.First(f => f.Email == "galco@gmail.com").ID,
                PostDate = new DateTime(2016, 8, 16, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 8, 16, 12, 30, 0, 0)
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "There is only an expectation, without interest",
                Title = "You do not owe me anything",
                WriterId = context.Fans.First(f => f.Email == "galco@gmail.com").ID,
                PostDate = new DateTime(2016, 9, 16, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 9, 16, 12, 30, 0, 0)
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "I drink too much and that's an issue",
                Title = "Hey, I was doing just fine before I met you",
                WriterId = context.Fans.First(f => f.Email == "galr@gmail.com").ID,
                PostDate = new DateTime(2016, 9, 17, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 9, 17, 12, 30, 0, 0)
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "Our life are strawberries",
                Title = "Tutim",
                WriterId = context.Fans.First(f => f.FirstName == "Ron").ID,
                PostDate = new DateTime(2016, 10, 15, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 10, 15, 12, 30, 0, 0)
            });

            context.BlogPosts.AddOrUpdate(new Models.BlogPost()
            {
                Content = "Birthday",
                Title = "Happy",
                WriterId = context.Fans.First(f => f.FirstName == "Ron").ID,
                PostDate = new DateTime(2016, 11, 15, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 11, 15, 12, 30, 0, 0)
            });

            context.SaveChanges();

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "You",
                Title = "To",
                WriterId = context.Fans.First(f => f.FirstName == "Roni").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 30, 0, 0),
                PostId = context.BlogPosts.First(b => b.Title == "Happy").ID
            });

            int closerPost = context.BlogPosts.First(b => b.Title == "Hey, I was doing just fine before I met you").ID;

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "Hey, you tell your friends it was nice to meet them",
                Title = "But I'm OK",
                WriterId = context.Fans.First(f => f.FirstName == "Roni").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 30, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 30, 0, 0),
                PostId = closerPost
            });

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "Again",
                Title = "But I hope I never see them",
                WriterId = context.Fans.First(f => f.FirstName == "Sapir").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 31, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 31, 0, 0),
                PostId = closerPost
            });

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "Moved to the city in a broke-down car",
                Title = "I know it breaks your heart",
                WriterId = context.Fans.First(f => f.FirstName == "Noa").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 32, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 32, 0, 0),
                PostId = closerPost
            });

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "Now you're looking pretty in a hotel bar",
                Title = "And four years, no calls",
                WriterId = context.Fans.First(f => f.FirstName == "Tami").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 33, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 33, 0, 0),
                PostId = closerPost
            });

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "No, I, I, I, I, I can't stop",
                Title = "And I, I, I, I, I can't stop",
                WriterId = context.Fans.First(f => f.FirstName == "Derek").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 34, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 34, 0, 0),
                PostId = closerPost
            });

            context.Comments.AddOrUpdate(new Models.Comment()
            {
                Content = "In the back seat of your Rover",
                Title = "So, baby, pull me closer",
                WriterId = context.Fans.First(f => f.FirstName == "Jack").ID,
                CommentDate = new DateTime(2016, 11, 17, 12, 35, 0, 0),
                UpdateDate = new DateTime(2016, 11, 17, 12, 35, 0, 0),
                PostId = closerPost
            });
        }
    }
}
