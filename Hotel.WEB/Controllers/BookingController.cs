using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Models;

namespace Hotel.WEB.Controllers
{
    public class BookingController : Controller
    {
        IBookingService service;
        IMapper mapper;
        IMapper mapper_reverse;
        public BookingController(IBookingService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();

            mapper_reverse= new MapperConfiguration(cfg =>
               cfg.CreateMap<BookingModel, BookingDTO>()).CreateMapper();
        }

        public ActionResult Index()
        {
            var all_bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(service.GetAllBookings());
            return View(all_bookings);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookingModel newBooking)
        {
            try
            {
                service.Create(mapper_reverse.Map<BookingModel, BookingDTO>(newBooking));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var Booking = mapper.Map<BookingDTO, BookingModel>(service.Get(id));
            return View(Booking);
        }
        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(int id, BookingModel booking)
        {
            try
            {
                service.Update(id, mapper_reverse.Map<BookingModel, BookingDTO>(booking));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var exactly_Booking = mapper.Map<BookingDTO, BookingModel>(service.Get(id));

            return View(exactly_Booking);
        }

        [HttpPost]
        public ActionResult Delete(int id, BookingModel Booking)
        {
            try
            {
                service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetFreeRoomsOnPeriod()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetFreeRoomsOnPeriod(DateTime date_from, DateTime date_to)
        {
            try
            {
                var room_mapper= new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();

                var free_rooms = room_mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(service.GetFreeRoomsOnPeriod(date_from,date_to));
                return View("FreeRooms",free_rooms);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetProfitForMonth()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProfitForMonth(DateTime date)
        {
            try
            {
                var profit = service.GetProfitForMonth(date);
                return View("ProfitForMonth",profit);
            }
            catch
            {
                return View();
            }
        }
    }
}