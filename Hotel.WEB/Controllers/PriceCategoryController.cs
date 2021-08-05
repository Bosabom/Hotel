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
    public class PriceCategoryController : Controller
    {

        IPriceCategoryService price_category_service;
        ICategoryService category_service;
        ILogService log_service;

        IMapper mapper;
        IMapper mapper_reverse;

        IMapper log_mapper;
        public PriceCategoryController(IPriceCategoryService service,ICategoryService categoryService, ILogService _log_service)
        {
            price_category_service = service;
            category_service = categoryService;
            log_service = _log_service;

            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()).CreateMapper();

            mapper_reverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>()).CreateMapper();

            log_mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<LogModel, LogDTO>()).CreateMapper();
        }

        private void CreatePriceCategoryLog(string _action, int _id, string _description)
        {

            log_service.Create(log_mapper.Map<LogModel, LogDTO>(new LogModel()
            {
                LogDate = DateTime.Now,
                User = User.Identity.Name,
                Action = _action,
                Entity = "Price Category",
                EntityId = _id,
                Details = _description
            }));
        }
        public ActionResult Index()
        {
            var all_price_categories = mapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(price_category_service.GetAllPriceCategories());
            return View(all_price_categories);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PriceCategoryModel new_Pricecategory)
        {
            try
            {   //проверка существования категории с таким id
                var category_with_this_id = category_service.Get(new_Pricecategory.CategoryId);
                if(category_with_this_id != null)
                {
                    price_category_service.Create(mapper_reverse.Map<PriceCategoryModel, PriceCategoryDTO>(new_Pricecategory));

                    var price_category_for_log = mapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(price_category_service.GetAllPriceCategories()).
                       FirstOrDefault(g => g.ToString() == new_Pricecategory.ToString());

                    CreatePriceCategoryLog("Create", price_category_for_log.Id, price_category_for_log.ToString());
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
        public ActionResult Delete(int id)
        {
            var Pricecategory_for_delete = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(price_category_service.Get(id));
            return View(Pricecategory_for_delete);
        }

        [HttpPost]
        public ActionResult Delete(int id, PriceCategoryModel price_category)
        {
            try
            {
                price_category = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(price_category_service.Get(id));
                price_category_service.Delete(id);

                CreatePriceCategoryLog("Delete", id, price_category.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}