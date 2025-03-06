using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Hotel.DAL.Entities;

namespace Hotel.DAL.EF
{
    public class HotelModel : DbContext
    {
        public HotelModel(string connectionString)
           : base(connectionString)
        {
            Database.SetInitializer<HotelModel>(new HotelInitializer());
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PriceCategory> PriceCategories { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}