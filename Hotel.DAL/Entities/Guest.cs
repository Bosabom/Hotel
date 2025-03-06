using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Passport { get; set; }

        public DateTime Birthday { get; set; }
    }
}