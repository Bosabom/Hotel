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
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Letters only!")]
        public string Surname { get; set; }

        [Required]
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Letters only!")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Passport Number")]
        [Range(0, Int64.MaxValue, ErrorMessage = "Digits only!")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Passport number must consists of 10 digits")]
        public string Passport { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [DisplayName("BirthDay")]
        public string Birthday_short
        {
            get
            {
                return Birthday.ToShortDateString();
            }
        }

        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                return ($"{Surname} {Name}");
            }
        }
        
        public override string ToString()
        {
            return ($"Full name = {FullName}; Passport = {Passport}; Birthday = {Birthday}" );
        }
    }
}