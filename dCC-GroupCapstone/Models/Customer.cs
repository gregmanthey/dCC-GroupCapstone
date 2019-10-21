using Newtonsoft.Json;
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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Interests")]
        public virtual List<string> Interests
        {
            get { return _Interests; }
            set { _Interests = value; }
        }

        private List<string> _Interests;

        public string InterestsSerialized
        {
            get { return JsonConvert.SerializeObject(_Interests); }
            set { _Interests = JsonConvert.DeserializeObject<List<string>>(value); }
        }

        [Display(Name = "Saved Vacations")]
        public List<Vacation> SavedVacations
        {
            get { return _SavedVacations; }
            set { _SavedVacations = value; }
        }

        private List<Vacation> _SavedVacations;

        public string SavedVacationsSerialized
        {
            get { return JsonConvert.SerializeObject(_SavedVacations); }
            set { _SavedVacations = JsonConvert.DeserializeObject<List<Vacation>>(value); }
        }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}