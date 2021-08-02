using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
namespace Hotel.DAL.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private HotelModel db;
        public UserRepository(HotelModel db)
        {
            this.db = db;
        }
        public void Create(User new_user)
        {
            db.Users.Add(new_user);
        }
        public User Get(User user)
        {
            return db.Users.FirstOrDefault(u=>u.Login == user.Login);
        }
       
    }
}
