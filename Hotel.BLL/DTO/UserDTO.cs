using Hotel.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string HashedPassword
        {
            get
            {
                return Crypto.Hash(this.Password);     
            }
        }
    }
}