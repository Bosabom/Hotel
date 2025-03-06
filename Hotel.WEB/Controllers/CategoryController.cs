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
    public class CategoryController : Controller
    {
        ICategoryService category_service;
        ILogService log_service;

        IMapper mapper;
        IMapper log_mapper;

        public CategoryController(ICategoryService service, ILogService _log_service)
        {
            category_service = service;
            log_service = _log_service;

            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
            log_mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<LogModel, LogDTO>()).CreateMapper();
        }

        private void CreateCategoryLog(string _action, int _id, string _description)
        {
            log_service.Create(log_mapper.Map<LogModel, LogDTO>(new LogModel()
            {
                LogDate = DateTime.Now,
                User = User.Identity.Name,
                Action = _action,
                Entity = "Category",
                EntityId = _id,
                Details = _description
            }));
        }

        public ActionResult Index()
        {
            var all_categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(category_service.GetAllCategories());
            return View(all_categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryModel new_category)
        {
            try
            {
                category_service.Create(mapper.Map<CategoryModel, CategoryDTO>(new_category));

                var category_for_log = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(category_service.GetAllCategories()).
                   FirstOrDefault(g => g.ToString() == new_category.ToString());

                CreateCategoryLog("Create", category_for_log.Id, category_for_log.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var category_for_delete = mapper.Map<CategoryDTO, CategoryModel>(category_service.Get(id));
            return View(category_for_delete);
        }

        [HttpPost]
        public ActionResult Delete(int id, CategoryModel category)
        {
            try
            {
                category = mapper.Map<CategoryDTO, CategoryModel>(category_service.Get(id));
                category_service.Delete(id);

                CreateCategoryLog("Delete", id, category.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}