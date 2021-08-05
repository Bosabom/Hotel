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
    public class LogController : Controller
    {
        // GET: Log
        IMapper mapper;
        ILogService service;

        public LogController(ILogService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
             cfg.CreateMap<LogDTO, LogModel>()).CreateMapper();
           
        }
        public ActionResult Index()
        {
            var all_logs = mapper.Map<IEnumerable<LogDTO>, List<LogModel>>(service.GetAll());
            return View(all_logs);
        }
    }
}