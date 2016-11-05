using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class ShaulisBlogContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ShaulisBlogContext() : base("name=ShaulisBlogContext")
        {
        }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.Fan> Fans { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.Permission> Permissions { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.BlogPost> BlogPosts { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.Location> Locations { get; set; }
    }
}
