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

        IPriceCategoryService service;
        IMapper mapper;
        IMapper mapper_reverse;
        public PriceCategoryController(IPriceCategoryService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()).CreateMapper();

            mapper_reverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>()).CreateMapper(); ;
        }

        public ActionResult Index()
        {
            var all_price_categories = mapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(service.GetAllPriceCategories());
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
            {    
                service.Create(mapper_reverse.Map<PriceCategoryModel, PriceCategoryDTO>(new_Pricecategory));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var Pricecategory_for_delete = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(service.Get(id));
            return View(Pricecategory_for_delete);
        }

        [HttpPost]
        public ActionResult Delete(int id, PriceCategoryModel price_category)
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