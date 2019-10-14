using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PlaceId { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public List<Interest> AssociatedInterests { get; set; }

    }
}