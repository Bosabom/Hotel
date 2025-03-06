using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IWorkUnit
    {
        IRepository<Room> Rooms { get; }

        IRepository<Guest> Guests { get; }

        IRepository<Category> Categories { get; }

        IRepository<PriceCategory> PriceCategories { get; }

        IRepository<Booking> Bookings { get; }
        
        IUserRepository<User> Users { get; }

        ILogRepository<Log> Logs { get; }

        void Save();
    }
}