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
    public class EFWorkUnit : IWorkUnit
    {
        private HotelModel db;
        private RoomRepository roomRepository;
        private GuestRepository guestRepository;
        private CategoryRepository categoryRepository;
        private PriceCategoryRepository priceCategoryRepository;
        private BookingRepository bookingRepository;
        private UserRepository userRepository;
        private LogRepository logRepository;

        public EFWorkUnit(string connectionString)
        {
            db = new HotelModel(connectionString);
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(db);
                }
                return roomRepository;
            }
        }

        public IRepository<Guest> Guests 
        {
            get
            {
                if (guestRepository == null)
                {
                    guestRepository = new GuestRepository(db);
                }
                return guestRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }
                return categoryRepository;
            }
        }

        public IRepository<PriceCategory> PriceCategories
        {
            get
            {
                if (priceCategoryRepository == null)
                {
                    priceCategoryRepository = new PriceCategoryRepository(db);
                }
                return priceCategoryRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(db);
                }
                return bookingRepository;
            }
        }

        public IUserRepository<User> Users
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public ILogRepository<Log> Logs
        {
            get
            {
                if (logRepository == null)
                {
                    logRepository = new LogRepository(db);
                }
                return logRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}