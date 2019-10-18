using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PlaceId { get; set; }
        public string LatLong { get; set; }
        
        public double Price { get; set; }
        
    }
}