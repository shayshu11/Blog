using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public enum PermissionType
    {
        ADMIN = 0,
        USER = 1
    }

    public class Permission
    {
        [Key]
        public int id { get; set; }

        public PermissionType type { get; set; }
    }
}