using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class GuestModel
    {
        
        public int Id { get; set; }

        [Required]
        [DisplayName("LastName")]

        public string Surname { get; set; }

        [Required]
        [DisplayName("FirstName")]

        public string Name { get; set; }

        [Required]
        [DisplayName("Passport Number")]

        public string Passport { get; set; }

        [Required]
        [DisplayName("Date of birth")]

        public DateTime Birthday { get; set; }

        [DisplayName("Full Name")]
        public string FullInfo
        {
            get
            {
                return ($"{Surname} {Name} ");
            }

        }
    }
}