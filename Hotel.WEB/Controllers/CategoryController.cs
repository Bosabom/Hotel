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
     
        ICategoryService service;
        IMapper mapper;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }
        // GET: Guest

        public ActionResult Index()
        {
            var all_categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(service.GetAllCategories());
            return View(all_categories);
        }
        // GET: Guest/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Guest/Create
        [HttpPost]
        public ActionResult Create(CategoryModel new_category)
        {
            try
            {
                // TODO: Add insert logic here
                service.Create(mapper.Map<CategoryModel, CategoryDTO>(new_category));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            var category_for_delete = mapper.Map<CategoryDTO, CategoryModel>(service.Get(id));
            return View(category_for_delete);
        }

        [HttpPost]
        public ActionResult Delete(int id, CategoryModel category)
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