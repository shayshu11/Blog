using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Name { get; set; }
    }
}