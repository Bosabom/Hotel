using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using AutoMapper;

namespace Hotel.BLL.Services
{
    public class PriceCategoryService:IPriceCategoryService
    {
        private IWorkUnit Database { get; set; }
        IMapper mapper;
        IMapper mapper_reverse;

        public PriceCategoryService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PriceCategory, PriceCategoryDTO>()).CreateMapper();

            mapper_reverse = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PriceCategoryDTO, PriceCategory>()).CreateMapper();
        }
        public IEnumerable<PriceCategoryDTO> GetAllPriceCategories()
        {
            return mapper.Map<IEnumerable<PriceCategory>, List<PriceCategoryDTO>>(Database.PriceCategories.GetAll());
        }

        public PriceCategoryDTO Get(int id)
        {
            return mapper.Map<PriceCategory, PriceCategoryDTO>(Database.PriceCategories.Get(id));
        }
        public void Create(PriceCategoryDTO newPriceCategory)
        {
            var allPriceCategories = GetAllPriceCategories();

            var data = allPriceCategories.Where(c => c.CategoryId == newPriceCategory.CategoryId
                       && c.StartDate == newPriceCategory.StartDate && c.EndDate == newPriceCategory.EndDate).FirstOrDefault();

            if (data != null)
            {
                throw new Exception();
            }

            Database.PriceCategories.Create(mapper_reverse.Map<PriceCategoryDTO, PriceCategory>(newPriceCategory));
            Database.Save();
        }

        public void Delete(int id)
        {
            var priceCategoryWithThisId = Database.PriceCategories.Get(id);
            if (priceCategoryWithThisId != null)
            {
                Database.PriceCategories.Delete(id);
                Database.Save();
            }
            else
                throw new Exception();
        }
    }
}