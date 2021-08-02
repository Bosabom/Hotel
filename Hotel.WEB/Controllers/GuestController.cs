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
        IGuestService service;
        IMapper mapper;
        public GuestController(IGuestService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
        }
        // GET: Guest

        public ActionResult Index()
        {
            var all_guests = mapper.Map<IEnumerable<GuestDTO>,List<GuestModel>>(service.GetAllGuests()); 
            return View(all_guests);
        }
        // GET: Guest/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Guest/Create
        [HttpPost]
        public ActionResult Create(GuestModel newGuest)
        {
            try
            {
                // TODO: Add insert logic here
                service.Create(mapper.Map<GuestModel, GuestDTO>(newGuest));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var guest=mapper.Map<GuestDTO, GuestModel>(service.Get(id));
            return View(guest);
        }
        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            var exactly_guest = mapper.Map<GuestDTO, GuestModel>(service.Get(id));

            return View(exactly_guest);
        }

        [HttpPost]
        public ActionResult Delete(int id, GuestModel guest)
        {
            try
            {
                // TODO: Add delete logic here
                service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}