﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class Vacation
    {
        [Key]
        public int Id { get; set; }
        public string VacationName { get; set; }
        public bool IsPrivate { get; set; }
        public string LocationQueried { get; set; }
        public int CustomerCreated { get; set; }
        public double Cost { get; set; }
        [ForeignKey("Hotel")]
        public int SavedHotel { get; set; }
        public Hotel Hotel { get; set; }
        public double AverageRating { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}