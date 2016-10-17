using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class Permission
    {
        [Key]
        public int id { get; set; }

        public string type { get; set; }

        public bool canPost { get; set; }

        public bool canComment { get; set; }

        public bool canDeletePost { get; set; }

        public bool canDeleteSelfComment { get; set; }

        public bool canDeleteAllComments { get; set; }
    }
}