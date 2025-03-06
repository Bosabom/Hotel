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
    public class CategoryService : ICategoryService
    {
        private IWorkUnit Database { get; set; }
        IMapper mapper;
        IMapper mapper_reverse;

        public CategoryService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            mapper_reverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
        }

        public CategoryDTO Get(int id)
        {
            return mapper.Map<Category, CategoryDTO>(Database.Categories.Get(id));
        }

        public void Create(CategoryDTO newCategory)
        {   
            //check if this category exists
            var allCategories = GetAllCategories();

            var data = allCategories.Where(c => c.Name == newCategory.Name 
                        && c.Number_Of_Places == newCategory.Number_Of_Places).FirstOrDefault();

            if (data != null)
            {
                throw new Exception();
            }

            Database.Categories.Create(mapper_reverse.Map<CategoryDTO, Category>(newCategory));
            Database.Save();
        }

        public void Delete(int id)
        {
            var CategoryWithThisId = Database.Categories.Get(id);
            if (CategoryWithThisId != null)
            {
                Database.Categories.Delete(id);
                Database.Save();
            }
            else
                throw new Exception();
        }
    }
}