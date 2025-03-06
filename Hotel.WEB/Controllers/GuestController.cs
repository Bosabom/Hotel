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
    public class GuestController : Controller
    {
        IGuestService guest_service;
        ILogService log_service;

        IMapper mapper;
        IMapper log_mapper;

        public GuestController(IGuestService service, ILogService _log_service)
        {
            guest_service = service;
            log_service = _log_service;

            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();

            log_mapper= new MapperConfiguration(cfg =>
               cfg.CreateMap<LogModel, LogDTO>()).CreateMapper();
        }

        private void CreateGuestLog(string _action, int _id, string _description)
        {
            log_service.Create(log_mapper.Map<LogModel, LogDTO>(new LogModel()
            {
                LogDate = DateTime.Now,
                User = User.Identity.Name,
                Action = _action,
                Entity = "Guest",
                EntityId = _id,
                Details = _description
            }));
        }

        public ActionResult Index()
        {
            var all_guests = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(guest_service.GetAllGuests()); 
            return View(all_guests);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GuestModel newGuest)
        {
            try
            {
                guest_service.Create(mapper.Map<GuestModel, GuestDTO>(newGuest));

                var guest_for_log = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(guest_service.GetAllGuests()).
                   FirstOrDefault(g => g.ToString() == newGuest.ToString());

                CreateGuestLog("Create", guest_for_log.Id, guest_for_log.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var guest = mapper.Map<GuestDTO, GuestModel>(guest_service.Get(id));
            return View(guest);
        }

        public ActionResult Delete(int id)
        {
            var exactly_guest = mapper.Map<GuestDTO, GuestModel>(guest_service.Get(id));

            return View(exactly_guest);
        }

        [HttpPost]
        public ActionResult Delete(int id, GuestModel guest)
        {
            try
            {
                guest = mapper.Map<GuestDTO, GuestModel>(guest_service.Get(id));

                guest_service.Delete(id);

                CreateGuestLog("Delete", id, guest.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}