using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Author")]
        public int WriterId { get; set; }
        public Fan Author { get; set; }

        [ForeignKey("BlogPost")]
        public int PostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public String Content { get; set; }

        public String Title { get; set; }

        public DateTime CommentDate { get; set; }
    }
}