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
        IRoomService room_service;
        ICategoryService category_service;
        ILogService log_service;

        IMapper room_mapper;
        IMapper room_mapper_reverse;
        IMapper log_mapper;

        public RoomController(IRoomService service, ICategoryService _categoryService, ILogService _log_service)
        {
            room_service = service;
            category_service = _categoryService;
            log_service = _log_service;

            room_mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();

            room_mapper_reverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();

            log_mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<LogModel, LogDTO>()).CreateMapper();
        }

        private void CreateRoomLog(string _action, int _id, string _description)
        {
            log_service.Create(log_mapper.Map<LogModel, LogDTO>(new LogModel()
            {
                LogDate = DateTime.Now,
                User = User.Identity.Name,
                Action = _action,
                Entity = "Room",
                EntityId = _id,
                Details = _description
            }));
        }

        public ActionResult Index()
        {
            var all_rooms = room_mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(room_service.GetAllRooms());
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
                var category_with_this_id = category_service.Get(new_Room.CategoryId);
                if(category_with_this_id != null)
                {
                    room_service.Create(room_mapper_reverse.Map<RoomModel, RoomDTO>(new_Room));
                    
                    var room_for_log = room_mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(room_service.GetAllRooms())
                                            .FirstOrDefault(g => g.ToString() == new_Room.ToString());

                    CreateRoomLog("Create", room_for_log.Id, room_for_log.ToString());
                }
                else
                {
                    ModelState.AddModelError("", "Category with entered ID doesn't exist");
                    return View();
                }
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
                room_service.Update(id, room_mapper_reverse.Map<RoomModel, RoomDTO>(room));
                
                var updated_room = room_mapper.Map<RoomDTO, RoomModel>(room_service.Get(id));

                CreateRoomLog("Update", updated_room.Id, $"Active Status = {updated_room.Active}");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var Room_for_delete = room_mapper.Map<RoomDTO, RoomModel>(room_service.Get(id));
            return View(Room_for_delete);
        }

        [HttpPost]
        public ActionResult Delete(int id, RoomModel Room)
        {
            try
            {
                Room = room_mapper.Map<RoomDTO, RoomModel>(room_service.Get(id));
                room_service.Delete(id);

                CreateRoomLog("Delete", Room.Id, Room.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}