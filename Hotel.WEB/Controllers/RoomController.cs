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
    public class RoomController : Controller
    {

        IRoomService service;
        IMapper mapper;
        IMapper mapper_reverse;

        public RoomController(IRoomService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();

            mapper_reverse= new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();

        }

        public ActionResult Index()
        {
            var all_rooms = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(service.GetAllRooms());
            return View(all_rooms);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoomModel new_Room)
        {
            try
            {
                service.Create(mapper_reverse.Map<RoomModel, RoomDTO>(new_Room));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Edit(int id, RoomModel room)
        {
            try
            {
                service.Update(id, mapper_reverse.Map<RoomModel, RoomDTO>(room));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var Room_for_delete = mapper.Map<RoomDTO, RoomModel>(service.Get(id));
            return View(Room_for_delete);
        }

        [HttpPost]
        public ActionResult Delete(int id, RoomModel Room)
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
    }
}