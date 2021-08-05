using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Hotel.DAL.Entities;


namespace Hotel.DAL.EF
{

    //DropCreateDatabaseAlways
    //DropCreateDatabaseIfModelChanges

    public class HotelInitializer: DropCreateDatabaseIfModelChanges<HotelModel>
    {
        private void RoomInitializer(HotelModel context)
        {
            var roomList = new List<Room>()
            {
                new Room()
                {
                    Name="101",
                    Active=true,
                    CategoryId=2
                },
                new Room()
                {   
                    Name="112a",
                    Active=true,
                    CategoryId=1
                },
                new Room()
                {
                    Name="208",
                    Active=true,
                    CategoryId=3
                },
                new Room()
                {
                    Name="210",
                    Active=true,
                    CategoryId=4
                },
                new Room()
                {
                    Name="303",
                    Active=true,
                    CategoryId=5
                },
                new Room()
                {
                    Name="305a",
                    Active=false,
                    CategoryId=5
                },
                new Room()
                {
                    Name="107",
                    Active=false,
                    CategoryId=1
                },
                new Room()
                {
                    Name="209",
                    Active=true,
                    CategoryId=4

                }

            };
            foreach (var room in roomList)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();
        }
        private void GuestInitializer(HotelModel context)
        {
            var ListOfGuests = new List<Guest>()
            {
                new Guest()
                {
                    Name="Alex",
                    Surname="Lisikov",
                    Passport="1029384756",
                    Birthday=new DateTime(1968,4,27)
                    
                },
                new Guest()
                {
                    Name="Kostya",
                    Surname="Kostin",
                    Passport="3900572422",
                    Birthday=new DateTime(2000,1,12)

                },
                new Guest()
                {
                    Name="Maxim",
                    Surname="Shevchenko",
                    Passport="0001284661",
                    Birthday=new DateTime(1999,12,5)
                },
                new Guest()
                {
                    Name="Denis",
                    Surname="Nozhenko",
                    Passport="1189377768",
                    Birthday=new DateTime(1990,2,10)

                },
                new Guest()
                {
                    Name="Alena",
                    Surname="Prosyanik",
                    Passport="0071497732",
                    Birthday=new DateTime(2003,8,19)
                }

            };
            foreach (var guest in ListOfGuests)
            {
                context.Guests.Add(guest);
            }

            context.SaveChanges();
        }
        private void CategoryInitializer(HotelModel context)
        {
            var ListOfCategories = new List<Category>()
            {
                new Category()
                {
                    Name="Lux",
                    Number_Of_Places=2
                },
                new Category()
                {
                    Name="Lux",
                    Number_Of_Places=3
                },
                new Category()
                {
                    Name="Standart",
                    Number_Of_Places=2
                },
                new Category()
                {
                    Name="Standart",
                    Number_Of_Places=3
                },
                new Category()
                {
                    Name="Economy",
                    Number_Of_Places=2
                }
            };
            foreach (var category in ListOfCategories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();

        }
        private void PriceCategoryInitializer(HotelModel context)
        {
            var ListOfPriceCategories = new List<PriceCategory>()
            {
                new PriceCategory()
                {
                    Price=149.99,
                    StartDate=new DateTime(2021,1,1),
                    EndDate=new DateTime(2021,12,31),
                    CategoryId=5
                },
                new PriceCategory()
                {
                    Price=290.5,
                    StartDate=new DateTime(2021,1,10),
                    EndDate=new DateTime(2021,4,10),
                    CategoryId=4
                },
                new PriceCategory()
                {
                    Price=250.99,
                    StartDate=new DateTime(2021,9,1),
                    EndDate=new DateTime(2021,12,1),
                    CategoryId=3
                },
                new PriceCategory()
                {
                    Price=350,
                    StartDate=new DateTime(2021,5,1),
                    EndDate=new DateTime(2021,11,1),
                    CategoryId=2
                },
                new PriceCategory()
                {
                    Price=300,
                    StartDate=new DateTime(2021,5,1),
                    EndDate=new DateTime(2021,7,30),
                    CategoryId=1
                }
            };

            foreach (var price_category in ListOfPriceCategories)
            {
                context.PriceCategories.Add(price_category);
            }

            context.SaveChanges();
        }
        private void BookingInitializer(HotelModel context)
        {
            var ListOfBookings = new List<Booking>()
            {
                new Booking()
                {
                    GuestId=4,
                    RoomId=2,
                    BookingDate=new DateTime(2021,5,8,12,25,40),
                    EnterDate=new DateTime(2021,5,15,14,0,0),
                    LeaveDate=new DateTime(2021,5,30,12,0,0),
                    IsGuestSettledIn=true
                },
                new Booking()
                {
                    GuestId=3,
                    RoomId=1,
                    BookingDate=new DateTime(2021,8,12,14,03,24),
                    EnterDate=new DateTime(2021,8,26,14,0,0),
                    LeaveDate=new DateTime(2021,9,3,12,0,0),
                    IsGuestSettledIn=false
                },
                new Booking()
                {
                    GuestId=1,
                    RoomId=4,
                    BookingDate=new DateTime(2021,2,15,10,40,02),
                    EnterDate=new DateTime(2021,2,20,14,0,0),
                    LeaveDate=new DateTime(2021,3,2,12,0,0),
                    IsGuestSettledIn=true
                },
                new Booking()
                {
                    GuestId=2,
                    RoomId=5,
                    EnterDate=new DateTime(2021,10,21,14,0,0),
                    LeaveDate=new DateTime(2021,10,29,12,0,0),
                    IsGuestSettledIn=true
                },
                new Booking()
                {
                    GuestId=5,
                    RoomId=8,
                    BookingDate=new DateTime(2021,3,11,16,51,59),
                    EnterDate=new DateTime(2021,3,24,14,0,0),
                    LeaveDate=new DateTime(2021,4,2,12,0,0),
                    IsGuestSettledIn=false
                }

            };
            foreach (var booking in ListOfBookings)
            {
                context.Bookings.Add(booking);
            }

            context.SaveChanges();
        }
       
        protected override void Seed(HotelModel context)
        {

            GuestInitializer(context);
            CategoryInitializer(context);
            RoomInitializer(context);
            PriceCategoryInitializer(context);
            BookingInitializer(context);
           
        }

    }
}
