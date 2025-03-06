using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string HashedPassword {get;set;}
    }
}