using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("Interest")]
        public List<Interest> Interests { get; set; }
        public Interest Interest { get; set; }

        [ForeignKey("Vacation")]
        public List<Vacation> SavedVacations { get; set; }
        public Vacation Vacation { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}