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
        IBookingService booking_service;
        IGuestService guest_service;
        IRoomService room_service;
        ILogService log_service;

        IMapper mapper;
        IMapper mapper_reverse;
        IMapper log_mapper;

        public BookingController(IBookingService service, IGuestService guestService, IRoomService roomService, ILogService logService)
        {
            booking_service = service;
            guest_service = guestService;
            room_service = roomService;
            log_service = logService;

            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();

            mapper_reverse= new MapperConfiguration(cfg =>
               cfg.CreateMap<BookingModel, BookingDTO>()).CreateMapper();

            log_mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<LogModel, LogDTO>()).CreateMapper();
        }

        private void CreateBookingLog(string _action, int _id, string _description)
        {
            log_service.Create(log_mapper.Map<LogModel, LogDTO>(new LogModel()
            {
                LogDate = DateTime.Now,
                User = User.Identity.Name,
                Action = _action,
                Entity = "Booking",
                EntityId = _id,
                Details = _description
            }));
        }

        public ActionResult Index()
        {
            var all_bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(booking_service.GetAllBookings());
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
                var guest_with_this_id = guest_service.Get(newBooking.GuestId);
                var room_with_this_id = room_service.Get(newBooking.RoomId);

                if(guest_with_this_id != null && room_with_this_id != null)
                {
                    booking_service.Create(mapper_reverse.Map<BookingModel, BookingDTO>(newBooking));

                    var booking_for_log = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(booking_service.GetAllBookings()).
                      FirstOrDefault(g => g.ToString() == newBooking.ToString());

                    CreateBookingLog("Create", booking_for_log.Id, booking_for_log.ToString());
                }
                else
                {
                    ModelState.AddModelError("", "There is no guest/room with this ID. Please, try again.");
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var Booking = mapper.Map<BookingDTO, BookingModel>(booking_service.Get(id));
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
                booking_service.Update(id, mapper_reverse.Map<BookingModel, BookingDTO>(booking));

                var updated_booking = mapper.Map<BookingDTO, BookingModel>(booking_service.Get(id));

                CreateBookingLog("Update", updated_booking.Id, $"Settlement = {updated_booking.IsGuestSettledIn};" +
                    $" Enter Date = {updated_booking.EnterDate}; Leave Date = {updated_booking.LeaveDate}");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var exactly_Booking = mapper.Map<BookingDTO, BookingModel>(booking_service.Get(id));

            return View(exactly_Booking);
        }

        [HttpPost]
        public ActionResult Delete(int id, BookingModel Booking)
        {
            try
            {
                Booking = mapper.Map<BookingDTO, BookingModel>(booking_service.Get(id));
                booking_service.Delete(id);

                CreateBookingLog("Delete", id, Booking.ToString());

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

                var free_rooms = room_mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(booking_service.GetFreeRoomsOnPeriod(date_from, date_to));
                FreeRoomsModel freeRoomsModel = new FreeRoomsModel()
                {
                    FirstDate = date_from.ToShortDateString(),
                    SecondDate = date_to.ToShortDateString(),
                    FreeRooms = free_rooms
                };

                return View("FreeRooms", freeRoomsModel);
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
                var profit = booking_service.GetProfitForMonth(date);

                ProfitModel profitModel = new ProfitModel()
                {
                    Year = date.Year,
                    Month = date.Month,
                    Profit_For_Month = profit
                };

                return View("ProfitForMonth",profitModel);
            }
            catch
            {
                return View();
            }
        }
    }
}